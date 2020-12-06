using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    class Quiz
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string name)
        {
            Name = name;

            List<Answer> answers = new List<Answer>
            {
                new Answer{ Text = "One", isCorrect = true},
                new Answer{ Text = "Two", isCorrect = false},
                new Answer{ Text = "Three", isCorrect = false},
                new Answer{ Text = "Four", isCorrect = false}
            };
            // Load some initial movies
            Questions = new List<Question>
            {
                new Question { Text = "Sameple?", Answers = answers }
            };
        }

    }
}
