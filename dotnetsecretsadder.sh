#!/usr/bin/env bash

dotnet user-secrets init --project $PWD/Backend/Backend.csproj
dotnet user-secrets set "connectionstring" "Host=myserver;Username=postgres;Password=postgres;Database=postgres" --project $PWD/Backend/Backend.csproj


