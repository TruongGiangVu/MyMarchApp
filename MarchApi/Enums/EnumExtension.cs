using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MarchApi.Enums;

/// <summary> Các hàm mở rộng của enum </summary>
public static class EnumExtension
{
    /// <summary>
    /// Convert ErrorCode enum to string
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns> String presents Error Code </returns>
    public static string ToErrorCodeString(this ErrorCode enumValue)
        => ((int)(IConvertible)enumValue).ToString("D2");

    /// <summary>
    /// Get ErrorCode display name
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns> String presents display name </returns>
    public static string GetDisplay(this Enum enumValue)
    {
        // Get the field info for the enum value
        string enumValueString = enumValue.ToString();
        FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValueString);

        // Get the Display attribute, if it exists
        DisplayAttribute? displayAttribute = fieldInfo?
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .Cast<DisplayAttribute>()
            .FirstOrDefault();

        // Return the Name property of the Display attribute, or the enum name if not found
        return displayAttribute?.Name ?? enumValueString;
    }
}
