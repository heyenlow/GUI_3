using GUI_3.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GUI_3.DataAccess
{
    class SqliteDB
    {
        public const string DB_FILENAME = "questions.db";

        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(DB_FILENAME, CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                var sql = "CREATE TABLE IF NOT EXISTS question" +
                    "(questionid INTEGER PRIMARY KEY, " +
                    "prompt NVARCHAR(512), " +
                    "answer1 NVARCHAR(512), " +
                    "answer2 NVARCHAR(512), " +
                    "answer3 NVARCHAR(512), " +
                    "answer4 NVARCHAR(512), " +
                    "correct_answer INTEGER)";

                var cmd = new SqliteCommand(sql, conn);
                cmd.ExecuteReader();
            }
        }

        public static long AddQuestion(Question question)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;

                // NULL tells Sqlite to use autoincrement value. Parameterized query prevents SQL injection attacks
                cmd.CommandText = "INSERT INTO question (prompt, answer1, answer2, answer3, answer4, correct_answer) VALUES (@Prompt, @Answer1, @Answer2, @Answer3, @Answer4, @CorrectAnswer); SELECT last_insert_rowid()";
                cmd.Parameters.AddWithValue("@Prompt", question.Text);
                cmd.Parameters.AddWithValue("@Answer1", question.Answers[0].Text);
                cmd.Parameters.AddWithValue("@Answer2", question.Answers[1].Text);
                cmd.Parameters.AddWithValue("@Answer3", question.Answers[2].Text);
                cmd.Parameters.AddWithValue("@Answer4", question.Answers[3].Text);

                int correctAnswer = 0;

                for (int i = 0; i < 4; ++i)
                {
                    if (question.Answers[i].isCorrect)
                    {
                        correctAnswer = i;
                        break;
                    }
                }

                cmd.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);


                // Get ID that was automatically assigned
                var newId = (long)cmd.ExecuteScalar();

                conn.Close();

                return newId;
            }
        }

        public static List<Question> GetAllQuestions()
        {
            var questions = new List<Question>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand("SELECT questionid, prompt, answer1, answer2, answer3, answer4, correct_answer FROM question", conn);
                SqliteDataReader query = cmd.ExecuteReader();
                while (query.Read())
                {
                    Question question = new Question(
                        query.GetString(1),
                        new Answer(query.GetString(2)),
                        new Answer(query.GetString(3)),
                        new Answer(query.GetString(4)),
                        new Answer(query.GetString(5)),
                        query.GetInt32(6));

                    question.Id = query.GetInt32(0);

                    questions.Add(question);
                }
                conn.Close();
            }

            return questions;
        }

        public static void UpdateQuestion(Question question)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;

                cmd.CommandText = "UPDATE question SET prompt = @Prompt, answer1 = @Answer1, answer2 = @Answer2, answer3 = @Answer3, answer4 = @Answer4, correct_answer = @CorrectAnswer WHERE questionid = @Id";

                cmd.Parameters.AddWithValue("@Prompt", question.Text);
                cmd.Parameters.AddWithValue("@Answer1", question.Answers[0].Text);
                cmd.Parameters.AddWithValue("@Answer2", question.Answers[1].Text);
                cmd.Parameters.AddWithValue("@Answer3", question.Answers[2].Text);
                cmd.Parameters.AddWithValue("@Answer4", question.Answers[3].Text);
                cmd.Parameters.AddWithValue("@Id", question.Id);

                int correctAnswer = 0;

                for (int i = 0; i < 4; ++i)
                {
                    if (question.Answers[i].isCorrect)
                    {
                        correctAnswer = i;
                        break;
                    }
                }

                cmd.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);

                cmd.ExecuteReader();

                conn.Close();
            }
        }

        public static void DeleteQuestion(long questionId)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_FILENAME);
            using (SqliteConnection conn = new SqliteConnection($"Filename={dbpath}"))
            {
                conn.Open();

                SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM question WHERE questionId = @Id";
                cmd.Parameters.AddWithValue("@Id", questionId);
                cmd.ExecuteReader();

                conn.Close();
            }
        }

    }
}
