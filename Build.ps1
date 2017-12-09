param(
    [string]$NugetSource = 'https://api.nuget.org/v3/index.json',
    [string]$Solution = 'src/Products.sln',
    [string]$Version = '0'
)

[string]$ArtifactVersion = '1.0.' + $Version

Write-Output "Building Products app"

dotnet restore $Solution --source $NugetSource --disable-parallel
if($LASTEXITCODE -ne 0) { exit 1}

dotnet build $Solution --configuration Release /p:Version=$ArtifactVersion --no-restore
if($LASTEXITCODE -ne 0) { exit 1}

foreach($project in dotnet sln $Solution list){
    if($project.ToLower().EndsWith('tests.csproj')){
        Write-Output "Running tests in $project"

        dotnet test src/$project --no-restore --no-build --configuration Release
        if($LASTEXITCODE -ne 0) { exit 1}
    }
}