param (
    [Parameter(Mandatory=$false)]
    [string]$androidManifestPath,
    [Parameter(Mandatory=$false)]
    [string]$version,
    [Parameter(Mandatory=$false)]
    [string]$buildNumber
)

$ErrorActionPreference = "Stop"

Write-Host "Updating AndroidManifest file: $androidManifestPath"
Write-Host "android:versionName: $version"
Write-Host "android:versionCode: $buildNumber"

#$ManifestFile = Get-ChildItem -Path $pwd -Filter AndroidManifest.xml -Recurse
$fileXml = [xml] (Get-Content $androidManifestPath )
$xpath = "//manifest"
Select-Xml -xml $fileXml -XPath $xpath | %{
    $_.Node.SetAttribute("android:versionName", $version)
    $_.Node.SetAttribute("android:versionCode", $buildNumber)
}
$fileXml.Save($androidManifestPath)