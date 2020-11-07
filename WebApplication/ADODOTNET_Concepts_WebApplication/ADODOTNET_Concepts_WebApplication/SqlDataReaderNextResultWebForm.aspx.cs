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
    public partial class SqlDataReaderNextResultWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                // Fetching multiple tables at the same time
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblProductInventory; SELECT * FROM tblProductCategories;", con);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // Fetching first table in same sequence as mentioned in query
                    // By default, data reader cursor points to first result set
                    gvProductInventory.DataSource = rdr;
                    gvProductInventory.DataBind();

                    // Moving the data reader cursor to next result set using SqlDataReader NextResult function
                    // This will loop through all result set, since in our case there is only one more, it move to one more and finish
                    while (rdr.NextResult())
                    {
                        gvProductCategories.DataSource = rdr; // Now rdr is pointing to next result set or table in our case
                        gvProductCategories.DataBind();
                    }
                }
            }
        }
    }
}