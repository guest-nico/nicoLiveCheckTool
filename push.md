﻿
## ニコ生のプッシュ通知の受信の手順

このツールで使用しているニコ生のプッシュ通知の受信の手順を記します。
アラートツールを製作されいる方の少しでもお役に立てていただければ幸いです。

### ブラウザプッシュ通知
このツールではfirefoxのプッシュ通知の仕組みを使っています。
1.p256dhのキーペアを生成します。

```
public void generateKey(out byte[] pri, out byte[] pub) {
			IAsymmetricCipherKeyPairGenerator aliceKeyGen = GeneratorUtilities.GetKeyPairGenerator ("ECDH");
            
            DHParametersGenerator aliceGenerator = new DHParametersGenerator ();
		    
		    aliceGenerator.Init (256, 30, new SecureRandom ());
		    DHParameters dhPara = aliceGenerator.GenerateParameters ();
		    
		
		}
        
```

2.Authを生成します。

```
public byte[] generateAuth() {
	var r = new byte[16];
	new Random().NextBytes(r);
	return r;
}
```
要素数16のランダムなバイト配列です。 byte[16]
