///*******************************************************************************
// * Copyright © 2016 NFine.Framework 版权所有
// * Author: NFine
// * Description: NFine快速开发平台
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