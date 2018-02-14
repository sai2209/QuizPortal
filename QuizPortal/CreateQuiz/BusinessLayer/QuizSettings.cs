using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class QuizSettings
    {
        [Display(Name = "Type:")]
        public string Quiztype { get; set; }
        [Display(Name = "Group:")]
        public string QuizGroup { get; set; }
        [Display(Name = "Name:")]
        public string Name { get; set; }
        [Display(Name = "Shuffle Answers")]
        public bool Shuffleanswers { get; set; }
        [Display(Name = "Time Limit")]
        public bool Timelimit { get; set; }
        [Display(Name = "Minutes")]
        [Range(0, 1000)]
        public int Minut { get; set; }
        [Display(Name = "Let Students See their Quiz")]
        public bool Studentsseequiz { get; set; }
        [Display(Name = "Show Which Answers Were Correct")]
        public bool Showcrctans { get; set; }
        [Display(Name = "Allow Multiple Attempts")]
        public bool Allowmultattempt { get; set; }
        [Display(Name = "Show One Question at a Time ")]
        public bool Showoneques { get; set; }
        [Display(Name = "Restrict this Quiz ")]
        public bool RestQuiz { get; set; }
        [Display(Name = "Please Enter your Instructions for the Quiz:")]
        public string instructions { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string Points { get; set; }

        public string ErrorMessage { get; set; }

        public int QuizID { get; set; }

        public int QuesID { get; set; }
    }
}
