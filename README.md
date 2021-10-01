# DNS Cacher
***Description***
> This simple C# program fetches hostnames and looks through domain name servers for A records. When it finds an IP in the DNS, it will cache the IP, DNS, and Hostname, so that when you fetch the same hostname, it will receive the cached results.

## Requirements
* .NET 3.1 or greater

## Examples

### Visual Studio
Download the source code: `git clone https://github.com/SoftwareFuze/DNS-Cacher.git`<br/>
Then, open `DNS-Cacher.csproj` with Visual Studio.

## .NET CLI
Download the source code: `git clone https://github.com/SoftwareFuze/DNS-Cacher.git`<br/>
Then, run the command `dotnet run`