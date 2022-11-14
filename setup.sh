#!/usr/bin/env bash

if [[ "$OSTYPE" == "linux-gnu"* ]]; then #linux
      echo "Detected OS is Linux"
      dotnet dev-certs https -ep ${HOME}/.aspnet/https/projectc.pfx -p projectCPassword
      dotnet dev-certs https --trust
elif [[ "$OSTYPE" == "darwin"* ]]; then #macOS
      echo "Detected OS is macOS"
      dotnet dev-certs https -ep ${HOME}/.aspnet/https/projectc.pfx -p projectCPassword
      dotnet dev-certs https --trust
elif [[ "$OSTYPE" == "cygwin" ]]; then # compatibility layer for windows emulation
      echo "Detected OS is Windows"
      dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\projectc.pfx -p projectCPassword
      dotnet dev-certs https --trust
elif [[ "$OSTYPE" == "msys" ]]; then # windows
      echo "Detected OS is Windows"
      dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\projectc.pfx -p projectCPassword
      dotnet dev-certs https --trust
elif [[ "$OSTYPE" == "win32" ]]; then # windows backup
      echo "Detected OS is Windows"
      dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\projectc.pfx -p projectCPassword
      dotnet dev-certs https --trust
fi
