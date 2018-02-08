$exe = "dotnet.exe"
$args = "ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41001 0";
$fullCommand = "& $exe $args";

echo $fullCommand;

invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41001 0 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe ClickEventService\bin\Debug\netcoreapp2.0\ClickEventService.dll 41002 1 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe TextEventService\bin\Debug\netcoreapp2.0\TextEventService.dll 41003 0 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe TextEventService\bin\Debug\netcoreapp2.0\TextEventService.dll 41004 1 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe MouseMoveEventService\bin\Debug\netcoreapp2.0\MouseMoveEventService.dll 41005 0 }'
invoke-expression 'cmd /c start powershell -noexit -Command { dotnet.exe MouseMoveEventService\bin\Debug\netcoreapp2.0\MouseMoveEventService.dll 41006 1 }'