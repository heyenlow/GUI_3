using GUI_3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.ViewModel
{
    public class AnswerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Answer answer;

        public AnswerViewModel(Answer answer)
        {
            this.answer = answer;
        }

        public String Text
        {
            get { return answer.Text; }
            set
            {
                answer.Text = value;
                OnPropertyChanged("Text");
            }
        }

        public bool isCorrect
        {
            get { return answer.isCorrect; }
            set
            {
                answer.isCorrect = value;
                OnPropertyChanged("isCorrect");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
