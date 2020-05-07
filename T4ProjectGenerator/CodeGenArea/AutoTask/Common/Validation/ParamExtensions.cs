using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECoupon.Common
{
    public static class ParamExtensions
    {
        public static string AsEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : source.Trim();
        }

        public static string AsString(this string source, string messageCode)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ParamException(messageCode);
            }
            return source.AsEmpty();
        }

        public static string AsStringDefault(this string source, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return defaultValue;
            }
            return source.AsEmpty();
        }

        public static TSource IsEqual<TSource>(this TSource source, TSource value, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(value) != 0)
            {
                throw new ParamException(messageCode);
            }
            return source;
        }

        public static TSource IsNotEqual<TSource>(this TSource source, TSource value, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(value) == 0)
            {
                throw new ParamException(messageCode);
            }
            return source;
        }

        /// <summary>
        /// 断言字符串长度是否在范围内
        /// </summary>
        public static TSource IsBetween<TSource>(this TSource source, TSource minLimit, TSource maxLimit, string messageCode)
            where TSource : IComparable, IConvertible
        {
            return source.IsGreaterEqual(minLimit, messageCode).IsLessEqual(maxLimit, messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否在范围内
        /// </summary>
        public static TSource IsBetween<TSource>(this TSource source, TSource minLimit, TSource maxLimit, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            return source.IsGreaterEqual(minLimit, defaultValue).IsLessEqual(maxLimit, defaultValue);
        }

        /// <summary>
        /// 断言字符串长度是否大于等于界限
        /// </summary>
        public static TSource IsGreaterEqual<TSource>(this TSource source, TSource limit, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) >= 0)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否大于等于界限
        /// </summary>
        public static TSource IsGreaterEqual<TSource>(this TSource source, TSource limit, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) >= 0)
            {
                return source;
            }
            return defaultValue;
        }


        /// <summary>
        /// 断言字符串长度是否大于界限
        /// </summary>
        public static TSource IsGreater<TSource>(this TSource source, TSource limit, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) > 0)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否大于界限
        /// </summary>
        public static TSource IsGreater<TSource>(this TSource source, TSource limit, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) > 0)
            {
                return source;
            }
            return defaultValue;
        }

        /// <summary>
        /// 断言字符串长度是否小于等于界限
        /// </summary>
        public static TSource IsLessEqual<TSource>(this TSource source, TSource limit, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) <= 0)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否小于等于界限
        /// </summary>
        public static TSource IsLessEqual<TSource>(this TSource source, TSource limit, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) <= 0)
            {
                return source;
            }
            return defaultValue;
        }


        /// <summary>
        /// 断言字符串长度是否小于界限
        /// </summary>
        public static TSource IsLess<TSource>(this TSource source, TSource limit, string messageCode)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) < 0)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否小于界限
        /// </summary>
        public static TSource IsLess<TSource>(this TSource source, TSource limit, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            if (source.CompareTo(limit) < 0)
            {
                return source;
            }
            return defaultValue;
        }


        /// <summary>
        /// 断言实体对象不能为NULL
        /// </summary>
        public static TSource IsNotNull<TSource>(this TSource source, string messageCode)
            where TSource : class
        {
            if (source == null)
            {
                throw new ParamException(messageCode);
            }
            return source;
        }

        /// <summary>
        /// 断言实体对象为NULL
        /// </summary>
        public static TSource IsNull<TSource>(this TSource source, string messageCode)
            where TSource : class
        {
            if (source != null)
            {
                throw new ParamException(messageCode);
            }
            return source;
        }

        public static TSource AsDynamic<TSource>(this string source, TSource defaultValue)
            where TSource : IComparable, IConvertible
        {
            try
            {
                return (TSource)Convert.ChangeType(source, typeof(TSource));
            }
            catch { return defaultValue; }
        }

        public static TSource AsDynamic<TSource>(this string source, string messageCode)
            where TSource : IComparable, IConvertible
        {
            try
            {
                return (TSource)Convert.ChangeType(source, typeof(TSource));
            }
            catch (Exception ex)
            {
                throw new ParamException(messageCode, ex);
            }
        }

        /// <summary>
        /// 断言枚举数字是否合法
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="message">错误提示消息</param>
        /// <returns>返回合法值</returns>
        public static TEnum AsEnum<TEnum>(this int source, string messageCode) where TEnum : struct
        {
            Type type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ParamException(messageCode);
            }

            foreach (var value in Enum.GetValues(type))
            {
                if (source == Convert.ToInt32(value))
                {
                    return (TEnum)Enum.ToObject(type, source);
                }
            }

            throw new ParamException(messageCode);
        }

        public static string AsStringEnum<TEnum>(this string source, string messageCode) where TEnum : struct
        {
            Type type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ParamException(messageCode);
            }

            foreach (var value in Enum.GetValues(type))
            {
                if (source.Equals(value.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return value.ToString();
                }
            }

            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否在范围内
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="minLength">最小长度并包含最小</param>
        /// <param name="maxLength">最大长度并包含最大</param>
        /// <param name="message">错误提示消息</param>
        /// <returns>返回合法值</returns>
        public static string IsBetween(this string source, int minLength, int maxLength, string messageCode)
        {
            return source.IsGreaterEqual(minLength, messageCode).IsLessEqual(maxLength, messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否大于等于界限
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="limit">界限值并包含界限值</param>
        /// <param name="message">错误提示消息</param>
        /// <returns>返回合法值</returns>
        public static string IsGreaterEqual(this string source, int limit, string messageCode)
        {
            if (source.Length >= limit)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

        /// <summary>
        /// 断言字符串长度是否小于等于界限
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="limit">界限值并包含界限值</param>
        /// <param name="message">错误提示消息</param>
        /// <returns>返回合法值</returns>
        public static string IsLessEqual(this string source, int limit, string messageCode)
        {
            if (source.Length <= limit)
            {
                return source;
            }
            throw new ParamException(messageCode);
        }

    }
}
