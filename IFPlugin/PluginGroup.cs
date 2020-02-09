#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：IFPlugin
*项目描述 ：
*命名空间 ：IFPlugin
*文件名称 ：PluginGroup.cs
* 功能描述 ：PluginGroup
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using System;
using System.Collections.Generic;
using System.Text;

namespace IFPlugin
{

    /// <summary>
    /// 对应面板
    /// </summary>
  public  class PluginGroup
    {
        public string Caption { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }
    }
}
