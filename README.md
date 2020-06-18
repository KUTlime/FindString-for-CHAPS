# FindString-for-CHAPS
> An interview test implementation for CHAPS.

# Solution overview
## FindString
- Core library 

## FindStringApp
- Client application as a console application

## FindString.Tests
- Project with UTs.

## FindstrWrapper
- A wrapper for Win32 application from Windows. 
- Could be used for bug-compatible testing.

# Problems with Findstr

Let's run findstr agains this directory:

```powershell
PS D:\Chaps> Get-ChildItem


    Directory: D:\Chaps


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
-a----       17.06.2020     21:45            365 Stats.ps1
-a----       16.06.2020     16:29             90 Text.txt
```

Let's check what is inside `Text.txt`:

```powershell
PS D:\Chaps> Get-Content Text.txt

Hello
there
ahoj
svete
asdf
asdfa df
asdf Hello 
asdf hello
asdf there
asdf There
```

```cmd
findstr hello there Text.txt
```

Doesn't work. The output is:

```powershell
FINDSTR: Cannot open there
Text.txt:Hello
Text.txt:asdf Hello
```
How about this [documentation](https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/findstr) example?
```powershell
findstr /c:hello there Text.txt
```
Nope, again it doesn't work...

```powershell
FINDSTR: Cannot open there
Text.txt:asdf
Text.txt:asdfa df
Text.txt:asdf Hello
Text.txt:asdf hello
Text.txt:asdf there
Text.txt:asdf There
```
How about this example? Matching line at the end...
```powershell
findstr /e Hello Text.txt
```
Output: 
```powershell
Hello
```
There should be two of those lines.