#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：PluginManager
*项目描述 ：
*命名空间 ：PluginManager
*文件名称 ：TypeProvider.cs
* 功能描述 ：TypeProvider
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using System;

namespace PluginManager
{

    /// <summary>
    /// 框架以外的操作，插件不能直接使用PluginTypeMgr
    /// </summary>
    public class TypeProvider
    {
        private static readonly Lazy<TypeProvider> provider = new Lazy<TypeProvider>();

        public static TypeProvider Instance
        {
            get { return provider.Value; }
        }


        #region 注册
        public void RegisterType<TSrv, TType>()
        {
            PluginTypeMgr.Instance.RegisterType<TSrv, TType>();
        }
        public void RegisterType<TType>(Type srv)
        {
            PluginTypeMgr.Instance.RegisterType<TType>(srv);
        }
        public void RegisterType(Type srv)
        {
            PluginTypeMgr.Instance.RegisterType(srv);
        }
        #endregion

        #region 获取
        public T GetByName<T>(string name)
        {
            return PluginTypeMgr.Instance.GetByName<T>(name);
        }
        public object GetByName(string name, Type type)
        {
            return PluginTypeMgr.Instance.GetByName(name,type);
        }

        public T GetSrv<T>()
        {
            return PluginTypeMgr.Instance.GetSrv<T>();
        }
        public object GetSrv(Type type)
        {
            return PluginTypeMgr.Instance.GetSrv(type);
        }
        #endregion
    }
}
