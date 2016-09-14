/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Web.Areas.ExampleManage.Controllers
{
    [Area("ExampleManage")]
    public class SendMailController : ControllerBase
    {
        private readonly ILogger<SendMailController> _logger;

        public SendMailController(ILogger<SendMailController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult SendMail(string account, string title, string content)
        {
            MailHelper mail = new MailHelper();
            //mail.MailServer = Configs.GetValue("MailHost");
            //mail.MailUserName = Configs.GetValue("MailUserName");
            //mail.MailPassword = Configs.GetValue("MailPassword");
            mail.MailName = "NFine快速开发平台";
            mail.Send(account, title, content);
            return Success("发送成功。");
        }
    }
}
