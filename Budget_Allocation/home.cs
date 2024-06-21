using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget_Allocation
{
    public partial class home : Form
    {
        functions Con;
        public home()
        {
            InitializeComponent();
            Con = new functions();
            ShowProposals();
        }
        private void ShowProposals()
        {
            string Query = "select * from ProposalTbl";
            ProposalList.DataSource = Con.GetData(Query);
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(ClubName.Text == "" || PBudget.Text == "" || TEvents.Text == "" )
                {
                    MessageBox.Show("Missing Data !!!");
                }
                else
                {
                    string Club_Name = ClubName.Text;
                    string Proposed_Budget = PBudget.Text;
                    string Total_Events = TEvents.Text;
                    string Query = "insert into ProposalTbl values('{0}','{1}','{2}')";
                    Query = string.Format(Query,Club_Name, Proposed_Budget, Total_Events);
                    Con.SetData(Query);
                    ShowProposals();
                    MessageBox.Show("club proposal added !!");
                    ClubName.Text = "";
                    PBudget.Text = "";
                    TEvents.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int key = 0;
        private void ProposalList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ClubName.Text = ProposalList.SelectedRows[0].Cells[1].Value.ToString();
            PBudget.Text  = ProposalList.SelectedRows[0].Cells[2].Value.ToString();
            TEvents.Text = ProposalList.SelectedRows[0].Cells[3].Value.ToString();
            if(ClubName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProposalList.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void adminBtn_Click(object sender, EventArgs e)
        {
            admin obj1 = new admin();
            obj1.Show();
            this.Hide();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string name=ClubName.Text;
            try
            {
                if (ClubName.Text == "")
                {
                    MessageBox.Show("Missing Data !!!");
                }
                else
                {
                    string Query = $"delete from ProposalTbl where ClubName = '{name}'";

                    var abc = Con.SetData(Query);
                    if (abc == 0)
                    {
                        MessageBox.Show("NO DATA FOUND !!");
                    }
                    else {
                        ShowProposals();
                        MessageBox.Show("club proposal deleted !!");
                        ClubName.Text = "";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {

            try
            {
                if (ClubName.Text == "" || PBudget.Text == "" || TEvents.Text == "")
                {
                    MessageBox.Show("Missing Data !!!");
                }
                else
                {
                    string Club_Name = ClubName.Text;
                    string Proposed_Budget = PBudget.Text;
                    string Total_Events = TEvents.Text;
                    string Query = "update  ProposalTbl set  ClubName = '{0}',PBudget = '{1}',TEvents = '{2}' where Clubname = {3}";
                    Query = string.Format(Query, Club_Name, Proposed_Budget, Total_Events,key);
                    Con.SetData(Query);
                    ShowProposals();
                    MessageBox.Show("club proposal updated !!");
                    ClubName.Text = "";
                    PBudget.Text = "";
                    TEvents.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
