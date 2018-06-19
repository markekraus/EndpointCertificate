[CmdletBinding()]
Param(
    [String]
    $OuputPath = (Join-Path $PSScriptRoot 'bin'),

    [Switch]
    $Clean
)

$Csproj = Join-Path $PSScriptRoot '\EndpointCertificate\EndpointCertificate.csproj'

if ($Clean) {
    Write-Verbose "Cleaning '$OutputPath'..."
    $removeItemSplat = @{
        Path = $OuputPath
        Recurse = $true
        Force = $true
        ErrorAction = 'SilentlyContinue'
    }
    Remove-Item @removeItemSplat
}

Write-Verbose "Creating '$OutputPath'..."
$newItemSplat = @{
    Path = $OuputPath
    ItemType = 'Directory'
    ErrorAction = 'SilentlyContinue'
    Force = $true
}
$null = New-Item @newItemSplat

Write-Verbose "Publishing '$Csproj' to '$OutputPath'..."

dotnet publish $Csproj -o $OuputPath
