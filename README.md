# Consuela
Service for cleaning out user specified folders on a configurable schedule. Written in dot net core so technically cross platform, but with every intention of using it as a Windows Service. I have unoriginally named this program "Consuela" because I thought it would be funny. It is obviously in reference to the Family Guy character. She makes me giggle and I can relate to her character since I grew up in a Latin household.

## Brief background
This service started as a LinqPad script that I run with Windows Task Scheduler. I have become reliant on this service to perform clean up for me on certain folders I think of "Dump" folders. You can see the original LinqPad script in my code-snippets repository [here](https://github.com/dyslexicanaboko/code-snippets/blob/develop/LinqPadScripts/Delete%20files%20older%20than/Delete%20files%20older%20than.linq).

# Why would I want this?
I don't know about you, but I have several folders I use for temporary throw away work. I refer to these folders as *Dump* folders, because the contents in it is usually only for "at the moment" work. The problem I found with keeping said Dump folders is that too much junk accumulates in them over time and worse I start to second guess if I need the contents in my Dump folder or not.

## The solution
Therefore the idea I came up with to solve this problem is to create a program (started off as a script) to periodically wipe out the contents of my Dump folders based on age of its contents. In other words, "Delete everything that is older than 30 days in folder X." I did this with the full knowledge and motto that:

> If it was important, then you shouldn't have put it in the Dump folder.

This may not work well for everyone, but it works great for me because at the beginning of each month I am guaranteed that anything older than 30 days is purged from my dump folders.

## Usage
I have a folder structure I use and a methodology to govern the use of this file deletion service.

> Before I dive into this, obviously you should BE CAREFUL when using this program. I promise it will delete your files whether you wanted it to or not because you instructed it to.

### Structure
I have two folders that I use for the purposes of dumping files. Some people use their desktop for this purpose, I hate that because I like a clean desktop, therefore I have designated a few purpose driven folders for dumping. I am going to be referencing the `C:` drive for the sake of argument in these examples.

- Dump folder
  - `C:\Dump\`
  - Temporary files of any kind. Can include more folders inside of it.
  - Examples:
    - Temporary saved images
    - Screen shots
    - Text file for temporary processing
    - Just need to save something somewhere right now
    - Creation of archive files (zip, rar, tar etc...)
    - Creating sheets in preparation for making a PDF binder
- Downloads folder
  - `C:\Dump\Downloads`
  - Like the name implies, this is where you download files to usually from your Browser such as Chrome, but can be from any program where you are performing a temporary "Save as..." operation.
  - Examples:
    - Installation files
    - Executable files
    - Pictures
    - Video
    - Music

### Methodology
As you can see from the structure above the `Downloads` folder is nested inside of the `Dump` folder. That's user preference, but I did that to prove that my ignore list would work when I was building the script out initially.
- I expect the `Dump` folder to be cleared out on the first of the month, but the `Downloads` folder should not be deleted.
- I expect the `Downloads` folder to be cleared out as well.
- Items are deleted based on age, so something I put in the folder yesterday is only one day old by tomorrow and should not be deleted.
- I have a rolling delete log that is generated to indicate what was deleted and any errors that may have occurred along the way. Windows is notorious for screwing up file operations that are at the C# level. This is still a worry of mine.
- When I want to group a bunch of temporary files together, I put them in the `Dump` folder and name them `Delete` with a number following like so:
  - `C:\Dump\delete1`
  - `C:\Dump\delete2`
  - `C:\Dump\delete3`
I will do my best not to review the contents of these folders because like I said above: **If it is important, then it shouldn't be in the dump folder.**
The same goes for download files. Want to keep a download file for the future? Then move it or it's gone in 30 days.

### Ignore list
There are some files I keep in the dump folder because technically it doesn't matter if they are deleted, but I like to have them around for constant use and re-use. These are my temp files and they are named exactly that. Think of it as scratch space where you are just trying to work something out and you don't know if it is worth keeping yet. Here are examples of that:
- temp.txt
- blah.txt
- temp.sql
- temp.linq
- temp.js
- ... you get the idea ...

These files are all part of an ignore list so that they are not deleted because there is no reason to delete them. It just causes me more work when I need to save a snippet of something in the near future. I also tend to have Notepad++ open 24/7 so that means that some or all of those files are already open in Notepad++ or another editor such as SSMS.

# Change log
1. 08/04/2018 - Initial upload of LinqPad Script named "Delete files older than.linq". Been using it in conjunction with Windows Task Scheduler for a long time. Works well as far as I can tell even if it is not perfect. The *worst* that can happen is sometimes it doesn't delete files which isn't a big deal, just annoying. This is a well known problem with C# and file system operations.
2. 04/22/2022 - Creation of repo and port of LinqPad script into C# dot net core project. Initial upload so I have a baseline. I have every intention of fleshing this program out more into a Windows Service and potentially hosting a Web Application management interface. I want to break down the code into interfaces and services so I can unit test and integration test appropriately. This program is difficuilt to test because of the file system component.

