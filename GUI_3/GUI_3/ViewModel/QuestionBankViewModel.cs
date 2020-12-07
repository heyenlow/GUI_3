using GUI_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

            Questions.CollectionChanged += Questions_CollectionChanged;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        private void Questions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Add new movie to database
                var questionViewModel = e.NewItems[0] as QuestionViewModel;
                questionViewModel.SaveNew();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // Delete movie from database
                var questionViewModel = e.OldItems[0] as QuestionViewModel;
                questionViewModel.Delete();
            }
        }
    }
}
