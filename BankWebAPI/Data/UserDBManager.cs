﻿using BankWebAPI.Models;
using System.Data.SQLite;

namespace BankWebAPI.Data
{
    public class UserDBManager
    {
        private static string connectionString = "Data Source=bankdatabase.db;Version=3;";

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
                        Username TEXT PRIMARY KEY,
                        Password TEXT,
                        Email TEXT,
                        PhoneNumber TEXT,
                        Address TEXT,
                        Pfp TEXT
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
                        command.CommandText = @"INSERT INTO UserTable (Username, Password, Email, PhoneNumber, Address, Pfp) VALUES (@Username, @Password, @Email, @PhoneNumber, @Address, @Pfp)";

                        command.Parameters.AddWithValue("@Username", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@Pfp", user.pfp);

                        int rowsInserted = command.ExecuteNonQuery();

                        connection.Close();
                        if (rowsInserted > 0)
                        {
                            Console.WriteLine("Successfully created account: \nUsername: " + user.UserName);
                            Console.WriteLine("Password: " + user.Password);
                            Console.WriteLine("Email: " + user.Email);
                            Console.WriteLine("PhoneNumber: " + user.PhoneNumber);
                            Console.WriteLine("Address: " + user.Address);
                            Console.WriteLine("Pfp: " + user.pfp);
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

        public static bool Update(String username, String password, String email, String phoneNumber, string address, string pfp)
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
                        command.Parameters.AddWithValue("@Username", username);
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
                user.pfp = "/image/img1.png";
                Insert(user);

                user = new User();
                user.UserName = "kungsoon2";
                user.Password = "kungsoon234";
                user.Email = "kungsoon22@gmail.com";
                user.PhoneNumber = "0123456788";
                user.Address = "Lakeside 02";
                user.pfp = "/image/img2.jpg";
                Insert(user);

                user = new User();
                user.UserName = "lusheng3";
                user.Password = "lusheng345";
                user.Email = "lusheng15@gmail.com";
                user.PhoneNumber = "0123456787";
                user.Address = "Lakeside 03";
                user.pfp = "/image/img3.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user4";
                user.Password = "user444";
                user.Email = "user4@gmail.com";
                user.PhoneNumber = "0123456444";
                user.Address = "Lakeside 04";
                user.pfp = "/image/img1.png";
                Insert(user);

                user = new User();
                user.UserName = "user5";
                user.Password = "user555";
                user.Email = "user5@gmail.com";
                user.PhoneNumber = "0123456555";
                user.Address = "Lakeside 05";
                user.pfp = "/image/img2.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user6";
                user.Password = "user666";
                user.Email = "user6@gmail.com";
                user.PhoneNumber = "0123456666";
                user.Address = "Lakeside 06";
                user.pfp = "/image/img3.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user7";
                user.Password = "user777";
                user.Email = "user7@gmail.com";
                user.PhoneNumber = "0123456777";
                user.Address = "Lakeside 07";
                user.pfp = "/image/img1.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user8";
                user.Password = "user888";
                user.Email = "user8@gmail.com";
                user.PhoneNumber = "0123456888";
                user.Address = "Lakeside 08";
                user.pfp = "/image/img2.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user9";
                user.Password = "user999";
                user.Email = "user9@gmail.com";
                user.PhoneNumber = "0123456999";
                user.Address = "Lakeside 09";
                user.pfp = "/image/img3.jpg";
                Insert(user);

                user = new User();
                user.UserName = "user0";
                user.Password = "user000";
                user.Email = "user0@gmail.com";
                user.PhoneNumber = "0123456000";
                user.Address = "Lakeside 00";
                user.pfp = "/image/img1.jpg";
                Insert(user);
            }
        }
    }
}
