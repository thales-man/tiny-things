Registers Application Insights for logging and telemetry information.

Add the assembly name:
    Tiny.Framework.Web.ApplicationInsights

to your
    participating_assemblies.json

file found in the assets folder of your application.

Add the the contents of:

"SystemMonitoring": {
  "InstrumentationKey": "[your app insights key]",
  "Environment": "[your environment name]"
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
}
