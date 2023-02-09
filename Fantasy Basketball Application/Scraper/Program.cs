// created using guide from https://medium.com/c-sharp-progarmming/create-your-own-web-scraper-in-c-in-just-a-few-minutes-c42649adda8

using CsvHelper;
using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
#nullable enable

namespace NBA_Program {
    class Scraper {

        static void Main(string[] args) {
            // use the htmlagility package tools to set a target, load a document object with that target set to it
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.basketball-reference.com/leagues/NBA_2022_per_game.html");
            
            // create a list of Row classes to store our data in
            var stats = new List<Row>();

                var IndivStats = doc.DocumentNode.SelectNodes("//td");
                // for each data node we have now filtered and loaded into our IndivStats node selection,
                // add a new row to our list containing the data from that node
                foreach (var stat in IndivStats) {
                    stats.Add(new Row {PlayerData = stat.InnerText});
                }

            // set the path to where we should write this data
            // in our case, a text file in our ADO.NET folder
            using (var writer = new StreamWriter("C:\\Users\\acass\\Documents\\- Whitworth 21-22\\Fall\\Database\\Final Project Compiled\\ADO.NET\\Scraper_data\\Scraped_data.txt"))
            // not sure what exactly this does, but I assume we are making a new csv file writer object with the
            // "writer" var made above
            // write records to the address listed
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            { csv.WriteRecords(stats); }
        }

        // each "row" is basically just a string that gets text loaded into it from the htmlagility package
        public class Row {
            public string PlayerData {get; set;}
        }
    }
}