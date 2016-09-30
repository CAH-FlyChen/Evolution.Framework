using Evolution.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.IInfrastructure
{
    public static class Tools
    {
        /// <summary>
        /// 密码计算规则
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="secretKey">SecretKey</param>
        /// <returns>加密后的密码</returns>
        public static string CaculatePWD(string password, string secretKey)
        {
            string pwd = password.ToLower();
            string aesString = AESEncrypt.Encrypt(pwd, secretKey).ToLower();
            string md5String = Md5.md5(aesString, 32).ToLower();
            return md5String;
        }
    }
}
