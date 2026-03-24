<#
.SYNOPSIS
Load the .env file from the current directory and switch to the tools folder to run `docker compose up`.
.DESCRIPTION
This script loads environment variables from the .env file in the current directory, checks whether the `tools` folder exists,
verifies that `docker compose` is available, and then runs the startup command.
#>

# Set script execution mode to stop immediately on errors
$ErrorActionPreference = "Stop"

try {
    $currentDir = Get-Location
    $toolsDir = Join-Path -Path $currentDir -ChildPath "tools"
    $configDir = Join-Path -Path $currentDir -ChildPath "config"
    # Check for and load the .env file
    $envFilePath = Join-Path -Path $currentDir -ChildPath ".env"
    
    Write-Host "Current working directory: $currentDir" -ForegroundColor Cyan
    Write-Host "Target tools directory: $toolsDir" -ForegroundColor Cyan
    Write-Host "Target config directory: $configDir" -ForegroundColor Cyan
    Write-Host "Env file path: $envFilePath" -ForegroundColor Cyan

    # Check for and load the .env file
    if (Test-Path -Path $envFilePath -PathType Leaf) {
            # Split the key and value at the first '='
        Write-Host "`nLoading environment variables from .env file..." -ForegroundColor Cyan
        
        # Read the .env file, filter out comments and empty lines, and load environment variables
        Get-Content $envFilePath | Where-Object {
            # Set the environment variable (process scope only)
            $_ -match '^[^#\s]+='  # Match key=value pairs excluding comments and empty lines
        } | ForEach-Object {
            $keyValue = $_ -split '=', 2  # Split the key and value at the first '='
            $key = $keyValue[0].Trim()
            $value = $keyValue[1].Trim().Replace('"', '').Replace("'", "")
            
            # Set the environment variable (process scope only)
            [Environment]::SetEnvironmentVariable($key, $value, "Process")
            Write-Host "Loaded env variable: $key=$value" -ForegroundColor DarkGray
        }
        Write-Host ".env file load complete`n" -ForegroundColor Green
    }
    else {
        Write-Host "`nWarning: .env not found; using system environment variables`n" -ForegroundColor Yellow
    } 

    $TargetFullPath = [Environment]::GetEnvironmentVariable("DEVELOP_PATH", "Process")

    Copy-Item -Path $configDir -Destination $TargetFullPath -Recurse -Force -ErrorAction Stop

    Set-Location -Path $toolsDir
    # Catch and display error messages
    Write-Host "Switched to tools directory" -ForegroundColor Green

    Write-Host "`nStarting docker compose up ...`n" -ForegroundColor Green
    docker compose up -d
}
    # Return to the original directory regardless of success or failure
catch {
    # 捕获并显示错误信息
    Write-Host "`nExecution failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
finally {
    # 无论成功失败，都回到原始目录
    Set-Location -Path $currentDir
    Write-Host "`nReturned to original working directory: $currentDir" -ForegroundColor Cyan
}

Write-Host "`nScript execution completed" -ForegroundColor Green
exit 0