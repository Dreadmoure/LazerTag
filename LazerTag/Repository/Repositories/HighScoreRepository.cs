using LazerTag.Domain;
using LazerTag.Repository.Mapper;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.Repository.Repositories
{
    public class HighScoreRepository : Repository
    {
        private readonly HighScoreMapper mapper;


        /// <summary>
        /// method for adding a score to the highscore database
        /// </summary>
        /// <param name="score">the score of the winner</param>
        /// <param name="name">the name of the winner</param>
        public void AddScore(int score, string name)
        {
            var cmd = new SQLiteCommand($"INSERT INTO HighScores (Name, Score) VALUES ('{name}','{(int)score}')", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for creating the database if it does not excist
        /// </summary>
        protected override void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS HighScores (Id INTEGER PRIMARY KEY, Name VARCHAR(50), Score INTEGER)", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for getting all the scores from the highscore table
        /// </summary>
        /// <returns>List of highscores</returns>
        public List<HighScore> GetAllScores()
        {
            var cmd = new SQLiteCommand("SELECT * from HighScores", (SQLiteConnection)Connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapHighScoreFromReader(reader);

            return result;
        }

        /// <summary>
        /// Method for updating a name of a score in the highscore database
        /// Users wont use this, only developers will to maintain the database
        /// </summary>
        /// <param name="id">the id where the score is</param>
        /// <param name="name">the name we want to change it to</param>
        public void UpdateScore(int id, string name)
        {
            var cmd = new SQLiteCommand($"UPDATE HighScores set Name = {name} WHERE Id = {id}", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for deleting a score based on its id
        /// Users wont use this, only developers will to maintain the database
        /// </summary>
        /// <param name="id">the position of the score you want to delete</param>
        public void DeleteScore(int id)
        {
            var cmd = new SQLiteCommand($"DELETE from HighScores WHERE Id= {id}", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
