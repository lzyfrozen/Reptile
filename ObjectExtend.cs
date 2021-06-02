using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Reptile
{
    public static class ObjectExtend
    {
        /// <summary>
        /// 拷贝对象到一个字典，如果对象为null，就返回空字典。
        /// </summary>
        public static Dictionary<string, object> ToDictionary(this object that, params string[] excludeProperties)
        {
            if (excludeProperties == null)
            {
                excludeProperties = new string[0];
            }

            var dic = new Dictionary<string, object>();

            if (that == null)
            {
                return dic;
            }

            foreach (var property in that.GetType().GetProperties())
            {
                if (property.CanRead && !excludeProperties.Contains(property.Name))
                {
                    if (property.PropertyType.IsValueType)
                    {
                        dic[property.Name] = property.GetValue(that, null);
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        dic[property.Name] = property.GetValue(that, null);
                    }
                    else if (property.PropertyType.IsArray)
                    {
                        var arr = property.GetValue(that, null) as Array;
                        for (int i = 0; i < arr.Length; i++)
                        {
                            var itemType = arr.GetValue(i).GetType();
                            if (itemType.IsValueType || itemType == typeof(string))
                            {
                                dic.Add(property.Name + $"[{i}]", arr.GetValue(i));
                            }
                            else
                            {
                                var tmpDic = ToDictionary(arr.GetValue(i));
                                foreach (var key in tmpDic.Keys)
                                {
                                    dic.Add(property.Name + $"[{i}]." + key, tmpDic[key]);
                                }
                            }
                        }
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        var list = property.GetValue(that, null) as IEnumerable;
                        int i = 0;
                        foreach (var item in list)
                        {
                            var itemType = item.GetType();
                            if (itemType.IsValueType || itemType == typeof(string))
                            {
                                dic.Add(property.Name + $"[{i}]", item);
                            }
                            else
                            {
                                var tmpDic = ToDictionary(item);
                                foreach (var key in tmpDic.Keys)
                                {
                                    dic.Add(property.Name + $"[{i}]." + key, tmpDic[key]);
                                }
                            }
                            i++;
                        }
                    }
                    else
                    {
                        var tmpDic = ToDictionary(property.GetValue(that, null));
                        foreach (var key in tmpDic.Keys)
                        {
                            dic.Add(property.Name + "." + key, tmpDic[key]);
                        }
                    }
                }
            }

            return dic;
        }

        public static string ToSafeString(this object obj)
        {
            if (obj == null)
                return string.Empty;
            return obj.ToString();
        }
    }
}
