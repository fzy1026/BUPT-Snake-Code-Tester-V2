$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$srcDir = Join-Path $root "src"
$distDir = Join-Path $root "dist"
$outExe = Join-Path $distDir "SnakeOJTester.exe"

$csc64 = Join-Path $env:WINDIR "Microsoft.NET\Framework64\v4.0.30319\csc.exe"
$csc32 = Join-Path $env:WINDIR "Microsoft.NET\Framework\v4.0.30319\csc.exe"
if (Test-Path $csc64) {
    $csc = $csc64
} elseif (Test-Path $csc32) {
    $csc = $csc32
} else {
    throw "csc.exe not found."
}

New-Item -ItemType Directory -Force -Path $distDir | Out-Null
$distWorkDir = Join-Path $distDir "work"
if (Test-Path $distWorkDir) {
    $resolvedDist = (Resolve-Path -LiteralPath $distDir).Path
    $resolvedWork = (Resolve-Path -LiteralPath $distWorkDir).Path
    if ($resolvedWork.StartsWith($resolvedDist)) {
        Remove-Item -LiteralPath $resolvedWork -Recurse -Force
    }
}
$sources = Get-ChildItem -Path $srcDir -Filter *.cs | Sort-Object Name | ForEach-Object { $_.FullName }

& $csc /nologo /target:winexe /platform:anycpu /optimize+ /codepage:65001 `
    /reference:System.Windows.Forms.dll /reference:System.Drawing.dll `
    /out:$outExe $sources
if ($LASTEXITCODE -ne 0) {
    throw "C# build failed with exit code $LASTEXITCODE."
}

Copy-Item -LiteralPath (Join-Path $root "README.md") -Destination (Join-Path $distDir "README.md") -Force
Copy-Item -LiteralPath (Join-Path $root "UNINSTALL.txt") -Destination (Join-Path $distDir "UNINSTALL.txt") -Force

Write-Host "Built: $outExe"
