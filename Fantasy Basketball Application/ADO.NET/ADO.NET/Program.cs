/// this is the main program for our nba stats analyzer
// references:
// https://medium.com/c-sharp-progarmming/create-your-own-web-scraper-in-c-in-just-a-few-minutes-c42649adda8 
// for C# web scraping


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace fantasy_db_updater
{
    class updater
    {
        // key for the names of the columns, since I have no access here to get a reader.GetName for column name
        static string[] COL_KEY = { "PlayerName", "Pos", "Age", "Team", "G", "GS", "MP", "FG", "FGA", "FGP", "THP", "THPA", "THPP", "TWP", "TWPA", "TWPP", "eFGP", "FT", "FTA",
                              "FTP", "ORB", "DRB", "TRB", "AST", "STL", "BLK", "TOV", "PF", "PTS" };

        static void Main(string[] args)
        {
            bool run = true;
            while (run) {
                Console.WriteLine("--------------------------");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Current Database Contents:");
                Console.WriteLine("--------------------------");
                Console.WriteLine("--------------------------");
                Console.WriteLine();
                array_print(COL_KEY);
                list_print(run_query(current_db_query()));
                Console.WriteLine(); Console.WriteLine();
                Console.WriteLine("-------------------------");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Current Scraped Contents:");
                Console.WriteLine("-------------------------");
                Console.WriteLine("-------------------------");
                Console.WriteLine();
                array_print(COL_KEY);
                list_print(load_scraped_data());
                update_database();
                Console.WriteLine("Database Updated Successfully.");

                // this ensures that the console doesnt automatically close out as soon as it is done writing
                if (Console.ReadLine() == "Done") run = false;
            }
        }
        
        // functionalized setup process for our login, reduces redundancy
        // create a new command object, load our connection data, and return it from this function
        static OleDbCommand set_connection() 
        {
            try
            {
                OleDbCommand t_cmd = new OleDbCommand(); // initialize a db command object
                // set parameters to join the server
                String stConnect = "Provider=sqloledb; Data Source = cs1; Initial Catalog = fantasyleague; User id = Acassidy23x; Password = 2253050";

                // create connection to server through our command object and open
                t_cmd.Connection = new OleDbConnection(stConnect);
                t_cmd.Connection.Open();

                return t_cmd;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error in connection setup: " + ex.Message + ex.StackTrace);
                return null;
            }
        }

        // function that calls the run_query multiple times in order to update our database with live stats
        static void update_database() {
            var db_data = run_query(current_db_query());    // database data
            var sc_data = load_scraped_data();              // scraped data
            run_query(send_changes_query(run_query(current_db_query()), load_scraped_data()));
        }

        // used mostly for debugging, prints a List<List<string>>
        static void list_print(List<List<string>> list) {
            foreach (var fir in list) {
                foreach (var sec in fir) {
                    Console.Write(sec + "  ");
                }
                Console.WriteLine();
            }
        }

        static void array_print(string[] arr) {
            foreach (var elem in arr) {
                Console.Write(elem + " | ");
            }
            Console.WriteLine();
        }

        // takes any sql query and runs it, storing values in a temp List<List<string>> and returning it
        static List<List<string>> run_query(string query) {
            try
            {
                // set_connection connection for the project
                OleDbCommand cmd = set_connection();

                // load the current query needed
                cmd.CommandText = query;

                Console.WriteLine(cmd.CommandText);

                //Creates a Datareader to retrieve rows from the database
                OleDbDataReader rdr = cmd.ExecuteReader();

                // temporary List<List<string>> to store our data in
                var data_collected = new List<List<string>>() {};


                //While loop which gets all information from database
                int j = 0;
                while (rdr.Read()) {
                    bool advance_column = true;
                    int i = 0;
                    data_collected.Add(new List<string>() {});
                    while (advance_column) {
                        try {
                            string el = rdr[i].ToString().Trim();
                            //Console.Write(el);
                            // add the data we are currently reading to our temp List<List<string>>
                            if (el != null || el != "")   
                                data_collected[j].Add(el);
                            //Console.WriteLine("1");
                            i++;
                        }
                        catch {
                            advance_column = false;
                        }
                    }
                    j++;
                    //Console.WriteLine();
                }
                rdr.Close(); //Closes connection 
                var temp = new List<List<string>>() {};
                foreach (var fir in data_collected) {
                    if (fir != null) temp.Add(fir);

                }
                    return data_collected;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Oops, error: " + ex.Message + ex.StackTrace);
                return null;
            }
        }

        // runs the scraper that we have created and saved on this computer
        // learned the scripting from https://stackoverflow.com/questions/527513/execute-powershell-script-from-c-sharp-with-commandline-arguments
        static void scrape_data() {
            ProcessStartInfo ps_script_info = new ProcessStartInfo();
            ps_script_info.FileName = @"powershell.exe";
            ps_script_info.Arguments = @"& '.\Scraper_data\scrape_command.ps1'"; // edit this to a full system path ?
            ps_script_info.RedirectStandardOutput = true;
            ps_script_info.RedirectStandardError = true;
            ps_script_info.UseShellExecute = false;
            ps_script_info.CreateNoWindow = true;
            Process p = new Process();
            p.StartInfo = ps_script_info;
            p.Start();
        }

        // this function will run our scraper and load the generated txt file into a List<List<string>>
        // learned from https://stackoverflow.com/questions/858756/how-to-parse-a-text-file-with-c-sharp
        static List<List<string>> load_scraped_data() {
            scrape_data();
            var scraped_data = new List<List<string>> { };
            StreamReader read;
            // wait for file to be written
            while (!File.Exists(".\\Scraper_data\\Scraped_data.txt")) {
                // do nothing, just wait until it exists! (it will)
            }
            // wait an additional 10 seconds for the file to be written to
            // otherwise, we get an error since the file is being accessed by powershell in the background
            System.Threading.Thread.Sleep(10000);
            read = File.OpenText(".\\Scraper_data\\Scraped_data.txt");
            string line; 

            // the ONLY reason this is hardcoded is because every player has exactly 29 lines worth of data
            line = read.ReadLine();
            line = read.ReadLine(); 
            while (line != null) {
                var out_stats = new List<string> { };
                for (int i = 0; i < 29; i++) {    
                    out_stats.Add(line); 
                    line = read.ReadLine();   
                }
                scraped_data.Add(out_stats);

            }
            read.Close();
            File.Delete(".\\Scraper_data\\Scraped_data.txt");
            Console.WriteLine("Data Scraped Successfully");
            return scraped_data;
        }

        // returns a query to pull our current data in the database
        static string current_db_query() {
            return "SELECT * FROM [Player_Stats]; ";
        }

        // only ever used once, simply for uploading our data to the db for the first time
        static string initialize_query(List<List<string>> upload_data) {
            string stQuery = "";
            // enumerate through each player in upload_data, and create and INSERT statement for them
            int j = 0;
            foreach (var player in upload_data) {
                stQuery += "INSERT INTO [Player_Stats] VALUES ( ";
                int i = 0;
                int nc = 0;
                foreach (var stat in player) {
                    if (stat != null) {
                        if (i == 0 || i == 1 || i == 3)
                            stQuery += "\'";

                        string tempst = "";
                        // checks to see if a player's name contains an apostrophe, since that makes SQL angry
                        if (stat != null && stat.Contains("\'") ) {
                            tempst = stat.Replace("\'", "");
                        } else {
                            if(stat == "") {
                                tempst = "0";
                            } else {
                                tempst = stat;
                            }
                            nc++;
                        }

                        stQuery += tempst;

                        if (i == 0 || i == 1 || i == 3)
                            stQuery += "\'";
                        if (i < upload_data[j].Count - 1)
                            stQuery += ", ";
                    } else {
                        stQuery += "";
                    }
                    i++;
                }
                stQuery += " ); ";
                j++;
            }
            return stQuery;
        }

        // this function will return a string that is the sql query to be run for updating our statistics
        // it calls get_update_query, and gives it the information needed to generate the query
        static string send_changes_query(List<List<string>> db, List<List<string>> sc)
        {
            // read in stats from online for given player, create list<string> of cols to be changed, and list<string> of vals for those cols
            // initialize those lists
            var player_key = new List<string> { };
            var col_change = new List<List<string>> { };
            var val_change = new List<List<string>> { };


            // compare the elements of both List<List<string>>'s
            // if they do not mach, add them to the pending changes for the update
            // query with the scrape's values overriding
            for (int i = 0; i < db.Count; i++) {
                var cols = new List<string>() {};
                var vals = new List<string>() {}; 
                for (int j = 0; j < db[i].Count; j++) {
                    if (db[i][j] != sc[i][j]) {
                        cols.Add(COL_KEY[j]);
                        vals.Add(sc[i][j]); 
                    }
                }
                player_key.Add(db[i][0]);
                col_change.Add(cols);
                val_change.Add(vals);
            }

            return get_update_query(player_key, col_change, val_change);
        }

        // generates a query to update the values in our database, returns it to the update_stats function
        // player_name acts as the key for our database
        static string get_update_query(List<string> player_key, List<List<string>> col_change, List<List<string>> val_change)
        {
            string stQuery = "";
            // enumerate through each player that needs updating, create a query and append it to our query string
            for (int j = 0; j <= player_key.Count - 1; j++)
            {
                // create an update query for this player
                stQuery += "UPDATE [Player_Stats] ";
                stQuery += "SET ";
                // writes the "SET" line for the update query, defines what columns we are changing and what value we are changing to
                for (int i = 0; i <= col_change[j].Count - 1; i++)
                {
                    
                    string tempst = val_change[j][i].Replace("\'", "");
                    stQuery += col_change[j][i] + " = \'" + tempst + "\'";
                    if (i < col_change[j].Count - 1) stQuery += ", ";
                    else stQuery += " ";
                }
                // writes the WHERE line, tells what player we are currently changing stats for
                stQuery += "WHERE PlayerName = \'" + player_key[j] + "\'; ";
            }
            return stQuery;
        }
    }
}
