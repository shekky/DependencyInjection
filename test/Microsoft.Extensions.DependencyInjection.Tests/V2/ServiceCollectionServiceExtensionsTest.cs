// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Testing;
using Microsoft.Extensions.DependencyInjection.Abstractions.V2;
using Microsoft.Extensions.DependencyInjection.Specification.Fakes;
using Microsoft.Extensions.DependencyInjection.V2;
using Xunit;
using AbstractionResources = Microsoft.Extensions.DependencyInjection.Abstractions.Resources;

namespace Microsoft.Extensions.DependencyInjection.Tests.V2
{
    public class ServiceCollectionServiceExtensionsTest
    {
        private static readonly FakeService _instance = new FakeService();

        public static TheoryData AddImplementationTypeData
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var implementationType = typeof(FakeService);
                return new TheoryData<Action<IServiceCollection2>, Type, Type, ServiceLifetime>
                {
                    { collection => collection.AddTransient(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Transient },
                    { collection => collection.AddTransient<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Transient },
                    { collection => collection.AddTransient<IFakeService>(), serviceType, serviceType, ServiceLifetime.Transient },
                    { collection => collection.AddTransient(implementationType), implementationType, implementationType, ServiceLifetime.Transient },

                    { collection => collection.AddScoped(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Scoped },
                    { collection => collection.AddScoped<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Scoped },
                    { collection => collection.AddScoped<IFakeService>(), serviceType, serviceType, ServiceLifetime.Scoped },
                    { collection => collection.AddScoped(implementationType), implementationType, implementationType, ServiceLifetime.Scoped },

                    { collection => collection.AddSingleton(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Singleton },
                    { collection => collection.AddSingleton<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Singleton },
                    { collection => collection.AddSingleton<IFakeService>(), serviceType, serviceType, ServiceLifetime.Singleton },
                    { collection => collection.AddSingleton(implementationType), implementationType, implementationType, ServiceLifetime.Singleton },
                };
            }
        }

        [Theory]
        [MemberData(nameof(AddImplementationTypeData))]
        public void AddWithTypeAddsServiceWithRightLifecyle(Action<IServiceCollection2> addTypeAction,
                                                            Type expectedServiceType,
                                                            Type expectedImplementationType,
                                                            ServiceLifetime lifeCycle)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act
            addTypeAction(collection);

            // Assert
            var descriptor = Assert.Single(collection);
            Assert.Equal(expectedServiceType, descriptor.ServiceType);
            Assert.Equal(lifeCycle, descriptor.Lifetime);
        }

        public static TheoryData AddImplementationFactoryData
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var implementationType = typeof(FakeService);
                var objectType = typeof(object);

                return new TheoryData<Action<IServiceCollection2>, Type, Type, ServiceLifetime>
                {
                    { collection => collection.AddTransient(serviceType, s => new FakeService()), serviceType, objectType, ServiceLifetime.Transient },
                    { collection => collection.AddTransient<IFakeService>(s => new FakeService()), serviceType, serviceType, ServiceLifetime.Transient },
                    { collection => collection.AddTransient<IFakeService, FakeService>(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Transient },

                    { collection => collection.AddScoped(serviceType, s => new FakeService()), serviceType, objectType, ServiceLifetime.Scoped },
                    { collection => collection.AddScoped<IFakeService>(s => new FakeService()), serviceType, serviceType, ServiceLifetime.Scoped },
                    { collection => collection.AddScoped<IFakeService, FakeService>(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Scoped },

                    { collection => collection.AddSingleton(serviceType, s => new FakeService()), serviceType, objectType, ServiceLifetime.Singleton },
                    { collection => collection.AddSingleton<IFakeService>(s => new FakeService()), serviceType, serviceType, ServiceLifetime.Singleton },
                    { collection => collection.AddSingleton<IFakeService, FakeService>(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Singleton },
                };
            }
        }

        [Theory]
        [MemberData(nameof(AddImplementationFactoryData))]
        public void AddWithFactoryAddsServiceWithRightLifecyle(
            Action<IServiceCollection2> addAction,
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifeCycle)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act
            addAction(collection);

            // Assert
            var descriptor = Assert.Single(collection);
            Assert.Equal(serviceType, descriptor.ServiceType);
            Assert.Equal(implementationType, descriptor.GetImplementationType());
            Assert.Equal(lifeCycle, descriptor.Lifetime);
        }

        public static TheoryData AddSingletonData
        {
            get
            {
                return new TheoryData<Action<IServiceCollection2>>
                {
                    { collection => collection.AddSingleton<IFakeService>(_instance) },
                    { collection => collection.AddSingleton(typeof(IFakeService), _instance) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(AddSingletonData))]
        public void AddSingleton_AddsWithSingletonLifecycle(Action<IServiceCollection2> addAction)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act
            addAction(collection);

            // Assert
            var descriptor = Assert.Single(collection);
            var instanceDescriptor = Assert.IsType<InstanceServiceDescriptor>(descriptor);

            Assert.Equal(typeof(IFakeService), instanceDescriptor.ServiceType);
            Assert.Same(_instance, instanceDescriptor.ImplementationInstance);
            Assert.Equal(ServiceLifetime.Singleton, instanceDescriptor.Lifetime);
        }

        [Theory]
        [MemberData(nameof(AddSingletonData))]
        public void TryAddNoOpFailsIfExists(Action<IServiceCollection2> addAction)
        {
            // Arrange
            var collection = new ServiceCollection2();
            addAction(collection);
            var d = ServiceDescriptor2.Transient<IFakeService, FakeService>();

            // Act
            collection.TryAdd(d);

            // Assert
            var descriptor = Assert.Single(collection);
            var instanceDescriptor = Assert.IsType<InstanceServiceDescriptor>(descriptor);

            Assert.Equal(typeof(IFakeService), instanceDescriptor.ServiceType);
            Assert.Same(_instance, instanceDescriptor.ImplementationInstance);
            Assert.Equal(ServiceLifetime.Singleton, instanceDescriptor.Lifetime);
        }

        public static TheoryData TryAddImplementationTypeData
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var implementationType = typeof(FakeService);
                return new TheoryData<Action<IServiceCollection2>, Type, Type, ServiceLifetime>
                {
                    { collection => collection.TryAddTransient(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Transient },
                    { collection => collection.TryAddTransient<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Transient },
                    { collection => collection.TryAddTransient<IFakeService>(), serviceType, serviceType, ServiceLifetime.Transient },
                    { collection => collection.TryAddTransient(implementationType), implementationType, implementationType, ServiceLifetime.Transient },

                    { collection => collection.TryAddScoped(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Scoped },
                    { collection => collection.TryAddScoped<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Scoped },
                    { collection => collection.TryAddScoped<IFakeService>(), serviceType, serviceType, ServiceLifetime.Scoped },
                    { collection => collection.TryAddScoped(implementationType), implementationType, implementationType, ServiceLifetime.Scoped },

                    { collection => collection.TryAddSingleton(serviceType, implementationType), serviceType, implementationType, ServiceLifetime.Singleton },
                    { collection => collection.TryAddSingleton<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Singleton },
                    { collection => collection.TryAddSingleton<IFakeService>(), serviceType, serviceType, ServiceLifetime.Singleton },
                    { collection => collection.TryAddSingleton(implementationType), implementationType, implementationType, ServiceLifetime.Singleton },
                };
            }
        }

        [Theory]
        [MemberData(nameof(TryAddImplementationTypeData))]
        public void TryAdd_WithType_AddsService(
            Action<IServiceCollection2> addAction,
            Type expectedServiceType,
            Type expectedImplementationType,
            ServiceLifetime expectedLifetime)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act
            addAction(collection);

            // Assert
            var descriptor = Assert.Single(collection);
            var typeServiceDescriptor = Assert.IsType<TypeServiceDescriptor>(descriptor);

            Assert.Equal(expectedServiceType, typeServiceDescriptor.ServiceType);
            Assert.Same(expectedImplementationType, typeServiceDescriptor.ImplementationType);
            Assert.Equal(expectedLifetime, typeServiceDescriptor.Lifetime);
        }

        [Theory]
        [MemberData(nameof(TryAddImplementationTypeData))]
        public void TryAdd_WithType_DoesNotAddDuplicate(
            Action<IServiceCollection2> addAction,
            Type expectedServiceType,
            Type expectedImplementationType,
            ServiceLifetime expectedLifetime)
        {
            // Arrange
            var collection = new ServiceCollection2();
            collection.Add(ServiceDescriptor2.Transient(expectedServiceType, expectedServiceType));

            // Act
            addAction(collection);

            // Assert
            var descriptor = Assert.Single(collection);
            var typeServiceDescriptor = Assert.IsType<TypeServiceDescriptor>(descriptor);

            Assert.Equal(expectedServiceType, typeServiceDescriptor.ServiceType);
            Assert.Same(expectedServiceType, typeServiceDescriptor.ImplementationType);
            Assert.Equal(ServiceLifetime.Transient, typeServiceDescriptor.Lifetime);
        }

        [Fact]
        public void TryAddIfMissingActuallyAdds()
        {
            // Arrange
            var collection = new ServiceCollection2();
            var d = ServiceDescriptor2.Transient<IFakeService, FakeService>();

            // Act
            collection.TryAdd(d);

            // Assert
            var descriptor = Assert.Single(collection);
            var typeServiceDescriptor = Assert.IsType<TypeServiceDescriptor>(descriptor);

            Assert.Equal(typeof(IFakeService), typeServiceDescriptor.ServiceType);
            Assert.Same(typeof(FakeService), typeServiceDescriptor.ImplementationType);
            Assert.Equal(ServiceLifetime.Transient, typeServiceDescriptor.Lifetime);
        }

        public static TheoryData TryAddEnumerableImplementationTypeData
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var implementationType = typeof(FakeService);
                return new TheoryData<ServiceDescriptor2, Type, Type, ServiceLifetime>
                {
                    { ServiceDescriptor2.Transient<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Transient },
                    { ServiceDescriptor2.Transient<IFakeService, FakeService>(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Transient },

                    { ServiceDescriptor2.Scoped<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Scoped },
                    { ServiceDescriptor2.Scoped<IFakeService, FakeService>(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Scoped },

                    { ServiceDescriptor2.Singleton<IFakeService, FakeService>(), serviceType, implementationType, ServiceLifetime.Singleton },
                    { ServiceDescriptor2.Singleton<IFakeService, FakeService >(s => new FakeService()), serviceType, implementationType, ServiceLifetime.Singleton },

                    { ServiceDescriptor2.Singleton<IFakeService>(_instance), serviceType, implementationType, ServiceLifetime.Singleton },
                };
            }
        }

        [Theory]
        [MemberData(nameof(TryAddEnumerableImplementationTypeData))]
        public void TryAddEnumerable_AddsService(
            ServiceDescriptor2 descriptor,
            Type expectedServiceType,
            Type expectedImplementationType,
            ServiceLifetime expectedLifetime)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act
            collection.TryAddEnumerable(descriptor);

            // Assert
            descriptor = Assert.Single(collection);
            var enumerableDesctiptor = Assert.IsType<EnumerableServiceDescriptor>(descriptor);
            var d = Assert.Single(enumerableDesctiptor.Descriptors);

            Assert.Equal(expectedServiceType, d.ServiceType);
            Assert.Equal(expectedImplementationType, d.GetImplementationType());
            Assert.Equal(expectedLifetime, d.Lifetime);
        }


        [Theory]
        [MemberData(nameof(TryAddEnumerableImplementationTypeData))]
        public void TryAddEnumerable_DoesNotAddDuplicate(
            ServiceDescriptor2 descriptor,
            Type expectedServiceType,
            Type expectedImplementationType,
            ServiceLifetime expectedLifetime)
        {
            // Arrange
            var collection = new ServiceCollection2();
            collection.TryAddEnumerable(descriptor);

            // Act
            collection.TryAddEnumerable(descriptor);

            // Assert
            descriptor = Assert.Single(collection);
            var enumerableDesctiptor = Assert.IsType<EnumerableServiceDescriptor>(descriptor);
            var d = Assert.Single(enumerableDesctiptor.Descriptors);

            Assert.Equal(expectedServiceType, d.ServiceType);
            Assert.Equal(expectedImplementationType, d.GetImplementationType());
            Assert.Equal(expectedLifetime, d.Lifetime);
        }

        public static TheoryData TryAddEnumerableInvalidImplementationTypeData
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var objectType = typeof(object);

                return new TheoryData<ServiceDescriptor2, Type, Type>
                {
                    { ServiceDescriptor2.Transient<IFakeService>(s => new FakeService()), serviceType, serviceType },
                    { ServiceDescriptor2.Transient(serviceType, s => new FakeService()), serviceType, objectType },

                    { ServiceDescriptor2.Scoped<IFakeService>(s => new FakeService()), serviceType, serviceType },
                    { ServiceDescriptor2.Scoped(serviceType, s => new FakeService()), serviceType, objectType },

                    { ServiceDescriptor2.Singleton<IFakeService>(s => new FakeService()), serviceType, serviceType },
                    { FactoryServiceDescriptor.Singleton(serviceType, s => new FakeService()), serviceType, objectType },
                };
            }
        }

        [Theory]
        [MemberData(nameof(TryAddEnumerableInvalidImplementationTypeData))]
        public void TryAddEnumerable_ThrowsWhenAddingIndistinguishableImplementationType(
            ServiceDescriptor2 descriptor,
            Type serviceType,
            Type implementationType)
        {
            // Arrange
            var collection = new ServiceCollection2();

            // Act & Assert
            ExceptionAssert.ThrowsArgument(
                () => collection.TryAddEnumerable(descriptor),
                "descriptor",
                AbstractionResources.FormatTryAddIndistinguishableTypeToEnumerable(implementationType, serviceType));
        }

        [Fact]
        public void AddSequence_AddsServicesToCollection()
        {
            // Arrange
            var collection = new ServiceCollection2();
            var descriptor1 = ServiceDescriptor2.Transient<IFakeService, FakeService>();
            var descriptor2 = ServiceDescriptor2.Transient<IFakeOuterService, FakeOuterService>();
            var descriptors = new[] { descriptor1, descriptor2 };

            // Act
            var result = collection.Add(descriptors);

            // Assert
            Assert.Equal(descriptors, collection);
        }

        [Fact]
        public void Replace_AddsServiceIfServiceTypeIsNotRegistered()
        {
            // Arrange
            var collection = new ServiceCollection2();
            var descriptor1 = ServiceDescriptor2.Transient<IFakeService, FakeService>();
            var descriptor2 = ServiceDescriptor2.Transient<IFakeOuterService, FakeOuterService>();
            collection.Add(descriptor1);

            // Act
            collection.Replace(descriptor2);

            // Assert
            Assert.Equal(new[] { descriptor1, descriptor2 }, collection);
        }

        [Fact]
        public void Replace_ReplacesFirstServiceWithMatchingServiceType()
        {
            // Arrange
            var collection = new ServiceCollection2();
            var descriptor1 = ServiceDescriptor2.Transient<IFakeService, FakeService>();
            collection.Add(descriptor1);
            var descriptor2 = ServiceDescriptor2.Singleton<IFakeService, FakeService>();

            // Act
            collection.Replace(descriptor2);

            // Assert
            Assert.Equal(new[] { descriptor2 }, collection);
        }

        [Fact]
        public void Add_ThrowsWhenAddingMultipleWithSameType()
        {
            // Arrange
            var collection = new ServiceCollection2();
            var descriptor1 = ServiceDescriptor2.Transient<IFakeService, FakeService>();
            collection.Add(descriptor1);
            var descriptor2 = ServiceDescriptor2.Singleton<IFakeService, FakeService>();

            // Act + Assert
            var exception = Assert.Throws<InvalidOperationException>(()=> collection.Add(descriptor2));
            Assert.Equal(exception.Message,
                $"There is already descriptor with service type '{typeof(IFakeService)}' registered.");
        }

        public static TheoryData AddOrderedOverloads
        {
            get
            {
                var serviceType = typeof(IFakeService);
                var implementationType = typeof(FakeServiceWithId);
                return new TheoryData<Action<IServiceCollection2>>
                {
                    collection =>
                    {
                        collection.AddEnumerable<IFakeService, FakeServiceWithId>();
                        collection.AddEnumerable<IFakeService, FakeServiceWithId>(_ => new FakeServiceWithId(1));
                        collection.AddEnumerable<IFakeService>(new FakeServiceWithId(2));
                    },
                    collection =>
                    {
                        collection.AddEnumerable(serviceType, implementationType);
                        collection.AddEnumerable(serviceType, _ => new FakeServiceWithId(1));
                        collection.AddEnumerable(serviceType, new FakeServiceWithId(2));
                    },
                    collection =>
                    {
                        collection.AddEnumerable((ServiceDescriptor2) ServiceDescriptor2.Singleton(serviceType, implementationType));
                        collection.AddEnumerable((ServiceDescriptor2) ServiceDescriptor2.Singleton(serviceType, _ => new FakeServiceWithId(1)));
                        collection.AddEnumerable((ServiceDescriptor2) ServiceDescriptor2.Singleton(serviceType, new FakeServiceWithId(2)));
                    },

                };
            }
        }

        [Theory]
        [MemberData(nameof(AddOrderedOverloads))]
        public void AddEnumerable_SupportsAllServiceKinds(Action<IServiceCollection2> addServices)
        {
            // Arrange
            var collection = new ServiceCollection2();
            addServices(collection);
            var provider = collection.BuildServiceProvider();

            // Act
            var ordered = provider.GetService<IEnumerable<IFakeService>>();
            var array = ordered.OfType<FakeServiceWithId>().ToArray();

            // Assert
            Assert.Equal(3, array.Length);
            Assert.Contains(array, i => i.Id == 0);
            Assert.Contains(array, i => i.Id == 1);
            Assert.Contains(array, i => i.Id == 2);
        }

    }
}