using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_3.ViewModel
{
    class QuizAttemptViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public QuizViewModel quiz;
        private QuestionViewModel currentQuestion;
        private int questionNumber = -1;
        private string resultMessage = "";

        private int numberCorrect = 0;
        private int numberOfQuestions = 0;

        public int QuestionNumber
        {
            get { return questionNumber;  }
        }

        public int NumberCorrect
        {
            get { return numberCorrect; }
        }

        public int NumberOfQuestions
        {
            get { return numberOfQuestions; }
        }

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

        public QuizAttemptViewModel(QuizViewModel quiz)
        {
            if (quiz != null) this.quiz = quiz.shuffled();

            questionNumber = -1;
            ResultMessage = "";

            numberCorrect = 0;
            numberOfQuestions = 0;
        }

        public void getNextQuestion()
        {
            ++questionNumber;

            if (questionNumber < quiz.Questions.Count)
            {
                CurrentQuestion = quiz.Questions[questionNumber];
                ResultMessage = "";
            }
        }

        public void submit(int selectedAnswer)
        {
            int correctAnswer = 0;

            for (correctAnswer = 0; correctAnswer < CurrentQuestion.Answers.Count; ++correctAnswer)
                if (CurrentQuestion.Answers[correctAnswer].isCorrect)
                    break;

            ++correctAnswer;

            if (selectedAnswer == correctAnswer)
            {
                ResultMessage = "Correct!";
                ++numberCorrect;
            }
            else
            {
                ResultMessage = "Incorrect";
            }

            ++numberOfQuestions;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
