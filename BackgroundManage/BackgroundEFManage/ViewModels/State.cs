using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundEFManage.ViewModels
{
  public  class State
  {
      /// <summary>
      /// 展开
      /// </summary>
      public bool opened { get; set; } = true;

      /// <summary>
      /// 禁用
      /// </summary>
      public bool disabled { get; set; } = false;

      /// <summary>
      /// 选中
      /// </summary>
      public bool selected { get; set; } = false;


  }
}
