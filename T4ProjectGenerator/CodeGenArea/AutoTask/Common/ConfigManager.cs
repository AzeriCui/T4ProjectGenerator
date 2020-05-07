using System;
using System.Collections.Generic;
using System.Configuration;

namespace AutoTask.Common
{
    public class ConfigManager
    {
        public static string GetValue(string key)
        {
            return GetValue<string>(key);
        }

        public static TSource GetValue<TSource>(string key)
        {
            return GetValue(key, default(TSource), true);
        }

        public static string GetValue(string key, string defaultValue)
        {
            return GetValue<string>(key, defaultValue);
        }

        public static TSource GetValue<TSource>(string key, TSource defaultValue)
        {
            return GetValue(key, defaultValue, false);
        }

        private static TSource GetValue<TSource>(string key, TSource defaultValue, bool throwException)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                if (throwException)
                {
                    throw new KeyNotFoundException("配置[" + key + "]不存在");
                }
                return defaultValue;
            }
            try
            {
                if (typeof(Enum).IsAssignableFrom(typeof(TSource)))
                {
                    return (TSource)Enum.Parse(typeof(TSource), value);
                }
                return (TSource)Convert.ChangeType(value, typeof(TSource));
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw ex;
                }
                return defaultValue;
            }
        }
    }
}