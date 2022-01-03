using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADONetCRUD.Models;

namespace ADONetCRUD
{
    public partial class StudentData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_Students();

        }


        private void Load_Students()
        {
            SqlDbHelper db = new SqlDbHelper();
            List<Student> students = db.GetStudents();


            gvStudents.DataSource = students;
            gvStudents.DataBind();
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Student student = new Student()
            {
                Name = txtName.Text,
                Age = Convert.ToInt32(txtAge.Text),
                Email = txtEmail.Text,
                DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text)
            };

            SqlDbHelper db = new SqlDbHelper();
            // bool status = db.InsertStudent(student);

            int rollNumber;

            if (db.InsertStudent(student, out rollNumber))
            {
                lblMessage.Text = "Student created successfully. Roll Number is " + rollNumber;
                Load_Students();
                clearFields();
            }
            else
            {
                lblMessage.Text = "Student creation failed";
            }
        }


        private void clearFields()
        {
            txtName.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDateOfBirth.Text = string.Empty;
            txtRollNumber.Text = string.Empty;
            //lblMessage.Text = string.Empty;
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
            lblMessage.Text = string.Empty;
        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int rollNumber = Convert.ToInt32(txtRollNumber.Text);

            SqlDbHelper db = new SqlDbHelper();

           Student student =  db.GetStudentById(rollNumber);

            if(student != null)
            {
                txtName.Text = student.Name;
                txtAge.Text = student.Age.ToString();
                txtEmail.Text = student.Email;
                txtDateOfBirth.Text = student.DateOfBirth.ToString("yyyy-MM-dd");

                lblMessage.Text = "Student loaded Successfully";
            }
            else
            {
                lblMessage.Text = "Problem in loading student";
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Student student = new Student()
            {
                RollNumber = Convert.ToInt32(txtRollNumber.Text),
                Name = txtName.Text,
                Age = Convert.ToInt32(txtAge.Text),
                Email = txtEmail.Text,
                DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text)
            };

            SqlDbHelper db = new SqlDbHelper();
            // bool status = db.InsertStudent(student);                   

            if (db.UpdateStudent(student))
            {
                lblMessage.Text = "Student Updated successfully.";
                Load_Students();
                clearFields();
            }
            else
            {
                lblMessage.Text = "Student update failed";
            }


        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlDbHelper db = new SqlDbHelper();

            int rollNumber = Convert.ToInt32(txtRollNumber.Text);

            if (db.DeleteStudnet(rollNumber))
            {
                lblMessage.Text = "Student Deleted Successfully";
                Load_Students();
            }
            else
            {
                lblMessage.Text = "Student Deletion Failed";
            }
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            SqlDbHelper db = new SqlDbHelper();

            if (db.BackupStudent())
            {
                lblMessage.Text = "Student Backup Successful at " + DateTime.Now.ToString(); ;
            }
            else
            {
                lblMessage.Text = "Student Backup Failed at " + DateTime.Now.ToString();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "Student-" + DateTime.Now + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                gvStudents.GridLines = GridLines.Both;
                gvStudents.HeaderStyle.Font.Bold = true;
                gvStudents.RenderControl(htmltextwrtter);
                Response.Write(strwritter.ToString());
                Response.End();

            
        }
    }
}