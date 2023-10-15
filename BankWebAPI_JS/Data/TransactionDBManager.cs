﻿using BankWebAPI_JS.Models;
using System.Data.SQLite;

namespace BankWebAPI_JS.Data
{
    public class TransactionDBManager
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
                            CREATE TABLE TransactionTable (
                                TransactionId TEXT,
                                FromId TEXT,
                                ToId TEXT,
                                Balance REAL,
                                TransactionDate DATETIME,
                                Description TEXT
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

        public static bool Insert(Transaction transaction)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand countCommand = connection.CreateCommand())
                    {
                        countCommand.CommandText = "SELECT COUNT(*) FROM TransactionTable";
                        int transactionCount = Convert.ToInt32(countCommand.ExecuteScalar());
                        using (SQLiteCommand insertCommand = connection.CreateCommand())
                        {
                            insertCommand.CommandText = @"INSERT INTO TransactionTable (TransactionId, FromId, ToId, Balance) VALUES (@TransactionId, @FromId, @ToId, @Balance)";

                            insertCommand.Parameters.AddWithValue("@TransactionId", transactionCount + 1); // Use transactionCount + 1 as the new TransactionId
                            insertCommand.Parameters.AddWithValue("@FromId", transaction.fromId);
                            insertCommand.Parameters.AddWithValue("@ToId", transaction.toId);
                            insertCommand.Parameters.AddWithValue("@Balance", transaction.bal);
                            insertCommand.Parameters.AddWithValue("@TransactionDate", transaction.transactionDate);
                            insertCommand.Parameters.AddWithValue("@Description", transaction.description);

                            int rowsInserted = insertCommand.ExecuteNonQuery();

                            if (rowsInserted > 0)
                            {
                                connection.Close();
                                return true; // Insertion was successful
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

            return false; // Insertion failed
        }

        public static bool Update(Transaction transaction, Account acc, Account acc2)
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
                        UpdateTo(transaction, acc2);
                        // Build the SQL command to update data by ID
                        command.CommandText = $"UPDATE AccountTable SET Balance = @Balance WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", transaction.fromId);
                        double deductamount = acc.acctBal - transaction.bal;
                        command.Parameters.AddWithValue("@Balance", deductamount);

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

        public static bool UpdateTo(Transaction transaction, Account acc2)
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
                        command.CommandText = $"UPDATE AccountTable SET Balance = @Balance WHERE ID = @ID";
                        command.Parameters.AddWithValue("@ID", transaction.toId);
                        double depositamount = acc2.acctBal + transaction.bal;
                        command.Parameters.AddWithValue("@Balance", depositamount);

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

        public static Transaction GetById(string id)
        {
            Transaction transaction = null;

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
                        command.CommandText = "SELECT * FROM TransactionTable WHERE TransactionId = @TransactionId";
                        command.Parameters.AddWithValue("@TransactionId", id);

                        // Execute the SQL command and retrieve data
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transaction = new Transaction();
                                transaction.transId = reader["TransactionId"].ToString();
                                transaction.fromId = reader["FromId"].ToString();
                                transaction.toId = reader["ToId"].ToString();
                                transaction.bal = Convert.ToDouble(reader["Balance"]);
                                transaction.transactionDate = Convert.ToDateTime(reader["TransactionDate"]);
                                transaction.description = reader["Description"].ToString();
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

            return transaction;
        }
    }
}
