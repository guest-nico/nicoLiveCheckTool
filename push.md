﻿
## ニコ生のプッシュ通知の受信の手順

このツールで使用しているニコ生のプッシュ通知の受信の手順を記します。
アラートツールを製作されている方に少しでもお役に立てていただければ幸いです。
また、アラートAPIがなくなることによるニコニコへのサーバー負荷の軽減を願い。  

これまでniconicoアプリのプッシュ通知機能を使用しておりましたが、通知を登録・取得できなくなってしまったために、2019年11月17日更新のver0.1.7.37よりニコニコ生放送アプリのプッシュ通知機能を使用するように修正致しました。

### ブラウザプッシュ通知
このツールではFirefoxのプッシュ通知の仕組みを使っています。  
実際の実装は[PushReceiver.cs](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushReceiver.cs)、[PushCrypto.cs](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)が担当しています。
仕様変更によりこちらのページの更新が追い付いていない場合、実際のコードでは動作できるかもしれません。更新が遅れてしまい申し訳ありません。  

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
[*PushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushReceiver.cs)  

2.UserAgentID(HTTPのUserAgentとは別で、プッシュサーバーで使うIDのようなものだと思います)を取得します。  
uaidを指定しなければ新規uaidが発行され、指定した場合はログインできます。
```
var mes = (uaid == null) ?
				"{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true}"
				: "{\"messageType\":\"hello\",\"broadcasts\":null,\"use_webpush\":true,\"uaid\":\"" + uaid + "\"}";
ws.Send(mes);
```
[*PushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushReceiver.cs)  
サーバーからhelloが返ってきて、そこにuaidが含まれています。  

3.チャンネルIDを登録します。  
```
keyに指定している「BC08Fdr2JChSL0kr5imO99L6z...」について  

ニコニコではブラウザ上でプッシュ通知を受信するにあたってサービスワーカーが利用されているかと思いますが、  
このサービスワーカーのコード中にpublickeyが記述されているようです。  
Google Chromeの場合、ニコニコの「アカウント設定」画面を開き、「ブラウザのプッシュ通知設定」をONやOFFにした後に  
デベロッパーツールのApplicationタブを開いていただくとサービスワーカーの「sw.js」の情報が表示されているかと思います。  
こちらからリンクを開き、参照されている「https://secure-dcdn.cdn.nimg.jp/nicopush/files/sw_release_2021-11-10_nicobus_prod.js」を開くと

> var o = new Uint8Array([4, 45, 60, 21, 218, 246, 36, 40, 82, 47, 73, 43, 230, 41, 142, 247, 210, 250, 205, 145, 186, 70, 125, 45, 4, 5, 141, 78, 90, 217, 124, 155, 108, 14, 135, 128, 190, 98, 82, 107, 176, 167, 80, 225, 233, 54, 23, 121, 204, 233, 52, 98, 116, 83, 160, 67, 147, 227, 182, 11, 122, 223, 3, 166, 40]);
> e.default = {
> 　URL: "https://public.api.nicovideo.jp/v1/nicopush/webpush/endpoints.json",
> 　LOGGIF_URL: "https://dcdn.cdn.nicovideo.jp/shared_httpd/log.gif",
> 　publicKey: o
> }
といったUint8配列があり、こちらをbase64エンコードしています。
```
channelIDはグローバル一意識別子を生成したものです。
```
var _chid = System.Guid.NewGuid().ToString();
var pubBase64 = "BC08Fdr2JChSL0kr5imO99L6zZG6Rn0tBAWNTlrZfJtsDoeAvmJSa7CnUOHpNhd5zOk0YnRToEOT47YLet8Dpig=";
var regMes = "{\"channelID\":\"" + _chid + "\",\"messageType\":\"register\",\"key\":\"" + pubBase64 + "\"}";
ws.Send(regMes);
```
[*PushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushReceiver.cs)  
websocket上で"messageType":"register"が返ってきて、pushEndpointが発行されていれば成功していると思われます。

4.p256dhのキーペアを生成します。

```
IAsymmetricCipherKeyPairGenerator aliceKeyGen = GeneratorUtilities.GetKeyPairGenerator ("ECDH");
var aliceGenerator = new DHParametersGenerator ();
aliceGenerator.Init (256, 30, new SecureRandom ());
DHParameters dhPara = aliceGenerator.GenerateParameters ();
```
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  

5.Authを生成します。
要素数16のランダムなバイト配列です。
```
var r = new byte[16];
var auth = new Random().NextBytes(r);
```
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
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
[*PushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushReceiver.cs)  

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
if (keyIdLen != 65) {
  util.debugWriteLine("Invalid sender public key bad dh PARAM");
  return null;
}
if (payload.Length <= 21 + keyIdLen) {
  util.debugWriteLine("Truncated payload  BAD_CRYPTO");
}
var _payloadList = new List<byte>(payload);
byte[] salt, senderKey, ciphertext;
salt = _payloadList.GetRange(0, 16).ToArray();
senderKey = _payloadList.GetRange(21, keyIdLen).ToArray();
ciphertext = _payloadList.GetRange(21 + keyIdLen, payload.Length - (21 + keyIdLen)).ToArray();
```
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
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
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
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
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
ciphertextをrs(レコードサイズ)ごとに分割。ブラウザプッシュ通知の場合は送られるデータの上限が大きくないので分割しなくとも問題ないかもしれません。
```
private List<byte[]> chunkArray(byte[] array, int size) {
  var start = 0;
  var index = 0;
  var result = new List<byte[]>();
  while(index + size <= array.Length) {
    var buf = new byte[size];
    Array.Copy(array, start + index, buf, 0, size);
    result.Add(buf);
    index += size;
  }
  if (index < array.Length) {
    var buf = new byte[array.Length - index];
    Array.Copy(array, start + index, buf, 0, buf.Length);
    result.Add(buf);
  }
  return result;
}
var _chunkArray = chunkArray(ciphertext, rs);
```
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
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
[*PushCrypto.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/PushCrypto.cs)  
ニコ生のユーザー放送の場合は  
{"data":{"on_click":"https://live.nicovideo.jp/watch/lv000000000?from=webpush&_topic=live_user_program_onairs","created_at":"2022-12-06T00:00:00.000+09:00","log_params":{"content_ids":"lv000000000","content_type":"live.user.program.onairs"},"ttl":600.0},"body":"コミュニティ名 で、「タイトル」を放送","title":"ユーザー名さんが生放送を開始","icon":"https://secure-dcdn.cdn.nimg.jp/nicoaccount/usericon/defaults/blank.jpg"}  
のような文字列が取得できます。  
uaidと4のキーペアと5のAuthを保存しておくと次回以降は同じIDを使い復号することができます。

### スマホアプリプッシュ通知
実際の実装は[AppPushReceiver.cs](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)が担当しています。  
仕様変更によりこちらのページの更新が追い付いていない場合、実際のコードでは動作できるかもしれません。更新が遅れてしまい申し訳ありません。  

1.各言語用のprotobufを用意します
<https://github.com/chromium/chromium>などからcheckin.protoとmcs.protoを入手し、使用している各言語用のコードを生成します。このツールでは<https://protobuf-compiler.herokuapp.com/>のサイトを使いました。proto2で書かれているので、「syntax = "proto3";」に書き直し、requiredやoptionalを削除するとコードが生成できるようになるかと思います。

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
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

3.トークンを取得します
```
var headers = new Dictionary<string, string>(){
	//androidIdとsecurityTokenを認証に使う
    {"Authorization","AidLogin " + androidId + ":" + securityToken},
    {"contenttype", "application/x-www-form-urlencoded"}
};

//ニコニコのsenderId
string param;
var isNicoCas = true; //[ver0.1.7.37]ニコニコ生放送アプリ用
if (isNicoCas) {
    param = "app=jp.co.dwango.nicocas";
    param += "&sender=13323994513";
} else {
    param = "app=jp.nicovideo.android";
    param += "&sender=812879448480"; //niconicoアプリ
}
                
param += "&device=" + androidId;
param += "&app_ver=107";
param += "&gcm_ver=15090013";
param += "&X-scope=GCM";
param += "&X-app_ver_name=4.48.0";
byte[] postDataBytes = Encoding.ASCII.GetBytes(param);

var url = "https://android.clients.google.com/c2dm/register3";
var r = util.postResStr(url, headers, postDataBytes);
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

4.ニコニコにトークンを登録します

```
string param;
//[ver0.1.7.37]ニコニコ生放送アプリ用
var url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/installations";
var headers = new Dictionary<string, string>() {
      {"Content-Type", "application/json; charset=UTF-8"},
      {"User-Agent", "nicocas-Android/3.44.1"}, //ニコニコ生放送アプリのバージョン
      {"Cookie", "user_session=" + userSession},
      {"X-Frontend-Id", "90"},
      {"X-Frontend-Version", "3.44.1"}, //ニコニコ生放送アプリのバージョン
      {"X-Os-Version", "22"},
      {"X-Model-Name", "dream2qltechn"},
      {"X-Connection-Environment", "wifi"},
      {"Connection", "Keep-Alive"},
      //{"Accept-Encoding", "gzip"},
  };
var param = "{\"token\": \"" + pushToken + "\"}";
byte[] postDataBytes = Encoding.ASCII.GetBytes(param);
var res = util.postResStr(url, headers, postDataBytes);

//プッシュ通知のブロック機能をオフ
url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/notification/blocks";
param = "{\"all\": [\"nicocas\"],\"channel\": [],\"user\": []}";
postDataBytes = Encoding.ASCII.GetBytes(param);
var _res = util.sendRequest(url, headers, postDataBytes, "DELETE");
if (res == null) check.form.addLogText("スマホ通知のブロック設定の送信に失敗しました");
else {
  using (var getResStream = _res.GetResponseStream())
  using (var resStream = new System.IO.StreamReader(getResStream)) {
      var _r = resStream.ReadToEnd();
      util.debugWriteLine("app push blocks delete " + _r);
      if (_r == null || _r.IndexOf("200") == -1) 
          check.form.addLogText("スマホ通知のブロック設定に失敗しました " + _r);
  }
}
    
    //プッシュ通知の時間指定をオフ
    url = "https://api.cas.nicovideo.jp/v1/services/ex/app/nicocas_android/notification/time";
    param = "{\"status\": \"disabled\",\"time\": {\"end\": \"0:00\", \"start\": \"7:00\"}}";
    postDataBytes = Encoding.ASCII.GetBytes(param);
    _res = util.sendRequest(url, headers, postDataBytes, "PUT");
    if (res == null) check.form.addLogText("スマホ通知の時間設定の送信に失敗しました");
    else {
        using (var getResStream = _res.GetResponseStream())
        using (var resStream = new System.IO.StreamReader(getResStream)) {
            var _r = resStream.ReadToEnd();
            util.debugWriteLine("app push time put " + _r);
            if (_r == null || _r.IndexOf("200") == -1) 
                check.form.addLogText("スマホ通知の時間設定に失敗しました" + _r);
        }
    }
}
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

ニコニコ生放送アプリのバージョンについて  
ヘッダーに指定するニコニコ生放送アプリのバージョンは、時々以前のバージョンでは接続できなくなることがあるようでした。  

```
//niconicoアプリ用 現在は使用していません
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
//niconicoアプリ用 ここまで
```    
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

5.プッシュサーバーに接続します  
protobufのLoginRequestを作成
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
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

protubufをsslstreamに送信する処理
```
private void sendMessage(IExtensible proto) {
  byte[] x;
  using (var ms = new MemoryStream()) {
      Serializer.Serialize(ms, proto);
      x = ms.ToArray();
  }
  var _buf = VarintBitConverter.GetVarintBytes((uint)x.Length);
	//送るメッセージの長さを伝えた後
    sslStream.Write(_buf);
	//本体のメッセージを送信します
    sslStream.Write(x);
    sslStream.Flush();
}
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

ログイン時に送る情報
```
enum MCSProtoTag {
  kHeartbeatPingTag = 0,
  kHeartbeatAckTag,
  kLoginRequestTag,
  kLoginResponseTag,
  kCloseTag,
  kMessageStanzaTag,
  kPresenceStanzaTag,
  kIqStanzaTag,
  kDataMessageStanzaTag,
  kBatchPresenceStanzaTag,
  kStreamErrorStanzaTag,
  kHttpRequestTag,
  kHttpResponseTag,
  kBindAccountRequestTag,
  kBindAccountResponseTag,
  kTalkMetadataTag,
  kNumProtoTypes,
};
const int kMCSVersion = 41;
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

接続
```
using (var client = new TcpClient("mtalk.google.com", 5228))
using (sslStream = new SslStream(client.GetStream(), false, delegate { return true; })) {
    sslStream.AuthenticateAsClient("mtalk.google.com");
    
	//最初にバージョンやタグなどを送信
    sslStream.Write(new byte[]{kMCSVersion, (byte)MCSProtoTag.kLoginRequestTag}); // {41,2}になります
	//LoginRequestメッセージを送信
    sendMessage(lr);
	//バージョンが返ってきます
    var version = sslStream.ReadByte();
    util.debugWriteLine("mcs version " + version);
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  
定期的にpingを送ります。(コード内では60秒に一回にしています。)
```
var ping = new HeartbeatPing();
sendMessage(ping);
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

6.通知を受信します。  
sslstreamから受信してprotobuf形式で取り出す処理
```
private IExtensible BuildProtobufFromTag(MCSProtoTag _tag, SslStream sslStream) {
  var msg = new List<byte>();

  var length = 0;
  var _lenBuf = new List<byte>();
  //メッセージの長さをsslstreamから取得します
  for (var i = 0; i < 10; i++) {
    var _slb = sslStream.ReadByte();
    _lenBuf.Add((byte)_slb);
    if (_lenBuf[_lenBuf.Count - 1] > 128) _lenBuf.Add((byte)sslStream.ReadByte());
    try {
      var length5 = VarintBitConverter.ToUInt32(_lenBuf.ToArray()) * 1;
      length = (int)length5;
      break;
    } catch (Exception e) {
      util.debugWriteLine("app push varint len " + e.Message + e.Source + e.StackTrace + e.TargetSite);
    }
  }
  if (length == 0) return null;
  
  //メッセージの長さ分をsslstreamから受信します
  while (msg.Count < length) {
    if (msg.Count != 0) util.debugWriteLine("recv msg 2shuume");

    byte[] readbuf = new byte[1000];
    var i = sslStream.Read(readbuf, 0, length - msg.Count);
    for (var j = 0; j < i; j++) msg.Add(readbuf[j]);
  }
  util.debugWriteLine("calc len " + length + " msg len " + msg.Count + " msg " + msg);

  //タグに応じてbyte配列をprotobufにデシリアライズ
  switch (_tag) {
    case MCSProtoTag.kLoginResponseTag:
      var loginResp = new LoginResponse();
      using (var ms = new MemoryStream(msg.ToArray())) {
        loginResp = Serializer.Deserialize<LoginResponse>(ms);
      }
      util.debugWriteLine("RECV LOGIN RESP " + loginResp);
      return loginResp;
    case MCSProtoTag.kIqStanzaTag:
      var iqStanza = new IqStanza();

      using (var ms = new MemoryStream(msg.ToArray())) {
        iqStanza = Serializer.Deserialize<IqStanza>(ms);
      }
      util.debugWriteLine("RECV IQ  id " + iqStanza);// + lresp.Id + " time " + lresp.ServerTimestamp + " streamid " + lresp.StreamId + " ");
      return iqStanza;
    case MCSProtoTag.kDataMessageStanzaTag:
      //放送情報などはこちら
      DataMessageStanza lresp;
      using (var ms = new MemoryStream(msg.ToArray())) {
        lresp = Serializer.Deserialize<DataMessageStanza>(ms);
      }
      return lresp;
    case MCSProtoTag.kHeartbeatPingTag:
      HeartbeatPing p;
      using (var ms = new MemoryStream(msg.ToArray())) {
        p = Serializer.Deserialize<HeartbeatPing>(ms);
      }
      return p;
    case MCSProtoTag.kHeartbeatAckTag:
      HeartbeatAck ack;
      using (var ms = new MemoryStream(msg.ToArray())) {
        ack = Serializer.Deserialize<HeartbeatAck>(ms);
      }
      return ack;
    default:
      return null;
  }
}
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

受信します
```
while (isRetry) {
  //sslstreamからメッセージの種類をタグとしてintで取得
  var responseTag = getTag(sslStream);
  if (responseTag == -1) {
    util.debugWriteLine("getTag error ");
    break;
  }
  //取得したタグをMCSProtoTag形式に変換
  var _tag = (MCSProtoTag)Enum.ToObject(typeof(MCSProtoTag), responseTag);
  util.debugWriteLine(DateTime.Now + " resp tag " + responseTag + " ");

  if (_tag == MCSProtoTag.kHeartbeatPingTag || _tag == MCSProtoTag.kCloseTag)
    break;
  else if (_tag != MCSProtoTag.kLoginResponseTag && _tag != MCSProtoTag.kIqStanzaTag
      && _tag != MCSProtoTag.kDataMessageStanzaTag && _tag != MCSProtoTag.kHeartbeatPingTag) {
    
  }

  var proto = BuildProtobufFromTag(_tag, sslStream);

  if (_tag == MCSProtoTag.kLoginResponseTag) {

  } else if (_tag == MCSProtoTag.kIqStanzaTag) {

  } else if (_tag == MCSProtoTag.kDataMessageStanzaTag) {
    //放送情報等の受信
    onReceiveData((DataMessageStanza)proto);
  } else {
    util.debugWriteLine("unknown response: " + _tag.ToString());
  }
}
```
[*AppPushReceiver.cs*](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/alart/AppPushReceiver.cs)  

[DataMessageStanza](https://github.com/guest-nico/nicoLiveCheckTool/blob/master/nicoNewStreamRecorderKakkoKari/alart4.0/src/util/mcs_pbs.cs)形式で放送の情報が取得できます。  
AndroidIdとSecurityTokenを保存しておくと次回以降は同じIDを使うことができます。
