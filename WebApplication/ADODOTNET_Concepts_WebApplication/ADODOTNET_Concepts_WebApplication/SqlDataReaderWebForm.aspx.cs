using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADODOTNET_Concepts_WebApplication
{
    public partial class GridViewWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayAllEmployee();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        { 
            AddNewEmployee();
            DisplayAllEmployee();
        }

        private void DisplayAllEmployee()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Method 1 of doing read from database and all values assigned to gridview and binded to it
                //gvEmployees.DataSource = cmd1.ExecuteReader();
                //gvEmployees.DataBind();

                // Creating a table with additional column that is not present in database 
                DataTable table = new DataTable();
                table.Columns.Add("Employee Id");
                table.Columns.Add("Name");
                table.Columns.Add("Gender");
                table.Columns.Add("Salary");
                table.Columns.Add("Bonus Salary"); // Not present in database
                
                con.Open();
                // If we need to make changes row by row or column by column per row, we can do it in following way
                using (SqlDataReader rdr = cmd.ExecuteReader()) // SqlDataReader is also connection oriented so we need to close reader when we are done reading hence using it inside using block
                {
                    while (rdr.Read())
                    {
                        // Creating a datarow object 
                        DataRow row = table.NewRow();

                        /*
                         * Following is just an example to show sequential reading concept, without it, we can just
                         * change original query in database side itself and then all this will not be needed
                         */
                        int salary = Convert.ToInt32(rdr["Salary"]);
                        int bonus = Convert.ToInt32(0.1 * salary);

                        // Creating row with column values from corresponding values coming from database
                        row["Employee Id"] = rdr["EmployeeId"];
                        row["Name"] = rdr["Name"];
                        row["Gender"] = rdr["Gender"];
                        row["Salary"] = rdr["Salary"];
                        row["Bonus Salary"] = bonus;

                        table.Rows.Add(row);
                    }
                }
                /* 
                 * Method 2 of doing sequential read from database, 
                 * creating and appending new column to existing column from database
                */
                gvEmployees.DataSource = table;
                gvEmployees.DataBind();
            }
        }
        private void AddNewEmployee()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Add_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", txtEmployeeName.Text);
                cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Salary", Convert.ToInt32(txtSalary.Text));

                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@EmployeeId";
                outputParameter.SqlDbType = SqlDbType.Int;
                outputParameter.Direction = ParameterDirection.Output; // Important in case of out parameter

                cmd.Parameters.Add(outputParameter);
                con.Open();
                cmd.ExecuteNonQuery();
                lblStatus.Text = "Insert Successful. Generated Employee Id is " + outputParameter.Value;
            }
        }
    }
}