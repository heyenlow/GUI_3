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
    public class QuestionBankViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private QuestionBank questionBank;
        public ObservableCollection<QuestionViewModel> Questions { get; set; }

        public QuestionBankViewModel(QuestionBank questionBank)
        {
            this.questionBank = questionBank;

            Questions = new ObservableCollection<QuestionViewModel>();

            foreach (var question in questionBank.Questions)
            {
                var newQ = new QuestionViewModel(question);
                newQ.PropertyChanged += OnPropertyChanged;
                Questions.Add(newQ);
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
    }
}
