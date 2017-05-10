using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReactiveSamples.Twitter.Common
{
    /// <summary>
    /// Simple implementation of an IoC (inversion of control) container. All registered  types are treated as singletons.
    /// </summary>
    /// <remarks>
    /// Do NOT use this in production. 
    /// </remarks>
    public static class SimpleIoC
    {
        private static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
        private static readonly IDictionary<Type, object> instances = new Dictionary<Type, object>();

        /// <summary>
        /// Register a contract and its concrete implementation.
        /// </summary>
        /// <typeparam name="TContract">The contract.</typeparam>
        /// <typeparam name="TImplementation">The implementation of the contract.</typeparam>
        public static void Register<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }

        /// <summary>
        /// Resolves the contract type to a concrete implementation.
        /// </summary>
        /// <typeparam name="T">The contract type.</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolves the contract type to a concrete implementation.
        /// </summary>
        /// <param name="contract">The contract type.</param>
        /// <returns></returns>
        public static object Resolve(Type contract)
        {
            if (instances.ContainsKey(contract))
                return instances[contract];

            object instance;
            Type implementation = types[contract];
            ConstructorInfo constructor = implementation.GetConstructors()[0];
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            if (!constructorParameters.Any())
            {
                instance = Activator.CreateInstance(implementation);
            }
            else
            {
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }

                instance = constructor.Invoke(parameters.ToArray());
            }
            instances.Add(contract, instance);
            return instance;
        }
    }
}
