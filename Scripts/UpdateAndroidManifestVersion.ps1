param (
    [Parameter(Mandatory=$true)]
    [string]$androidManifestPath,
    [Parameter(Mandatory=$true)]
    [string]$version,
    [Parameter(Mandatory=$true)]
    [string]$buildNumber
)

$ErrorActionPreference = "Stop"

$fullAndroidManifestPath = (Resolve-Path $androidManifestPath).Path

Write-Host "Updating AndroidManifest file: $fullAndroidManifestPath"
Write-Host "android:versionName: $version"
Write-Host "android:versionCode: $buildNumber"

$fileXml = [xml] (Get-Content $fullAndroidManifestPath )
$xpath = "//manifest"
Select-Xml -xml $fileXml -XPath $xpath | %{
    $_.Node.SetAttribute("android:versionName", $version)
    $_.Node.SetAttribute("android:versionCode", $buildNumber)
}
$fileXml.Save($fullAndroidManifestPath)