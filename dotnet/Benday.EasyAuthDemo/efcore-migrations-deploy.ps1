$base = Get-Location
Set-Location src\Benday.EasyAuthDemo.Api

if ($args.Length -eq 0) {
  dotnet ef database update
}
else {
  dotnet ef database update $args[0]
}

Set-Location $base