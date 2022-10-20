using System.Reflection;
using System;

namespace Nt.Core.Services.Internal
{
    internal static class ParameterDefaultValue
    {

        public static bool TryGetDefaultValue(ParameterInfo parameter, out object defaultValue)
        {
            // TODO: To Test.
            bool hasDefaultValue = true; // CheckHasDefaultValue(parameter, out bool tryToGetDefaultValue);
            bool tryToGetDefaultValue = true;
            defaultValue = null;

            if (hasDefaultValue)
            {
                if (tryToGetDefaultValue)
                {
                    defaultValue = parameter.DefaultValue;
                }

                bool isNullableParameterType = parameter.ParameterType.IsGenericType &&
                    parameter.ParameterType.GetGenericTypeDefinition() == typeof(Nullable<>);

                // Workaround for https://github.com/dotnet/runtime/issues/18599
                if (defaultValue == null && parameter.ParameterType.IsValueType
                    && !isNullableParameterType) // Nullable types should be left null
                {
                    defaultValue = CreateValueType(parameter.ParameterType);
                }

                //[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2067:UnrecognizedReflectionPattern",
                //    Justification = "CreateValueType is only called on a ValueType. You can always create an instance of a ValueType.")]
                object CreateValueType(Type t) =>
                // TODO: To test.
                null; // FormatterServices.GetUninitializedObject(t);

                // Handle nullable enums
                if (defaultValue != null && isNullableParameterType)
                {
                    Type underlyingType = Nullable.GetUnderlyingType(parameter.ParameterType);
                    if (underlyingType != null && underlyingType.IsEnum)
                    {
                        defaultValue = Enum.ToObject(underlyingType, defaultValue);
                    }
                }
            }

            return hasDefaultValue;
        }
    }
}