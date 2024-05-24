namespace ApiCommons.DTOs;
using System;

public static class EnumRemover
{
    public static string RemoveEnums<TEnum>(this TEnum enumValue)
    {
        string enumString = enumValue.ToString();
        if (enumString.EndsWith("Enum"))
        {
            return enumString.Substring(0, enumString.Length - 4);
        }
        return enumString;
    }
}