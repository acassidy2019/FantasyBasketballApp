# Project Set Up
This scraper uses a couple of packages (HTMLAgility and CSVHelper) that are meant for .NET SDK rather than .NET Legacy, unlike the ADO.NET project. So, in order for this to work correctly, it must write to a .txt file located in ADO.NET and then be called to run through a background powershell script from ADO.NET.  Before running, the user should:

### 1. Edit write path in Scraper program
Open Program.cs under the folder titled "Scraper." Go to line 30, and replace the path listed with your system's full path to the Scrapet_data folder under ADO.NET.  At the end of this, add "Scraped_data.txt".  This tells the CSVHelper to create a new document at that location with out data in it.  

### 2. Install Scraper packages
Open the new terminal under the Scraper program folder.  In this terminal, run two commands:

    dotnet add package csvhelper
    dotnet add package htmlagilitypack

### 3. Edit open path in PowerShell script
Open scrape_command.ps1 (under the Scraper_data folder in ADO.NET), and replace the path with your system's full path to the Scraper folder under Final Project Compiled.  This ensures that the script is opennign the correct location to run our data scraper in the background when it is called from ADO.NET.
