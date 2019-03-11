﻿  
## ニコ生のプッシュ通知の受信の手順  
  
このツールで使用しているニコ生のプッシュ通知の受信の手順を記します。  
アラートツールを製作されている方に少しでもお役に立てていただければ幸いです。  
また、アラートAPIがなくなることによるニコニコへのサーバー負荷の軽減に繋がることを願い  
  
### ブラウザプッシュ通知  
このツールではFirefoxのプッシュ通知の仕組みを使っています。  
  
  
1.プッシュサーバーへ接続します。  
FireFoxはautopushを採用しているため、WebSocketが使えます。  
```  
var url = "wss://push.services.mozilla.com";  
var headers = new List<KeyValuePair<string, string>>();  
headers.Add(new KeyValuePair<string, string>("Sec-WebSocket-Protocol", "push-notification"));  
headers.Add(new KeyValuePair<string, string>("Ssc-WebSocket-Version", "13"));  
  
ws = new WebSocket(url, "", null, headers, UserAgent, "",   
		WebSocketVersion.Rfc6455, null, SslProtocols.None);  
ws.Opened += onOpen;  
ws.Closed += onClose;  
ws.DataReceived += onDataReceive;  
ws.MessageReceived += onMessageReceive;  
ws.Error += onError;  
  
ws.Open();  
```  
  
2.UserAgentID(HTTPのUserAgentとは別で、プッシュサーバーで使うIDのようなものだと思います)を取得します。  
uaidを指定しなければ新規uaidが発行され、指定した場合はログインできます。  
```  
var mes = (uaid == null) ?  
				"{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true}"  
				: "{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true,\"uaid\":\"" + uaid + "\"}";  
ws.Send(mes);  
```  
サーバーからhelloが返ってきて、そこにuaidが含まれています。  
3.チャンネルIDを登録します。  
keyに指定するのはニコニコのserviceworker内の「https://public.api.nicovideo.jp/v1/nicopush/webpush/endpoints.json」のpublickeyに定義されている配列をbase64エンコードしたものです。  
channelIDはグローバル一意識別子を生成したものです。  
```  
var _chid = System.Guid.NewGuid().ToString();  
var pubBase64 = Convert.ToBase64String(publicKey);  
pubBase64 = pubBase64.Replace("/", "_").Replace("+", "-");  
pubBase64 = "BC08Fdr2JChSL0kr5imO99L6zZG6Rn0tBAWNTlrZfJtsDoeAvmJSa7CnUOHpNhd5zOk0YnRToEOT47YLet8Dpig=";  
  
var regMes = "{\"channelID\":\"" + _chid + "\",\"messageType\":\"register\",\"key\":\"" + pubBase64 + "\"}";  
ws.Send(regMes);  
```  
"messageType":"register"が返ってきて、pushEndpointが発行されていれば成功していると思われます。  
  
4.p256dhのキーペアを生成します。  
  
```  
IAsymmetricCipherKeyPairGenerator aliceKeyGen = GeneratorUtilities.GetKeyPairGenerator ("ECDH");  
var aliceGenerator = new DHParametersGenerator ();  
aliceGenerator.Init (256, 30, new SecureRandom ());  
DHParameters dhPara = aliceGenerator.GenerateParameters ();  
```  
  
5.Authを生成します。  
要素数16のランダムなバイト配列です。  
```  
var r = new byte[16];  
var auth = new Random().NextBytes(r);  
```  
6.pushEndpointをニコニコに送ります。  
```  
//5で生成したAuthをbase64エンコード  
var sendAuth = Convert.ToBase64String(auth);  
//4で生成したpublickeyをbase64エンコード  
var _pub = Convert.ToBase64String(publicKey);  
var param = "{\"clientapp\":\"nico_account_webpush\",\"endpoint\":{\"endpoint\":\"" + endpoint + "\",\"auth\":\"" + sendAuth + "\",\"p256dh\":\"" + _pub + "\"}}";  
var url = "https://public.api.nicovideo.jp/v1/nicopush/webpush/endpoints.json";  
  
byte[] postDataBytes = Encoding.ASCII.GetBytes(param);  
  
var req = (HttpWebRequest)WebRequest.Create(url);  
req.Method = "POST";  
req.Proxy = null;  
//ニコニコのuser_sessionを含むCookie  
req.CookieContainer = cookieContainer;  
req.Referer = "https://account.nicovideo.jp/my/account";  
req.ContentLength = postDataBytes.Length;  
req.ContentType = "application/json";  
req.Headers.Add("x-request-with", "https://account.nicovideo.jp/my/account");  
req.Headers.Add("x-frontend-id", "8");  
using (var stream = req.GetRequestStream()) {  
	stream.Write(postDataBytes, 0, postDataBytes.Length);  
}  
var res = req.GetResponse();  
var resStream = new StreamReader(res.GetResponseStream());  
var resStr = resStream.ReadToEnd();  
//{"meta":{"status":200}}が返ってくれば成功  
```  
  
7.通知を受信  
"messageType":"notification"が送られてくれば受信できていることになります。data要素の中にbase64エンコードされた通知内容が入っています。  
  
8.通知内容の復号  
```  
//byte配列に戻します  
var mes = message.Replace("-", "+").Replace("_", "/");  
var a = 4 - (mes.Length % 4);  
for (var pi = 0; pi < a && a != 4; pi++) mes += "=";  
var payload = System.Convert.FromBase64String(mes);  
  
//復号に使う情報とペイロードを取り出します  
var rs = (payload[16] << 24) | (payload[17] << 16) | (payload[18] << 8) | payload[19];  
var keyIdLen = payload[20];  
var _payloadList = new List<byte>(payload);  
byte[] salt, senderKey, ciphertext;  
salt = _payloadList.GetRange(0, 16).ToArray();  
senderKey = _payloadList.GetRange(21, keyIdLen).ToArray();  
ciphertext = _payloadList.GetRange(21 + keyIdLen, payload.Length - (21 + keyIdLen)).ToArray();  
```  
取り出したsenderKeyと4で生成したprivatekeyから共有鍵を生成します  
```  
const string Algorithm = "ECDH";  
X9ECParameters ecPars = NistNamedCurves.GetByName("P-256");  
ECDomainParameters ecDomPars = new ECDomainParameters(ecPars.Curve, ecPars.G, ecPars.N, ecPars.H, ecPars.GetSeed());  
ECCurve curve = ecDomPars.Curve;  
ECPoint pubQ = curve.DecodePoint(pub);  
  
IBasicAgreement _aliceKeyAgree = AgreementUtilities.GetBasicAgreement (Algorithm);  
  
var priKeyPara = new ECPrivateKeyParameters(new BigInteger(pri), ecDomPars);//priQ.Curve.  
_aliceKeyAgree.Init (priKeyPara);  
  
AsymmetricKeyParameter pubKeyPara = new ECPublicKeyParameters(pubQ, ecDomPars);  
SubjectPublicKeyInfo _key = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKeyPara);  
  
BigInteger aliceAgree = _aliceKeyAgree.CalculateAgreement (pubKeyPara);  
  
var sharedKey = new KeyParameter (aliceAgree.ToByteArrayUnsigned ()).GetKey();  
```  
暗号鍵とnonceを生成します  
```  
var authKdf = new Hkdf();  
var authInfo = new List<byte>();  
authInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("WebPush: info\0"));  
  
//4で生成したpublickey  
authInfo.AddRange(pub);  
//さきほど取り出したpublickeyであるsenderkey  
authInfo.AddRange(senderPub);  
//auth: 5で生成したAuth key: さきほど生成した共有鍵  
var prk = authKdf.DeriveKey(auth, key, authInfo.ToArray(), 32);  
  
var prkKdf = new Hkdf();  
var keyInfo = new List<byte>();  
keyInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("Content-Encoding: aes128gcm\0"));  
//暗号鍵  
var keyInfoPrk = prkKdf.DeriveKey(salt, prk, keyInfo.ToArray(), 16);  
  
var Kdf = new Hkdf();  
var nonceInfo = new List<byte>();  
nonceInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("Content-Encoding: nonce\0"));  
//nonce  
var nonceInfoPrk = prkKdf.DeriveKey(salt, prk, nonceInfo.ToArray(), 12);  
```  
ciphertextをrs(レコードサイズ)ごとに分割。ブラウザプッシュ通知の場合は送られるデータの上限が大きくないので分割しなくとも問題ないかもしれません。  
```  
var start = 0;  
var index = 0;  
var result = new List<byte[]>();  
while(index + rs <= ciphertext.Length) {  
    var buf = new byte[rs];  
    Array.Copy(ciphertext, start + index, buf, 0, rs);  
    result.Add(buf);  
    index += rs;  
}  
if (index < ciphertext.Length) {  
    var buf = new byte[ciphertext.Length - index];  
    Array.Copy(ciphertext, start + index, buf, 0, buf.Length);  
    result.Add(buf);  
}  
```  
分割された部分ごとに復号します。  
```  
//IVを生成します  
private byte[] generateIV(byte[] _base, int index) {  
    var nonce = new byte[12];  
    Array.Copy(_base, 0, nonce, 0, 12);  
  
    for (var i = 0; i < 6; ++i) {  
        var b = System.Math.Pow(256, i);  
        int num0 = (int)(index / System.Math.Pow(256, i));  
        var tes0 = num0 & (byte)0xff;  
  
        var nonceI = nonce.Length - 1 - i;  
        nonce[nonceI] ^= (byte)tes0;  
    }  
    return nonce;  
}  
          
var aes = new AESGCM();  
for (var i = 0; i < chunkArray.Count; i++) {  
	//分割された部分ごとにIVを生成　プッシュ通知の場合は常に一つかもしれません  
    var iv = generateIV(nonce, i);  
    //復号  
    var dec = aes.DecryptWithKey(chunkArray[0], key, iv);  
    return dec;  
}  
```  
ニコ生のユーザー放送の場合は  
{"title":"{ユーザー名}さんが生放送を開始","body":"{コミュニティ名} で、「{タイトル}」を放送","icon":"{プッシュ通知に表示されるアイコンURL}","data":{"on_click":"{クリックした際に表示されるURL}?from=webpush&_topic=live_user_program_onairs","created_at":"2019-03-01T00:00:00.000+09:00","ttl":600,"log_params":{"content_type":"live.user.program.onairs","content_ids":"lv000000000"}}}  
のような文字列が取得できます。  
uaidと4のキーペアと5のAuthは保存しておくと次回以降は同じIDを使い復号することができます。  
  
### スマホアプリプッシュ通知  
  
1.各言語用のprotobufを用意します  
https://github.com/chromium/chromiumなどからcheckin.protoとmcs.protoを入手し、使用している各言語用のコードを生成します。このツールではhttps://protobuf-compiler.herokuapp.com/のサイトを使いました。proto2で書かれているので、C#などのproto3しか受け付けない言語の場合は「syntax = "proto3";」に書き直し、requiredやoptionalを削除するとコードが生成できるようになるかと思います。  
  
2.AndroidIdとSecurityTokenを取得します。  
```  
var url = "http://android.clients.google.com/checkin";  
  
var cr = new CheckinRequest();  
cr.Imei = "000000000000000";  
cr.AndroidId= 0;  
var build = new CheckinRequest.Types.Checkin.Types.Build();  
build.Fingerprint = "google/razor/flo:5.0.1/LRX22C/1602158:user/release-keys";  
build.Hardware = "flo";  
build.Brand = "google";  
build.Radio = "FLO-04.04";  
build.ClientId = "android-google";  
var checkinM = new CheckinRequest.Types.Checkin();  
checkinM.Build = build;  
cr.Checkin = checkinM;  
cr.Checkin.LastCheckinMs = 0;  
cr.Locale = "en";  
cr.LoggingId = 1;  
//全て0じゃなければランダムで大丈夫そうです  
cr.MacAddress.Add("000000000001");  
cr.Meid = "01234567890123";  
cr.AccountCookie.Add("");  
cr.TimeZone = "GMT";  
cr.Version = 3;  
cr.OtaCert.Add("--no-output--"); // 71Q6Rn2DDZl1zPDVaaeEHItd  
cr.Esn = "01234567";  
cr.MacAddressType.Add("wifi");  
cr.Fragment = 0;  
cr.UserSerialNumber = 0;  
var postDataBytes = cr.ToByteArray();  
var headers = new Dictionary<string, string>() {  
    {"contenttype", "application/x-protobuffer"},  
    {"useragent", "Android-Checkin/2.0 (vbox86p JLS36G); gzip"}  
};  
var rb = util.postResBytes(url, headers, postDataBytes);  
  
var parser = new Google.Protobuf.MessageParser<CheckinResponse>(() => new CheckinResponse());  
var checkinRes = parser.ParseFrom(new MemoryStream(rb));  
  
androidId = checkinRes.AndroidId.ToString();  
securityToken = checkinRes.SecurityToken.ToString();  
```  
  
3.トークンを取得します  
```  
var headers = new Dictionary<string, string>(){  
	//androidIdとsecurityTokenを認証に使う  
    {"Authorization","AidLogin " + androidId + ":" + securityToken},  
    {"contenttype", "application/x-www-form-urlencoded"}  
};  
var param = "app=jp.nicovideo.android";  
//ニコニコのsenderId  
param += "&sender=812879448480";  
param += "&device=" + androidId;  
param += "&app_ver=107";  
param += "&gcm_ver=15090013";  
param += "&X-scope=GCM";  
param += "&X-app_ver_name=4.48.0";  
byte[] postDataBytes = Encoding.ASCII.GetBytes(param);  
  
var url = "https://android.clients.google.com/c2dm/register3";  
var r = util.postResStr(url, headers, postDataBytes);  
```  
  
4.ニコニコにトークンを登録します  
```  
var url = "https://api.gadget.nicovideo.jp/notification/clientapp/registration";   
var headers = new Dictionary<string, string>() {  
    {"Content-Type", "application/x-www-form-urlencoded"},  
    {"User-Agent", "Niconico/1.0 (Linux; U; Android 5.1.1; ja-jp; nicoandroid SM-G9550) Version/5.06.0"},  
    //Cookieのuser_sessionを指定  
    {"Cookie", "SP_SESSION_KEY=" + userSession},  
    {"Cookie2", "$Version=1"},  
    {"Accept-Language", "ja-jp"},  
    {"X-Nicovideo-Connection-Type", "wifi"},  
    {"X-Frontend-Id", "1"},  
    {"X-Frontend-Version", "5.06.0"},  
    {"X-Os-Version", "5.1.1"},  
    {"X-Request-With", ""},  
    {"X-Model-Name", "dream2qltechn"}  
};  
//tokenにはuser_sessionを指定し、registerIdに3で取得したトークンを指定する  
var param = "token=" + userSession;  
param += "&registerId=" + pushToken;  
  
byte[] postDataBytes = Encoding.ASCII.GetBytes(param);  
var r = util.postResStr(url, headers, postDataBytes);  
```  
  
5.プッシュサーバーに接続します  
```  
var lr = new LoginRequest();  
lr.AdaptiveHeartbeat = false;  
lr.AuthService = LoginRequest.Types.AuthService.AndroidId;  
lr.AuthToken = securityToken;  
lr.Id = "android-11";  
lr.Domain = "mcs.android.com";  
lr.DeviceId = "android-" + long.Parse(androidId).ToString("x");  
lr.NetworkType = 1;  
lr.Resource = androidId;  
lr.User = androidId;  
lr.UseRmq2 = true;  
lr.AccountId = long.Parse(androidId);  
lr.ReceivedPersistentId.Add("");  
var setting = new Setting();  
setting.Name = "new_vc";  
setting.Value = "1";  
lr.Setting.Add(setting);  
var x = lr.ToByteArray();  
  
  
using (var client = new TcpClient("mtalk.google.com", 5228))  
using (sslStream = new SslStream(client.GetStream(), false, delegate { return true; })) {  
    sslStream.AuthenticateAsClient("mtalk.google.com");  
    var _buf = VarintBitConverter.GetVarintBytes((uint)x.Length);  
    sslStream.Write(new byte[]{41, 2});  
    sslStream.Write(_buf);  
    sslStream.Write(x);  
    sslStream.Flush();  
    var version = sslStream.ReadByte();  
```  
定期的にpingを送ります。  
```  
sslStream.Write(new byte[]{0x07, 0x0e, 0x10, 0x01, 0x1a, 0x00, 0x3a, 0x04, 0x08, 0x0d, 0x12, 0x00, 0x50, 0x03, 0x60, 0x00});  
```  
6.通知を受信します。  
```  
while (isRetry) {  
    var responseTag = sslStream.ReadByte();  
    var msg = new List<byte>();  
    if (responseTag == 0x03 || responseTag == 0x07  
            || responseTag == 0x08) {  
          
        var length = 0;  
        var _lenBuf = new List<byte>();  
        for (var i = 0; i < 10; i++) {  
            var _slb = sslStream.ReadByte();  
            _lenBuf.Add((byte)_slb);  
            try {  
				var length5 = VarintBitConverter.ToUInt32(_lenBuf.ToArray()) * 1;  
                length = (int)length5;  
                break;  
            } catch (Exception e) {}  
        }  
        while (msg.Count < length) {  
            byte[] readbuf = new byte[1000];  
            var i = sslStream.Read(readbuf, 0, length - msg.Count);  
            for (var j = 0; j < i; j++) msg.Add(readbuf[j]);  
        }  
    }  
  
    if (responseTag == 0x03) {  
    	//ログイン  
        var lresp = new LoginResponse();  
        using (var ms = new MemoryStream(msg.ToArray()))  
        using (var cs = new CodedInputStream(ms)) {  
            lresp.MergeFrom(cs);  
        }   
        util.debugWriteLine("RECV LOGIN RESP " + lresp);  
    } else if (responseTag == 0x07) {  
        //定期的に送られてくる  
  
    } else if (responseTag == 0x08) {  
    	//通知受信  
        var lresp = DataMessageStanza.Parser.ParseFrom(msg.ToArray());  
  
    } else if (responseTag == 0x04) {  
    	//終了  
        break;  
    } else {  
        break;  
    }  
}  
```  
ニコ生のユーザー放送の場合は  
{ "id": "00000000", "from": "812879448480", "category": "jp.nicovideo.android", "appData": [ { "key": "lvid", "value": "lv000000000" }, { "key": "message", "value": "[生放送開始]{ユーザー名}さんが「{タイトル}」を開始しました。" } ], "persistentId": "0:0000000000000000%0000000000000000", "lastStreamIdReceived": 0, "ttl": 0000000, "sent": "1551366000000" }  
のような文字列が取得できます。  
AndroidIdとSecurityTokenは保存しておくと次回以降は同じIDを使うことができます。
