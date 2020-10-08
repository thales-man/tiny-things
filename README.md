Tiny Framework: Everywhere (almost)
===============

Built with VS 2019 for Mac. Tiny Framework (4) is now .Net Standard 2 compliant; it extends Christian Falch's work on custom control design work to offer content clipping for Borders and Round Buttons. Tiny Framework 4 also has vector graphics support with Geometry Brushes and Drawings for Paths to be defined in resources (similarly to WPF). 

To learn more about Xamarin.Forms visit: http://www.xamarin.com/forms

To learn about developing native iOS, Android, and Windows apps in C# visit: http://www.xamarin.com

Message Relay
===
The message relay is a macOS project build using the Tiny Framework, the Tiny API and Xamarin.

The drivers for the Message Relay is Home Automation. Using an IOS application called Locative the Message Relay is able to interact with Geofence notifications, a Home Automation system (in my case Domoticz) and family members through the use of iMessage. Interactions can be scripted using Apple Script and the Message Relay to conduct operations on the Home System or to notify users of things happening without having to rely on systems outside those provided directly by the platform. This reduces the surface area and in turn increases security and self reliance.

Marcus Kida's Locative project can be found here:
https://github.com/LocativeHQ

Hanselman.Forms: Hanselman Everywhere
===
The most awesome Hanselman app now using MVVM, DI and Programmed by Contract. TIny Framework now provides scaffolding for Xamarin's master detail views, RSS blogging and video feeds. DI is provided through the Tiny Frameworks MEF bootstrapper / implementation.

The 'settings' page in this project contains some 'big' controls and the bottom one should be a scaled vector image containg a picture (of Scott Hanselman). You might think it doesn't work properly, you'd be right. I haven't managed to get the image mask to scale properly, i'm working on it...

James Montemagmo's original project can be found here:
    https://github.com/jamesmontemagno/Hanselman.Forms
    
NControl (2)
===
 The Tiny Framework implementation of Christian Falch's NControl demo.
 
 Christian Falch's original project can be found here:
    https://github.com/chrfalch/NControl

Tiny Framework Web
===
I've added tiny framework web packages offering the following functionalty:
> Web Shell
>> has some bootstrapping and service registration functionality to emapty out and simplify your kestrel applications.

> Web
>> has patterns of work to increase resiliency and speed up your micro service development.

> Web Data
>> configuration, data access and session management. not really just for the 'web'; but i seemed to have some problem just calling the folder 'data'.

> Web Redis
>> add this to take advantage of redis and use the storage cache service in your micro service development.

> Web Swashbuckle
>> add this to instantly document your new API's, using Swashbuckle / Swagger.

> Web NSwag
>> add this to instantly document your new API's, using NSwag / Swagger.

> Web Application Insights
>> add this pack to instantly add system montioring to your app.

in all cases, don't forget to read and implement what's in the read me file. you can find an example of it's use here: 
https://github.com/thales-man/tslc-web/tree/main/back

Addendum
===
As the Framework moves forward there are some residual items that will, over time, either dissappear or evolve into a portable provision. These items will have a WPF desktop heritage and may not be relevant of suitable for the portable future the framework has; but their original purpose will have been in support of further decoupling of presentation from business logic. This is something I believe Xamarin still has a long way to go on; static dependencies are all to frequent and the platforms are currently far too close for this to be fully achieved. 

Update: 1st October 2020
Added aspnet core libraries and frameworks to enable faster API development with reusable types and patterns of work to increase resiliency, extensibility in order to reduce the impact on code when the requirements change (as they always do). 

No improvement with Xamarin, but lot's of promises of MAUI. We wait with bated breath. To be honest it doesn't sound like a major rewrite; it sounds like a lot of compromises whilst they try and reign in some of these development teams into a way overdue architectural strategy.

Update: 17th April 2020
Libraries updated to Net Standard 2.0 have remained here; there seems little point bringing them to Net Standad 2.1. There has been some rejigging as the presentation project was broken up to 'renable' the prospect of a Windows (Desktop) version again; which is where all this started out from. There is no sign of XAML Standard or WPF compliancy from within Xamarin. 

As a developer I can't say I'm entirely happy with the direction Xamarin is going; they seem more directed to 'form' rather than 'function'. A classic is a List View that doesn't respond to clicks (on iOS). Or a List View that doesn't even draw properly on macOS. Or buttons that don't draw properly... But we have a great 'shell' framework that plenty of other people could have developed and peddled independently, WOW!!! And we still use great design patterns like the "Service Locator Pattern"! Wow! And we're doing our damnedest to make your code as untestable as our own! Wow!! More statics please!

Most of the products generate NuGet packages; but some cannot due to ongoing issues with Xamarin. Xamarin project build pipelines are currently unable to determine secondary/tertiary package dependencies. Xamarin seems to have massive issues with package referencing and 'platforming' as builds result in thousands of errors relating to MM2001, MSB3243, CS1703 and CS0518. There seems to be a real screw up somewhere with the MSCOR version 2.0.5 and 4.0 for NETSTANDARD deployments and platforms. As a result some platform specific projects (macOS, iOS, Android) using Xamarin will only build with 'referenced' project versions and not with the Nuget packages as I'd like them to be built with. It looks like there are some efforts being made to fix this as the contents of project files are appearing to be modernised; for me as a developer with existing items this is a really painful process as project files end up being constantly hacked to fix issues generated during each successive deployment of Xamarin. There really doesn't appear to be much in the way of quality control or testing going on with this stuff before they push it out..