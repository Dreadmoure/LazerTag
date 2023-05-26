using LazerTag.Domain;
using LazerTag.Repository.Mapper;
using LazerTag.Repository.Provider;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerTag.Repository.Repositories
{
    /// <summary>
    /// Forfatter : Denni, Ida
    /// </summary>
    public class HighScoreRepository : Repository
    {
        private readonly HighScoreMapper mapper;

        /// <summary>
        /// Constructor for the HighScoreRepository
        /// </summary>
        /// <param name="provider">the databaseprovider</param>
        /// <param name="mapper">the specific mapper to the repositroy</param>
        public HighScoreRepository(IDatabaseProvider provider, HighScoreMapper mapper)
        {
            Provider = provider;
            this.mapper = mapper;
        }

        /// <summary>
        /// method for adding a score to the highscore database
        /// </summary>
        /// <param name="name">the name of the winner</param>
        /// <param name="score">the score of the winner</param>
        public void AddScore(string name, int score)
        {
            var cmd = new SQLiteCommand($"INSERT INTO HighScore (Name, Score) VALUES ('{name}','{(int)score}')", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for creating the database if it does not excist
        /// </summary>
        protected override void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS HighScore (Id INTEGER PRIMARY KEY, Name VARCHAR(50), Score INTEGER)", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for getting all the scores from the highscore table
        /// </summary>
        /// <returns>List of highscores</returns>
        public List<HighScore> GetAllScores()
        {
            var cmd = new SQLiteCommand("SELECT * from HighScore", (SQLiteConnection)Connection);
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
            var cmd = new SQLiteCommand($"UPDATE HighScore set Name = '{name}' WHERE Id = {id}", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        public void UpdateScore(int id, string name, int score)
        {
            var cmd = new SQLiteCommand($"UPDATE HighScore set Name = '{name}', Score = {score} WHERE Id = {id}", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// method for deleting a score based on its id
        /// Users wont use this, only developers will to maintain the database
        /// </summary>
        /// <param name="id">the position of the score you want to delete</param>
        public void DeleteScore(int id)
        {
            var cmd = new SQLiteCommand($"DELETE from HighScore WHERE Id= {id}", (SQLiteConnection)Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
