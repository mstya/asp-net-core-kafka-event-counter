$exe = "dotnet.exe"
$args = "ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41001 0";
$fullCommand = "& $exe $args";

echo $fullCommand;

invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41001 0 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41002 1 }'