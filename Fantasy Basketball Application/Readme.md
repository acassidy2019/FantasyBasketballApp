# NBA Statistics
By Austin Fischer, Jorjei Ngoche, Adam Cassidy

BEFORE RUNNING: Please read the setup.md file as well

## What it Does
This program uses the .NET library with C# to give the user a real-time stat comparison tool for NBA players.  

There are three sub-programs built into this one whole project, but not all of them necessarily need to be running at all times. 

### FinalGraph
The FinalGraph folder holds the program that is the main interaction with the user.  It uses XAML to let the user select a query to run on our database, and creates a graph comparing players' stats accordingly.  

### ADO.NET
The ADO.NET folder holds the program that controls the live data updating for our database in \\\cs1.  The program makes use of a powershell script to run our Scraper program and pull statistics from the web regarding our players.  It then generates queries to pull the current state of our database stats table, compare the values between the web raw data and our database raw data, and then upload the current stats from the web to our database if it is out of date.  

### Scraper 
The Scraper folder holds the program that scrapes a certain website for NBA Player data.  It is called from within ADO.NET, but runs in a separate folder due to it needing a .NET SDK environment, unlike ADO.NET which requires a .NET Legacy environment.  

## How to Run
### FinalGraph 
To use the Graphing and table display you are going to need to uncomment different things depending on what example or stat you choose. For example, if you just want to find the top 3 players based on the total amount of points per game you have to uncomment everything related to PTS which is a double value and Barlabel. Also, you are going to have to replace a variable with the double PTS variable in the Column creation section if you want it to display on the graph (explained more in code). However, if you want to run Example 2 you have to uncomment only the doubles related to the data fields you are trying to get through the query and the Barlabel for example 2. Also, uncomment the Values variable in the column section which is accosted with example 2. Next, if you want to run Example 3 uncomment all doubles, the last barLabel and value variable in the Column section. After the selection has been made, simply open a terminal in FinalGraph and write

    dotnet build
    dotnet run

### ADO.NET
Open a new terminal under ADO.NET and write

    dotnet build
    .\ADO.NET\bin\Debug\ADO.NET.exe

### Scraper
This is called from within ADO.NET, so there is no need to run it independently.  If you wish to anyway, open a terminal under Scraper and write

    dotnet build
    dotnet run