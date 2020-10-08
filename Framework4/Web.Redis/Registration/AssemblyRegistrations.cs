// -----------------------------------------------------------------------------
// copyright (c) 2020, the striped lawn company limited. all rights reserved.
// the striped lawn company licenses this file to you under the GPLv3 license.
// see the LICENSE file in the project root for more information.
// -----------------------------------------------------------------------------
using Tiny.Framework.Registration.Attributes;
using Tiny.Framework.Web.Redis;
using Tiny.Framework.Web.Redis.Configuration;
using Tiny.Framework.Web.Redis.Services;

// Project level
// Services
[assembly: InternalRegistration(typeof(IProvideCacheStorage), typeof(CacheStorageProvider), TypeOfRegistrationScope.Singleton)]

// Configuration
[assembly: ConfigurationRegistration(typeof(IConfigureCacheStorage), typeof(CacheStorageConfiguration), CacheStorageConfiguration.AppSettingsKey)]

// Build Routines
[assembly: AddRoutineRegistration(typeof(CacheRegistration), CacheRegistration.AddCacheingRoutine)]
