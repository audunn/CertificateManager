# Certificate Manager

## About
A simple Certificate Manger API to generate self signed certificates for testing purposes.
### Why
Me and my son run a [Minecraft](https://minecraft.net) server at home for fun and he sometimes gets into trouble running it when the client has updated but the server has not. My son is still young so he does not know yet how to update it himself.
So we made this to be used in Minecraft servers startup script to update the server automaticaly. 
As a learning experience more than anything else we made it in c# using .net core 2.1

### Prerequisites

You will need to have .NET Core

### Usage
To run the application, copy to the Minecraft server directory and type the following command:
```
dotnet CertifivateManager.dll
```
This will run the API:


## Built With

* [.netcore](https://dotnet.github.io/) - The general-purpose development platform used
* [Serilog](https://github.com/serilog/serilog-aspnetcore) - Serilog logging for ASP.NET Core. Simple .NET logging with fully-structured events
* [BouncyCastle.NetCore](https://github.com/chrishaly/bc-csharp) - The Bouncy Castle Crypto package is a C# implementation of cryptographic algorithms and protocols

## Authors

* **Auðunn Baldvinsson** - *Initial work* - [audunn](https://github.com/audunn)

See also the list of [contributors](https://github.com/audunn/CertificateManager/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details

## Acknowledgments

* Inspiration by https://github.com/adorsys/psd2-accelerator


