using System.Threading.Tasks;

namespace Hanselman.Services
{
    public interface IGetJsonTokenValues
    {
        Task<string> GetValueFor(string theToken, string fromJson);
    }
}
