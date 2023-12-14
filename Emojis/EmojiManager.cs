using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Emojis
{
    static class EmojiManager
    {
        private static List<string?> GetAllValues()
        {
            Emojis e = new Emojis();
            return e.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string)).ToDictionary(f => f.Name, f => (string?)f.GetValue(null)).Values.ToList();
        }
        private static List<string> GetNonNull()
        {
            List<string> values = new List<string>();
            foreach (var item in GetAllValues())
                if (item != null)
                    values.Add(item);
            return values;
        }
        public static List<string> GetEmojis()
        {
            return GetNonNull();
        }
    }
}
