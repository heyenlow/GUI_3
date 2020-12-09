using GUI_3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

        public QuizViewModel shuffled()
        {
            QuizViewModel quizViewModel = new QuizViewModel(quiz);
            quizViewModel.Questions = new ObservableCollection<QuestionViewModel>(Questions);

            // Fisher-Yates shuffle
            // Code courtesy of u/grenade on StackOverflow (https://stackoverflow.com/questions/273313/randomize-a-listt)
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = quizViewModel.Questions.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                QuestionViewModel value = quizViewModel.Questions[k];
                quizViewModel.Questions[k] = quizViewModel.Questions[n];
                quizViewModel.Questions[n] = value;
            }

            return quizViewModel;
        }

    }
}
