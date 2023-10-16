using BankWebAPI_Admin.Models;
using System.Data.SQLite;
using System.Security.AccessControl;

namespace BankWebAPI_Admin.Data
{
    public class AccountDBManager
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
                    CREATE TABLE AccountTable (
                        ID TEXT,
                        Name TEXT,
                        Balance REAL
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

        public static bool Insert(Account account)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO AccountTable (ID, Name, Balance) VALUES (@ID, @Name, @Balance)";

                        command.Parameters.AddWithValue("@ID", account.acctNo);
                        command.Parameters.AddWithValue("@Name", account.acctName);
                        command.Parameters.AddWithValue("@Balance", account.acctBal);

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

        public static bool Delete(string id)
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
                        command.CommandText = $"DELETE FROM AccountTable WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", id);

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

        public static bool Update(Account account)
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
                        command.CommandText = $"UPDATE AccountTable SET Name = @Name, Balance = @Balance WHERE ID = @ID";
                        command.Parameters.AddWithValue("@Name", account.acctName);
                        command.Parameters.AddWithValue("@Balance", account.acctBal);
                        command.Parameters.AddWithValue("@ID", account.acctNo);

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

        public static Account GetById(string id)
        {
            Account account = null;

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
                        command.CommandText = "SELECT * FROM AccountTable WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", id);

                        // Execute the SQL command and retrieve data
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                account = new Account();
                                account.acctNo = reader["ID"].ToString();
                                account.acctName = reader["Name"].ToString();
                                account.acctBal = Convert.ToDouble(reader["Balance"]);

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

            return account;
        }

        public static void DBInitialize()
        {
            if (CreateTable())
            {
                Account account = new Account();
                account.acctNo = "123456789";
                account.acctName = "Sajib";
                account.acctBal = 20000;
                Insert(account);

                account = new Account();
                account.acctNo = "123456788";
                account.acctName = "Abu";
                account.acctBal = 10000;
                Insert(account);

                account = new Account();
                account.acctNo = "123456787";
                account.acctName = "Ali";
                account.acctBal = 915.70;
                Insert(account);
            }
        }
    }
}
