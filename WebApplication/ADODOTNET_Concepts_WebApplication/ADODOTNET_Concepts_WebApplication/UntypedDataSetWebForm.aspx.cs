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
    public partial class StronglyTypedDataSetWebForm : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblStudents", con);

                    DataSet ds = new DataSet();
                    /*
                     *  Unlike data reader, data adapter does not need connection opened explicitly before executing Fill,
                     *  it automatically opens connection using this statement whenever needed and closes it immediately
                     *  as soon as the operation is finished.
                     */
                    da.Fill(ds, "Students");

                    Session["DATASET"] = ds;

                    // Using LINQ to select each row entry from dataset and mapping it to Student object
                    // This is untyped dataset
                    gvStudents.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                            select new Student() 
                                            { 
                                                ID = Convert.ToInt32(dataRow["ID"]), 
                                                Name = Convert.ToString(dataRow["Name"]),
                                                Gender = Convert.ToString(dataRow["Gender"]),
                                                TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                            };
                    gvStudents.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = (DataSet)Session["DATASET"];
            if (!String.IsNullOrEmpty(txtNameToSearch.Text))
            {
                // Using LINQ to select each row entry from dataset, filter entries based on string passed by user and mapping the filtered result to Student object
                // This is untyped dataset
                gvStudents.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                        where dataRow["Name"].ToString().ToUpper().Contains(txtNameToSearch.Text.ToUpper())
                                        select new Student()
                                        {
                                            ID = Convert.ToInt32(dataRow["ID"]),
                                            Name = Convert.ToString(dataRow["Name"]),
                                            Gender = Convert.ToString(dataRow["Gender"]),
                                            TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                        };
            }
            else
            {
                Response.Write("<span style='font-weight:bold; color:red'>Please enter a text to search in the textbox.</span><br /><br />");

                // Using LINQ to select each row entry from dataset and mapping it to Student object
                // This is untyped dataset
                gvStudents.DataSource = from dataRow in ds.Tables["Students"].AsEnumerable()
                                        select new Student()
                                        {
                                            ID = Convert.ToInt32(dataRow["ID"]),
                                            Name = Convert.ToString(dataRow["Name"]),
                                            Gender = Convert.ToString(dataRow["Gender"]),
                                            TotalMarks = Convert.ToInt32(dataRow["TotalMarks"])
                                        };
            }
            gvStudents.DataBind();
        }
    }
}