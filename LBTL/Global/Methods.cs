using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KMCCC.Authentication;
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
            //Variable.Core.JavaPath = DataBaseStorage.GetSettingValue("JavaPath");
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
                try
                {
                    DataBaseStorage.InsertSetting("Version", Variable.Core.GetVersions().ToArray()[0].Id);
                }
                catch
                {
                    DataBaseStorage.InsertSetting("Version", "null");
                }
            }
        }

        public static void LaunchGame(bool onlineMode, string playerName, string email, string password, KMCCC.Launcher.Version version, int maxMemory)
        {
            IAuthenticator auth = null;
            if (onlineMode)
            {
                auth = new YggdrasilLogin(email, password, false);
            }
            else
            {
                auth = new OfflineAuthenticator(playerName);
            }

            var option = new LaunchOptions()
            {
                Version = version,
                VersionType = "LBTL",
                Authenticator = auth,
                MaxMemory = maxMemory,
                Mode = LaunchMode.BmclMode
            };
            var result = Variable.Core.Launch(option);
            if (!result.Success)
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        public static string Encrypt(string text, string key)
        {
            var aes = new AesCryptoServiceProvider();
            var textBytes = Encoding.Unicode.GetBytes(text);
            var keyBytes = Encoding.Unicode.GetBytes(key);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, aes.CreateEncryptor(keyBytes, keyBytes), CryptoStreamMode.Write);
            cs.Write(textBytes, 0, textBytes.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string text, string key)
        {
            var aes = new AesCryptoServiceProvider();
            var textBytes = Convert.FromBase64String(text);
            var keyBytes = Encoding.Unicode.GetBytes(key);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, aes.CreateDecryptor(keyBytes, keyBytes), CryptoStreamMode.Write);
            cs.Write(textBytes, 0, textBytes.Length);
            cs.FlushFinalBlock();
            return Encoding.Unicode.GetString(ms.ToArray());
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
