using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCLI.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Takes an input with the signature of p:password and returns the password only.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitCharacter"></param>
        /// <returns></returns>
        public static string RightSideOf(this string value, string splitCharacter)
        {
            var values = Regex.Split(value, splitCharacter);
            return values.Skip(1).First();
        }

        /// <summary>
        /// Takes an input with the signature of p:password and returns the p value only.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitCharacter"></param>
        /// <returns></returns>
        public static string LeftSideOf(this string value, string splitCharacter)
        {
            var values = Regex.Split(value, splitCharacter);
            return values.First();
        }

        /// <summary>
        /// Takes a dynamic object and checks to see if there is a node by the param passed.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static bool Has(dynamic item, string nodeName)
        {
            if (item == null) return false;
            if (string.IsNullOrEmpty(nodeName)) return false;

            if (!item.ContainsKey(nodeName)) return false;
            return true;
        }


        /// <summary>
        /// Takes a dynamic object and checks to see if there is a node by the param passed.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static bool HasCollection(dynamic item, int location)
        {
            if (item == null) return false;

            try
            {
                if (item[location] != null) return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
