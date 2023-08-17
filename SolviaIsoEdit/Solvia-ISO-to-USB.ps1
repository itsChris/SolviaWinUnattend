# Get Disk (USB only) -> See the *Number*
Get-Disk | where BusType -eq 'USB'

# Then clear the USB stick/pendrive using the *Number* from the last command
Clear-Disk -Number 1 -RemoveData -Confirm:$false -PassThru

# Create a new partition, again using the *Number* from the first command
New-Partition -DiskNumber 1 -UseMaximumSize -AssignDriveLetter | Format-Volume -FileSystem NTFS

# Now mount the DiskImage (ISO) using Mount-DiskImage
Mount-DiskImage -ImagePath C:\SW_DVD9_Win_Pro_11_21H2_64BIT_English_Pro_Ent_EDU_N_MLF_-3_X22-89962.ISO

# Get the DeviceID from the mounted DiskImage
Get-CimInstance Win32_LogicalDisk | ?{ $_.DriveType -eq 5} | select DeviceID

# Copy the content of the ISO to the USB stick
xcopy E:\ D:\ /e