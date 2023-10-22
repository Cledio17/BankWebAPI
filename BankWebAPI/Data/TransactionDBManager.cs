using BankWebAPI.Models;
using System.Data.SQLite;

namespace BankWebAPI.Data
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
                        ID TEXT,
                        Operation INT,
                        Amount REAL,
                        FOREIGN KEY (ID) REFERENCES AccountTable (ID)
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

        //public static bool Insert(Transaction transaction)
        //{
        //    try
        //    {
        //        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        //        {
        //            connection.Open();

        //            using (SQLiteCommand command = connection.CreateCommand())
        //            {

        //                command.CommandText = @"INSERT INTO TransactionTable (TransactionId, FromId, ToId, Balance) VALUES (@TransactionId, @FromId, @ToId, @Balance)";

        //                command.Parameters.AddWithValue("@TransactionId", transaction.transId);
        //                command.Parameters.AddWithValue("@FromId", transaction.fromId);
        //                command.Parameters.AddWithValue("@ToId", transaction.toId);
        //                command.Parameters.AddWithValue("@Balance", transaction.bal);

        //                int rowsInserted = command.ExecuteNonQuery();

        //                connection.Close();
        //                if (rowsInserted > 0)
        //                {
        //                    return true; // Insertion was successful
        //                }
        //            }
        //            connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //    }

        //    return false; // Insertion failed
        //}
        public static bool Insert(Transaction transaction)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand countCommand = connection.CreateCommand())
                    {
                        using (SQLiteCommand insertCommand = connection.CreateCommand())
                        {
                            insertCommand.CommandText = @"INSERT INTO TransactionTable (ID, Operation, Amount) VALUES (@ID, @Operation, @Amount)";

                            insertCommand.Parameters.AddWithValue("@ID", transaction.acctNo);
                            insertCommand.Parameters.AddWithValue("@Operation", transaction.operation);
                            insertCommand.Parameters.AddWithValue("@Amount", transaction.amount);

                            int rowsInserted = insertCommand.ExecuteNonQuery();

                            if (rowsInserted > 0)
                            {
                                connection.Close();
                                if (transaction.operation == 1) //Deposit
                                {
                                    Console.Write("Deposit of $" + transaction.amount + " into " + transaction.acctNo + " is Successful\n");
                                }
                                else if (transaction.operation == 2) //Withdrawal
                                {
                                    Console.Write("Withdrawal of $" + transaction.amount + " from " + transaction.acctNo + " is Successful\n");
                                }
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


        public static bool Update(Transaction transaction, Account acc)
        {
            try
            {
                // Create a new SQLite connection
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    using (SQLiteCommand histCommand = connection.CreateCommand())
                    {
                        histCommand.CommandText = $"SELECT TransHist FROM AccountTable WHERE ID = @ID";
                        histCommand.Parameters.AddWithValue("@ID", transaction.acctNo);
                        string newHist = Convert.ToString(histCommand.ExecuteScalar());
                        // Create a new SQLite command to execute SQL
                        using (SQLiteCommand countCommand = connection.CreateCommand())
                        {
                            countCommand.CommandText = $"SELECT Balance FROM AccountTable WHERE ID = @ID";
                            countCommand.Parameters.AddWithValue("@ID", transaction.acctNo);
                            double newBalance = Convert.ToDouble(countCommand.ExecuteScalar());
                            using (SQLiteCommand command = connection.CreateCommand())
                            {
                                if (transaction.operation == 1)//Deposit
                                {
                                    // Build the SQL command to update data by ID
                                    newBalance = newBalance + transaction.amount;
                                    command.CommandText = $"UPDATE AccountTable SET Balance = @Balance WHERE ID = @ID";
                                    command.Parameters.AddWithValue("@ID", transaction.acctNo);
                                    command.Parameters.AddWithValue("@Balance", newBalance);
                                    acc.acctBal = newBalance;
                                    command.ExecuteNonQuery();
                                    newHist = newHist + "Deposit $" + transaction.amount;
                                    command.CommandText = $"UPDATE AccountTable SET TransHist = @TransHist WHERE ID = @ID";
                                    command.Parameters.AddWithValue("@ID", transaction.acctNo);
                                    command.Parameters.AddWithValue("@TransHist", newHist);
                                    acc.transHist = newHist;
                                    // Execute the SQL command to update data
                                    int rowsUpdated = command.ExecuteNonQuery();
                                    connection.Close();
                                    // Check if any rows were updated
                                    if (rowsUpdated > 0)
                                    {
                                        return true; // Update was successful
                                    }
                                }
                                else if (transaction.operation == 2) //Withdrawal
                                {
                                    // Build the SQL command to update data by ID
                                    newBalance = newBalance - transaction.amount;
                                    command.CommandText = $"UPDATE AccountTable SET Balance = @Balance WHERE ID = @ID";
                                    command.Parameters.AddWithValue("@ID", transaction.acctNo);
                                    command.Parameters.AddWithValue("@Balance", newBalance);
                                    acc.acctBal = newBalance;
                                    command.ExecuteNonQuery();
                                    newHist = newHist + "Withdraw $" + transaction.amount;
                                    command.CommandText = $"UPDATE AccountTable SET TransHist = @TransHist WHERE ID = @ID";
                                    command.Parameters.AddWithValue("@ID", transaction.acctNo);
                                    command.Parameters.AddWithValue("@TransHist", newHist);
                                    acc.transHist = newHist;
                                    // Execute the SQL command to update data
                                    int rowsUpdated = command.ExecuteNonQuery();
                                    connection.Close();
                                    // Check if any rows were updated
                                    if (rowsUpdated > 0)
                                    {
                                        return true; // Update was successful
                                    }
                                }
                            }

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

        /*public static Transaction GetById(string id)
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
        }*/

        public static void DBInitialize()
        {
            if (CreateTable())
            {
                Transaction transaction = new Transaction();
                transaction.acctNo = "123456789";
                transaction.operation = 1;
                transaction.amount = 200;
                Account account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456788";
                transaction.operation = 2;
                transaction.amount = 100;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456787";
                transaction.operation = 1;
                transaction.amount = 300;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456444";
                transaction.operation = 2;
                transaction.amount = 444;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456555";
                transaction.operation = 1;
                transaction.amount = 555;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456666";
                transaction.operation = 2;
                transaction.amount = 666;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }

                transaction = new Transaction();
                transaction.acctNo = "123456777";
                transaction.operation = 1;
                transaction.amount = 777;
                account = AccountDBManager.GetById(transaction.acctNo);
                if (account != null)
                {
                    if (Insert(transaction))
                    {
                        Update(transaction, account);
                    }
                }
            }
        }
    }
}
