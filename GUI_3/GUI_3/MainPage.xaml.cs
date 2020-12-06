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
        public QuizViewModel Quiz { get; set; }
        public MainPage()
        {
            this.InitializeComponent(); 
            Question = new QuestionViewModel(new Question(" "," "," "," "," ",0));
            Quiz = new QuizViewModel(new Quiz("Test"));
        }

        private void addQuizButton_Click(object sender, RoutedEventArgs e)
        {
            //NEED to add for changing the answers
            Quiz.Questions.Add(new QuestionViewModel(new Question(Question.Text, "something", "something2", "something3", "something4", 0)));
        }

        private void QuestionListView_Tapped(object sender, TappedRoutedEventArgs e)
        {
           Debug.WriteLine(Quiz.Questions[this.QuestionListView.SelectedIndex].Answers[0].Text);
            this.AnswersListView.ItemsSource = Quiz.Questions[this.QuestionListView.SelectedIndex].Answers;
        }
    }
}
