/*
 * Created by SharpDevelop.
 * User: zack
 * Date: 2019/02/02
 * Time: 22:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Diagnostics;

using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;


namespace namaichi.alart
{
	/// <summary>
	/// Description of PushCrypto.
	/// </summary>
	public class PushCrypto
	{
		public PushCrypto()
		{
		}
		public void generateKey(out byte[] pri, out byte[] pub) {
			IAsymmetricCipherKeyPairGenerator aliceKeyGen = GeneratorUtilities.GetKeyPairGenerator ("ECDH");
			/*
			BigInteger[] safePrimes = geneHelper.DHParametersHelper.GenerateSafePrimes(256, 30, new SecureRandom());
			BigInteger p = safePrimes[0];
			BigInteger _q = safePrimes[1];
			BigInteger g = geneHelper.DHParametersHelper.SelectGenerator(p, _q, new SecureRandom());
			
			var dhPara = new DHParameters(p, g, _q, 100, 0, BigInteger.Two, null);
			*/
			//var alicePara = new DHParameters();
			
				
				
		    DHParametersGenerator aliceGenerator = new DHParametersGenerator ();
		    
		    aliceGenerator.Init (256, 30, new SecureRandom ());
		    DHParameters dhPara = aliceGenerator.GenerateParameters ();
		    
		    //var aliceParameters = dhPara;
		
		    var x92 = ECNamedCurveTable.GetByName("P-256");
            var ecc2 = new ECDomainParameters(x92.Curve, x92.G, x92.N, x92.H, x92.GetSeed());
            
		    //KeyGenerationParameters aliceKGP = new DHKeyGenerationParameters (new SecureRandom (), aliceParameters);
		    KeyGenerationParameters aliceKGP = new ECKeyGenerationParameters (ecc2, new SecureRandom());
			aliceKeyGen.Init (aliceKGP);

		    //aliceKeyGen.Init(keygenerationParameters);
		    //IBasicAgreement aliceKeyAgree = AgreementUtilities.GetBasicAgreement ("ECDH");
		    //aliceKeyAgree.
		    AsymmetricCipherKeyPair aliceKeyPair = aliceKeyGen.GenerateKeyPair ();
		    
		    //var  csPri = ((ECPrivateKeyParameters)aliceKeyPair.Private).Parameters.G.GetEncoded();
		    //var pri_ = ((ECPrivateKeyParameters)aliceKeyPair.Private).D.ToByteArray();
		    //var _pri = new ECPrivateKeyParameters(new BigInteger(pri_), ecDomPars2).Parameters.G.GetEncoded();
		    
		    var csPub = ((ECPublicKeyParameters)aliceKeyPair.Public).Q.GetEncoded();
		    var csPri = ((ECPrivateKeyParameters)aliceKeyPair.Private).D.ToByteArray();
		    pub = csPub;
			pri = csPri;
			util.debugWriteLine("pub " + util.getArrStr(pub));
			util.debugWriteLine("pub base64 " + System.Convert.ToBase64String(pub));
		    util.debugWriteLine("pri " + util.getArrStr(pri));
		    
//		    var auth = System.Convert.FromBase64String("MRxTrqwecC9880jcfqsM9A==");
		   
		}
		public byte[] generateAuth() {
			var r = new byte[16];
			new Random().NextBytes(r);
			return r;
		}
		public string decrypt(string message, 
				byte[] pri, byte[] pub, byte[] auth) {
			
			//var mes2 = "tML3Z3nVBOY9fdx5oM8YiwAAEABBBDpYmXBk8jAnGXHm8JvFnVdx4aWE0l7EXQ5GWTqpEkKp1OpAedtaRgnBrYPyzGDqK7mq621TAA1uwcgHOGbMHxCHg_bh5t3I7W3Qmg8rFgb25aMdKixeepVNhslZurI-myIJ-o1OJ9iLCMO62GZcSUKZ3QKMV7dvM7NS6JLFT_dDZZI5LqHTy4Bx7RSgN826MGpmxqhQCsZlk2ujOpM6rCT15TjKPXjPrEH1Yr1BA5HSBKLi1yk5EXUQm6c_BFs2q7rpu1vloLFsG24hf-Ua6wxKKAYJLUg0l9Dyh1v6upsl87nJhvUeGKN0GDYbLLmaJIjAWpMEPsiDq9kzMmiOUF0YhNzLEGiWClzUVO-CgE1grnNYHeJpj43phe6Ee6rnE_XRLuAsC9tvbaYfT2omiNhlSpqkaY2RNrjKqG-ecnvopr1p0USKJ_GRlQxPdX02DoxU7B53VddAW29IZ8ZI2xTunAYoHYtz4HrFtbM2ADnTjbdrtMIjPzbydcvPr_0LJJz5igjTCpdMEMtAjLS1_khoNXjNJ0Zk_wtsbUSpMYV6dijrlUcXbCeXnuHODmIauckRZWoZz6P3K_YSIKJGo88ZHVe-dyEkH0B1E65zXyh3GTIGD2R0ufA71zLAy6Ie3vZkXPBIWnIwwc1k0b3h-HrYAgknXYAjmSGm3KRDrR3ILISsFaohVOe0lAEJ_s2zeSXlCgwraZWxJ7BVKopLp2-Btua303pL927WLK7QX0Cn8f4USy9N";
			var mes = message.Replace("-", "+").Replace("_", "/");
			var a = 4 - (mes.Length % 4);
			for (var pi = 0; pi < a && a != 4; pi++) mes += "=";
			
			var payload = System.Convert.FromBase64String(mes);
			var cryptoParams = getCryptoParamsFromPayload(payload);
			
			if (cryptoParams.ciphertext.Length == 0) {
				util.debugWriteLine(" Zero length messages will be passed as null.");
				return null;
			}
			
			//var ikm = computeSharedSecret(pri, pub);
			var ikm = computeSharedSecret(pri, cryptoParams.senderKey);
			
			//var ivBase64Str = auth;
			//var auth = System.Convert.FromBase64String(ivBase64Str);
			//var _pubBase64 = "BKIODluKUeUzaATh9/Ibtqe+RtxDwzfTB/YS7wPbMcCL9Ks/KhbSnR9BkW6qV6ycpPsRN16obNPD8rRF8+pGW20=";
			//var pubBase64 = System.Convert.FromBase64String(_pubBase64);
			var senderKey = cryptoParams.senderKey;
			var salt = cryptoParams.salt;
			var ciphertext = cryptoParams.ciphertext;
			var rs = cryptoParams.rs;
			
			byte[] key, nonce;
			deriveKeyAndNonce(ikm, auth, 
					out key, out nonce, 
					pub, 
					senderKey, salt);
			var _chunkArray = chunkArray(ciphertext, rs);
			var decodedChunks = decodeChunks(_chunkArray, nonce, key);
			var text = System.Text.Encoding.UTF8.GetString(decodedChunks);
			
			return text;
		}
		
		//public string decryptTest(byte[] crypted, byte[] key) {
		public string decryptTest() {
			var mes2 = "tML3Z3nVBOY9fdx5oM8YiwAAEABBBDpYmXBk8jAnGXHm8JvFnVdx4aWE0l7EXQ5GWTqpEkKp1OpAedtaRgnBrYPyzGDqK7mq621TAA1uwcgHOGbMHxCHg_bh5t3I7W3Qmg8rFgb25aMdKixeepVNhslZurI-myIJ-o1OJ9iLCMO62GZcSUKZ3QKMV7dvM7NS6JLFT_dDZZI5LqHTy4Bx7RSgN826MGpmxqhQCsZlk2ujOpM6rCT15TjKPXjPrEH1Yr1BA5HSBKLi1yk5EXUQm6c_BFs2q7rpu1vloLFsG24hf-Ua6wxKKAYJLUg0l9Dyh1v6upsl87nJhvUeGKN0GDYbLLmaJIjAWpMEPsiDq9kzMmiOUF0YhNzLEGiWClzUVO-CgE1grnNYHeJpj43phe6Ee6rnE_XRLuAsC9tvbaYfT2omiNhlSpqkaY2RNrjKqG-ecnvopr1p0USKJ_GRlQxPdX02DoxU7B53VddAW29IZ8ZI2xTunAYoHYtz4HrFtbM2ADnTjbdrtMIjPzbydcvPr_0LJJz5igjTCpdMEMtAjLS1_khoNXjNJ0Zk_wtsbUSpMYV6dijrlUcXbCeXnuHODmIauckRZWoZz6P3K_YSIKJGo88ZHVe-dyEkH0B1E65zXyh3GTIGD2R0ufA71zLAy6Ie3vZkXPBIWnIwwc1k0b3h-HrYAgknXYAjmSGm3KRDrR3ILISsFaohVOe0lAEJ_s2zeSXlCgwraZWxJ7BVKopLp2-Btua303pL927WLK7QX0Cn8f4USy9N";
			var mes2Len = mes2.Length;
			mes2 = mes2.Replace("-", "+").Replace("_", "/");
			var payload = System.Convert.FromBase64String(mes2);
			var cryptoParams = getCryptoParamsFromPayload(payload);
			
			if (cryptoParams.ciphertext.Length == 0) {
				util.debugWriteLine(" Zero length messages will be passed as null.");
				return null;
			}
			
			var ikm = new byte[]{229,117,31,140,147,25,166,89,216,167,168,76,102,138,17,45,215,222,57,18,219,59,152,99,41,194,251,100,180,254,223,60};
			var ivBase64Str = "MRxTrqwecC9880jcfqsM9A=="; //auth
			var auth = System.Convert.FromBase64String(ivBase64Str);
			var _pubBase64 = "BKIODluKUeUzaATh9/Ibtqe+RtxDwzfTB/YS7wPbMcCL9Ks/KhbSnR9BkW6qV6ycpPsRN16obNPD8rRF8+pGW20=";
			var pubBase64 = System.Convert.FromBase64String(_pubBase64);
			var senderKey = new Byte[]{4,58,88,153,112,100,242,48,39,25,113,230,240,155,197,157,87,113,225,165,132,210,94,196,93,14,70,89,58,169,18,66,169,212,234,64,121,219,90,70,9,193,173,131,242,204,96,234,43,185,170,235,109,83,0,13,110,193,200,7,56,102,204,31,16};
			var salt = new Byte[]{180,194,247,103,121,213,4,230,61,125,220,121,160,207,24,139};
			var ciphertext = new Byte[]{135,131,246,225,230,221,200,237,109,208,154,15,43,22,6,246,229,163,29,42,44,94,122,149,77,134,201,89,186,178,62,155,34,9,250,141,78,39,216,139,8,195,186,216,102,92,73,66,153,221,2,140,87,183,111,51,179,82,232,146,197,79,247,67,101,146,57,46,161,211,203,128,113,237,20,160,55,205,186,48,106,102,198,168,80,10,198,101,147,107,163,58,147,58,172,36,245,229,56,202,61,120,207,172,65,245,98,189,65,3,145,210,4,162,226,215,41,57,17,117,16,155,167,63,4,91,54,171,186,233,187,91,229,160,177,108,27,110,33,127,229,26,235,12,74,40,6,9,45,72,52,151,208,242,135,91,250,186,155,37,243,185,201,134,245,30,24,163,116,24,54,27,44,185,154,36,136,192,90,147,4,62,200,131,171,217,51,50,104,142,80,93,24,132,220,203,16,104,150,10,92,212,84,239,130,128,77,96,174,115,88,29,226,105,143,141,233,133,238,132,123,170,231,19,245,209,46,224,44,11,219,111,109,166,31,79,106,38,136,216,101,74,154,164,105,141,145,54,184,202,168,111,158,114,123,232,166,189,105,209,68,138,39,241,145,149,12,79,117,125,54,14,140,84,236,30,119,85,215,64,91,111,72,103,198,72,219,20,238,156,6,40,29,139,115,224,122,197,181,179,54,0,57,211,141,183,107,180,194,35,63,54,242,117,203,207,175,253,11,36,156,249,138,8,211,10,151,76,16,203,64,140,180,181,254,72,104,53,120,205,39,70,100,255,11,108,109,68,169,49,133,122,118,40,235,149,71,23,108,39,151,158,225,206,14,98,26,185,201,17,101,106,25,207,163,247,43,246,18,32,162,70,163,207,25,29,87,190,119,33,36,31,64,117,19,174,115,95,40,119,25,50,6,15,100,116,185,240,59,215,50,192,203,162,30,222,246,100,92,240,72,90,114,48,193,205,100,209,189,225,248,122,216,2,9,39,93,128,35,153,33,166,220,164,67,173,29,200,44,132,172,21,170,33,84,231,180,148,1,9,254,205,179,121,37,229,10,12,43,105,149,177,39,176,85,42,138,75,167,111,129,182,230,183,211,122,75,247,110,214,44,174,208,95,64,167,241,254,20,75,47,77};
			var rs = 4096;
			
			byte[] key, nonce;
			deriveKeyAndNonce(ikm, auth, 
					out key, out nonce, 
					pubBase64, 
					senderKey, salt);
			var _chunkArray = chunkArray(ciphertext, rs);
			var decodedChunks = decodeChunks(_chunkArray, nonce, key);
			var text = System.Text.Encoding.UTF8.GetString(decodedChunks);
			
			return text;
		}
		public CryptoParams getCryptoParamsFromPayload(byte[] payload) {
			if (payload.Length < 21) {
				util.debugWriteLine("bad payload length");
				return null;
			}
			//var _rs0 = (payload[16] << 24); 
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
//			salt = senderKey = ciphertext = null;
			//Array.Copy(payload.co, 0, salt, 0, 16);
			//Array.Copy(payload, 21, senderKey, 0, keyIdLen);
			//Array.Copy(payload, 21 + keyIdLen, ciphertext, 0, payload.Length - (21 + keyIdLen));
			salt = _payloadList.GetRange(0, 16).ToArray();
			senderKey = _payloadList.GetRange(21, keyIdLen).ToArray();
			ciphertext = _payloadList.GetRange(21 + keyIdLen, payload.Length - (21 + keyIdLen)).ToArray();
			return new CryptoParams(salt,
				rs, senderKey,
				ciphertext);
		}
		public class CryptoParams {
			public byte[] salt;
			public int rs;
			public byte[] senderKey;
			public byte[] ciphertext;
			public CryptoParams(byte[] salt, int rs, 
					byte[] senderKey, byte[] ciphertext) {
				this.salt = salt;
				this.rs = rs;
				this.senderKey = senderKey;
				this.ciphertext = ciphertext;
			}
		}
		public bool deriveKeyAndNonce(byte[] key, byte[] auth,
				out byte[] bits, out byte[] nonce, 
				byte[] pub, byte[] senderPub, byte[] salt) {
			var authKdf = new Hkdf();
			
			var authInfo = new List<byte>();
			authInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("WebPush: info\0"));
			//authInfo.AddRange(System.Text.Encoding.UTF8.GetBytes(pub));
			//authInfo.AddRange(System.Text.Encoding.UTF8.GetBytes(senderPub));
			authInfo.AddRange(pub);
			authInfo.AddRange(senderPub);
			var prk = authKdf.DeriveKey(auth, key, authInfo.ToArray(), 32);
			
			var prkKdf = new Hkdf();
			var keyInfo = new List<byte>();
			keyInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("Content-Encoding: aes128gcm\0"));
			var keyInfoPrk = prkKdf.DeriveKey(salt, prk, keyInfo.ToArray(), 16);
			
			var Kdf = new Hkdf();
			var nonceInfo = new List<byte>();
			nonceInfo.AddRange(System.Text.Encoding.UTF8.GetBytes("Content-Encoding: nonce\0"));
			var nonceInfoPrk = prkKdf.DeriveKey(salt, prk, nonceInfo.ToArray(), 12);
			
			bits = keyInfoPrk;
			nonce = nonceInfoPrk;
			return true;
		}
		
		class Hkdf
		{
		Func<byte[],byte[],byte[]> keyedHash;

			public Hkdf()
		    {
		        var hmac = new HMACSHA256();
		        keyedHash = (key, message)=>
		        {
		            hmac.Key=key;
		            return hmac.ComputeHash(message);
		        };
		    }
		
		    public byte[] Extract(byte[] salt, byte[] inputKeyMaterial)
		    {
		        return keyedHash(salt, inputKeyMaterial);
		    }
		
		    public byte[] Expand(byte[] prk, byte[] info, int outputLength)
		    {
		        var resultBlock = new byte[0];
		        var result = new byte[outputLength];
		        var bytesRemaining = outputLength;
		        for (int i = 1; bytesRemaining > 0; i++)
		        {
		            var currentInfo = new byte[resultBlock.Length + info.Length + 1];
		            Array.Copy(resultBlock, 0, currentInfo, 0, resultBlock.Length);
		            Array.Copy(info, 0, currentInfo, resultBlock.Length, info.Length);
		            currentInfo[currentInfo.Length - 1] = (byte)i;
		            resultBlock = keyedHash(prk, currentInfo);
		            Array.Copy(resultBlock, 0, result, outputLength - bytesRemaining, Math.Min(resultBlock.Length, bytesRemaining));
		            bytesRemaining -= resultBlock.Length;
		        }
		        return result;
		    }
		
		    public byte[] DeriveKey(byte[] salt, byte[] inputKeyMaterial, byte[] info, int outputLength)
		    {
		        var prk = Extract(salt, inputKeyMaterial);
		        var result = Expand(prk, info, outputLength);
		        return result;
		    }
		}
		private List<byte[]> chunkArray(byte[] array, int size) {
			/*
			var start = array.byteOffset || 0;
			array = array.buffer || array;
			*/
			var start = 0;
			var index = 0;
			var result = new List<byte[]>();
			while(index + size <= array.Length) {
				var buf = new byte[size];
				Array.Copy(array, start + index, buf, 0, size);
				result.Add(buf);
				//result.Add(array.Copynew Uint8Array(array, start + index, size));
				index += size;
			}
			if (index < array.Length) {
				var buf = new byte[array.Length - index];
				Array.Copy(array, start + index, buf, 0, buf.Length);
				result.Add(buf);
				//result.push(new Uint8Array(array, start + index));
			}
			return result;
		}
		private byte[] decodeChunks(List<byte[]> chunkArray, byte[] nonce, byte[] key) {
			//(slice, index, nonce, key, last)  last index >= chunks.length - 1)
			var aes = new AESGCM();
			for (var i = 0; i < chunkArray.Count; i++) {
				var iv = generateNonce(nonce, i);
				
				var dec = aes.DecryptWithKey(chunkArray[0], key, iv);
				//var base64 = Convert.ToBase64String(chunkArray);
				//new Org.BouncyCastle.Crypto.BufferedAeadBlockCipher(aead
				//var decrypted = Encryption.AESGCM.testDecrypt(chunkArray[i], key, nonce);
				//var j = 0;
				//var enc = Encryption.AESGCM.SimpleEncrypt("aaa", key);
				return dec;
			}
			/*
			let params = {
				name: "AES-GCM",
				iv: generateNonce(nonce, index)
			};
			let decoded = await crypto.subtle.decrypt(params, key, slice);
			return this.unpadChunk(new Uint8Array(decoded), last);
			*/
			return null;
		}
		private byte[] generateNonce(byte[] _base, int index) {
			if (index >= System.Math.Pow(2, 48)) {
				util.debugWriteLine("Nonce index is too large  BAD_CRYPTO");
				return null;
			}
			var nonce = new byte[12];
			Array.Copy(_base, 0, nonce, 0, 12);
			
			
			for (var i = 0; i < 6; ++i) {
				var b = System.Math.Pow(256, i);
				int num0 = (int)(index / System.Math.Pow(256, i));
				var tes0 = num0 & (byte)0xff;
				
				//byte b = nonce[nonce.Length - 1 - i] ^ yte)num1 & (byte)3);
				var nonceI = nonce.Length - 1 - i;
				nonce[nonceI] ^= (byte)tes0;
				//nonce[nonce.Length - 1 - i] ^= (byte)(index / System.Math.Pow(256, i)) & 0xff;
				//(index / Math.pow(256, i)) & 0xff;
			}
			return nonce;
		}
		public byte[] computeSharedSecret(byte[] pri, byte[] pub) {
			const string Algorithm = "ECDH";
			//const int KeyBitSize = 128;
			
			X9ECParameters ecPars = NistNamedCurves.GetByName("P-256");
  			ECDomainParameters ecDomPars = new ECDomainParameters(ecPars.Curve, ecPars.G, ecPars.N, ecPars.H, ecPars.GetSeed());
		    ECCurve curve = ecDomPars.Curve;
		    
		    ECPoint pubQ = curve.DecodePoint(pub);
		    //ECPoint pubQ = ecPars.Curve.DecodePoint(pub);
		    //ECPoint priQ = curve.DecodePoint(pri);
		    IBasicAgreement _aliceKeyAgree = AgreementUtilities.GetBasicAgreement (Algorithm);
		    
		    //csPri = ((ECPrivateKeyParameters)aliceKeyPair.Private).D.ToByteArray();
		    var priKeyPara = new ECPrivateKeyParameters(new BigInteger(pri), ecDomPars);//priQ.Curve.
		    _aliceKeyAgree.Init (priKeyPara);
		    
		    AsymmetricKeyParameter pubKeyPara = new ECPublicKeyParameters(pubQ, ecDomPars);
		    //SubjectPublicKeyInfo _key = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKeyPara);
		    //var pubPara = new ECPublicKeyParameters(priQ, ecDomPars);
		    BigInteger aliceAgree = _aliceKeyAgree.CalculateAgreement (pubKeyPara);
		    
		    KeyParameter sharedKey = new KeyParameter (aliceAgree.ToByteArrayUnsigned ());
		    
           	//return sharedKey.;
           	return sharedKey.GetKey();
		}

	}
}
