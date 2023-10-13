using BankWebAPI.Models;
using System.Data.SQLite;

namespace BankWebAPI.Data
{
    public class UserDBManager
    {
        private static string connectionString = "Data Source=accountdatabase.db;Version=3;";

        public static bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // SQL command to create a table named "AccountTable"
                        command.CommandText = @"
                    CREATE TABLE UserTable (
                        Username TEXT,
                        Password TEXT,
                        Email TEXT,
                        PhoneNumber TEXT,
                        Address TEXT, 
                        Pfp TEXT,   
                        ID TEXT
                    )";

                        // Execute the SQL command to create the table
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                Console.WriteLine("Table created successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false; // Create table failed

        }

        public static bool Insert(User user)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO UserTable (Username, Password, Email, PhoneNumber, Address, Pfp, ID) VALUES (@Username, @Password, @Email, @PhoneNumber, @Address, @Pfp, @ID)";

                        command.Parameters.AddWithValue("@Username", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@Pfp", user.pfp);
                        command.Parameters.AddWithValue("@ID", user.acctNo);

                        int rowsInserted = command.ExecuteNonQuery();

                        connection.Close();
                        if (rowsInserted > 0)
                        {
                            return true; // Insertion was successful
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false; // Insertion failed
        }

        public static bool DeleteByUsername(string id)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to delete data by ID
                        command.CommandText = $"DELETE FROM UserTable WHERE Username = @Username";
                        command.Parameters.AddWithValue("@Username", id);

                        // Execute the SQL command to delete data
                        int rowsDeleted = command.ExecuteNonQuery();

                        // Check if any rows were deleted
                        connection.Close();
                        if (rowsDeleted > 0)
                        {
                            return true; // Deletion was successful
                        }
                    }
                    connection.Close();
                }

                return false; // No rows were deleted
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Deletion failed
            }
        }

        public static bool DeleteByEmail(string id)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to delete data by ID
                        command.CommandText = $"DELETE FROM UserTable WHERE Email = @Email";
                        command.Parameters.AddWithValue("@Email", id);

                        // Execute the SQL command to delete data
                        int rowsDeleted = command.ExecuteNonQuery();

                        // Check if any rows were deleted
                        connection.Close();
                        if (rowsDeleted > 0)
                        {
                            return true; // Deletion was successful
                        }
                    }
                    connection.Close();
                }

                return false; // No rows were deleted
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Deletion failed
            }
        }

        public static bool Update(String id, String password, String email, String phoneNumber, string address, string pfp)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to update data by ID
                        command.CommandText = $"UPDATE UserTable SET Password = @Password, Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, Pfp = @Pfp WHERE Username = @Username";
                        command.Parameters.AddWithValue("@Username", id);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Pfp", pfp);

                        // Execute the SQL command to update data
                        int rowsUpdated = command.ExecuteNonQuery();
                        connection.Close();
                        // Check if any rows were updated
                        if (rowsUpdated > 0)
                        {
                            return true; // Update was successful
                        }
                    }
                    connection.Close();
                }

                return false; // No rows were updated
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false; // Update failed
            }
        }

        public static User GetById(string id)
        {
            User user = null;

            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create a new SQLite command to execute SQL
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        // Build the SQL command to select a student by ID
                        command.CommandText = "SELECT * FROM UserTable WHERE Username = @Username";
                        command.Parameters.AddWithValue("@Username", id);

                        // Execute the SQL command and retrieve data
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User();
                                user.UserName = reader["Username"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.Email = reader["Email"].ToString();
                                user.PhoneNumber = reader["PhoneNumber"].ToString();
                                user.Address = reader["Address"].ToString();
                                user.pfp = reader["Pfp"].ToString();
                                user.acctNo = reader["ID"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return user;
        }

        public static void DBInitialize()
        {
            if (CreateTable())
            {
                User user = new User();
                user.UserName = "clement1";
                user.Password = "clement123";
                user.Email = "clement17@gmail.com";
                user.PhoneNumber = "0123456789";
                user.Address = "Lakeside 01";
                user.pfp = "C:\\Users\\User\\source\\repos\\BankWebAPI\\captainamerica.jpg";
                user.acctNo = "123456789";
                Insert(user);

                user = new User();
                user.UserName = "kungsoon2";
                user.Password = "kungsoon234";
                user.Email = "kungsoon22@gmail.com";
                user.PhoneNumber = "0123456788";
                user.Address = "Lakeside 02";
                user.pfp = "C:\\Users\\User\\source\\repos\\BankWebAPI\\thor.jpg";
                user.acctNo = "123456788";
                Insert(user);

                user = new User();
                user.UserName = "lusheng3";
                user.Password = "lusheng345";
                user.Email = "lusheng15@gmail.com";
                user.PhoneNumber = "0123456787";
                user.Address = "Lakeside 03";
                user.pfp = "C:\\Users\\User\\source\\repos\\BankWebAPI\\ironman.jpg";
                user.acctNo = "123456787";
                Insert(user);

            }
        }
    }
}
