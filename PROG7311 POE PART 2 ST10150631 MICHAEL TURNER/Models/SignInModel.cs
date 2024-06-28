using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models
{
    public class SignInModel
    {
        UserModel user = new UserModel();
        PasswordHashing Hashing = new PasswordHashing();
        string ConnectionString = Properties.Resources.ConnectString;
        /// <summary>
        /// Default constructor
        /// </summary>
        public SignInModel()
        {
            
        }
        /// <summary>
        /// Verifies that the username and password match
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedPasswordHash"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        private static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Convert the stored password hash back to bytes
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);

            // Extract the salt from the hash bytes
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute a new hash of the entered password using the stored salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000))
            {
                byte[] testHash = pbkdf2.GetBytes(32); // 32 bytes is a common choice for the hash size

                // Compare the computed hash with the stored hash
                for (int i = 0; i < 32; i++)
                {
                    if (testHash[i] != hashBytes[i + 16])
                    {
                        return false; // Passwords don't match
                    }
                }

                return true; // Passwords match
            }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Retrieves user details from the database as a UserModel
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<UserModel> GetUserDetails(string username)
        {
            UserModel user = null;

            string query = "SELECT * FROM Users WHERE Username = @Username";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();

                    // Use ExecuteScalarAsync to efficiently retrieve a single row
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Map the data from the database to a User object
                            user = new UserModel
                            {
                                Username = reader["Username"].ToString(),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Role = (int)reader["Role_ID"],
                                PasswordHash = reader["PasswordHash"].ToString()
                                // Map other properties as needed
                            };
                        }
                    }
                }
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to get user details");
                return null;
            }


            
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Adds a New Employee to the database 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<bool> AddEmployee(string username, string password, string name, string email)
        {
            
            
                var PasswordHash = Hashing.HashPassword(password);
                string query = "INSERT INTO [dbo].[Users] (Username, Name, Email, PasswordHash, Role_ID) VALUES (@Username, @Name, @Email, @PasswordHash, 1)";
            try
            {


                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                    command.Parameters.AddWithValue("@Role_ID", 1);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                CoreModel.SignedInUser = username;
                CoreModel.UserRole = 1;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to add user");
                return false;
            }


        }

        //======================================================= End of Method ===================================================

        /// <summary>
        /// Adds a new Farmer to the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<bool> AddFarmer(string username, string password, string name, string email)
        {
            
                var PasswordHash = Hashing.HashPassword(password);
                string query = "INSERT INTO [dbo].[Users] (Username, Name, Email, PasswordHash, Role_ID) VALUES (@Username, @Name, @Email, @PasswordHash, 2)";
            try 
            { 
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PasswordHash", PasswordHash);
                        command.Parameters.AddWithValue("@Role_ID", 2);

                    connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failure to add user");
                    return false;
                }
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// Checks if the username and password are correct 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public UserModel Login(string username, string password)
        {
            try
            {
                var user = GetUserDetails(username).Result;
                if (user.Username == username && VerifyPassword(password, user.PasswordHash))
                {
                    CoreModel.SignedInUser = username;
                    CoreModel.UserRole = user.Role;
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure to login");
                return null;
            }

        }
        //======================================================= End of Method ===================================================


    }
}
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> END OF FILE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>