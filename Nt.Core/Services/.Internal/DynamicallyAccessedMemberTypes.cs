using System;

namespace Nt.Core.Services.Internal
{
    [Flags]
    internal enum DynamicallyAccessedMemberType
    {
        /// <summary>
        /// Specifies no members.
        /// </summary>
        None = 0x0,
        //
        // Resumen:
        //     Specifies the default, parameterless public constructor.
        /// <summary>
        /// 
        /// </summary>
        PublicParameterlessConstructor = 0x1,
        //
        // Resumen:
        //     Specifies all public constructors.
        /// <summary>
        /// 
        /// </summary>
        PublicConstructors = 0x3,
        //
        // Resumen:
        //     Specifies all non-public constructors.
        /// <summary>
        /// 
        /// </summary>
        NonPublicConstructors = 0x4,
        //
        // Resumen:
        //     Specifies all public methods.
        /// <summary>
        /// 
        /// </summary>
        PublicMethods = 0x8,
        //
        // Resumen:
        //     Specifies all non-public methods.
        /// <summary>
        /// 
        /// </summary>
        NonPublicMethods = 0x10,
        //
        // Resumen:
        //     Specifies all public fields.
        /// <summary>
        /// 
        /// </summary>
        PublicFields = 0x20,
        //
        // Resumen:
        //     Specifies all non-public fields.
        /// <summary>
        /// 
        /// </summary>
        NonPublicFields = 0x40,
        //     Specifies all public nested types.
        /// <summary>
        /// Specifies all public nested types.
        /// </summary>
        PublicNestedTypes = 0x80,
        /// <summary>
        /// Specifies all non-public nested types.
        /// </summary>
        NonPublicNestedTypes = 0x100,
        /// <summary>
        /// Specifies all public properties.
        /// </summary>
        PublicProperties = 0x200,
        /// <summary>
        /// Specifies all non-public properties.
        /// </summary>
        NonPublicProperties = 0x400,
        /// <summary>
        /// Specifies all public events.
        /// </summary>
        PublicEvents = 0x800,
        /// <summary>
        /// Specifies all non-public events.
        /// </summary>
        NonPublicEvents = 0x1000,
        /// <summary>
        /// Specifies all interfaces implemented by the type.
        /// </summary>
        Interfaces = 0x2000,
        /// <summary>
        /// Specifies all members.
        /// </summary>
        All = -1
    }
}
