using GUI_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.ViewModel
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Quiz quiz;

        public ObservableCollection<QuestionViewModel> Questions { get; set; }
 
 
        public string Name
        {
            get { return quiz.Name; }
            set
            {
                quiz.Name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
 
        public QuizViewModel(Quiz quiz)
        {
            this.quiz = quiz;

            Questions = new ObservableCollection<QuestionViewModel>();

            foreach(var question in quiz.Questions)
            {
                var newQ = new QuestionViewModel(question);
                newQ.PropertyChanged += OnPropertyChanged;
                Questions.Add(newQ);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

    }
}
