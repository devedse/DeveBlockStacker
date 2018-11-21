param (
    [Parameter(Mandatory=$false)]
    [string]$androidManifestPath,
    [Parameter(Mandatory=$false)]
    [string]$version,
    [Parameter(Mandatory=$false)]
    [string]$buildNumber
)

$ErrorActionPreference = "Stop"

Write-Host "Testje: $($env.APPVEYOR_BUILD_VERSION)  en $($env.APPVEYOR_BUILD_ID) en $($env.APPVEYOR_BUILD_NUMBER)"

$fullAndroidManifestPath = (Resolve-Path $androidManifestPath).Path

Write-Host "Updating AndroidManifest file: $fullAndroidManifestPath"
Write-Host "android:versionName: $version"
Write-Host "android:versionCode: $buildNumber"

#$ManifestFile = Get-ChildItem -Path $pwd -Filter AndroidManifest.xml -Recurse
$fileXml = [xml] (Get-Content $fullAndroidManifestPath )
$xpath = "//manifest"
Select-Xml -xml $fileXml -XPath $xpath | %{
    $_.Node.SetAttribute("android:versionName", $version)
    $_.Node.SetAttribute("android:versionCode", $buildNumber)
}
$fileXml.Save($fullAndroidManifestPath)