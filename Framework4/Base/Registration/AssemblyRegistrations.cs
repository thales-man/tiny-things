using Tiny.Framework.Diagnostic;
using Tiny.Framework.Diagnostic.Internal;
using Tiny.Framework.Flow;
using Tiny.Framework.Flow.Internal;
using Tiny.Framework.IO;
using Tiny.Framework.IO.Internal;
using Tiny.Framework.Providers;
using Tiny.Framework.Providers.Internal;
using Tiny.Framework.Registration.Attributes;

// Project level
// Diagnostic
[assembly: InternalRegistration(typeof(ICreateDiagnosticMessages), typeof(DiagnosticMessageFactory), TypeOfRegistrationScope.Singleton)]

// Flow
[assembly: InternalRegistration(typeof(IManageMessageMediation), typeof(MediationMessageManager), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(IProvideSafeOperations), typeof(SafeOperationsProvider), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(IManageTimedActions), typeof(ManageTimedActions), TypeOfRegistrationScope.Transient)]
[assembly: InternalRegistration(typeof(ISynchroniseActions), typeof(SynchroniseActions), TypeOfRegistrationScope.Transient)]

// IO
[assembly: InternalRegistration(typeof(ISerializeJsonTypes), typeof(JsonConverter), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(ISerializeXMLTypes), typeof(XMLConverter), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(IManageAssets), typeof(AssetManager), TypeOfRegistrationScope.Singleton)]

// Providers
[assembly: InternalRegistration(typeof(IResolveGroupedResources), typeof(GroupedResourceResolver), TypeOfRegistrationScope.Singleton)]
[assembly: InternalRegistration(typeof(IProvideRegistrationDetails), typeof(RegistrationDetailProvider), TypeOfRegistrationScope.Singleton)]
