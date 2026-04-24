#!/bin/bash
cd ../src
dotnet build -c Release
read -n 1 -s -p "Press any key to continue..."
