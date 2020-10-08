using System.Threading.Tasks;

namespace Tiny.Framework.Services
{
    /// <summary>
    /// i access (the) clipboard
    /// </summary>
    public interface IAccessClipboard
    {
        /// <summary>
        /// get text from the clipboard
        /// </summary>
        /// <returns>the currently running task</returns>
        Task<string> GetText();

        /// <summary>
        /// put the text on the clipboard
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task PutText(string text);
    }
}
