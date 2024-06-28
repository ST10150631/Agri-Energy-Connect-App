using Microsoft.Data.SqlClient;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Properties;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class HomeModel
    {
        private string connectionString = Properties.Resources.ConnectString;
        public HomeModel()
        {
        }
        /// <summary>
        ///  Returns a list of all posts asynchronously with the latest post first
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<PostModel>> GetPosts()
        {
            List<PostModel> posts = new List<PostModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
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
                                post.Topic = reader["PostTopic"].ToString();
                                post.PostDate = reader.GetDateTime(reader.GetOrdinal("DatePosted"));
                                post.Creator = reader["Creator"].ToString();

                                // Check if the "PostImage" column contains data
                                if (!reader.IsDBNull(reader.GetOrdinal("PostImage")))
                                {
                                    post.PostImage = (byte[])reader["PostImage"];
                                }
                                else
                                {
                                    Console.WriteLine("No Image for post ID: " + post.ID);
                                }

                                posts.Add(post);
                            }
                        }
                        catch (Exception e)
                        {
                            // Handle exceptions
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                }
            }
            // Order posts by PostDate before returning
            posts = posts.OrderByDescending(p => p.PostDate).ToList();
            return posts;
        }

        //======================================================= End of Method ===================================================

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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[POSTS] WHERE PostTopic = @Topic ORDER BY DatePosted DESC", conn))
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
            posts = posts.OrderByDescending(p => p.PostDate).ToList();
            return posts;
        }

        //======================================================= End of Method ===================================================


    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>