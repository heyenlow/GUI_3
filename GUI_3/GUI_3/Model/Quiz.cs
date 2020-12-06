using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.Model
{
    public class Quiz
    {
        public string Name { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(string name)
        {
            Name = name;
            Questions = new List<Question>();
        }

    }
}
