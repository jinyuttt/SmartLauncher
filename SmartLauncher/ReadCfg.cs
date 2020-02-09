#region   文件版本注释
/************************************************************************
*CLR版本  ：4.0.30319.42000
*项目名称 ：SmartLauncher
*项目描述 ：
*命名空间 ：SmartLauncher
*文件名称 ：ReadCfg.cs
* 功能描述 ：ReadCfg
* 创建时间 ：2020
*版本号   :   2020|V1.0.0.0 
---------------------------------------------------------------------
* Copyright @ jinyu 2020. All rights reserved.
---------------------------------------------------------------------

***********************************************************************/
#endregion



using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartLauncher
{
   public class ReadCfg
    {
        public Dictionary<string,string> read(string file)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using(StreamReader  rd=new StreamReader(file))
            {
                while(rd.Read()!=-1)
                {
                   string line=  rd.ReadLine().Trim();
                    if(line.StartsWith("#"))
                    {
                        continue;
                    }
                    string[] cfg = line.Split("=");
                    if(cfg.Length==2)
                    {
                        dic[cfg[0].Trim()] = cfg[1].Trim();
                    }
                }
                return dic;
            }
        }
    }
}
