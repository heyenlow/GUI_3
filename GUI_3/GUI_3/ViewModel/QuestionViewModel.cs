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
    public class QuestionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Question question;
        public ObservableCollection<AnswerViewModel> Answers { get; set; }
        public QuestionViewModel(Question question)
        {
            this.question = question;

            Answers = new ObservableCollection<AnswerViewModel>();

            foreach (var answer in question.Answers)
            {
                var newA = new AnswerViewModel(answer);
                newA.PropertyChanged += OnPropertyChanged;
                Answers.Add(newA);
            }
        }

        public string Text
        {
            get { return question.Text; }
            set
            {
                question.Text = value;
                OnPropertyChanged("Text");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        public void SaveNew()
        {
            question.AddToDb();
        }

        public void Delete()
        {
            question.RemoveFromDb();
        }

    }
}
