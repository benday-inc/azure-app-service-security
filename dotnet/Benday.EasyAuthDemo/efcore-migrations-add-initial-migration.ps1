$base = Get-Location
Set-Location src\Benday.EasyAuthDemo.Api

dotnet ef migrations add InitialSetup

Set-Location $base