using GUI_3.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GUI_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private QuizAttemptViewModel quiz;
        private QuestionViewModel currentQuestion;
        private string resultMessage;
        private int selectedAnswer = 1;

        public QuestionViewModel CurrentQuestion
        {
            get { return currentQuestion; }
            set
            {
                currentQuestion = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("CurrentQuestion"));
            }
        }

        public string ResultMessage
        {
            get { return resultMessage; }
            set
            {
                resultMessage = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("ResultMessage"));
            }
        }

        public QuizPage()
        {
            this.InitializeComponent();
        }

        private void getNextQuestion()
        {
            quiz.getNextQuestion();
            CurrentQuestion = quiz.CurrentQuestion;
            if (quiz.QuestionNumber < quiz.quiz.Questions.Count)
            {
                ResultMessage = "";
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    A1.IsChecked = false;
                    A2.IsChecked = false;
                    A3.IsChecked = false;
                    A4.IsChecked = false;
                });
            }
            else
            {
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => this.Frame.Navigate(typeof(ResultsPage), (quiz.NumberCorrect, quiz.NumberOfQuestions)));
            }
        }

        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                selectedAnswer = Int32.Parse(rb.Tag.ToString());
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>  this.Frame.GoBack());
                return;
            }

            quiz = new QuizAttemptViewModel(e.Parameter as QuizViewModel);
            ResultMessage = "";

            if (quiz.quiz == null || quiz.quiz.Questions.Count == 0)
            {
                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => this.Frame.GoBack());
                return;
            }

            getNextQuestion();
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            quiz.submit(selectedAnswer);
            ResultMessage = quiz.ResultMessage;

            Timer timer = new Timer(2000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            getNextQuestion();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => PropertyChanged?.Invoke(sender, e));
        }
    }
}
