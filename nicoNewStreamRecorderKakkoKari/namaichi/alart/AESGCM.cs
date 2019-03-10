using System;
using System.IO;
using System.Text;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace namaichi.alart
{
    
    public class AESGCM
    {
        
        private const int DEFAULT_KEY_BIT_SIZE = 128;
        private const int DEFAULT_MAC_BIT_SIZE = 128;
        private const int DEFAULT_NONCE_BIT_SIZE = 128;

        private readonly int _keySize;
        private readonly int _macSize;
        private readonly int _nonceSize;

        

        public AESGCM()
			: this(DEFAULT_KEY_BIT_SIZE, DEFAULT_MAC_BIT_SIZE, DEFAULT_NONCE_BIT_SIZE)
		{ }

		public AESGCM(int keyBitSize, int macBitSize, int nonceBitSize)
        {

			_keySize = keyBitSize;
			_macSize = macBitSize;
			_nonceSize = nonceBitSize;
        }

        public string DecryptWithKey(string encryptedMessage, string key, int nonSecretPayloadLength = 0)
        {
            if (string.IsNullOrEmpty(encryptedMessage))
            {
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
            }

            var decodedKey = Convert.FromBase64String(key);

            var cipherText = Convert.FromBase64String(encryptedMessage);

            var plaintext = DecryptWithKey(cipherText, decodedKey, new byte[16]);

            return Encoding.UTF8.GetString(plaintext);
        }

        public string EncryptWithKey(string messageToEncrypt, string key, byte[] nonSecretPayload = null)
        {
            if (string.IsNullOrEmpty(messageToEncrypt))
            {
                throw new ArgumentException("Secret Message Required!", "messageToEncrypt");
            }

            var decodedKey = Convert.FromBase64String(key);

            var plainText = Encoding.UTF8.GetBytes(messageToEncrypt);
            var cipherText = EncryptWithKey(plainText, decodedKey, nonSecretPayload);
            return Convert.ToBase64String(cipherText);
        }

        public byte[] DecryptWithKey(byte[] encryptedMessage, byte[] key, byte[] nonce)
        {

            if (encryptedMessage == null || encryptedMessage.Length == 0)
            {
                throw new ArgumentException("Encrypted Message Required!", "encryptedMessage");
            }

            using (var cipherStream = new MemoryStream(encryptedMessage))
            using (var cipherReader = new BinaryReader(cipherStream))
            {
                
				var cipher = new GcmBlockCipher(new AesEngine());
				var parameters = new AeadParameters(new KeyParameter(key), _macSize, nonce);
                cipher.Init(false, parameters);

                //Decrypt Cipher Text
                var cipherText = cipherReader.ReadBytes(encryptedMessage.Length);
                var plainText = new byte[cipher.GetOutputSize(cipherText.Length)];
                var outSize = cipher.GetOutputSize(cipherText.Length);

                var len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
                cipher.DoFinal(plainText, len);

                return plainText;
                //return null;
            }
        }

		public byte[] EncryptWithKey(byte[] messageToEncrypt, byte[] key, byte[] nonce)
		{

			var cipher = new GcmBlockCipher(new AesEngine());
			var parameters = new AeadParameters(new KeyParameter(key), _macSize, nonce);
            cipher.Init(true, parameters);

            //Generate Cipher Text With Auth Tag
            var cipherText = new byte[cipher.GetOutputSize(messageToEncrypt.Length)];
            var len = cipher.ProcessBytes(messageToEncrypt, 0, messageToEncrypt.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            //Assemble Message
            using (var combinedStream = new MemoryStream())
            {
                using (var binaryWriter = new BinaryWriter(combinedStream))
                {
                    //Prepend Authenticated Payload
//                    binaryWriter.Write(nonSecretPayload);
                    //Prepend Nonce
//                    binaryWriter.Write(nonce);
                    //Write Cipher Text
                    binaryWriter.Write(cipherText);
                }
                return combinedStream.ToArray();
            }
            
           return null;
        }        
    }
}