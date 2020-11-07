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
    public partial class DisconnectedDataAccessWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        private void GetAllStudentsFromDB()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM tblStudents";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Students");

                // Identifying/Specifying the primary key in dataset for editing and deleting purpose when used in gridview
                ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["ID"] };

                // Storing the dataset in cache
                Cache.Insert("DATA", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

                gvStudents.DataSource = ds;
                gvStudents.DataBind();

                lblMessage.Text = "Data Loaded from Database";
            }
        }
        private void GetAllStudentsFromCache()
        {
            if (Cache["DATA"] != null)
            { 
                DataSet ds = new DataSet();
                ds = (DataSet)Cache["DATA"];
                // Identifying/Specifying the primary key in dataset for editing and deleting purpose when used in gridview
                ds.Tables["Students"].PrimaryKey = new DataColumn[] { ds.Tables["Students"].Columns["ID"] };

                gvStudents.DataSource = ds;
                gvStudents.DataBind();

                lblMessage.Text = "Data Loaded from Cache";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnGetDataFromDB_Click(object sender, EventArgs e)
        {
            GetAllStudentsFromDB();
        }

        protected void gvStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStudents.EditIndex = e.NewEditIndex;
            GetAllStudentsFromCache();
        }

        protected void gvStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Cache["DATA"] != null)
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Cache["DATA"];

                // Find the current selected row using primary key("ID")'s value
                DataRow row = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);
                row["Name"] = e.NewValues["Name"];
                row["Gender"] = e.NewValues["Gender"];
                row["TotalMarks"] = e.NewValues["TotalMarks"];

                // Updating the dataset in cache
                Cache.Insert("DATA", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

                // Deselect selected row after updation is done
                gvStudents.EditIndex = -1;

                // Loading the data from cache after updating entry in dataset
                GetAllStudentsFromCache();
            }
        }

        protected void gvStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["DATA"] != null)
            {
                DataSet ds = new DataSet();
                ds = (DataSet)Cache["DATA"];
                DataRow row = ds.Tables["Students"].Rows.Find(e.Keys["ID"]);
                row.Delete();

                // Updating the dataset in cache
                Cache.Insert("DATA", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

                // Loading the data from cache after deleting entry in dataset
                GetAllStudentsFromCache();
            }
        }

        protected void gvStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Deselect selected row when cancel is clicked when editing is going on
            gvStudents.EditIndex = -1;
            GetAllStudentsFromCache();
        }

        protected void btnUpdateDB_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM tblStudents";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = (DataSet)Cache["DATA"];

                string updateQuery = "UPDATE tblStudents SET Name=@Name, Gender=@Gender, TotalMarks=@TotalMarks WHERE ID = @ID";
                // Associate update query with data adapter using sql command object
                da.UpdateCommand = new SqlCommand(updateQuery, con);
                da.UpdateCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50, "Name");
                da.UpdateCommand.Parameters.Add("@Gender", SqlDbType.NVarChar, 20, "Gender");
                da.UpdateCommand.Parameters.Add("@TotalMarks", SqlDbType.Int, 0, "TotalMarks");
                da.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "ID");

                string deleteQuery = "DELETE FROM tblStudents WHERE ID = @ID";
                // Associate delete query with data adapter using sql command object
                da.DeleteCommand = new SqlCommand(deleteQuery, con);
                da.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "ID");

                // Relay the changes to database
                da.Update(ds, "Students");

                GetAllStudentsFromDB();

                lblMessage.Text = "Database updated";
            }
        }
    }
}