param (
    [Parameter(Mandatory=$true)]
    [string]$clientid,
    [Parameter(Mandatory=$true)]
    [string]$clientsecret,
    [Parameter(Mandatory=$true)]
    [string]$commitdescription,
    [Parameter(Mandatory=$true)]
    [string]$version
)

$baseurl = "https://developer.amazon.com/api/appstore"
$appid = "amzn1.devportal.mobileapp.7715ea2ef9f64e46b632065a01e34660"
$apiversion = "v1"

$replaceedit = $false
$replaceapk = $true


$apksToReplace = @(
    'Scripts/apks/deveblockstacker_android.deveblockstacker_android-Signed.apk'
)



$body = @{
    "grant_type"= "client_credentials"
    "client_id"= $clientid
    "client_secret"= $clientsecret
    "scope"= "appstore::apps:readwrite"
} | ConvertTo-Json

$header = @{
    "Content-Type"="application/json"
} 

$result = Invoke-RestMethod -Uri "https://api.amazon.com/auth/O2/token" -Method 'Post' -Body $body -Headers $header
$token = $result.access_token


$header = @{
    "Content-Type"="application/json"
    "Authorization"="Bearer $token"
} 

# val activeEdit = editsService.getActiveEdit()
# @GET("{version}/applications/{appId}/edits")
$result_activeEdit = Invoke-WebRequest  -Uri "$baseurl/$apiversion/applications/$appid/edits" -Method 'Get' -Headers $header -UseBasicParsing
$activeEdit = $result_activeEdit.Content | ConvertFrom-Json
Write-Host $activeEdit


# if (amazon.replaceEdit) {
#     println("️↕️️ Deleting edit...")
#     if (activeEdit != null &&
#             activeEdit.id.isNotBlank()) {
#         editsService.deleteEdit(activeEdit)
#     }
# }

if ($replaceedit -eq $true) {
    Write-Host "Deleting edit..."
    if ($null -ne $activeEdit -and [string]::IsNullOrEmpty($activeEdit.id) -eq $false) {

        $etag = $result_activeEdit.Headers["ETag"]
        if ($etag -is [array]) {
            $etag = $etag[0]
        }

        $header2 = @{
            "Authorization"="Bearer $token"
            "Content-Type"="application/json"
            'If-Match'= $etag
        }

        # @DELETE("{version}/applications/{appId}/edits/{editId}")
        Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits/$($activeEdit.id)" -Method 'Delete' -Headers $header2 -SkipHeaderValidation
    }
}

# var newEdit = editsService.getActiveEdit()
# if (newEdit == null ||
#         newEdit.id.isBlank()) {
#     println("️\uD83C\uDD95️ Creating new edit...")
#     newEdit = editsService.createEdit()
# }

# @GET("{version}/applications/{appId}/edits")
$newEdit = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits" -Method 'Get' -Headers $header
if ($null -eq $newEdit -or [string]::IsNullOrEmpty($newEdit.id) -eq $true) {
    Write-Host "Creating new edit..."
    # @POST("{version}/applications/{appId}/edits")
    $newEdit = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits" -Method 'Post' -Headers $header
}

# if (amazon.replaceApks) {
#     replaceExistingApksOnEdit(apkService, newEdit!!, amazon.pathToApks)
# }
# else {
#     deleteExistingApksOnEdit(apkService, newEdit!!)
#     uploadApksAndAttachToEdit(
#             apkService,
#             newEdit,
#             amazon.pathToApks
#     )
# }

if ($replaceapk -eq $true) {
    Write-Host "Replacing existing apks on edit..."
   
    # val apks = apkService.getApks(activeEdit.id)

    #@GET("{version}/applications/{appId}/edits/{editId}/apks")
    $apks = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/apks" -Method 'Get' -Headers $header
    # $result_apks = Invoke-WebRequest -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/apks" -Method 'Get' -Headers $header -UseBasicParsing
    # $apks = $result_apks.Content | ConvertFrom-Json

    # if (apks.size != apksToReplace.size) {
    #     throw IllegalStateException("Number of existing APKs on edit (${apks.size}) does not match" +
    #         "the number of APKs to upload (${apksToReplace.size})")
    # }

    if ($apks.Length -ne $apksToReplace.Length) {
        throw "Number of existing APKs on edit ($($apks.Length)) does not match" +
            "the number of APKs to upload ($($apksToReplace.Length))"
    }

    # println("\ud83d\udd04 Replacing APKs in existing edit...")
    # apksToReplace.forEachIndexed { index, apkFile ->
    #     println("\u23eb Uploading ${apkFile}...")
    #     val status = apkService.replaceApk(activeEdit.id, apks[index].id, apkFile, apkFile.getName())
    #     if (!status) {
    #         println("Failed to upload APK")
    #         throw IllegalStateException("Failed to upload APK")
    #     }
    # }

    Write-Host "Replacing APKs in existing edit..."
    $index = 0;
    foreach ($apkFile in $apksToReplace) {
        Write-Host "Uploading $apkFile..."
        
        $curapk = $apks[$index]

        # @GET("{version}/applications/{appId}/edits/{editId}/apks/{apkId}")
        $result_apk = Invoke-WebRequest -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/apks/$($curapk.id)" -Method 'Get' -Headers $header -UseBasicParsing
        $apk = $result_apk.Content | ConvertFrom-Json

        $etag = $result_apk.Headers["ETag"]
        if ($etag -is [array]) {
            $etag = $etag[0]
        }

        $header2 = @{
            "Authorization"="Bearer $token"
            'Content-Type'= 'application/vnd.android.package-archive'
            'If-Match'= $etag
            'filename'=$version + "_" + [System.IO.Path]::GetFileName($apkFile)
        }

        $status = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/apks/$($curapk.id)/replace" -Method 'Put' -Headers $header2 -SkipHeaderValidation -InFile $apkFile
        
        $index++
    }
    # println("\uD83C\uDF89 New APK(s) published to the Amazon App Store")
    Write-Host "New APK(s) published to the Amazon App Store"


    Write-Host "Updating listing with recent changes (Release notes)..."
    # /{apiVersion}/applications/{appId}/edits/{editId}/listings
    $result_listings = Invoke-WebRequest "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/listings/en-US" -Method 'Get' -Headers $header -UseBasicParsing
    $listings = $result_listings.Content | ConvertFrom-Json

    $listings.recentChanges = $commitdescription

    $etag = $result_listings.Headers["ETag"]
    if ($etag -is [array]) {
        $etag = $etag[0]
    }

    $header2 = @{
        "Authorization"="Bearer $token"
        "Content-Type"="application/json"
        'If-Match'= $etag
    }

    $updatelisting = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/listings/en-US" -Method 'Put' -Headers $header2 -SkipHeaderValidation -Body ($listings | ConvertTo-Json)


    Write-Host "Publishing commit..."

    # /{apiVersion}/applications/{appId}/edits/{editId}
    $result_edit = Invoke-WebRequest "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)" -Method 'Get' -Headers $header -UseBasicParsing
    $edit = $result_edit.Content | ConvertFrom-Json

    $etag = $result_edit.Headers["ETag"]
    if ($etag -is [array]) {
        $etag = $etag[0]
    }

    $header2 = @{
        "Authorization"="Bearer $token"
        "Content-Type"="application/json"
        'If-Match'= $etag
    }
    
    # ​/{apiVersion}​/applications​/{appId}​/edits​/{editId}​/commit
    $commit = Invoke-RestMethod -Uri "$baseurl/$apiversion/applications/$appid/edits/$($newEdit.id)/commit" -Method 'Post' -Headers $header2 -SkipHeaderValidation

    Write-Host "Commit completed"

}
else {
    Write-Host "Deleting existing apks on edit..."
    
    # val apks = apkService.getApks(activeEdit.id)
    # println("Remove APKs from previous edit...")
    # apks.forEach {
    #     val status = apkService.deleteApk(activeEdit.id, it.id)
    #     if (!status) {
    #         throw IllegalStateException("Failed to delete existing APK")
    #     }
    # }

    
    

    # apksToUpload.forEachIndexed { index, apk ->
    #     println("Uploading new APK(s)...")
    #     val result = apkService.uploadApk(activeEdit.id, apk, "APK-$index")
    #     if (result) {
    #         println("\uD83C\uDF89 New APK(s) published to the Amazon App Store...")
    #     } else {
    #         println("Failed to upload new APK(s)...")
    #         throw IllegalStateException("Failed to upload new APK(s)...")
    #     }
    # }
}

Write-Host "Script completed"
