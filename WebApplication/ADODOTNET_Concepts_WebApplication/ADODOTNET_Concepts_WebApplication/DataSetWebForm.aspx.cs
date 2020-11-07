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
    public partial class DataSetWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            if (Cache["Data"] == null)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    // Fetching multiple tables at the same time
                    SqlDataAdapter da = new SqlDataAdapter("spGetProductAndCategoriesData", con);
                    // Another way without using constructor
                    // da.SelectCommand = new SqlCommand("spGetProductAndCategoriesData", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    // To pass any value as paramter
                    // da.SelectCommand.Parameters.AddWithValue("@ProductID", Convert.ToInt32(txtProductId.Text));

                    DataSet ds = new DataSet();
                    /*
                     *  Unlike data reader, data adapter does not need connection opened explicitly before executing Fill,
                     *  it automatically opens connection using this statement whenever needed and closes it immediately
                     *  as soon as the operation is finished.
                     */
                    da.Fill(ds);

                    // Loading data in cache
                    Cache["Data"] = ds;

                    AssignDataSourceToGridViews(ds);
                    lblStatus.Text = "Data loaded from database.";
                }
            }
            else
            {
                // Loading data from cache
                DataSet ds = (DataSet)Cache["Data"];
                AssignDataSourceToGridViews(ds);
                lblStatus.Text = "Data loaded from cache.";
            }
        }

        private void AssignDataSourceToGridViews(DataSet ds)
        {
            // We can give name to datasets/table fetched from database for better reading using Table collection inside dataset
            ds.Tables[0].TableName = "Inventory";
            ds.Tables[1].TableName = "Categories";

            // We can select table either using index or name given by us or default name assigned eg. Table, Table1, etc
            gvProductInventory.DataSource = ds.Tables["Inventory"];
            gvProductInventory.DataBind();

            gvProductCategories.DataSource = ds.Tables["Categories"];
            gvProductCategories.DataBind();
        }

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            if(Cache["Data"] != null)
            {
                // Removing data from cache
                Cache.Remove("Data");
                lblStatus.Text = "Data cleared from cache.";
            }
            else
            {
                lblStatus.Text = "No data left to remove from cache.";
            }
        }
    }
}