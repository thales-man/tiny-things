using System.Collections.Generic;
using Hanselman.Models;

namespace Hanselman.Services
{
    public interface ITweetStore
    {
        void Save(IReadOnlyCollection<Tweet> tweets);
    }
}

