using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace LBTL.Api
{
    public static class DataBaseStorage
    {
        private static LiteDatabase db = new LiteDatabase("Filename=LBTL\\LBTL.db; Password=b5fb1fcb-1c74-48d9-be52-cd2200c59882; Journal=false");
        private static LiteCollection<Pair> settingPage = db.GetCollection<Pair>("settings");

        public static void InsertSetting(string key, string value)
        {
            InsertSetting(new Pair { Key = key, Value = value });
        }

        public static void InsertSetting(Pair keyValuePair)
        {
            if (settingPage.Exists(x => x.Key == keyValuePair.Key))
            {
                settingPage.Delete(x => x.Key == keyValuePair.Key);
            }
            settingPage.Insert(keyValuePair);
        }

        public static string GetSettingValue(string key)
        {
            if (settingPage.Exists(x => x.Key == key))
            {
                return settingPage.FindOne(x => x.Key == key).Value;
            }
            return null;
        }
    }

    public class Pair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
