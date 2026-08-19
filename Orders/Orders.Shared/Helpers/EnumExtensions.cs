using System.ComponentModel;
using System.Reflection;

namespace Orders.Shared.Helpers;

public static class EnumExtensions
{
    public static string GetEnumDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            DescriptionAttribute? attribute = Attribute.GetCustomAttribute(
                field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }
}