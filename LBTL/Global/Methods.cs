using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMCCC.Launcher;
using LBTL.Api;
using Microsoft.Win32;

namespace LBTL.Global
{
    public static class Methods
    {
        public static void Initialize()
        {
            launcherCoreInitialize();
            fileInitialize();
        }

        private static void launcherCoreInitialize()
        {
            Variable.Core = LauncherCore.Create();
        }

        private static void fileInitialize()
        {
            if (!Directory.Exists("LBTL"))
                Directory.CreateDirectory("LBTL");
            if (!File.Exists("LBTL\\LBTL.db"))
            {
                DataBaseStorage.InsertSetting("MaxMemory", "1024");
                DataBaseStorage.InsertSetting("OnlineMode", "false");
                DataBaseStorage.InsertSetting("Player", "Player");
                DataBaseStorage.InsertSetting("Email", "example@example.com");
                DataBaseStorage.InsertSetting("Password", "");
                DataBaseStorage.InsertSetting("JavaPath", FromBMCL.GetJavaDir() ?? "null");
            }
        }

        public static void LaunchGame()
        {
            
        }
    }

    public static class FromBMCL
    {
        public static string GetJavaDir()
        {
            try
            {
                var reg = Registry.LocalMachine;
                var openSubKey = reg.OpenSubKey("SOFTWARE");
                var registryKey = openSubKey?.OpenSubKey("JavaSoft");
                if (registryKey != null)
                    reg = registryKey.OpenSubKey("Java Runtime Environment");
                if (reg == null) return null;
                var javaList = new List<string>();
                foreach (var ver in reg.GetSubKeyNames())
                {
                    try
                    {
                        var command = reg.OpenSubKey(ver);
                        if (command == null) continue;
                        var str = command.GetValue("JavaHome").ToString();
                        if (str != "")
                            javaList.Add(str + @"\bin\javaw.exe");
                    }
                    catch { return null; }
                }
                //优先java8
                foreach (var java in javaList)
                {
                    if (java.ToLower().Contains("jre8") || java.ToLower().Contains("jdk1.8") || java.ToLower().Contains("jre1.8"))
                    {
                        return java;
                    }
                }
                return javaList[0];
            }
            catch { return null; }

        }
    }
}
