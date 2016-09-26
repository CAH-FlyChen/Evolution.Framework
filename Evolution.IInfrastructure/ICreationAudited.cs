///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
using System;

namespace Evolution.Domain
{
    public interface ICreationAudited
    {
        string CreatorUserId { get; set; }
        DateTime? CreatorTime { get; set; }
    }
}