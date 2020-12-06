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
        public Question(String text)
        {
            this.Text = text;
            Answers = new List<Answer> {
                new Answer("A", false),
                new Answer("B", false),
                new Answer("C", true),
                new Answer("D", false)
            };
        }

    }
}
