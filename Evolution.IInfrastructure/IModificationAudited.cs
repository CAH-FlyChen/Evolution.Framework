///*******************************************************************************
// * Copyright © 2016 NFine.Framework 版权所有
// * Author: NFine
// * Description: NFine快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
using System;

namespace Evolution.Domain
{
    public interface IModificationAudited
    {
        string LastModifyUserId { get; set; }
        DateTime? LastModifyTime { get; set; }
    }
}