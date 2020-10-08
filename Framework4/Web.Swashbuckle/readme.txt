Registers the swashbuckle swagger document description service.

Add the assembly name:
    Tiny.Framework.Web.Swashbuckle

to your
    participating_assemblies.json

file found in the assets folder of your application.

passing content in nuget-packages doens't seem to work very well anymore. so...

create a "swagger_info.json" file and place it in your assets folder.
you need to set the file to "copy on change".

the file needs to contains something like:

{
    "MainAssembly": "your-shell-application-assembly-name",
    "Version": "version-number (v1)",
    "Title": "document-title",
    "Description": "api-description",
    "TermsOfService": "your-terms-of-service-link",
    "Contact": {
        "Name": "your-contact-name",
        "Email": "your-contact-email",
        "Url": "your-contact-url"
    },
    "License": {
        "Name": "GPLv3",
        "Url": "https://www.gnu.org/licenses/gpl-3.0.html"
    }
}
