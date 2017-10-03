using System;
using System.Linq;
using System.Reflection;

namespace Level2Workshop
{
    public class Validation
    {
        public void Validate<T>(T obj)
        {
            ValidateCustomAttribute(obj, typeof(RequiredAttribute), CheckForPropertyNotPopulated);

            ValidateCustomAttribute(obj, typeof(ValidTaxIdAttribute), CheckForInvalidTaxId);
        }

        private static void ValidateCustomAttribute(object obj, Type customAttributeType, Action<object, PropertyInfo> checkValidity)
        {
            var objType = obj.GetType();

            foreach (var propertyInfo in objType.GetProperties()
                .Where(p => p.CustomAttributes.Any(a => a.AttributeType == customAttributeType)))
            {
                var value = propertyInfo.GetValue(obj);
                checkValidity(value, propertyInfo);
            }
        }

        private static void CheckForPropertyNotPopulated(object value, PropertyInfo propertyInfo)
        {
            if (value == null 
                || string.IsNullOrWhiteSpace(value.ToString()) 
                || (propertyInfo.PropertyType == typeof(int) && (int)value == 0))
            {
                throw new ArgumentException("Required property is not set", propertyInfo.Name);
            }
        }

        private static void CheckForInvalidTaxId(object value, PropertyInfo propertyInfo)
        {
            int taxId;

            if (!Int32.TryParse(value.ToString(), out taxId) || taxId < 1 || taxId > 999999999)
            {
                throw new ArgumentOutOfRangeException(propertyInfo.Name, value, "Tax ID number must be numeric and between 1 and 999,999,999");
            }
        }
    }
}
