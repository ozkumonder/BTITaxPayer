using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BTITaxPayerService.Core
{
    public static class CryptExtensions
    {
        private static DESCryptoServiceProvider desProvider = null;
        private static byte[] des_Key;
        private static byte[] des_IV;

        private static readonly byte[] _rgbKey;
        private static readonly byte[] _rgbIv;

        static CryptExtensions()
        {
            desProvider = new DESCryptoServiceProvider();
            des_Key = Encoding.ASCII.GetBytes("BTIDanismanlik");
            des_IV = Encoding.ASCII.GetBytes("BTIProjectTeam");
            _rgbKey = Encoding.ASCII.GetBytes("03595252");
            _rgbIv = Encoding.ASCII.GetBytes("78787878");
        }

        public static string DecryptIt(this string toDecrypt)
        {
            string end;
            string str;
            if (!string.IsNullOrWhiteSpace(toDecrypt))
            {
                try
                {
                    byte[] numArray = Convert.FromBase64String(toDecrypt);
                    MemoryStream memoryStream = new MemoryStream(numArray.Length);
                    DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(_rgbKey, _rgbIv), CryptoStreamMode.Read);
                    memoryStream.Write(numArray, 0, numArray.Length);
                    memoryStream.Position = (long)0;
                    end = (new StreamReader(cryptoStream)).ReadToEnd();
                    cryptoStream.Close();
                }
                catch (CryptographicException cryptographicException)
                {
                    throw cryptographicException;
                }
                str = end;
            }
            else
            {
                str = string.Empty;
            }
            return str;
        }

        public static string EncryptIt(this string toEnrypt)
        {
            if (string.IsNullOrWhiteSpace(toEnrypt))
            {
                toEnrypt = string.Empty;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(toEnrypt);
            MemoryStream memoryStream = new MemoryStream(1024);
            DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(_rgbKey, _rgbIv), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] numArray = new byte[(int)memoryStream.Position];
            memoryStream.Position = (long)0;
            memoryStream.Read(numArray, 0, numArray.Length);
            cryptoStream.Close();
            return Convert.ToBase64String(numArray);
        }

        #region DES

        /// <summary>
        /// DES Algoritması Şifreleme Methodu algorithm 
        /// </summary>
        /// <param name="source">Şifrelenecek string parametre.</param>
        /// <returns>Şifrelenmiş string döner.</returns>
        public static string EncryptIt_Des(this string source)
        {

            return Convert.ToBase64String(Encypt<DESCryptoServiceProvider>(desProvider, source, des_Key, des_IV));
        }

        /// <summary>
        /// Des Algoritması Şifre Çözme Methodu
        /// </summary>
        /// <param name="source">Şifre çözümü olacak parametre</param>
        /// <returns>Şifresiz string döner.</returns>
        public static string DecryptIt_Des(this string source)
        {
            var x = Convert.FromBase64String(source);

            return Decrypt<DESCryptoServiceProvider>(desProvider, x, des_Key, des_IV);
        }

        #endregion

        #region Generic şifreleme ve çözümleme metodları
        /// <summary>
        /// Generic Şifreleme Methodu
        /// </summary>
        /// <typeparam name="T">Algoritma Sağlayıcı Sınıf <c>DESCryptoServiceProvider<c></typeparam>
        /// <param name="provider"></param>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        static byte[] Encypt<T>(T provider, string data, byte[] key, byte[] iv) where T : SymmetricAlgorithm
        {
            byte[] result = null;
            try
            {
                ICryptoTransform encryptor = provider.CreateEncryptor(key, iv);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sWriter = new StreamWriter(cs))
                        {
                            sWriter.Write(data);
                        }
                        result = ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                var mes = e.Message;
                result = null;
            }


            return result;
        }
        /// <summary>
        /// Generic Şifre Çözme Methodu
        /// </summary>
        /// <typeparam name="T">Algoritma Sağlayıcı Sınıf <c>DESCryptoServiceProvider<c></typeparam>
        /// <param name="provider"></param>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        static string Decrypt<T>(T provider, byte[] source, byte[] key, byte[] iv) where T : SymmetricAlgorithm
        {
            string result = string.Empty;

            try
            {
                ICryptoTransform decryptor = provider.CreateDecryptor(key, iv);


                using (MemoryStream ms = new MemoryStream(source))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sReader = new StreamReader(cs))
                        {
                            result = sReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var mes = e.Message;
                result = string.Empty;
            }
            return result;
        }

        #endregion




    }
}
