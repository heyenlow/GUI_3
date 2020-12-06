using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    class Question
    {
        public enum ans {A,B,C,D };
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

    }
}
