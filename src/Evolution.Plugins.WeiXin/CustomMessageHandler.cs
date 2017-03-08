using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Agent;
using Senparc.Weixin.Context;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using System.IO;
using Senparc.Weixin.MP.Entities.Request;
using System.Xml;
using System.Xml.Linq;

namespace Evolution.Plugins.WeiXin
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(XDocument doc, PostModel postModel, int maxRecordCount = 0)
            : base(doc, postModel, maxRecordCount)
        {

        }
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //ResponseMessageText也可以是News等其他类型
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            return base.OnEventRequest(requestMessage);
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return base.OnTextRequest(requestMessage);
        }
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return base.OnEvent_SubscribeRequest(requestMessage);
        }

    }
}
