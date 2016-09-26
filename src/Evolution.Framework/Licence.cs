/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Microsoft.AspNetCore.Http;

namespace Evolution.Framework
{
    public sealed class Licence
    {
        public static bool IsLicence(string key,HttpContext _context)
        {
            //string host = _context.Request.Url.Host.ToLower();
            //if (host.Equals("localhost"))
            //    return true;
            //string licence = ConfigurationManager.AppSettings["LicenceKey"];
            //if (licence != null && licence == Md5.md5(key, 32))
            //    return true;

            return false;
        }
        public static string GetLicence()
        {
            //var licence = Configs.GetValue("LicenceKey");
            //if (string.IsNullOrEmpty(licence))
            //{
            //    licence = Common.GuId();
            //    Configs.SetValue("LicenceKey", licence);
            //}
            //return Md5.md5(licence, 32);
            return "";
        }
    }
}
