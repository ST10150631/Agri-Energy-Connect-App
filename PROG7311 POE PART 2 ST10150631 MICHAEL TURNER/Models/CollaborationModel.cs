using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class CollaborationModel
    {
        /// <summary>
        /// Connection string to the database
        /// </summary>
        string connectionString = "Server=tcp:varsity2024server.database.windows.net,1433;Initial Catalog=PROG7311_POE_DB;Persist Security Info=False;User ID=st10150631;Password=Br1s1ngr1047;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public CollaborationModel()
        {
            
        }

        /// <summary>
        /// Retrieves all messages from the database
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<List<MessageModel>> GetMessages()
        {
            List<MessageModel> messages = new List<MessageModel>();
            string query = "SELECT * FROM [dbo].[MESSAGE]";
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        MessageModel message = new MessageModel();
                        message.Id = reader.GetInt32(0);
                        message.Message = reader["MessageContent"].ToString();
                        message.Time = reader.GetDateTime(2);
                        message.Sender = reader["SENDER"].ToString();
                        messages.Add(message);
                }
                }
            messages = messages.OrderByDescending(p => p.Time).ToList();
            return messages;
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Adds a message to the database
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task AddMessage(MessageModel message)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].MESSAGE (MessageContent, MessageDate, SENDER) VALUES (@Message, @Time, @Sender)", connection))
                {
                    command.Parameters.AddWithValue("@Message", message.Message);
                    command.Parameters.AddWithValue("@Time", message.Time);
                    command.Parameters.AddWithValue("@Sender", message.Sender);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        //======================================================= End of Method ===================================================


    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>