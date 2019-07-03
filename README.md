# Certificate Manager

## About
A simple Certificate Manger API that can be used to generate self-signed certificates for testing purposes.

### Why
Initialy it was used to generated self-signed eIDAS certificates for interanl use while developing solutions for the European PSD2 directive(Revised Directive on Payment Services).

PSD2 is the European Commission proposal to create safer and more innovative European payments (PSD2, Directive (EU) 2015/2366). The new rules aim to better protect consumers when they pay online, promote the development and use of innovative online and mobile payments such as through open banking, and make cross-border European payment services safer.

An important important element of the directive is the demand for common and secure communication (CSC). eIDAS-defined qualified certificates are demanded for website authentication and electronic seals used for communication between financial services players. The technical specification ETSI TS 119 495 defines a standard for implementing these requirements

In order to be able to test the solution we needed a simple convenient way to generate test certificates and this api provides this service eIDAS certificates. 

In theory can be used to create other certificates as well omitting the eIDAS extensions.
### Prerequisites

You will need to have .NET Core

### Usage
To run the application type the following command:
```
dotnet CertifivateManager.dll
```
This will run the API:
```
Hosting environment: Production
Content root path: C:\Users\audunn\source\repos\CertificateManager\CertificateManager\bin\Debug\netcoreapp2.2
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
Application started. Press Ctrl+C to shut down.
```
When you browse to the url you will get a swaggerUI interface to explore the endpoints.

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


