using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Base {
    public class CryptoHelper {

        private static readonly Encoding encoding = Encoding.UTF8;

        //used when signing a payload
        public static string GetSignedKey(string uri, string apiSecret) {
            var keyByte = encoding.GetBytes(apiSecret);
            var signedKey = "";
            using (var hmacsha512 = new HMACSHA512(keyByte)) {
                hmacsha512.ComputeHash(encoding.GetBytes(uri));
                signedKey = ConvertByteToString(hmacsha512.Hash);
            }
            return signedKey;
        }

        static string ConvertByteToString(byte[] buff) {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("X2"); /* hex format */
            return sbinary;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            string key = "$ghG$R0U!54R3aD#262P_#2dafQw24VB2";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = "$ghG$R0U!54R3aD#262P_#2dafQw24VB2";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string DecryptPassword(string StringToDecrypt)
        {
            double dblCountLength;
            int intLengthChar;
            string strCurrentChar;
            double dblCurrentChar;
            int intCountChar;
            int intRandomSeed;
            int intBeforeMulti;
            int intAfterMulti;
            int intSubNinetyNine;
            int intInverseAsc;
            string decrypt = "";

            for (dblCountLength = 0; dblCountLength < @StringToDecrypt.Length; dblCountLength++)
            {
                intLengthChar = int.Parse(@StringToDecrypt.Substring((int)dblCountLength, 1));
                strCurrentChar = @StringToDecrypt.Substring((int)(dblCountLength + 1), intLengthChar);
                dblCurrentChar = 0;
                for (intCountChar = 0; intCountChar < strCurrentChar.Length; intCountChar++)
                {
                    dblCurrentChar = dblCurrentChar + (Convert.ToInt32(char.Parse(strCurrentChar.Substring(intCountChar, 1))) - 33) * (Math.Pow(93, (strCurrentChar.Length - (intCountChar + 1))));
                }

                intRandomSeed = int.Parse(dblCurrentChar.ToString().Substring(2, 2));
                intBeforeMulti = int.Parse(dblCurrentChar.ToString().Substring(0, 2) + dblCurrentChar.ToString().Substring(4, 2));
                intAfterMulti = intBeforeMulti / intRandomSeed;
                intSubNinetyNine = intAfterMulti - 99;
                intInverseAsc = 256 - intSubNinetyNine;
                decrypt += Convert.ToChar(intInverseAsc);
                dblCountLength = dblCountLength + intLengthChar;
            }
            return decrypt;
        }
    }
}
