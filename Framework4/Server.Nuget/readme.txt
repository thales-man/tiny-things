
all packs using the tiny framework have a new version number aligned to an 
initial value of 2.0.0 as all of the packs now conform to .net standard 2.

you should never consume this pack directly.

it only serves a purpose to the Tiny.API, which requires some of the contracts 
the service configuration and bootstrapping functionality.

however...

you will still need a file called:

    participating_assemblies.xml

in the Assets (or Resources) folder of your solution. the file will need 
to be copied to the output folder of your project. the file contains a short 
list of assemblies that contain the exports you are interested in to run your 
solution. the content of the file needs to look like this:

<?xml version="1.0" encoding="utf-8" ?>
<ArrayOfstring xmlns:i="http://www.w3.org/2001/XMLSchema-instance"
               xmlns="http://schemas.microsoft.com/2003/10/Serialization/Arrays">
    <string>tiny.framework</string>
    <string>tiny.framework.server</string>
    <!--
    <string>add the names</string>
    <string>of your exe's</string>
    <string>and dll's here</string>
    -->
</ArrayOfstring>

this version of the file will need to contain references to the tiny framework 
and the tiny framework server libraries.

you will also need a service configuration file in the same location
the service configuration file should hold one of the hosts IP addresses; this 
can be the loopback address.

<?xml version="1.0" encoding="utf-8"?>
<ServiceConfiguration
    xmlns:i="http://www.w3.org/2001/XMLSchema-instance"
    xmlns="http://schemas.thestripedlawn.co/tiny.bootstrap">
        <ListeningAddress>127.0.0.1</ListeningAddress>
        <ListeningPort>1026</ListeningPort>
    <ServicePrefix>api</ServicePrefix>
</ServiceConfiguration>

this file is called:
    
    service_configuration.xml

on many systems there are strictions using "lower end" port numbers. Mac's don't 
allow access to ports under 1024 without admin permission. the "Service Prefix" 
is a piece of text that appears betweet the IP address / domain name - port 
number and the endpoint call.

http://localhost:1026/ api /myrequest?message=enjoy%21

in both cases, there should only be one of these files per solution.

Enjoy :)