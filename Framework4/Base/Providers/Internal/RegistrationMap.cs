using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Tiny.Framework.Registration.Attributes;

namespace Tiny.Framework.Providers.Internal
{
    /// <summary>
    /// an internally used registration map.
    /// </summary>
    internal sealed class RegistrationMap :
        Dictionary<TypeOfRegistrationScope, Action<IServiceCollection, ContainerRegistrationAttribute>>
    {
    }
}
