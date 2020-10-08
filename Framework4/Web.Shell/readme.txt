delete all controllers.
remove startup.cs from your application.
clean out the contents of your program class and make it look like this:

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Tiny.Framework.Web.Shell;

namespace [your.application.thingy].Shell
{
    /// <summary>
    /// the program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// the program entry point, main.
        /// </summary>
        /// <param name="args">the commandline arguments.</param>
        public static void Main(string[] args)
        {
            WebShell.BuildWebHost<Startup>(args).Run();
        }

        /// <summary>
        /// the inherited start up class.
        /// </summary>
        public sealed class Startup : ShellStartup
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Startup"/> class.
            /// </summary>
            /// <param name="configuration">the configuration.</param>
            /// <param name="environment">the environment.</param>
            public Startup(IConfiguration configuration, IHostEnvironment environment)
                : base(configuration, environment)
            {
            }
        }
    }
}

passing content in nuget-packages doens't seem to work very well anymore. so...

you will need to create two files:
    participating_assemblies.json
        this file contains the names of the libraries with registration information.
with this sort of content.
[
    "Tiny.Framework",
    "Tiny.Framework.Web",
    "Tiny.Framework.Web.Shell",
    "Tiny.Framework.Web.Swashbuckle",
    "Tiny.Framework.Web.Redis",
    "Tiny.Framework.Web.ApplicationInsights"
]

if you choose not to add the any tiny framework web packs (swashbuckle, redis
or application insights), you can remove those lines. you will probably have
to add some of your own libraries too, overtime as they grow.

    service_configuration.json
        this file contains the setting for the host, localsettings.json is ignored.
with this sort of content.
{
    "ListeningProtocol": "http",
    "ListeningAddress": "*",
    "ListeningPort": 5000,
    "ContentRootPath": "your-site-content-path-here",
    "WebRootFolderName": "your-root-folder-name-here"
}

you can change the listening address and port to suite your needs.
you need to set "copy on change" to these files.

the shell can load pretty much anything you like, by configuration.

    authentication
    authorization
    https redirection
    forwarded headers
    static files
    cross-origin resource sharing (cors)
    url 'slugify'

if you have stuff you want to register for loading you can, consider using the
standard service and configuration registration process. but in the event you're
consuming somebodies else code and they use those 'infernal' static routines you
can expose them like this:

create a file called registrations.cs and add lines simlar to these:

[assembly: AddRoutineRegistration(typeof(MyStartupThingyRegistration), MyStartupThingyRegistration.AddMyStartupThingyRoutine)]
[assembly: UseRoutineRegistration(typeof(MyStartupThingyRegistration), MyStartupThingyRegistration.UseMyStartupThingyRoutine)]

these can be mix and match; you don't necessarily need to use both of them.

web service configuration is still done using appsettings.json
sample:
{
    "AllowedHosts": "*",
    "SystemMonitoring": {
        "InstrumentationKey": "your-application-insights-key",
        "Environment": "your-environment-name"
    },
    "Logging": {
        "ApplicationInsights": {
            "LogLevel": {
                "Default": "Information",
                "Microsoft": "Error"
            }
        },
        "LogLevel": {
            "Default": "Information"
        }
    },
    "Startup": {
        "Authentication": {
            "IsActivated": true (or false, or missing = false)
        },
        "Authorization": {
            "IsActivated": true (or false, or missing = false)
        },
        "SlugifyTransformer": {
            "IsActivated": true (or false, or missing = false)
        },
        "StaticFiles": {
            "IsActivated": true (or false, or missing = false)
        },
        "CrossOrigins": {
            "IsActivated": true (or false, or missing = false)

            "AcceptedOrigins": [
                "your-accepted-origin-1",
                "your-accepted-origin-2"
            ],
            "AcceptedHeaders": [
                "your-accepted-header-1",
                "your-accepted-header-2"
            ],
            "AcceptedMethods": [ "GET", "PUT" ], (get, put, post, patch, delete etc)
            "AllowAnyHeader": false, (or true, or missing = true)
            "AllowAnyMethod": false, (or true, or missing = true)
            "AllowAnyOrigin": false, (or true, or missing = true)
            "AllowCredentials": false (or true, or missing = true)
        },
        "ForwardedHeaders": {
            "IsActivated": true (or false, or missing = false)

            "ForwardedForHeaderName": "your-custom-forwarded-for-name-here", (missing, defaults to LAMP standards)
            "ForwardedProtoHeaderName": "your-custom-forwarded-proto-name-here", (missing, defaults to LAMP standards)
            "ForwardLimit": 1, (default, probably best not to change this, leave this out and it will be one)
            "KnownProxies": [ "your-proxy-1", "your-proxy-2" ],
            "AllowedHosts": [ "your-known-host-1", "your-known-host-2" ]
        },
    },
    "StorageCache": {
        "ConnectionString": "your-azure-redis-connection-string",
        "ItemLifetimeInMinutes": 30
    }
}
