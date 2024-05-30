using Microsoft.Data.SqlClient;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class HomeModel
    {
        private readonly string connectionString = "Server=tcp:varsity2024server.database.windows.net,1433;Initial Catalog=PROG7311_POE_DB;Persist Security Info=False;User ID=st10150631;Password=Br1s1ngr1047;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public HomeModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<PostModel>> GetPosts()
        {

            List<PostModel> posts = new List<PostModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[POSTS]", conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        try
                        {
                            while (await reader.ReadAsync())
                            {
                                PostModel post = new PostModel();
                                post.ID = reader.GetInt32(0);
                                post.Content = reader["PostText"].ToString();
                                post.PostImage = (byte[])reader["PostImage"];
                                post.Topic = reader["PostTopic"].ToString();
                                post.PostDate = (DateTime)reader["DatePosted"];
                                post.Creator = reader["Creator"].ToString();
                                posts.Add(post);
                            }
                        }
                        catch (Exception e)
                        {
                            while (await reader.ReadAsync())
                            {
                                PostModel post = new PostModel();
                                post.ID = reader.GetInt32(0);
                                post.Content = reader["PostText"].ToString();
                                post.Topic = reader["PostTopic"].ToString();
                                post.PostDate = (DateTime)reader["DatePosted"];
                                post.Creator = reader["Creator"].ToString();
                                posts.Add(post);
                            }
                            Console.WriteLine("No Image for this post");
                        }

                    }
                }
            }
            return posts;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="topic"></param>
        /// <param name="postDate"></param>
        /// <param name="creator"></param>
        /// <param name="postImage"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public void AddPost(string content, string topic, byte[] PostImage)
        {
            var postDate = DateTime.Now;
            var creator = CoreModel.SignedInUser;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[POSTS](DatePosted,PostTopic, Creator,PostImage,PostText) VALUES(@PostDate, @Topic, @Creator,@PostImage, @Content)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Topic", topic);
                        cmd.Parameters.AddWithValue("@PostDate", postDate);
                        cmd.Parameters.AddWithValue("@Creator", creator);
                        cmd.Parameters.AddWithValue("@PostImage", PostImage);
                        cmd.Parameters.AddWithValue("@Content", content);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[POSTS](DatePosted,PostTopic, Creator,PostText) VALUES(@PostDate, @Topic, @Creator, @Content)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Content", content);
                        cmd.Parameters.AddWithValue("@Topic", topic);
                        cmd.Parameters.AddWithValue("@PostDate", postDate);
                        cmd.Parameters.AddWithValue("@Creator", creator);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }

        //======================================================= End of Method ===================================================

        /// <summary>
        /// Gets all posts with a specific topic and returns them as a List of PostModel
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<PostModel>> GetFilteredPosts(string topic)
        {
            List<PostModel> posts = new List<PostModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[POSTS] WHERE PostTopic = @Topic", conn))
                {
                    cmd.Parameters.AddWithValue("@Topic", topic);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PostModel post = new PostModel();
                            post.ID = reader.GetInt32(0);
                            post.Content = reader["PostText"].ToString();
                            post.PostImage = (byte[])reader["PostImage"];
                            post.Topic = reader["PostTopic"].ToString();
                            post.PostDate = (DateTime)reader["DatePosted"];
                            post.Creator = reader["Creator"].ToString();
                            posts.Add(post);
                        }
                    }
                }
            }
            return posts;
        }

        //======================================================= End of Method ===================================================


    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>