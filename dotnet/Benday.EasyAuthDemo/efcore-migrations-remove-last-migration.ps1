$base = Get-Location
Set-Location src\Benday.EasyAuthDemo.Api

dotnet ef migrations remove

Set-Location $base