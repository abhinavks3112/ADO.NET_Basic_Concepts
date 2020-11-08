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
    public partial class SqlBulkCopyWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSqlBulkCopy_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/Data/Data.xml"));

                DataTable dtDepartment = ds.Tables["Department"];
                DataTable dtEmployee = ds.Tables["Employee"];

                con.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = "Departments";
                    
                    // Mapping between source and destination column is to be provided if source and destination type are different eg xml to sql table
                    bulkCopy.ColumnMappings.Add("ID", "ID");
                    bulkCopy.ColumnMappings.Add("Name", "Name");
                    bulkCopy.ColumnMappings.Add("Location", "Location");

                    // Write to database
                    bulkCopy.WriteToServer(dtDepartment);
                }

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = "Employees";
                    
                    // Mapping between source and destination column is to be provided if source and destination type are different eg xml to sql table
                    bulkCopy.ColumnMappings.Add("ID", "ID");
                    bulkCopy.ColumnMappings.Add("Name", "Name");
                    bulkCopy.ColumnMappings.Add("Gender", "Gender");
                    bulkCopy.ColumnMappings.Add("DepartmentId", "DepartmentId");
                    
                    // Write to database
                    bulkCopy.WriteToServer(dtEmployee);
                }
            }
        }
    }
}