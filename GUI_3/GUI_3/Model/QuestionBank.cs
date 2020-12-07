using GUI_3.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    public class QuestionBank
    {
        public List<Question> Questions { get; set; }
        public QuestionBank()
        {
            Questions = SqliteDB.GetAllQuestions();
        }
    }
}
