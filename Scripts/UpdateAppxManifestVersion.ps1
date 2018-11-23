param (
    [Parameter(Mandatory=$true)]
    [string]$appxManifestPath,
    [Parameter(Mandatory=$true)]
    [string]$version
)

$ErrorActionPreference = "Stop"

$fullAppxManifestPath = (Resolve-Path $appxManifestPath).Path
$fullVersion = "$($version).0"

Write-Host "Updating AppxManifest file: $fullAppxManifestPath"
Write-Host "version: $version"
Write-Host "fullVersion: $fullVersion"

$fileXml = [xml] (Get-Content $fullAppxManifestPath )
$xpath = "//e:Package//e:Identity"

$namespace = @{e="http://schemas.microsoft.com/appx/manifest/foundation/windows10"}

Select-Xml -xml $fileXml -XPath $xpath -Namespace $namespace | %{
    $_.Node.SetAttribute("Version", $fullVersion)
}
$fileXml.Save($fullAppxManifestPath)