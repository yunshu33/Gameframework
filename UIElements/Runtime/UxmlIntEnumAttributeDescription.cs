using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yun.UIElements
{
    public class UxmlIntEnumAttributeDescription : TypedUxmlAttributeDescription<int>
    {
        public UxmlIntEnumAttributeDescription(int maxInt)
        {
            type = "string";
            typeNamespace = "http://www.w3.org/2001/XMLSchema";
            defaultValue = 0;
            var uxmlEnumeration = new UxmlEnumeration();
            var stringList = new List<string>();

            this.maxInt = maxInt;
            
            for (var i = 0; i < maxInt; i++)
            {
                stringList.Add(i.ToString());
              
            }

           
            
            uxmlEnumeration.values = stringList;
            restriction = uxmlEnumeration;
        }

        public int maxInt;

        /// <summary>
        ///        <para>
        /// Retrieves the value of this attribute from the attribute bag. Returns it if it is found, otherwise return defaultValue.
        /// </para>
        ///      </summary>
        /// <param name="bag">The bag of attributes.</param>
        /// <param name="cc">The context in which the values are retrieved.</param>
        /// <returns>
        ///   <para>The value of the attribute.</para>
        /// </returns>
        public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc) => GetValueFromBag(bag, cc,
            (Func<string, int, int>)((s, i) => i),
            this.defaultValue);

        public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value) =>
            this.TryGetValueFromBag<int>(bag, cc,
                (Func<string, int, int>)((s, i) => i),
                this.defaultValue, ref value);

        private static int ConvertValueToInt(string v, int defaultValue)
        {
            return v == null || !int.TryParse(v, out var result) ? defaultValue : result;
        }
    }
}