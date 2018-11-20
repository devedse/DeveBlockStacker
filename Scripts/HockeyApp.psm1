#region Private Functions
function Get-AsciiBytes($str){
    return  [System.Text.Encoding]::ASCII.GetBytes($str)
}

function Write-MultiPartProperty {
    param(
        [parameter(Mandatory=$true)][System.IO.MemoryStream] $body,
        [parameter(Mandatory=$true)][string] $boundary,
        [parameter(Mandatory=$true)][string] $key,
        [string] $value
    )
    if(!$value){ return }

    $encoded = Get-AsciiBytes('--' + $boundary)
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)
                
    $encoded = (Get-AsciiBytes('Content-Disposition: form-data; name="' + $key + '"'))
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)
    $body.Write($crlf, 0, $crlf.Length)
                
    $encoded = (Get-AsciiBytes "$value")
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)
        
}

function Write-MultiPartFile {
    param(
        [parameter(Mandatory=$true)][System.IO.MemoryStream] $body,
        [parameter(Mandatory=$true)][string] $boundary,
        [parameter(Mandatory=$true)][string] $name,
        [string] $file
    )
    if(!$file){ return }
    
    $encoded = Get-AsciiBytes('--' + $boundary)
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)
                
    $fileName = (Get-ChildItem $file).Name
    $encoded = (Get-AsciiBytes('Content-Disposition: form-data; name="' + $name + '"; filename="' + $fileName + '"'))
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)            

    $encoded = (Get-AsciiBytes 'Content-Type:application/octet-stream')
    $body.Write($encoded, 0, $encoded.Length)
    $body.Write($crlf, 0, $crlf.Length)
    $body.Write($crlf, 0, $crlf.Length)
                
    $encoded = [System.IO.File]::ReadAllBytes($file)
    $body.Write($encoded, 0, $encoded.Length)
}

function Close-MultiPartStream{
    param(
        [System.IO.MemoryStream] $body,
        [string] $boundary
    )
    
    $encoded = Get-AsciiBytes('--' + $boundary)
    $body.Write($crlf, 0, $crlf.Length)
    $body.Write($encoded, 0, $encoded.Length)
                
    $encoded = (Get-AsciiBytes '--');
    $body.Write($encoded, 0, $encoded.Length);
    $body.Write($crlf, 0, $crlf.Length);
}
#endregion

<#
    .SYNOPSIS 
        Lists all versions of a specific app

    .DESCRIPTION
        In order to use the `-overwrite` parameter on `Push-ToHockeyApp`, we must be able to figure out if the version already exists. 
        This method serves to help us find if it exists.

    .PARAMETER apiKey
        HockeyApp API Key

    .PARAMETER appId
        The ID of the app to get versions of

    .PARAMETER page
        The page number to show (only useful if there are > 500 versions
#>
function Get-HockeyAppVersions{
    [CmdletBinding()]
    param(
        [parameter(Mandatory=$true, position = 0)][string] $apiKey,
        [parameter(Mandatory=$true, position = 1)][string] $appId,
        [parameter(Mandatory=$false)][int] $page
    )
    [System.Uri] $url = "https://rink.hockeyapp.net/api/2/apps/$appId/app_versions"
    $headers = @{"X-HockeyAppToken"="$apiKey"}

    $body = New-Object System.IO.MemoryStream
    $boundary = [Guid]::NewGuid().ToString().Replace('-','')

    if($page){
        Write-MultiPartProperty $body $boundary 'page' $page
        Close-MultiPartStream $body $boundary
    }
    
    try {
        (New-Object System.Net.WebClient).Proxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials 
        if($page){
            $response = Invoke-RestMethod -Headers $headers -Uri $URL -Method 'POST' -ContentType "multipart/form-data; boundary=$boundary" -Body $body.ToArray()
        } else {
            $response = Invoke-RestMethod -Headers $headers -Uri $URL
        }
        Write-Output $response
    }
    catch [System.Net.WebException] {
        Write-Error( "FAILED to reach '$URL': $_" )
        throw $_
    }
}

<#
    .SYNOPSIS 
        Gets a single version of an app if it exits

    .DESCRIPTION
        In order to use the `-overwrite` parameter on `Push-ToHockeyApp`, we must be able to figure out if the version already exists. 
        This method serves to help us find if it exists.

    .PARAMETER apiKey
        HockeyApp API Key

    .PARAMETER appId
        The ID of the app to get version of

    .PARAMETER appVersion
        The version of the app to get the details of
#>
function Get-HockeyAppVersion {
    [CmdletBinding()]
    param(
        [parameter(Mandatory=$true, position = 0)][string] $apiKey,
        [parameter(Mandatory=$true, position = 1)][string] $appId,
        [parameter(Mandatory=$true, position = 2)][string] $appVersion
    )
    $allVersions = Get-HockeyAppVersions $apiKey $appId
    $resp = $allVersions.app_versions | ? { $_.version -eq $appVersion  }
    Write-Output $resp
}

<#
    .SYNOPSIS 
        Gets a single version of an app if it exits

    .DESCRIPTION
        In order to use the `-overwrite` parameter on `Push-ToHockeyApp`, we must be able to figure out if the version already exists. 
        This method serves to help us find if it exists.

    .PARAMETER apiKey
        HockeyApp API Key

    .PARAMETER appId
        The ID of the app to get version of

    .PARAMETER file
        file data of the .ipa for iOS, .app.zip for Mac OS X, or .apk file for Android
        
    .PARAMETER dsym
        optional, file data of the .dSYM.zip file (iOS and OS X) or mapping.txt (Android); note that the extension has to be .dsym.zip
        
    .PARAMETER notes
        optional, release notes as Textile or Markdown (after 5k characters notes are truncated)

    .PARAMETER notesType
        optional, type of release notes: 0 for Text, 1 for Markdown
        
    .PARAMETER notify
         optional, notify testers (can only be set with full-access tokens): 0 for don't notify, 1 for notify all testers that can install this app, 2 for notify all testers
    
    .PARAMETER status
        optional, download status (can only be set with full-access tokens): 1 for don't allow users to download or install the app, 2 for available for download and installation
    
    .PARAMETER tags
        optional, restrict download to comma-separated list of tags
        
    .PARAMETER teams
        optional, restrict download to comma-separated list of team IDs

    .PARAMETER users
        optional, restrict download to comma-separated list of user IDs
    
    .PARAMETER mandatory
        optional, set version as mandatory: 0 for NO, 1 for YES
        
    .PARAMETER version
        The version of the app to overwrite (only used if the -overwrite flag is present)
        
    .PARAMETER overwrite
        overwrite an existing app of the same version, otherwise uploads a new one.
#>
function Push-ToHockeyApp {
    [CmdletBinding()]
    param (
        [parameter(Mandatory=$true, position = 0)][string] $file,
        [parameter(Mandatory=$true, position = 1)][string] $apiKey,
        [parameter(Mandatory=$true, position = 2)][string] $appId,
        [parameter(Mandatory=$false)][int[]] $teams,
        [parameter(Mandatory=$false)][int[]] $users,
        [parameter(Mandatory=$false)][string[]] $tags,
        [parameter(Mandatory=$false)][string] $dsym,
        [parameter(Mandatory=$false)][string]$notes,
        [parameter(Mandatory=$false)][string]$notesType,
        [parameter(Mandatory=$false)][int] $notify,
        [parameter(Mandatory=$false)][int] $status,
        [parameter(Mandatory=$false)][int] $mandatory,
        [parameter(Mandatory=$false)][int] $version,
        [parameter(Mandatory=$false)][switch] $overwrite
    ) 
    [byte[]]$crlf = 13, 10
    [System.URI] $url = "https://rink.hockeyapp.net/api/2/apps/$appId/app_versions/upload"
    $method = 'POST'
    $headers = @{"X-HockeyAppToken"="$apiKey"}
    $body = New-Object System.IO.MemoryStream
    $boundary = [Guid]::NewGuid().ToString().Replace('-','')

    Write-Host "Uploading version [$version] of [$appId] to HockeyApp."

    if($overwrite.IsPresent){
        if(!$version){
            throw "you  must specify the version number if you wish to overwrite it."
        }
        $app = Get-HockeyAppVersion $apiKey $appId $version
        if($app){
            Write-Host "Existing version [$version] found for [$appId].`tUpdating."
            [System.URI] $url = "https://rink.hockeyapp.net/api/2/apps/$appId/app_versions/$($app.id)"
            $method = 'PUT'
        }
    }

    Write-MultiPartProperty $body $boundary 'teams' $($teams -join ',') 
    Write-MultiPartProperty $body $boundary 'users' $($users -join ',') 
    Write-MultiPartProperty $body $boundary 'tags' $($tags -join ',') 
    Write-MultiPartProperty $body $boundary 'status' $status 
    Write-MultiPartProperty $body $boundary 'notify' $notify 
    Write-MultiPartProperty $body $boundary 'mandatory' $mandatory 
    Write-MultiPartProperty $body $boundary 'notes' $notes 
    Write-MultiPartProperty $body $boundary 'notes_type' $notesType 
    Write-MultiPartFile $body $boundary 'dsym' $dsym 
    Write-MultiPartFile $body $boundary 'ipa' $file
    Close-MultiPartStream $body $boundary

    try {
        (New-Object System.Net.WebClient).Proxy.Credentials = [System.Net.CredentialCache]::DefaultNetworkCredentials 
        $response = Invoke-RestMethod -Headers $headers -Uri $URL -Method $method -ContentType "multipart/form-data; boundary=$boundary" -Body $body.ToArray()
        Write-Output $response
    }
    catch [System.Net.WebException] {
        Write-Error( "FAILED to reach '$URL': $_" )
        throw $_
    }
}

export-modulemember -function Push-ToHockeyApp
export-modulemember -function Get-HockeyAppVersions
export-modulemember -function Get-HockeyAppVersion