#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：PluginManager
*项目描述 ：
*命名空间 ：PluginManager
*文件名称 ：PluginLoadContext.cs
* 功能描述 ：PluginLoadContext
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace PluginManager
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;
        private AssemblyDependencyResolver resolver;
        private ConcurrentDictionary<string, AssemblyDependencyResolver> _DependencyDLL = null;
        public string[] DependencyDir = null;

        public PluginLoadContext(string pluginPath)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        
              _DependencyDLL = new ConcurrentDictionary<string, AssemblyDependencyResolver>();
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            this.Resolving += PluginLoadContext_Resolving;
        }
        public PluginLoadContext()
        {
            _DependencyDLL = new ConcurrentDictionary<string, AssemblyDependencyResolver>();
        }
        private Assembly PluginLoadContext_Resolving(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            if (DependencyDir != null)
            {

                var  files= DependencyDir.SelectMany(X => Directory.GetFileSystemEntries(X, assemblyName.Name + ".dll"));
                foreach(var file in files)
                {
                   return   LoadFromAssemblyPath(file);
                }
            }
            return null;
        }

        private Assembly Default_Resolving(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            string assemblyFileName = null;
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName?.Split(',')[0] == assemblyName.Name) ;
            if(assembly==null)
            {
                //if (_DependencyDLL.ContainsKey(assemblyName.Name))
                //{
                //    var index = _DependencyDLL[assemblyName.Name];
                //    if(index<DependencyDir.Length-1)
                //    {
                //        assemblyFileName=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,DependencyDir[index], assemblyName.Name,".dll");
                        
                //    }
                //    assembly= LoadFromAssemblyPath(assemblyFileName);
                //    _DependencyDLL[assemblyName.Name]=index++;
                //    return assembly;
                //}
                //else
                //{
                //    assemblyFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".dll");

                //}
            }
           
            
            return assembly;
           
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            resolver = _resolver;
            if(resolver == null)
            {
                if (!_DependencyDLL.TryGetValue(assemblyName.Name, out resolver))
                {
                    string file = null;
                     file = DependencyDir.SelectMany(X => Directory.GetFileSystemEntries(X, assemblyName.Name + ".dll")).FirstOrDefault();
                    if (file == null)
                    {
                        file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName.Name + ".dll");
                    }
                    if(!File.Exists(file))
                    {
                        return null;
                    }
                    resolver = new AssemblyDependencyResolver(file);
                    _DependencyDLL[assemblyName.Name] = resolver;
                }
            }
            string assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }

            return IntPtr.Zero;
        }
    }
}
