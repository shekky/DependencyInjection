// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Extensions.DependencyInjection.Abstractions.V2
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection2" />.
    /// </summary>
    public static class ServiceCollectionServiceExtensions2
    {
        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient(
            this IServiceCollection2 services,
            Type serviceType,
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(services, serviceType, implementationType, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient(
            this IServiceCollection2 services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Add(services, serviceType, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient<TService, TImplementation>(this IServiceCollection2 services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddTransient(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient(
            this IServiceCollection2 services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return services.AddTransient(serviceType, serviceType);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient<TService>(this IServiceCollection2 services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddTransient(typeof(TService));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient<TService>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddTransient(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection2 AddTransient<TService, TImplementation>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddTransient(typeof(TService), implementationFactory);
        }



        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped(
            this IServiceCollection2 services,
            Type serviceType,
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(services, serviceType, implementationType, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped(
            this IServiceCollection2 services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Add(services, serviceType, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped<TService, TImplementation>(this IServiceCollection2 services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped(
            this IServiceCollection2 services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return services.AddScoped(serviceType, serviceType);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped<TService>(this IServiceCollection2 services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddScoped(typeof(TService));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped<TService>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddScoped(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection2 AddScoped<TService, TImplementation>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddScoped(typeof(TService), implementationFactory);
        }


        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton(
            this IServiceCollection2 services,
            Type serviceType,
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Add(services, serviceType, implementationType, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton(
            this IServiceCollection2 services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Add(services, serviceType, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton<TService, TImplementation>(this IServiceCollection2 services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton(
            this IServiceCollection2 services,
            Type serviceType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return services.AddSingleton(serviceType, serviceType);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton<TService>(this IServiceCollection2 services)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton(typeof(TService));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton<TService>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton<TService, TImplementation>(
            this IServiceCollection2 services,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return services.AddSingleton(typeof(TService), implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton(
            this IServiceCollection2 services,
            Type serviceType,
            object implementationInstance)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            var serviceDescriptor = ServiceDescriptor2.Singleton(serviceType, implementationInstance);
            services.Add(serviceDescriptor);
            return services;
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection2"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection2"/> to add the service to.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection2 AddSingleton<TService>(
            this IServiceCollection2 services,
            TService implementationInstance)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return services.AddSingleton(typeof(TService), implementationInstance);
        }

        private static IServiceCollection2 Add(
            IServiceCollection2 collection,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
        {
            collection.Add(new TypeServiceDescriptor(serviceType, implementationType, lifetime));
            return collection;
        }

        private static IServiceCollection2 Add(
            IServiceCollection2 collection,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            ServiceLifetime lifetime)
        {
            collection.Add(new FactoryServiceDescriptor(serviceType, implementationFactory, lifetime));
            return collection;
        }
    }
}