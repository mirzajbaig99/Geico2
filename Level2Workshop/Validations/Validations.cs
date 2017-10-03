using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Level2Workshop.Validations.Attributes;

namespace Level2Workshop.Validations
{
    public static class Validations
    {
        public static void validateRequired<T>(this T t) where T : class
        {
            foreach (var p in t.GetType().GetProperties()
                .Where(x => x.GetCustomAttributesData()
                .Any(attr => attr.AttributeType.Name == "RequiredAttribute"))
                )
            {
                object value = p.GetValue(t);
                object defaultValue = p.PropertyType.GetDefaultValue();
                if (value == null || (defaultValue != null && defaultValue.Equals(value)))
                {
                    throw new ArgumentException();
                }
            }
        }
    }

    public static class typeExtensions
    {
        public static Object GetDefaultValue(this Type T)
        {
            if (T.IsValueType)
            {
                if (!T.IsGenericType) //&& T.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                //    Type t = Nullable.GetUnderlyingType(T);
                //    return Activator.CreateInstance(t);
                //} else
                //{
                    return Activator.CreateInstance(T);
                }
            }

            return null;
        }
    }
}
