using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMCCC.Launcher;

namespace LBTL.Global
{
    public static class Methods
    {
        public static void Initialize()
        {
            launcherCoreInitialize();
        }

        private static void launcherCoreInitialize()
        {
            Variable.Core = LauncherCore.Create();
        }
    }
}
