using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace ADONetCRUD.Models
{
    public class SqlDbHelper
    {

        private string connectionString;
        private string BkpconnectionString;
        private SqlConnection con;
        private SqlConnection BkpCon;

        public SqlDbHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DbB15"].ConnectionString;
            BkpconnectionString = ConfigurationManager.ConnectionStrings["DbB15Bkp"].ConnectionString;
            con = new SqlConnection(connectionString);
            BkpCon = new SqlConnection(BkpconnectionString);
        }

        //commented code added 
        //first line
        // second line

        public List<Student> GetStudents()
        {
            #region SqlDataReader class
            //try
            //{
            //    List<Student> students = new List<Student>();

            //    SqlCommand cmd = new SqlCommand("Select * from Student", con);
            //    con.Open();
            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Student s = new Student();

            //        s.RollNumber = (int)reader["RollNumber"];
            //        s.Name = reader["Name"].ToString();
            //        s.Age = (int)reader["Age"];
            //        s.Email = reader["Email"].ToString();
            //        s.DateOfBirth = (DateTime)reader["DateOfBirth"];

            //        students.Add(s);
            //    }
            //    return students;
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
            //finally
            //{
            //    if (con != null)
            //        con.Dispose();
            //}
            #endregion SqlDataReader class

            #region SqlDataAdapter Class
            try
            {
                List<Student> students = new List<Student>();

                SqlDataAdapter adapter = new SqlDataAdapter("Select * from Student", con);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        Student s = new Student();
                        s.RollNumber = (int)row["RollNumber"];
                        s.Name = row["Name"].ToString();
                        s.Age = (int)row["Age"];
                        s.Email = row["Email"].ToString();
                        s.DateOfBirth = (DateTime)row["DateOfBirth"];

                        students.Add(s);
                    }
                }
                else
                {

                }
                return students;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }

            #endregion SqlDataAdapter Class
        }


        public Student GetStudentById(int rollNumber)
        {
            Student s = null;
            try
            {
                List<Student> students = new List<Student>();

                // string cmdText = "Select * from Student where RollNumber = @RollNumber";
                string cmdText = "usp_GetStudentById";
                SqlCommand cmd = new SqlCommand(cmdText, con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        s = new Student();

                        s.Name = reader["Name"].ToString();
                        s.Age = (int)reader["Age"];
                        s.Email = reader["Email"].ToString();
                        s.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    }

                }
                return s;


            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }


        }


        public bool InsertStudent(Student student, out int StudentRollNumber)
        {
            try
            {
                List<Student> students = new List<Student>();

                SqlCommand cmd = new SqlCommand("usp_InsertStudent", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);

                SqlParameter rollNumber = new SqlParameter();
                rollNumber.ParameterName = "@RollNumber";
                rollNumber.SqlDbType = SqlDbType.Int;
                rollNumber.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(rollNumber);

                con.Open();
                cmd.ExecuteNonQuery();

                StudentRollNumber = (int)rollNumber.Value;

                return true;
            }
            catch (Exception e)
            {
                StudentRollNumber = 0;
                return false;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
        }


        public bool UpdateStudent(Student student)
        {
            try
            {
                List<Student> students = new List<Student>();

                SqlCommand cmd = new SqlCommand("usp_UpdateStudent", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
        }


        public bool DeleteStudnet(int RollNumber)
        {
            try
            {
                List<Student> students = new List<Student>();

                SqlCommand cmd = new SqlCommand("usp_DeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RollNumber", RollNumber);
                con.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
        }

        public bool BackupStudent()
        {
            try
            {
                string readStudentCommand = "select * from student where AddedDate = @AddedDate and " + 
                    "IsBackedUp = 0";
                SqlDataAdapter adapter = new SqlDataAdapter(readStudentCommand, con);
                adapter.SelectCommand.Parameters.AddWithValue("@AddedDate", DateTime.Now.ToString("yyyy/MM/dd"));
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BkpCon.Open();
                SqlBulkCopy copy = new SqlBulkCopy(BkpCon);
                copy.DestinationTableName = "dbo.Student";
                copy.WriteToServer(dt);

                if(dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        int rollNumber = (int)row["RollNumber"];
                        string cmdText = "update student set IsBackedUp = 1 where RollNumber =" + rollNumber;
                        SqlCommand cmd1 = new SqlCommand(cmdText, con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
                if (BkpCon != null)
                    BkpCon.Dispose();
            }
        }

    }
}