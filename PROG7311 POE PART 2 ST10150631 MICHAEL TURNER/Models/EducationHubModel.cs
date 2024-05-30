using Microsoft.Data.SqlClient;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class EducationHubModel
    {
        string connectionString = "Server=tcp:varsity2024server.database.windows.net,1433;Initial Catalog=PROG7311_POE_DB;Persist Security Info=False;User ID=st10150631;Password=Br1s1ngr1047;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        /// <summary>
        /// default constructor 
        /// </summary>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public EducationHubModel()
        {

        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Retrieves all courses from the database
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<CourseModel>> GetAllCourses()
        {
            List<CourseModel> courses = new List<CourseModel>();
            string query = "SELECT * FROM [dbo].[COURSES]";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            CourseModel course = new CourseModel();
                            course.CourseID = reader.GetInt32(0);
                            course.Name = reader["Name"].ToString();
                            course.Topic = reader["Topic"].ToString();
                            course.Content = reader["Content"].ToString();
                            course.Author = reader["AuthorName"].ToString();
                            if (!reader.IsDBNull(reader.GetOrdinal("CourseImage")))
                            {
                                course.CourseImage = (byte[])reader["CourseImage"];
                            }
                            else
                            {
                                // Handle the case when CourseImage is NULL
                                // For example, you might assign a default image or set it to null
                                course.CourseImage = null; // or assign a default image
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("CourseVideo")))
                            {
                                course.CourseVideo = (byte[])reader["CourseVideo"];
                            }
                            else
                            {
                                // Handle the case when CourseVideo is NULL
                                // For example, you might assign a default video or set it to null
                                course.CourseVideo = null; // or assign a default video
                            }
                            courses.Add(course);
                        }
                    

                }
            return courses;
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Adds a course to the database
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="courseTopic"></param>
        /// <param name="courseContent"></param>
        /// <param name="courseAuthor"></param>
        /// <param name="courseImage"></param>
        /// <param name="courseVideo"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task AddCourseDB(string courseName, string courseTopic, string courseContent, byte[] courseImage, byte[] courseVideo)
        {
            string courseAuthor = CoreModel.SignedInUser;
            string query = "INSERT INTO [dbo].[COURSES] (Name, Topic, Content, AuthorName, CourseImage, CourseVideo) VALUES (@CourseName, @CourseTopic, @CourseContent, @CourseAuthor, @CourseImage, @CourseVideo)";
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    command.Parameters.AddWithValue("@CourseTopic", courseTopic);
                    command.Parameters.AddWithValue("@CourseContent", courseContent);
                    command.Parameters.AddWithValue("@CourseAuthor", courseAuthor);
                    if(courseImage == null)
                    {
                        command.Parameters.AddWithValue("@CourseImage", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CourseImage", courseImage);
                    }
                    if (courseVideo == null)
                    {
                        command.Parameters.AddWithValue("@CourseVideo", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CourseVideo", courseVideo);
                    }
                    await command.ExecuteNonQueryAsync();
                }
            
        }
        //======================================================= End of Method ===================================================
    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>