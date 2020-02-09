#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：SmartLauncher
*项目描述 ：
*命名空间 ：SmartLauncher
*文件名称 ：AssemblyLoader.cs
* 功能描述 ：AssemblyLoader
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace PluginManager
{
   public class AssemblyLoader
    {
        private static readonly ICompilationAssemblyResolver AssemblyResolver;
        private static readonly ConcurrentDictionary<string, CompilationLibrary> DependencyDLL;

        static AssemblyLoader()
        {
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            AssemblyResolver = new CompositeCompilationAssemblyResolver(
                new ICompilationAssemblyResolver[]{
                    new AppBaseCompilationAssemblyResolver(AppDomain.CurrentDomain.BaseDirectory),
                    new ReferenceAssemblyPathResolver(),
                    new PackageCompilationAssemblyResolver()
                });
            DependencyDLL = new ConcurrentDictionary<string, CompilationLibrary>();
        }

        private static Assembly Default_Resolving(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
        {
            if (DependencyDLL.ContainsKey(assemblyName.Name))
            {
                var compilationLibrary = DependencyDLL[assemblyName.Name];
                var assemblies = new List<string>();
                if (AssemblyResolver.TryResolveAssemblyPaths(compilationLibrary, assemblies) && assemblies.Count > 0)
                {
                    var assembly = assemblyLoadContext.LoadFromAssemblyPath(assemblies[0]);
                    FindDependency(assembly);
                    return assembly;
                }
            }
            return null;

        }

        public static Assembly GetAssembly(string assemblyName)
        {
            string assemblyFileName = assemblyName;
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName?.Split(',')[0] == assemblyName.TrimEnd()) ?? AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFileName);
            FindDependency(assembly);
            return assembly;
        }

        private static void FindDependency(Assembly assembly)
        {
            DependencyContext dependencyContext = DependencyContext.Load(assembly);
            if (dependencyContext != null)
            {
                foreach (var compilationLibrary in dependencyContext.CompileLibraries)
                {
                    if (!DependencyDLL.ContainsKey(compilationLibrary.Name)
                    && !AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.Split(',')[0] == compilationLibrary.Name))
                    {
                        RuntimeLibrary library = dependencyContext.RuntimeLibraries.FirstOrDefault(runtime => runtime.Name == compilationLibrary.Name);
                        var cb = new CompilationLibrary(
                            library.Type,
                            library.Name,
                            library.Version,
                            library.Hash,
                            library.RuntimeAssemblyGroups.SelectMany(g => g.AssetPaths),
                            library.Dependencies,
                            library.Serviceable);

                        DependencyDLL[library.Name] = cb;
                    }
                }
            }
        }
    
   }
}
