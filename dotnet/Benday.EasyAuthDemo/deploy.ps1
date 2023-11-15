param (
    [Parameter(Mandatory = $false)]
    [bool]$dropDatabase = $false, 
    [Parameter(Mandatory = $false)]       
    [string]$databaseName = "EasyAuthDemo",

    [Parameter(Mandatory = $false)]       
    [string]$serverName = "localhost",

    [Parameter(Mandatory = $false)]       
    [string]$username = "sa",

    [Parameter(Mandatory = $false)]       
    [string]$userPassword = "Pa`$`$word"
)
$ErrorActionPreference = "Stop"


# check if sqlserver module is installed
if (Get-Module -ListAvailable -Name SqlServer) {
    Write-Host "SqlServer module is installed"
} else {
    Write-Host "SqlServer module is not installed.  To install it, run the following command:"
    Write-Host "Install-Module -Name SqlServer -Scope CurrentUser -Force"
    Write-Host "Then, close and re-open this window and re-run this script."
}

if (Get-Command -Name Invoke-Sqlcmd -ErrorAction SilentlyContinue) {
    Write-Host "Invoke-Sqlcmd is available"
} else {
    Write-Host "Invoke-Sqlcmd is not available.  To install it, run the following command:"
    Write-Host "Install-Module -Name SqlServer -Scope CurrentUser -Force"
    Write-Host "Then, close and re-open this window and re-run this script."
}

$startingPath = Get-Location
$base = Get-Location

Write-Host "***** RESTORING DOTNET TOOLS *****"
dotnet tool restore

# if $dropDatabase is true, drop the database
if ($dropDatabase) {
    Write-Host "***** DROPPING DATABASE *****"
    Invoke-Sqlcmd -Query "drop database if exists [$databaseName]" -Database "master" -Username $username -Password $userPassword -ServerInstance $serverName -TrustServerCertificate
}
else {
    Write-Host "***** NOT DROPPING DATABASE *****"
}

Write-Host "***** CREATING DATABASE *****"
Invoke-Sqlcmd -Query "create database [$databasename]" -Database "master" -Username $username -Password $userPassword -ServerInstance $serverName -TrustServerCertificate

Set-Location "src\Benday.EasyAuthDemo.Api"
Write-Host (Get-Location)

Write-Host "***** DEPLOYING EF CORE MIGRATIONS *****"
dotnet ef database update

Set-Location $base

Write-Host "***** POPULATING DATABASE PERMISSIONS *****"
Set-Location "misc\database"
.\populate-sql-server-permissions.ps1

Set-Location $base

Set-Location $startingPath
Write-Host "***** UPDATING LOOKUP VALUES *****"
Set-Location "misc\database"
.\update-lookup-values.ps1

Write-Host "Set-Location $startingPath"
Set-Location $startingPath

Write-Host "***** ADDITIONAL DEPLOYMENT STEPS *****"
if (Test-Path "deploy-additional-steps.ps1") {
    Write-Host "Found deploy-additional-steps.ps1.  Calling it..."
    .\deploy-additional-steps.ps1
    Set-Location $startingPath
} else {
    Write-Host "INFO: No deploy-additional-steps.ps1 file found.  FYI, if you want to specify more things to do on deployment, create this file."
}

Write-Host "***** DONE *****"