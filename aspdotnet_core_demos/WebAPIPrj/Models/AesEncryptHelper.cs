using System.Security.Cryptography;

namespace WebAPIPrj.Models
{
    public class AesEncryptionHelper
    {
        private readonly string _key;
        private readonly string _iv;

        public AesEncryptionHelper(/*string key, string iv*/)
        {
            //var aes = Aes.Create();
            //aes.KeySize = 256; // for AES-256
            //aes.GenerateKey();
            //aes.GenerateIV();

            //string base64Key = Convert.ToBase64String(aes.Key); // Save this securely
            //string base64IV = Convert.ToBase64String(aes.IV);   // Save this securely
            //_key = base64Key;// key;
            //_iv = base64IV;// iv;

            _key = "PmfLjDfG3cHk6F4JZ1zgoEfFrD6gUKD+OufZ6NptZ9Y=";
            _iv = "NTaU3OnRgkHXLwvwHh7Gqw==";           
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var encryptor = aes.CreateEncryptor();

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string encryptedText)
        {
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(_key);
            aes.IV = Convert.FromBase64String(_iv);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var decryptor = aes.CreateDecryptor();

            using var ms = new MemoryStream(Convert.FromBase64String(encryptedText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
