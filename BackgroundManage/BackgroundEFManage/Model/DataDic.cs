using System;
using System.Collections.Generic;

namespace BackgroundEFManage.Model
{
    /// <summary>
    /// 数据字典类
    /// </summary>
    public class DataDic
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string DChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string DEnName { get; set; }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool DReadonly { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Founder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FoundTime { get; set; }
        /// <summary>
        /// 字典详细集合
        /// </summary>
        public virtual ICollection<DataDicDetail> DataDicDetails { get; set; }
    }
}
