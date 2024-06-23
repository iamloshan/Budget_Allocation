using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget_Allocation
{
    public partial class admin : Form
    {

        functions Con;
        public admin()
        {
            InitializeComponent();
            Con = new functions();
        }

        static int max(int a, int b) { return (a > b) ? a : b; }

       
        static List<int> selectedItems = new List<int>();

        
        static int knapSack(int W, int[] wt, int[] val, int n, List<int> items)
        {
            
            if (n == 0 || W == 0)
                return 0;

        
            if (wt[n - 1] > W)
                return knapSack(W, wt, val, n - 1, items);

            
            else
            {
               
                List<int> includeItems = new List<int>(items);
                List<int> excludeItems = new List<int>(items);

                
                includeItems.Add(n - 1);
                int includeValue = val[n - 1] + knapSack(W - wt[n - 1], wt, val, n - 1, includeItems);

               
                int excludeValue = knapSack(W, wt, val, n - 1, excludeItems);

                
                if (includeValue > excludeValue)
                {
                    items.Clear();
                    items.AddRange(includeItems);
                    return includeValue;
                }
                else
                {
                    items.Clear();
                    items.AddRange(excludeItems);
                    return excludeValue;
                }
            }
        }

        private string ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Public\Documents\budget\BudgetDb.mdf;Integrated Security=True;Connect Timeout=30";
        public List<int> GetValues(string query)
        {
             
            List<int> values = new List<int>();

            using (SqlConnection con = new SqlConnection(ConStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        values.Add(reader.GetInt32(0));
                    }
                }
            }

            return values;
        }

        public List<string> GetNames(string query)
        {

            List<string> values = new List<string>();

            using (SqlConnection con = new SqlConnection(ConStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        values.Add(reader.GetString(0));
                    }
                }
            }

            return values;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string query = "SELECT TEvents FROM ProposalTbl ORDER BY ClubName";
            string query2 = "SELECT PBudget FROM ProposalTbl ORDER BY ClubName";
            string query3 = "SELECT ClubName FROM ProposalTbl ORDER BY ClubName";

            string output = "";

            List<int> events2 = GetValues(query);
            List<int> weight2 = GetValues(query2);
            List<string> clubs2 = GetNames(query3);

            string[] clubs = clubs2.ToArray();
            int[] events = events2.ToArray();
            int[] weight = weight2.ToArray();
            int W = (int)ABudget.Value;
            int n = events.Length;

            List<int> items = new List<int>();
            int maxProfit = knapSack(W, weight, events, n, items);

            Console.WriteLine("Maximum Events: " + maxProfit);
            Console.WriteLine("Selected Clubs: ");
            foreach (int i in items)
            {
                string x = "Club Name " + clubs[i] + " With Total Events " + events[i] + " and Allocatod Budget " + weight[i];
                output =output + "\n " + x;
                Console.WriteLine("Club Name " + clubs[i] + " with Total Events " + events[i] + " and Allocatod Budget " + weight[i]);               
            }

            OutputLabel.Text= output;
        }

        private void homeBtn_Click(object sender, EventArgs e)
        {
            home obj2 = new home();
            obj2.Show();
            this.Hide();
        }

        private void approvedList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void adminBtn_Click(object sender, EventArgs e)
        {
            home obj2 = new home();
            obj2.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
