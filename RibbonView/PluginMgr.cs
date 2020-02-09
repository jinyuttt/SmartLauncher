#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：RibbonView
*项目描述 ：
*命名空间 ：RibbonView
*文件名称 ：PluginMgr.cs
* 功能描述 ：PluginMgr
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
using System.Windows;

namespace RibbonView
{
    public class PluginMgr : IFPlugin.IMainView
    {
        private const string PluginName = "RibbonView";
        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Init(Dictionary<string, string> arg)
        {
           
        }

        public void Show()
        {
            RibbonMain window = new RibbonMain();
            window.Show();
            Application.Current.MainWindow = window;  
        }

        public string StartupUri()
        {
            // return "RibbonMain.xaml";
            return "pack://application:,,,/RibbonView;component/RibbonMain.xaml";
        }
    }
}
