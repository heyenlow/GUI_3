using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    public class Answer
    {
        public string Text { get; set; }
        public bool isCorrect { get; set; }
        public Answer(string Text, bool isCorrect = false)
        {
            this.Text = Text;
            this.isCorrect = isCorrect;
        }
    }
}
