using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    public class Question
    {
        
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public Question(string text, string answer1, string answer2, string answer3, string answer4, int whichIsCorrect)
        {
            this.Text = text;
            Answers = new List<Answer> {
                new Answer(answer1),
                new Answer(answer2),
                new Answer(answer3),
                new Answer(answer4)
            };
            Answers[(int)whichIsCorrect].isCorrect = true;
        }

    }
}
