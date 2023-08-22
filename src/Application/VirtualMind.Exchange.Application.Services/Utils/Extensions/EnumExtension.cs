using System.ComponentModel;

namespace VirtualMind.Exchange.Application.Services.Utils.Extensions
{
    public static class EnumExtension
    {
        public static TEnum GetEnumValueFromDescription<TEnum>(this string description) where TEnum : Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description.ToUpper())
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }
                else if (field.Name == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }

        public static string GetDescriptionFromValue<TEnum>(this TEnum value) where TEnum : Enum
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (descriptionAttributes != null && descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].Description;
            }

            return value.ToString();
        }
    }
}
