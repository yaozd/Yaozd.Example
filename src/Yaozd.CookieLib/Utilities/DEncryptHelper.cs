using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yaozd.CookieLib
{
    internal class DEncryptHelper
    {
        private const string EncryptKeys = "BBDmwTjBsF7IwTIyGWt1bmFntRyUgMQL";

        #region 使用 缺省密钥字符串 加密/解密string

        /// <summary>
        /// 使用缺省密钥字符串加密string
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, EncryptKeys);
        }
        /// <summary>
        /// 使用缺省密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, EncryptKeys, System.Text.Encoding.Default);
        }

        #endregion

        #region 使用 给定密钥字符串 加密/解密string

        /// <summary>
        /// 使用给定密钥字符串加密string
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// 使用给定密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 使用给定密钥字符串解密string,返回指定编码方式明文
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        #endregion

        #region 使用 缺省密钥字符串 加密/解密/byte[]
        /// <summary>
        /// 使用缺省密钥字符串解密byte[]
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(EncryptKeys);
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(EncryptKeys);
            return Encrypt(original, key);
        }
        #endregion

        #region  使用 给定密钥 加密/解密/byte[]

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }


        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion

        #region md5

        public static string MD5Decrypt(string str)
        {
            return MD5Decrypt(str, 16);
        }

        public static string MD5Decrypt(string str, int code)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）  
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
            else //32位加密  
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }
        }

        /// <summary>
        /// md5加密，不做大小写处理
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string MD5En(string str, int code)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (code == 16) //16位MD5加密（取32位加密的9~25字符）
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }
            else //32位加密  
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
        }

        public static bool CheckMD5(string source, string target, int code)
        {
            return MD5Decrypt(source, code) == target.ToLower();
        }
        public static string GetMD5(string text)
        {
            return
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "MD5").ToUpper();
        }
        #endregion

        #region aes


        //默认密钥向量 
        //private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// AES加密算法,utf8编码(ECB)
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="privateKey">16位密钥</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public static string AESEncrypt(string plainText, string privateKey)
        {
            return AESEncrypt(plainText, privateKey, Encoding.UTF8);
        }

        /// <summary>
        /// AES加密算法（ECB）
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="privateKey">16位密钥</param>
        /// <param name="en">编码，默认Utf8</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public static string AESEncrypt(string plainText, string privateKey, Encoding en)
        {
            // password = ShortMD5(password, charsetName);  
            byte[] plainBytes = en.GetBytes(plainText);
            byte[] keyBytes = en.GetBytes(privateKey);
            Aes kgen = Aes.Create("AES");
            kgen.Mode = CipherMode.ECB;
            kgen.Key = keyBytes;
            ICryptoTransform cTransform = kgen.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);


            ////分组加密算法
            //Aes des = Aes.Create("AES");
            //des.Mode = CipherMode.ECB;  
            ////SymmetricAlgorithm des = Rijndael.Create();
            //byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组 
            ////设置密钥及密钥向量
            //des.Key = Encoding.UTF8.GetBytes(privateKey);
            ////des.IV = _key1;
            //byte[] cipherBytes = null;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            //    {
            //        cs.Write(inputByteArray, 0, inputByteArray.Length);
            //        cs.FlushFinalBlock();
            //        cipherBytes = ms.ToArray();//得到加密后的字节数组
            //        cs.Close();
            //        ms.Close();
            //    }
            //}
            //return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// AES解密（ECB），utf8编码格式
        /// </summary>
        /// <param name="showText">密文</param>
        /// <param name="privateKey">加密串</param>
        /// <returns></returns>
        public static string AESDecrypt(string showText, string privateKey)
        {
            return AESDecrypt(showText, privateKey, Encoding.UTF8);
        }

        /// <summary>
        /// AES解密(ECB)
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <param name="privateKey">16位密钥</param>
        /// <param name="en">编码</param>
        /// <returns>返回解密后的明文字符串</returns>
        public static string AESDecrypt(string showText, string privateKey, Encoding en)
        {
            showText = showText.Replace(" ", "+");
            byte[] cipherText = Convert.FromBase64String(showText);
            Aes des = Aes.Create();
            //SymmetricAlgorithm des = Rijndael.Create();
            des.Key = en.GetBytes(privateKey);
            des.Mode = CipherMode.ECB;
            //des.IV = _key1;
            byte[] decryptBytes = new byte[cipherText.Length];
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    cs.Close();
                    ms.Close();
                }
            }
            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");   ///将字符串后尾的'\0'去掉
        }

        #endregion

    }
}
