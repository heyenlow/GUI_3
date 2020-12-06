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
        public Question(string text, Answer answer1, Answer answer2, Answer answer3, Answer answer4, int whichIsCorrect)
        {
            this.Text = text;
            Answers = new List<Answer> {
                answer1,
                answer2,
                answer3,
                answer4
            };
            Answers[(int)whichIsCorrect].isCorrect = true;
        }

    }
}
