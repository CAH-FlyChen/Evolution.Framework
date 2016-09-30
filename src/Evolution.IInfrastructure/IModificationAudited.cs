///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
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