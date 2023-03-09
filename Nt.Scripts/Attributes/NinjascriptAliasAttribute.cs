using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nt.Scripts.Attributes
{
    /// <summary>
    /// Defines alias to be used in configure options.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class NinjascriptAliasAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="NinjascriptAliasAttribute"/> instance.
        /// </summary>
        /// <param name="alias">The alias to set.</param>
        public NinjascriptAliasAttribute(string alias)
        {
            Alias = alias;
        }

        /// <summary>
        /// The alias of the provider.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets the alias from ninjascript type. If provider doesn't hasn't alias, return null.
        /// </summary>
        /// <param name="ninjascriptType">The type of the ninjascript.</param>
        /// <returns>The alias string.</returns>
        public static string GetAlias(Type ninjascriptType)
        {
            string fullName = typeof(NinjascriptAliasAttribute).FullName;

            IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(ninjascriptType);

            for (int i = 0; i < attributes.Count; i++)
            {
                CustomAttributeData attributeData = attributes[i];
                if (attributeData.AttributeType.FullName == fullName &&
                    attributeData.ConstructorArguments.Count > 0)
                {
                    CustomAttributeTypedArgument arg = attributeData.ConstructorArguments[0];

                    System.Diagnostics.Debug.Assert(arg.ArgumentType == typeof(string));

                    return arg.Value?.ToString();
                }
            }

            return null;
        }
    }
}
