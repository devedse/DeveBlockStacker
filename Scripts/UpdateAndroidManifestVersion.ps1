param (
    [Parameter(Mandatory=$true)]
    [string]$androidManifestPath,
    [Parameter(Mandatory=$true)]
    [string]$version,
    [Parameter(Mandatory=$true)]
    [string]$buildNumber
)

$ErrorActionPreference = "Stop"

#$ManifestFile = Get-ChildItem -Path $pwd -Filter AndroidManifest.xml -Recurse
$fileXml = [xml] (Get-Content $androidManifestPath )
$xpath = "//manifest"
Select-Xml -xml $fileXml -XPath $xpath | %{
    $_.Node.SetAttribute("android:versionName", $version)
    $_.Node.SetAttribute("android:versionCode", $buildNumber)
}
$fileXml.Save($androidManifestPath)