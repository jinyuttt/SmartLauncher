#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：SmartLauncher
*项目描述 ：
*命名空间 ：SmartLauncher
*文件名称 ：LauncherApp.cs
* 功能描述 ：LauncherApp
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace SmartLauncher
{
    public class LauncherApp:Application
    {
        static Dictionary<string, string> cfg = null;
        [STAThread]
        public static void Main()
        {
            //
            ReadCfg readCfg = new ReadCfg();
            cfg= readCfg.read("LauncherApp.ini");
            Init();
            LauncherApp app = new LauncherApp();//创建application对象
            app.InitializeComponent();
            app.Run();
           
        }

        private static void Init()
        {
            string tmp = "Plugins";
            if(cfg.ContainsKey("Plugins"))
            {
                tmp = cfg["Plugins"];
            }
            //view插件
            PluginManager.PluginTypeMgr.Instance.FilterPlugin = (X) =>
            {
                var r = typeof(IFPlugin.IView).IsAssignableFrom(X) && !X.IsAbstract;
                return r;
            };

            PluginManager.PluginTypeMgr.Instance.FilterSrvPlugin = (X) =>
              {
                //找到
                var name = X.GetField("PluginName", BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
                  if (name != null)
                  {
                      return name.GetValue(null).ToString();
                  }
                  return "";
              };
            PluginManager.PluginTypeMgr.Instance.AddIFDir(new string[] { tmp,AppDomain.CurrentDomain.BaseDirectory });
            //
            PluginManager.PluginTypeMgr.Instance.FilterCommon = (X) =>
            {
                return X.GetCustomAttribute<IFPlugin.ComSrvAttribute>() != null;
            };
            PluginManager.PluginTypeMgr.Instance.FilterSrvCommon = (X) =>
            {
                return X.GetCustomAttribute<IFPlugin.ComSrvAttribute>().Name;
            };
           
            PluginManager.PluginTypeMgr.Instance.AddAttributeDir(new string[] { tmp, AppDomain.CurrentDomain.BaseDirectory });

        }
        public void InitializeComponent()
        {
           
             var srv=  PluginManager.PluginTypeMgr.Instance.GetSrv<IFPlugin.IMainView>();
            if(srv!=null)
            {
                srv.Init(cfg);
                 string uri=  srv.StartupUri();
                if(!string.IsNullOrEmpty(uri))
                {
                    StartupUri = new Uri(uri,UriKind.RelativeOrAbsolute);
                }
                else
                {
                    srv.Show();
                }
            }
         

        }
        protected override void OnStartup(StartupEventArgs e)
        {
          
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string err = "异常信息:" + e.Exception.Message.ToString();
            MessageBox.Show(err);
            e.Handled = true;
        }
        private void Application_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            //询问用户是否允许终止会话
            string msg = string.Format("{0} 是否要终止Windows会话？", e.ReasonSessionEnding);
            MessageBoxResult result = MessageBox.Show(msg, "Session Ending", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {//如果点击yes，允许终止，否则禁止终止会话
                e.Cancel = true;
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            
        }
    }
}
