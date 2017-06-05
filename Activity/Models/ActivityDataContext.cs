using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class ActivityDataContext
    {
        
static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["SuperUniversityEntities"].ConnectionString;

        //讀取所有產品分類資料
        public static List<Activity> LoadActivites()
        {
            List<Activity> Activites = new List<Activity>();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                String strCmd = "select ActivityID,ActivityName,ClassID,Description,DateTime,Location,Host from Activites";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Activity _Activity = new Activity();
                        _Activity.ActivityID = Convert.ToInt32(dr["ActivityID"]);
                        _Activity.ActivityName = dr["ActivityName"].ToString();
                        _Activity.ClassID = Convert.ToInt32(dr["ClassID"]);
                        _Activity.Description = dr["Description"].ToString();
                        _Activity.Location = dr["Location"].ToString();
                        _Activity.Host = dr["Host"].ToString();
                        Activites.Add(_Activity);
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            return Activites;

        }
        //新增產品分類資料
        public static void InsertActivity(Activity Activity)
        {
            string strCmd = "insert Activites(ActivityName,Description,Location,Host)values(@ActivityName,@Description,@Location,@Host)";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityName", Activity.ActivityName);
                    cmd.Parameters.AddWithValue("@Description", Activity.Description);
                    cmd.Parameters.AddWithValue("@Location", Activity.Location);
                    cmd.Parameters.AddWithValue("@Host", Activity.Host);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        //根據分類編號讀取要修改的產品分類資料
        public static Activity LoadActivityByID(int ActivityID)
        {
            Activity _Activity = new Activity();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string strCmd = "select ActivityID,ActivityName,Description,ClassID,Location,Host from Activites where ActivityID=@ActivityID";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        _Activity.ActivityID = Convert.ToInt32(dr["ActivityID"]);
                        _Activity.ActivityName = dr["ActivityName"].ToString();
                        _Activity.Description = dr["Description"].ToString();
                        _Activity.ClassID = Convert.ToInt32(dr["ClassID"]);
                        _Activity.Location = dr["Location"].ToString();
                        _Activity.Host = dr["Host"].ToString();

                    }
                    dr.Close();
                    conn.Close();
                }
            }
            return _Activity;

        }
        //修改產品分類資料
        public static void EditActivity(Activity Activity)
        {
            string strCmd = "update Activites set ActivityName=@ActivityName, Description = @Description,Location = @Location,Host = @Host where ActivityID = @ActivityID";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", Activity.ActivityID);
                    cmd.Parameters.AddWithValue("@ActivityName", Activity.ActivityName);
                    cmd.Parameters.AddWithValue("@Description", Activity.Description);
                    cmd.Parameters.AddWithValue("@Location", Activity.Location);
                    cmd.Parameters.AddWithValue("@Host", Activity.Host);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

        }
        public static void DeleteActivity(int ActivityID)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                String strCmd = "Delete Activites where ActivityID=@ActivityID";
                using (SqlCommand cmd = new SqlCommand(strCmd, conn))
                {
                    cmd.Parameters.AddWithValue("@ActivityID", ActivityID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }






    }
}