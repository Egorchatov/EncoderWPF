using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EncoderWPF
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }

            return description;
        }

        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach(Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }

        public static IList ToList(this Enum[] enumValues)
        {
            if(enumValues == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();

            foreach(Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }
    }
}
