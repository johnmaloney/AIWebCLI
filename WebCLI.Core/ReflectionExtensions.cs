using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Will instantiate a type from the string typeIdentifier and return an instance of that type as the T variable type. 
        /// </summary>
        /// <typeparam name="T">A type that can be used to represent the instantiated object</typeparam>
        /// <param name="typeIdentifier">Full assembly name and class name in the standard format</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Throws when the casting of the type is not possible and if the assembly name is incorrect</exception>
        public static T InstantiateObject<T>(this string typeIdentifier)
        {
            object instance;
            var split = typeIdentifier.Split(',');
            if (split.Length > 1)
            {
                try
                {
                    var plus = Activator.CreateInstance(split[0], split[1]);
                    instance = plus.Unwrap();

                }
                catch (Exception ex)
                {
                    throw new NotSupportedException("The Assembly Name given was not found. See Inner Exception.", ex);
                }
                
                if (instance is T)
                    return (T)instance;

                throw new NotSupportedException($"The type given [{instance.GetType()}] cannot be cast to {typeof(T)}");
            }

            throw new NotSupportedException(
                $"The type reference [{typeIdentifier}] is not properly formed. The supported text " +
                $"is [Assembly Name], [Class Name]. Example: 'WebCLI.Core.Tests, WebCLI.Core.Tests.Pipes.PlusOnePipe'");
        }
    }
}
