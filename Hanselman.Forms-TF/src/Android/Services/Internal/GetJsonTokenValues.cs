using System.Composition;
using System.Json;
using System.Threading.Tasks;

namespace Hanselman.Services.Internal
{
    [Shared]
    [Export(typeof(IGetJsonTokenValues))]
    public class GetJsonTokenValues :
        IGetJsonTokenValues
    {
        public async Task<string> GetValueFor(string theToken, string fromJson) =>
            await Task.Run(() =>
            {
                var temp = JsonValue.Parse(fromJson);
                return temp[theToken];
            });
    }
}

