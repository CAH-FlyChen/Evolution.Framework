///*******************************************************************************
// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：http://www.nfine.cn
//*********************************************************************************/
using System;

namespace Evolution.Domain
{
    public interface IDeleteAudited
    {
        /// <summary>
        /// 逻辑删除标记
        /// </summary>
        bool? DeleteMark { get; set; }

        /// <summary>
        /// 删除实体的用户
        /// </summary>
        string DeleteUserId { get; set; }

        /// <summary>
        /// 删除实体时间
        /// </summary>
        DateTime? DeleteTime { get; set; }
    }
}