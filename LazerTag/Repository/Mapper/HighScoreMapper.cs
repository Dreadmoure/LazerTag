using LazerTag.Domain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.Repository.Mapper
{
    public class HighScoreMapper
    {
        /// <summary>
        /// Read the database table for Inventory
        /// </summary>
        /// <param name="reader">the reader, we read from</param>
        /// <returns>List of HighScores</returns>
        public List<HighScore> MapHighScoreFromReader(SQLiteDataReader reader)
        {
            //make a list for the result
            var result = new List<HighScore>();

            //read all data from the database table for Highscores
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var name = reader.GetString(1);
                var score = reader.GetInt32(2);

                //add the new HighScore, with its data, to the result list 
                result.Add(new HighScore() { ID = id, Name = name, Score = score });
            }

            //return list of all the HighScores in the game 
            return result;
        }
    }
}
