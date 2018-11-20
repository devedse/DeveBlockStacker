Import-Module "$here\modules\hockeyapp.psm1"
$params = @{
    file="DeveBlockStacker.Android\bin\Android\AnyCPU\Release\deveblockstacker_android.deveblockstacker_android-Signed.apk"
    apiKey = $env:AppCenterApiKey
    appId = 'DeveBlockStacker.Android'
    notify = 1
    status = 2
    version = $env:APPVEYOR_BUILD_VERSION
}

Push-ToHockeyApp @params -overwrite