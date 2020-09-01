using System;
using System.ComponentModel;
using System.Reflection;

namespace System
{
    public class StringValue : Attribute
    {
        public string Value { get; private set; }

        public StringValue(string value)
        {
            Value = value;
        }
    }
}

namespace HealthyGamerPortal.Common.Extensions
{
    /// <summary>
    /// Extension class for Enum types.
    /// </summary>
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            string stringValue = value.ToString();
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValue[] attrs = fieldInfo.
                GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }
            return stringValue;
        }

        /// <summary>
        /// Gets the value of the <see cref="DescriptionAttribute"/> on an Enum value if any.
        /// </summary>
        /// <param name="value">The value of an Enum to try and get the description for.</param>
        /// <returns>The value of the <see cref="DescriptionAttribute"/> of the specified Enum <paramref name="value"/> if any, Otherwise the name of the specified  Enum <paramref name="value"/></returns>
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : value.ToString();
        }

        /// <summary>
        /// Gets the <c>int</c> value of an Enum as a <c>string</c> to display it somewhere easily.
        /// </summary>
        /// <param name="value">The value of an Enum to get the <c>int</c> value of as a <c>string</c>.</param>
        /// <returns>The <c>string</c> representation of the <c>int</c> value of the specified <paramref name="value"/>.</returns>
        public static string GetValueAsString(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var type = value.GetType();
            var val = Enum.Parse(type, fi.Name);
            return ((int)val).ToString();
        }
    }
}