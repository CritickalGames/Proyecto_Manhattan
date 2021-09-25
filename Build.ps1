$dateTime = (Get-Date).ToString('dd:MM:yy:hh:mm:ss')
$projectPath=".\"
$buildMethodWin64="BuildProject.BuildWindows64"
$buildMethodWin32="BuildProject.BuildWindows32"
$env:gameName="Haste"
$env:projectVersion="$env:gameName v0.1.$dateTime"
$env:environment="Test"

echo "Project Path: $projectPath"
echo "Project environment: $env:environment"
echo "Project version: $env:projectVersion"
echo ""
echo "Building Windows 64 bit executable, Using build method: $buildMethodWin64"

Start-Process -Wait 'C:\Program Files\Unity\Hub\Editor\2020.3.12f1\Editor\Unity.exe' -ArgumentList "-nographics -batchmode -projectPath $buildPath -executeMethod $buildMethodWin64 -quit"

if($? -ne "true"){
	echo $?
	echo "Error building Win 64 Executable"
	exit 1
}


echo "Building Windows 32 bit executable, Using build method: $buildMethodWin32"

Start-Process -Wait 'C:\Program Files\Unity\Hub\Editor\2020.3.12f1\Editor\Unity.exe' -ArgumentList "-nographics -batchmode -projectPath $buildPath -executeMethod $buildMethodWin32 -quit"

if($? -ne "true"){
	echo $?
	echo "Error building Win 32 Executable"
	exit 1
}

exit 0

Read-Host -Prompt 'Proceso terminado'