using Evolution.Data;
using Evolution.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Evolution.Plugins.WeiXin;

namespace Evolution.Plugins.WeiXin.Entities
{
    public enum MenuActionType
    {
        ���� = 0,
        ͼ�� = 1,
        ��ͼ�� = 2
    };
    /// <summary>
    /// �洢�˵����Ƽ��ṹ
    /// </summary>
    public class CustomizeMenuEntity: EntityBase, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string AppId { get; set; }
        public string Title { get; set; }
        public decimal LevelNUM { get; set; }
        public string ParentId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public MenuActionType ActionType { get; set; }
        /// <summary>
        /// ����Id
        /// </summary>
        public string ActionId { get; set; }
        public int NeedOAuth { get; set; }
        public string TenantId { get; set; }
    }
}
