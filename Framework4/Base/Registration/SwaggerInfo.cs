using Microsoft.OpenApi.Models;

namespace Tiny.Framework.Registration
{
    /// <summary>
    /// the swagger information structure, an extension to the microsoft open api info.
    /// </summary>
    public class SwaggerInfo :
        OpenApiInfo
    {
        /// <summary>
        /// the open api cmmon infomration file name.
        /// </summary>
        public const string CommonFileName = "swagger_info.json";

        /// <summary>
        /// gets or sets the main assembly name.
        /// </summary>
        public string MainAssembly { get; set; }
    }
}
