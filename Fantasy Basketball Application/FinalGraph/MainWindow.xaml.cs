using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Libarays for the Graphs to generate & commands used to populate graph
using LiveCharts;
using LiveCharts.Wpf;

//Library used to connect to the the server
using System.Data.SqlClient;
using System.Configuration;

namespace FinalGraph
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.fantasyleagueConnectionString);
            //intilizes the window -- basically a constructor
            InitializeComponent();

            if (dc.DatabaseExists())
            {
                //ExampleDategrid.ItemsSource = dc.Temp_Table_of_Player_Stats;
                ExampleDategrid.ItemsSource = dc.Player_Stats;
            }

            //populates the Graph with data from the server
            GetChartData();

            //Labels for all the different elements from the server inputed into the graph
            //Example Top player based on stat F5 will have the label F5 (will be F5 until script is done tonight)
            //BarLabels = new[] { "G"};
            //BarLabels = new[] { "GS"};
            //BarLabels = new[] { "MP"};
            //BarLabels = new[] { "FG"};
            //BarLabels = new[] { "FGA"};
            //BarLabels = new[] { "FGP"};
            //BarLabels = new[] { "THP"};
            //BarLabels = new[] { "THP"};
            //BarLabels = new[] { "THPA"};
            //BarLabels = new[] { "THPP"};
            //BarLabels = new[] { "TWP"};
            //BarLabels = new[] { "TWPA"};
            //BarLabels = new[] { "TWPP"};
            //BarLabels = new[] { "eFGP"};
            //BarLabels = new[] { "FT"};
            //BarLabels = new[] { "FTA"};
            //BarLabels = new[] { "FTP"};
            //BarLabels = new[] { "ORB"};
            //BarLabels = new[] { "DRB"};
            //BarLabels = new[] { "TRB"};
            //BarLabels = new[] { "AST"};
            //BarLabels = new[] { "STL"};
            //BarLabels = new[] { "BLK"};
            //BarLabels = new[] { "TOV"};
            //BarLabels = new[] { "PF"};
            //BarLabels = new[] { "PTS"};

            //EXAMPLE 2:
            //BarLabels = new[] { "PTS", "PF", "TOV"};

            //EXAMPLE 3:
            //Graphs visual kinda breaks and doesn't show them on the x-axis
            //However if you hover over the column number they are all correctly labeled
            BarLabels = new[] { "G", "GS", "MP", "FG", "FGA", "FGP", "THP", "THPA", "THPP", "TWP", "TWPA", "TWPP", "eFGP", "FT", "FTA", "FTP", "ORB", "DRB", "TRB", "AST", "STL", "BLK", "PTS", "PF", "TOV"};



            //
            Formatter = value => value.ToString();

            //This pushes everything to the window that will be generated
            DataContext = this;
        }

        //The Function will Connent to the server, read in a query, taking the info from the table based on the query,
        //create a Series object list to sort the info read in while in a loop
        public void GetChartData()
        {
            //DataClasses1DataContext dc = new DataClasses1DataContext(Properties.Settings.Default.fantasyleagueConnectionString);
            //Stores the Connection string which is used to connect to server
            string dc = Properties.Settings.Default.fantasyleagueConnectionString;
            //creating a new connection to the server
            using (SqlConnection con = new SqlConnection(dc))
            {
                int counter = 0; //counter to make sure only one object list is created 

                //Creates a command to be run by SQL server which is the query below

                //EXAMPLE 1: For Top 3 best player based on one given stat
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, G FROM[Player_Stats] ORDER BY G DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, GS FROM[Player_Stats] ORDER BY GS DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, MP FROM[Player_Stats] ORDER BY MP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FG FROM[Player_Stats] ORDER BY FG DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FGA FROM[Player_Stats] ORDER BY FGA DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FGP FROM[Player_Stats] ORDER BY FGP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, THP FROM[Player_Stats] ORDER BY THP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, THPA FROM[Player_Stats] ORDER BY THPA DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, THPP FROM[Player_Stats] ORDER BY THPP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, TWP FROM[Player_Stats] ORDER BY TWP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, TWPP FROM[Player_Stats] ORDER BY TWPP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, eFGP FROM[Player_Stats] ORDER BY eFGP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FT FROM[Player_Stats] ORDER BY FT DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FTP FROM[Player_Stats] ORDER BY FTP DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, FTA FROM[Player_Stats] ORDER BY FTA DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, ORB FROM[Player_Stats] ORDER BY ORB DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, DRB FROM[Player_Stats] ORDER BY DRB DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, TRB FROM[Player_Stats] ORDER BY TRB DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, AST FROM[Player_Stats] ORDER BY STL DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, BLK FROM[Player_Stats] ORDER BY BLK DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, TOV FROM[Player_Stats] ORDER BY TOV DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, PF FROM[Player_Stats] ORDER BY PF DESC", con);
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, PTS FROM[Player_Stats] ORDER BY PTS DESC", con);

                //EXAMPLE 2: For Top 3 player for a given, but still want to see other data
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(3) PlayerName, PTS, PF, TOV FROM[Player_Stats] ORDER BY PTS DESC", con);

                //EXAMPLE 3: For Comparing the top 2 player based on a given stat (however, you can input name to compare two players) 
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT TOP(2) * FROM[Player_Stats] ORDER BY PTS DESC", con);


                //Opening the connection
                con.Open();

                //Creates a data reader varible which reads the info from the table
                SqlDataReader rdr = cmd.ExecuteReader();
                
                //Loop to go through all instances of the query table
                while (rdr.Read())
                {
                    //Conneverts the Data from the reader object --> string, then string --> double
                    //since all stats are all double or ints
                    double G = Convert.ToDouble(rdr["G"].ToString());
                    double GS = Convert.ToDouble(rdr["GS"].ToString());
                    double MP = Convert.ToDouble(rdr["MP"].ToString());
                    double FG = Convert.ToDouble(rdr["FG"].ToString());
                    double FGA = Convert.ToDouble(rdr["FGA"].ToString());
                    double FGP = Convert.ToDouble(rdr["FGP"].ToString());
                    double THP = Convert.ToDouble(rdr["THP"].ToString());
                    double THPA = Convert.ToDouble(rdr["THPA"].ToString());
                    double TWP = Convert.ToDouble(rdr["TWP"].ToString());
                    double THPP = Convert.ToDouble(rdr["THPP"].ToString());
                    double TWPA = Convert.ToDouble(rdr["TWPA"].ToString());
                    double TWPP = Convert.ToDouble(rdr["TWPP"].ToString());
                    double eFGP = Convert.ToDouble(rdr["eFGP"].ToString());
                    double FT = Convert.ToDouble(rdr["FT"].ToString());
                    double FTA = Convert.ToDouble(rdr["FTA"].ToString());
                    double FTP = Convert.ToDouble(rdr["FTP"].ToString());
                    double ORB = Convert.ToDouble(rdr["ORB"].ToString());
                    double TRB = Convert.ToDouble(rdr["TRB"].ToString());
                    double DRB = Convert.ToDouble(rdr["DRB"].ToString());
                    double AST = Convert.ToDouble(rdr["AST"].ToString());
                    double STL = Convert.ToDouble(rdr["STL"].ToString());
                    double BLK = Convert.ToDouble(rdr["BLK"].ToString());
                    double TOV = Convert.ToDouble(rdr["TOV"].ToString());
                    double PF = Convert.ToDouble(rdr["PF"].ToString());
                    double PTS = Convert.ToDouble(rdr["PTS"].ToString());

                    //Making sure its the first instance so the series object list can be created
                    if (counter == 0)
                    {
                        //Object list creation with first instance
                        SeriesCollection = new SeriesCollection
                        {
                            //Has to be column since we are trying to do Column chart instead of Line chart
                            new ColumnSeries
                            {
                                Title = rdr["PlayerName"].ToString(), //Makes the Series Name the name of the player
                                 //makes different sections based on the number of inputed stats on those players
                                 //To Run Example 1: you must replace the double inside the brace with the table associated to
                                 //the stat you a running. Also you must comment the deleted stat. Finally if you wan the label to be 
                                 //correct when graphing comment out deleted stat and uncomment stat you are runing
                                //Values = new ChartValues<double> {PTS} 

                                //for when you run example 2 you must uncomment PTS, PF, TOV double varible. 
                                //You must also uncomment the BarLabel varible for Example 2 and comment everything that not it
                                //Values = new ChartValues<double> {PTS, PF, TOV} -- Use for when you run example 2

                                //for when you run example 3 you must uncomment all double varible. 
                                //You must also uncomment the BarLabel varible for Example 3 and comment everything that not it
                                Values = new ChartValues<double> {G, GS, MP, FG, FGA, FGP, THP, THPA, THPP, TWP, TWPA, TWPP, eFGP, FT, FTA, FTP, ORB, DRB, TRB, AST, STL, BLK, PTS, PF, TOV}
                            }
                        };
                        counter++; //increase counter so a new list isn't created
                    } else
                    {
                        //adding to the already created list
                        SeriesCollection.Add(new ColumnSeries
                        {
                            Title = rdr["PlayerName"].ToString(), //Makes the Series Name the name of the player
                            // makes different sections based on the number of inputed stats on those players
                            
                            //To Run Example 1: you must replace the double inside the brace with the table associated to
                            //the stat you a running. Also you must comment the deleted stat. Finally if you wan the label to be 
                            //correct when graphing comment out deleted stat and uncomment stat you are runing
                            //Values = new ChartValues<double> { PTS }

                            //for when you run example 2 you must uncomment PTS, PF, TOV double varible. 
                            //You must also uncomment the BarLabel varible for Example 2 and comment everything that not it
                            //Values = new ChartValues<double> {PTS, PF, TOV} -- Use for when you run example 2

                            //for when you run example 3 you must uncomment all double varible. 
                            //You must also uncomment the BarLabel varible for Example 3 and comment everything that not it
                            Values = new ChartValues<double> {G, GS, MP, FG, FGA, FGP, THP, THPA, THPP, TWP, TWPA, TWPP, eFGP, FT, FTA, FTP, ORB, DRB, TRB, AST, STL, BLK, PTS, PF, TOV}

                        });
                        counter++; //increase counter so a new list isn't created
                    }
                }
            }
        }

        
        //Stores a collection of series to plot, this notifies the changes every time
        //you add/remove any recored series
        public SeriesCollection SeriesCollection { get; set;}

        //String array to hold the label for the x-axis instance from the different series
        public string[] BarLabels { get; set;}

        //Once of the required functions to make the Graph actually work
        //Function that allowing the data to be set
            //Value = double
            //Title = string
        public Func<double, string> Formatter { get; set; }

    }
}
