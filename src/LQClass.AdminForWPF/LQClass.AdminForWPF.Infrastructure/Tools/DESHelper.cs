using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LQClass.AdminForWPF.Infrastructure.Tools;

public class DESHelper
{
    private const string DES_KEY = "lqclass.com";

    #region 3DES 加解密

    public static string Encrypt3Des(string encryStr, string key = DES_KEY)
    {
        try
        {
            var inputArry = Encoding.Default.GetBytes(encryStr);
            var hashmd5 = new MD5CryptoServiceProvider();
            var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
            var byIv = byKey;
            var ms = new MemoryStream();
            using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
            {
                tDescryptProvider.Mode = CipherMode.ECB;
                using (var cs = new CryptoStream(ms, tDescryptProvider.CreateEncryptor(byKey, byIv),
                           CryptoStreamMode.Write))
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return str;
        }
        catch (Exception)
        {
            return encryStr;
        }
    }

    public static string Decrypt3Des(string decryStr, string key = DES_KEY)
    {
        try
        {
            var inputArry = Convert.FromBase64String(decryStr);
            var hashmd5 = new MD5CryptoServiceProvider();
            var byKey = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
            var byIv = byKey;
            var ms = new MemoryStream();
            using (var tDescryptProvider = new TripleDESCryptoServiceProvider())
            {
                tDescryptProvider.Mode = CipherMode.ECB;
                using (var cs = new CryptoStream(ms, tDescryptProvider.CreateDecryptor(byKey, byIv),
                           CryptoStreamMode.Write))
                {
                    cs.Write(inputArry, 0, inputArry.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
            }

            var str = Encoding.Default.GetString(ms.ToArray());
            ms.Close();
            return str;
        }
        catch (Exception)
        {
            return decryStr;
        }
    }

    #endregion
}