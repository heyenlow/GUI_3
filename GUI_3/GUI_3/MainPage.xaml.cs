using GUI_3.Model;
using GUI_3.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GUI_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public QuestionViewModel Question { get; set; }
        public QuestionBankViewModel QuestionBank { get; set; }
        public QuizViewModel Quiz { get; set; }
        public AnswerViewModel Answer1 { get; set; }
        public AnswerViewModel Answer2 { get; set; }
        public AnswerViewModel Answer3 { get; set; }
        public AnswerViewModel Answer4 { get; set; }
        private int selecetedAnswer;
        public MainPage()
        {
            this.InitializeComponent(); 
            Question = new QuestionViewModel(new Question(" ",new Answer(""), new Answer(""), new Answer(""), new Answer(""), 0));
            QuestionBank = new QuestionBankViewModel(new QuestionBank());
            Quiz = new QuizViewModel(new Quiz(""));
            Answer1 = new AnswerViewModel(new Answer(""));
            Answer2 = new AnswerViewModel(new Answer(""));
            Answer3 = new AnswerViewModel(new Answer(""));
            Answer4 = new AnswerViewModel(new Answer(""));
            selecetedAnswer = 1;
        }

        private void addQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (Question.Text != "" && Answer1.Text != "" && Answer2.Text != "" && Answer3.Text != "" && Answer4.Text != "")
            {
                this.errorCreateQuestion.Visibility = Visibility.Collapsed;
                QuestionBank.Questions.Add(new QuestionViewModel(new Question(Question.Text, new Answer(Answer1.Text), new Answer(Answer2.Text), new Answer(Answer3.Text), new Answer(Answer4.Text), selecetedAnswer - 1)));
            }
            else
            {
                this.errorCreateQuestion.Visibility = Visibility.Visible;
            }
        }

        private void QuestionListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Debug.WriteLine(QuizBank.Questions[this.QuestionListView.SelectedIndex].Answers[0].Text);
            if (this.QuestionListView.SelectedIndex >= 0 && this.QuestionListView.SelectedIndex < QuestionBank.Questions.Count())
            {
                this.errorAddQuestion.Visibility = Visibility.Collapsed;
                this.AnswersListView.ItemsSource = QuestionBank.Questions[this.QuestionListView.SelectedIndex].Answers;
                int correctAnswer = 0;
                foreach (AnswerViewModel a in QuestionBank.Questions[this.QuestionListView.SelectedIndex].Answers)
                {
                    if (a.isCorrect) { correctAnswer = QuestionBank.Questions[this.QuestionListView.SelectedIndex].Answers.IndexOf(a); }
                }
                char c = (char)('A' + correctAnswer);
                this.ShowAnswer.Text = c.ToString();
            }
            else
            {
                this.errorAddQuestion.Visibility = Visibility.Visible;
            }
        }

        private void A1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                selecetedAnswer = Int32.Parse(rb.Tag.ToString());
            }
        }

        private void addToQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.QuestionListView.SelectedIndex >= 0 && this.QuestionListView.SelectedIndex < QuestionBank.Questions.Count()) {
                this.errorAddQuestion.Visibility = Visibility.Collapsed;
                Quiz.Questions.Add(QuestionBank.Questions[this.QuestionListView.SelectedIndex]);
            }
            else
            {
                this.errorAddQuestion.Visibility = Visibility.Visible;
            }
        }

        private void takeQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (Quiz.Questions.Count > 0)
            {
                this.errorTakeQuiz.Visibility = Visibility.Collapsed;
                this.Frame.Navigate(typeof(QuizPage), Quiz);
            }
            else
            {
                this.errorTakeQuiz.Visibility = Visibility.Visible;
            }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }
    }
}
