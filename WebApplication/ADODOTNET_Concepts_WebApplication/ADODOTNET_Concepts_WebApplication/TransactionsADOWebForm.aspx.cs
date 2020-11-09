using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADODOTNET_Concepts_WebApplication
{
    public partial class TransactionsADOWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ACCOUNTS", con);
                con.Open();
                using(SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        if(rdr["AccountNumber"].ToString() == "A1")
                        {
                            lblAccountNumber1.Text = "A1";
                            lblName1.Text = rdr["CustomerName"].ToString();
                            lblBalance1.Text =rdr["Balance"].ToString();
                        }
                        if (rdr["AccountNumber"].ToString() == "A2")
                        {
                            lblAccountNumber2.Text = "A2";
                            lblName2.Text = rdr["CustomerName"].ToString();
                            lblBalance2.Text = rdr["Balance"].ToString();
                        }
                    }
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlTransaction transaction = con.BeginTransaction();

                // Associate sqlcommand with specified transaction by passing transaction instance as parameter
                SqlCommand cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance - 10 WHERE AccountNumber = 'A1'", con, transaction);
               
                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        cmd = new SqlCommand("UPDATE Accounts1 SET Balance = Balance + 10 WHERE AccountNumber = 'A2'", con, transaction);
                        rowsAffected = cmd.ExecuteNonQuery();

                        if(rowsAffected == 1)
                        {
                            transaction.Commit();
                        }
                    }

                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Transaction is successful!!";

                    LoadData();
                }
                catch
                {
                    transaction.Rollback();

                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Transaction failed!!";
                }
            }
        }
    }
}