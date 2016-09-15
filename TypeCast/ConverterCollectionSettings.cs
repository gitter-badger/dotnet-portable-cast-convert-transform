﻿// <copyright file=mitlicense.md url=http://lsauer.mit-license.org/ >
//             Lo Sauer, 2013-2016
// </copyright>
// <summary>   A tested, generic, portable, runtime-extensible type converter library   </summary
// <language>  C# > 6.0                                                                 </language>
// <version>   3.0.1.4                                                                  </version>
// <author>    Lorenz Lo Sauer; people credited in the sources                          </author>
// <project>   https://github.com/lsauer/dotnet-portable-type-cast                      </project>
namespace Core.TypeCast
{
    using System;
    using System.Globalization;

    using Core.TypeCast.Base;

    /// <summary>
    /// The settings for the <see cref="ConverterCollection"/>.
    /// </summary>
    public class ConverterCollectionSettings
    {
        /// <summary>
        /// If to use the converter-default wrapper or throw an exception.
        /// </summary>
        private bool converterDefaultWrapperOrException;

        /// <summary>
        /// If to use the function-default wrapper.
        /// </summary>
        private bool useFunctionDefaultWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConverterCollectionSettings"/> class.
        /// </summary>
        /// <param name="defaultValueAnyType">Set to `true` if any argument type is allowed. If set to `false` <see cref="ObjectExtension.ConvertTo{TIn, TOut}(TIn, object)"/> cannot be used.</param>
        /// <param name="useFunctionDefaultWrapper">
        /// Whether to use a function default wrapper if the <see cref="Converter.FunctionDefault"/> is `null`, yet required for <see cref="Converter.Convert"/>.
        /// </param>
        /// <param name="numberFormat">
        /// The number format used for the conversion functions within the <see cref="ConverterCollection"/> instance.
        /// </param>
        /// <param name="converterMissingException">  Whether to throw a converter missing exception.  </param>
        /// <param name="allowGenericTypes">Whether to allow generic types as source or target types of converters</param>
        /// <param name="converterDefaultWrapperOrException">
        /// Whether to use a default-value wrapper if one is required or throw a default-function missing exception.
        /// </param>
        public ConverterCollectionSettings(
            NumberFormatInfo numberFormat = null,
            bool defaultValueAnyType = false,
            bool useFunctionDefaultWrapper = true,
            bool converterMissingException = false,
            bool allowGenericTypes = true,
            bool converterDefaultWrapperOrException = true,
            int boundedCapacity = 10000)
        {
            this.DefaultValueAnyType = defaultValueAnyType;
            this.UseFunctionDefaultWrapper = useFunctionDefaultWrapper;
            this.NumberFormat = numberFormat ?? DefaultNumberFormat;
            this.ConverterMissingException = converterMissingException;
            this.AllowGenericTypes = allowGenericTypes;
            this.ConverterDefaultWrapperOrException = converterDefaultWrapperOrException;
            this.BoundedCapacity = boundedCapacity;
        }

        /// <summary>
        /// Gets or sets the default number format.
        /// </summary>
        public static NumberFormatInfo DefaultNumberFormat { get; set; } = new NumberFormatInfo { NumberGroupSeparator = ".", NumberDecimalDigits = 2 };

        /// <summary>
        /// Gets or sets whether to throw an <see cref="ConverterException"/> if generic types are passed as the source our target <see cref="Type"/>.
        /// </summary>
        public bool AllowGenericTypes { get; set; }

        /// <summary>
        /// Gets or sets the bounded capacity of <see cref="BlockingCollection{Converter}"/> instance in <see cref="ConverterCollection"/>, 
        /// which limit the collection size of <see cref="ConverterCollection.Items"/> to a specific number of items at any given time.
        /// </summary>
        public int BoundedCapacity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use a default-value wrapper if no default-function is set or throw a default-function is missing exception.
        /// </summary>
        public bool ConverterDefaultWrapperOrException
        {
            get
            {
                return this.converterDefaultWrapperOrException;
            }

            set
            {
                this.useFunctionDefaultWrapper = value;
                this.converterDefaultWrapperOrException = !value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to throw a default-function is missing exception.
        /// </summary>
        public bool ConverterMissingException { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the second argument of <see cref="Converter.FunctionDefault"/> is <see cref="Type"/>-checked to 
        /// enforce type-equality of the argument/default-value with the return Type of the Function
        /// </summary>
        /// <remarks>Sets the <see cref="Converter.DefaultValueAnyType"/> auto-property upon adding a <see cref="Converter"/> instance 
        /// to the ´<see cref="ConverterCollection"/>. However if the value changed whilst adding converters, only instances added after the value change will be affected.</remarks>
        /// <seealso cref="Converter.DefaultValueAnyType"/>
        public bool DefaultValueAnyType { get; set; }

        /// <summary>
        /// Gets or sets the number format used by default for <see cref="Converter"/> instances.
        /// </summary>
        public NumberFormatInfo NumberFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use default-value function-wrapper.
        /// </summary>
        public bool UseFunctionDefaultWrapper
        {
            get
            {
                return this.useFunctionDefaultWrapper;
            }

            set
            {
                this.useFunctionDefaultWrapper = value;
                this.converterDefaultWrapperOrException = !value;
            }
        }
    }
}