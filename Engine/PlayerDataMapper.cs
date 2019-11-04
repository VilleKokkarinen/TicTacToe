using System;
using System.Data;
using System.Data.SqlClient;

namespace Engine
{
    public static class PlayerDataMapper
    {
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True";

        public static Player CreateFromDatabase()
        {
            try
            {
                // connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection, so we can perform SQL commands
                    connection.Open();

                    Player player;

                    // Create a SQL command object, that uses the connection to our database
                    // The SqlCommand object is where we create our SQL statement
                    using (SqlCommand savedGameCommand = connection.CreateCommand())
                    {
                        savedGameCommand.CommandType = CommandType.Text;
                        // This SQL statement reads the first rows in the SavedGame table.
                        // For this program, we should only ever have one row,
                        // but this will ensure we only get one record in our SQL query results.
                        savedGameCommand.CommandText = "SELECT TOP 1 * FROM SavedGame";

                        // Use ExecuteReader when you expect the query to return a row, or rows
                        SqlDataReader reader = savedGameCommand.ExecuteReader();

                        // Check if the query did not return a row/record of data
                        if (!reader.HasRows)
                        {
                            // There is no data in the SavedGame table, 
                            // so return null (no saved player data)
                            return null;
                        }

                        // Get the row/record from the data reader
                        reader.Read();

                        // Get the column values for the row/record
                        int experiencePoints = (int)reader["ExperiencePoints"];

                        // Create the Player object, with the saved game values
                        player = Player.CreatePlayerFromDatabase(experiencePoints);
                    }

                    // Now that the player has been built from the database, return it.
                    return player;
                }
            }
            catch (Exception)
            {
                // Ignore errors. If there is an error, this function will return a "null" player.
            }

            return null;
        }

        public static void SaveToDatabase(Player player)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection, so we can perform SQL commands
                    connection.Open();

                    // Insert/Update data in SavedGame table
                    using (SqlCommand existingRowCountCommand = connection.CreateCommand())
                    {
                        existingRowCountCommand.CommandType = CommandType.Text;
                        existingRowCountCommand.CommandText = "SELECT count(*) FROM SavedGame";

                        // Use ExecuteScalar when your query will return one value
                        int existingRowCount = (int)existingRowCountCommand.ExecuteScalar();

                        if (existingRowCount == 0)
                        {
                            // There is no existing row, so do an INSERT
                            using (SqlCommand insertSavedGame = connection.CreateCommand())
                            {
                                insertSavedGame.CommandType = CommandType.Text;
                                insertSavedGame.CommandText =
                                    "INSERT INTO SavedGame " +
                                    "(ExperiencePoints) " +
                                    "VALUES " +
                                    "(@ExperiencePoints)";

                                // Pass the values from the player object, to the SQL query, using parameters
                                insertSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@ExperiencePoints"].Value = player.ExperiencePoints;

                                // Perform the SQL command.
                                // Use ExecuteNonQuery, because this query does not return any results.
                                insertSavedGame.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // There is an existing row, so do an UPDATE
                            using (SqlCommand updateSavedGame = connection.CreateCommand())
                            {
                                updateSavedGame.CommandType = CommandType.Text;
                                updateSavedGame.CommandText =
                                    "UPDATE SavedGame " +
                                    "SET ExperiencePoints = @ExperiencePoints";

                                // Pass the values from the player object, to the SQL query, using parameters
                                // Using parameters helps make your program more secure.
                                // *to prevent SQL injections.
                                updateSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@ExperiencePoints"].Value = player.ExperiencePoints;

                                // Perform the SQL command.
                                // Use ExecuteNonQuery, because this query does not return any results.
                                updateSavedGame.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}