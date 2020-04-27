FROM mcr.microsoft.com/dotnet/core/runtime:3.1
COPY NetCoreImage/bin/Release/netcoreapp3.1/publish/ NetCoreImage/
ENTRYPOINT ["dotnet", "NetCoreImage/NetCoreImage.dll"]