cd ../src
dotnet publish --os win --self-contained
dotnet publish --os linux --self-contained
read -n 1 -s -p "Press any key to continue..."
