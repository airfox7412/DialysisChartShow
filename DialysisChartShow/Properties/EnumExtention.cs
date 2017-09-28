using System;
using System.Reflection;
using System.ComponentModel;

public static class EnumExtention
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        var result = attributes.Length > 0 ? attributes[0].Description : value.ToString();
        return result;
    }
}
