# Install MonoGame
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

$tempDir="$pwd\temp"
$installerUrl="https://github.com/MonoGame/MonoGame/releases/download/v3.7/MonoGameSetup.exe"
$installerFile="$tempDir\MonoGameSetup.exe"
$programFiles = "${env:ProgramFiles(x86)}\"

Write-Host "Downloading MonoGame to $installerFile..."

If ((Test-Path  $tempDir) -eq 0) {
    New-Item -ItemType Directory $tempDir
}

Invoke-WebRequest $installerUrl -OutFile $installerFile

Write-Host "Installing MonoGame"
Start-Process -FilePath "$installerFile" -ArgumentList "/S /v /qn"
Write-Host "MonoGame successfully installed"