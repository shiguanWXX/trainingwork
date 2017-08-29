using System;
using System.ComponentModel.DataAnnotations;

namespace BackgroundEFManage.Model
{
    /// <summary>
    /// 字典详细类
    /// </summary>
  public  class DataDicDetail
    {
        /// <summary>
        /// 字典详细Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string DeChName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string DeEnName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Founder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime FoundTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifyPerson { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 字典Id
        /// </summary>
        public int DId { get; set; }
        public DataDic DataDic { get; set; }
    }
}
