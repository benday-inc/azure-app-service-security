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

$sqlCommandPrefix = "./set-nocount.sql,"

Write-Host "***** DATABASE CREDENTIALS *****"
Write-Host "Server name: $servername"
Write-Host "Database name: $databasename"
Write-Host "User name: $username"
Write-Host "Password: $userPassword"
Write-Host "*****"

sqlcmd -S $servername -i "$($sqlCommandPrefix)./create-database-logins.sql" -d master -U $username -P $userPassword
sqlcmd -S $servername -i "$($sqlCommandPrefix)./create-database-users.sql" -d $databasename -U $username -P $userPassword

