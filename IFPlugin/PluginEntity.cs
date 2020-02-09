#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：IFPlugin
*项目描述 ：
*命名空间 ：IFPlugin
*文件名称 ：PluginEntity.cs
* 功能描述 ：PluginEntity
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
  public  class PluginEntity
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public PluginGroup Group { get; set; }


        public int Order { get; set; }

        public List<FormEntity> Forms { get; set; }
    }
}
