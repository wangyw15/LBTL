using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace LBTL.Api
{
    public static class Settings
    {
        private static LiteDatabase db = new LiteDatabase("");
        private static LiteCollection<Pair> page = db.GetCollection<Pair>("settings");

        public static void Insert(string key, string value)
        {
            Insert(new Pair { Key = key, Value = value });
        }

        public static void Insert(Pair keyValuePair)
        {
            if (page.Exists(x => x.Key == keyValuePair.Key))
            {
                page.Delete(x => x.Key == keyValuePair.Key);
            }
            page.Insert(keyValuePair);
        }

        public static string GetValue(string key)
        {
            if (page.Exists(x => x.Key == key))
            {
                return page.FindOne(x => x.Key == key).Value;
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
