using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADODOTNET_Concepts_WebApplication
{
    public partial class StronglyTypedDataSetWebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                StudentDataSetTableAdapters.StudentsTableAdapter studentsTableAdapter = new StudentDataSetTableAdapters.StudentsTableAdapter();
                StudentDataSet.StudentsDataTable studentsDataTable = new StudentDataSet.StudentsDataTable();
                studentsTableAdapter.Fill(studentsDataTable);

                Session["DATATABLE"] = studentsDataTable;

                // Using LINQ to select each row entry from student datatable, and mapping the filtered result to new object as defined by student datatable columns
                // This is strongly typed datatable
                gvStudents.DataSource = from student in studentsDataTable
                                        select new 
                                        { 
                                            student.ID, 
                                            student.Name, 
                                            student.Gender, 
                                            student.TotalMarks 
                                        };
                gvStudents.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StudentDataSet.StudentsDataTable studentsDataTable = new StudentDataSet.StudentsDataTable();


            studentsDataTable = (StudentDataSet.StudentsDataTable)Session["DATATABLE"];
            if (!String.IsNullOrEmpty(txtTextToSearch.Text))
            {
                // Using LINQ to select each row entry from student datatable, filter entries based on string passed by user and mapping the filtered result to new object as defined by student datatable columns
                // This is strongly typed datatable
                gvStudents.DataSource = from student in studentsDataTable
                                        where student.Name.ToUpper().Contains(txtTextToSearch.Text.ToUpper())
                                        select new
                                        {
                                            student.ID, 
                                            student.Name, 
                                            student.Gender, 
                                            student.TotalMarks
                                        };
            }
            else
            {
                Response.Write("<span style='font-weight:bold; color:red'>Please enter a text to search in the textbox.</span><br /><br />");

                // Using LINQ to select each row entry from student datatable, and mapping the filtered result to new object as defined by student datatable columns
                // This is strongly typed datatable
                gvStudents.DataSource = from student in studentsDataTable
                                        select new
                                        {
                                            student.ID,
                                            student.Name,
                                            student.Gender,
                                            student.TotalMarks
                                        };
            }
            gvStudents.DataBind();
        }
    }
}