#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：PluginManager
*项目描述 ：
*命名空间 ：PluginManager
*文件名称 ：PluginMgr.cs
* 功能描述 ：PluginMgr
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using Autofac;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Reflection;

namespace PluginManager
{

    /// <summary>
    /// 插件对象管理，不提供框架外部使用
    /// </summary>
    public class PluginTypeMgr
    {
        private IContainer container = null;
        readonly ContainerBuilder  builder = new ContainerBuilder();
        private static readonly Lazy<PluginTypeMgr> Mgr = new Lazy<PluginTypeMgr>();
       
        public string[] DependencyDir = new string[] { "Libs", "Dev", "Frame", "Plugins" };
        public static PluginTypeMgr Instance
        {
            get { return Mgr.Value; }
        }
        public PluginTypeMgr()
        {
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
           
        }

     

        private Assembly Default_Resolving(AssemblyLoadContext arg1, AssemblyName assemblyName)
        {
           
            if (DependencyDir != null)
            {

                var files = DependencyDir.SelectMany(X => Directory.GetFileSystemEntries(X, assemblyName.Name + ".dll"));
                foreach (var file in files)
                {
                    FileInfo info = new FileInfo(file);
                    return arg1.LoadFromAssemblyPath(info.FullName);
                }
            }
            return null;
        }

        public IContainer Container
        {
            get { if (container == null){ container = builder.Build(); };
                return container; }
        }
        #region 注册
        public Func<Type,bool> FilterPlugin { get; set; }
      
        public Func<Type, string> FilterSrvPlugin { get; set; }

        public Func<Type,bool> FilterCommon { get; set; }

        public Func<Type, string> FilterSrvCommon { get; set; }

        /// <summary>
        /// 添加接口插件目录
        /// </summary>
        /// <param name="dirs"></param>
        public void AddIFDir(string[]  dirs)
        {
            foreach(string dir in  dirs)
            {
                string[] files = Directory.GetFiles(dir, "*.dll");
                AddIFDLL(files);
            }
        }

        /// <summary>
        /// 添加接口插件程序集
        /// </summary>
        /// <param name="files"></param>
        public void AddIFDLL (string[] files)
        {
            foreach(string file in files)
            {
              
                FileInfo info = new FileInfo(file);
                 if(info.Name.StartsWith("DevExpress"))
                {
                    continue;
                }

                
                var asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(info.FullName);
             
                var plugins= asm.ExportedTypes.Where(X => FilterPlugin(X));
                foreach (var srv in plugins)
                {
                    builder.RegisterType(srv).As(srv.GetInterfaces()[0]).InstancePerLifetimeScope();
                    builder.RegisterType(srv).Named(FilterSrvPlugin(srv), srv.GetInterfaces()[0]).SingleInstance();
                }

            }
        }

        /// <summary>
        /// 添加特性插件目录
        /// </summary>
        /// <param name="dirs"></param>
        public  void AddAttributeDir(string[] dirs)
        {
            foreach (string dir in dirs)
            {
                string[] files = Directory.GetFiles(dir, "*.dll");
                AddIFDLL(files);
            }
        }

        /// <summary>
        /// 添加接口插件
        /// </summary>
        /// <param name="files"></param>
        public void AddAttributeDll(string[] files)
        {
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                var asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(info.FullName);
                var plugins = asm.ExportedTypes.Where(FilterCommon);
                foreach (var srv in plugins)
                {
                    builder.RegisterType(srv).As(srv.GetInterfaces()[0]);
                    builder.RegisterType(srv).Named(FilterSrvCommon(srv), srv.GetInterfaces()[0]);
                }

            }
        }

        /// <summary>
        /// 注册接口服务
        /// </summary>
        /// <typeparam name="TSrv">接口实现类</typeparam>
        /// <typeparam name="TType">接口</typeparam>
        public void RegisterType<TSrv, TType>()
        {
            builder.RegisterType<TSrv>().As<TType>();
        }

        /// <summary>
        /// 注册接口服务
        /// </summary>
        /// <typeparam name="TType">接口</typeparam>
        /// <param name="srv">实现接口类型</param>
        public void RegisterType<TType>(Type srv)
        {
            builder.RegisterType(srv).As<TType>();
        }

        /// <summary>
        /// 注册接口服务，注册直接接口
        /// </summary>
        /// <param name="srv">实现接口类</param>
        public void RegisterType(Type srv)
        {
            builder.RegisterType(srv).As(srv.GetInterfaces()[0]);
        }
        #endregion

        #region  获取

        /// <summary>
        /// 按照名称获取服务接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetByName<T>(string name)
        {
           return Container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// 按照接口类型获取服务接口
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetByName(string name,Type type)
        {
            return Container.ResolveNamed(name,type);
        }

        /// <summary>
        /// 直接获取接口类
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public T GetSrv<T>()
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 直接获取接口类
        /// </summary>
        /// <param name="type">接口类型</param>
        /// <returns></returns>
        public object GetSrv(Type type)
        {
            return Container.Resolve(type);
        }
        #endregion
    }
}
