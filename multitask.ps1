#robocopy p1 p2 /dcopy:DAT /copy:DAT /mir /compress /w:3
Clear-Host

$myitems = @([pscustomobject] @{ time = 5; info = "AAA" },
    [pscustomobject] @{ time = 7; info = "BBB" },
    [pscustomobject] @{ time = 4; info = "CCC" },
    [pscustomobject] @{ time = 6; info = "DDD" },
    [pscustomobject] @{ time = 8; info = "EEE" },
    [pscustomobject] @{ time = 3; info = "FFF" },
    [pscustomobject] @{ time = 12; info = "GGG" },
    [pscustomobject] @{ time = 10; info = "HHH" },
    [pscustomobject] @{ time = 9; info = "III" },
    [pscustomobject] @{ time = 13; info = "JJJ" })

$ScriptBlock = {
    param([Int] $id, [Int] $time)
    [datetime] $before = Get-Date
    Start-Sleep $time
    [datetime] $after = Get-Date
    return [pscustomobject] @{Id = $id; Wait = $time; Before = $before; After = $after; "Total (ms)" = (New-TimeSpan -Start $before -End $after).TotalMicroseconds }
}

#Write-Output yahoo.com facebook.com google.com uol.com.br | ForEach-Object { $_ | start-threadjob { test-netconnection $input } } | receive-job -wait -auto | Format-Table -a

[Int] $cont = 0
$myitems | ForEach-Object {
    Start-ThreadJob $ScriptBlock -ArgumentList (++$cont), (Get-Random -Maximum $_.time) -ThrottleLimit 3
} | receive-job -wait -auto | Sort-Object -Property After, "Total (ms)" | Format-Table -a

# Cleanup
Remove-Job *