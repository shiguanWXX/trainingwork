using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BackgroundEFManage.DataBLL
{
    /// <summary>
    /// 获取模型验证的错误信息
    /// </summary>
    public static  class CommonBLL
    {
        public static IEnumerable<ViewModels.ModelError> AllModelStateErrors(this ModelStateDictionary modelState)
        {
            var result = new List<ViewModels.ModelError>();
            //找到出错的字段以及出错信息
            var errorFieldsAndMsgs = modelState.Where(m => m.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            foreach (var item in errorFieldsAndMsgs)
            {
                //获取键
                var fieldKey = item.Key;
                //获取键对应的错误信息
                var fieldErrors = item.Errors
                    .Select(e => new ViewModels.ModelError(fieldKey, e.ErrorMessage));
                result.AddRange(fieldErrors);
            }
            return result;
        }
    }
}
