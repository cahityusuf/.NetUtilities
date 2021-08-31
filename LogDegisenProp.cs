using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jkip.Abstractions.Dto.Arsiv;

namespace Jkip.Application.Utilities
{
    public class LogDegisenProp
    {
        public static List<LogDegisenPropDto> DegisenAlanVarmi<T>(T self, T to) where T : class
        {
            List<LogDegisenPropDto> degisenLogList = new List<LogDegisenPropDto>();
            if (self != null && to != null)
            {
                Type type = typeof(T);
                
                var propList = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                foreach (System.Reflection.PropertyInfo pi in propList)
                {
                    if (IsSimple(pi.PropertyType))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            degisenLogList.Add(new LogDegisenPropDto()
                            {
                                PropName = pi.Name,
                                NewValue = toValue.ToString(),
                                OldValue = selfValue.ToString()
                            });

                            return degisenLogList;
                        }
                    }
                }
                return degisenLogList;
            }
            return degisenLogList;
        }

        static bool IsSimple(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
                   || type.IsEnum
                   || type.Equals(typeof(string))
                   || type.Equals(typeof(decimal));
        }
    }
}
