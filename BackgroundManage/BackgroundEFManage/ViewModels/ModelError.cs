using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundEFManage.ViewModels
{
    /// <summary>
    /// 模型验证错误信息
    /// </summary>
    public class ModelError
    {

        public ModelError(string key, string message)
        {
            Key = key;
            Message = message;
        }
        /// <summary>
        /// 错误字段
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    } 
}
