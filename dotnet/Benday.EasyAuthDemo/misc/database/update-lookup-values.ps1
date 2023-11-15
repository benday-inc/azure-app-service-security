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

Write-Host "**** running ${MyInvocation.MyCommand.Name} from $PWD ****"

$sqlCommandPrefix = "./set-nocount.sql,"

Write-Host "***** DATABASE CREDENTIALS *****"
Write-Host "Server name: $servername"
Write-Host "Database name: $databasename"
Write-Host "User name: $username"
Write-Host "Password: $userPassword"
Write-Host "*****"

Write-Host "sqlcmd -S $servername -i $($sqlCommandPrefix)./update-lookup-values.sql -d $databasename -U $username -P $userPassword"

sqlcmd -S $servername -i "$($sqlCommandPrefix)./update-lookup-values.sql" -d $databasename -U $username -P $userPassword