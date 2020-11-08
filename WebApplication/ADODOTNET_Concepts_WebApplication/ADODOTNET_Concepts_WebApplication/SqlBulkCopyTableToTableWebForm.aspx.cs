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
    public partial class SqlBulkCopyTableToTableWebForm : System.Web.UI.Page
    {
        string sourceCS = ConfigurationManager.ConnectionStrings["SourceCS"].ConnectionString;
        string destinationCS = ConfigurationManager.ConnectionStrings["DestinationCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSqlBulkCopy_Click(object sender, EventArgs e)
        {
            using (SqlConnection sourceCon = new SqlConnection(sourceCS))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Departments", sourceCon);

                sourceCon.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationCon = new SqlConnection(destinationCS))
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationCon))
                        {
                            bulkCopy.DestinationTableName = "Departments";
                            // Mapping between source and destination column is not needed if source and destination type are same eg same table in one database to same table in another database
                            destinationCon.Open();
                            bulkCopy.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("SELECT * FROM Employees", sourceCon);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (SqlConnection destinationCon = new SqlConnection(destinationCS))
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationCon))
                        {
                            bulkCopy.DestinationTableName = "Employees";
                            // Mapping between source and destination column is not needed if source and destination type are same eg same table in one database to same table in another database
                            destinationCon.Open();
                            bulkCopy.WriteToServer(rdr);
                        }
                    }
                }
            }
        }
    }
}