using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
   public class QuizListBusinessLayer
    {
        public List<GetQuizList> GetQuizList()
        {
            List<GetQuizList> QuizList = new List<GetQuizList>();
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand cmnd = new SqlCommand("GetQuizNames", con);
            cmnd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmnd.ExecuteReader();
            while (dr.Read())
            {
                GetQuizList ListOfQuiz = new GetQuizList();
                ListOfQuiz.QuizList = dr["QuizName"].ToString();
                QuizList.Add(ListOfQuiz);
            }
            return QuizList;
        }

        public List<GetQuizList> GetQuestionAndAnswers(string Quiz)
        {
            List<GetQuizList> QuizList = new List<GetQuizList>();
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand cmnd = new SqlCommand("GetAllQuestionsData", con);
            cmnd.CommandType = CommandType.StoredProcedure;

            SqlParameter ParamCheckname = new SqlParameter();
            ParamCheckname.ParameterName = "@QuizName";
            ParamCheckname.Value = Quiz;
            cmnd.Parameters.Add(ParamCheckname);
            con.Open();
            SqlDataReader dr = cmnd.ExecuteReader();
            while (dr.Read())
            {
                GetQuizList ListOfQuiz = new GetQuizList();
                ListOfQuiz.QuesID = dr["QuesID"].ToString();
                ListOfQuiz.Question = dr["Question"].ToString();
                ListOfQuiz.Answer1 = dr["Answer1"].ToString();
                ListOfQuiz.Answer2 = dr["Answer2"].ToString();
                ListOfQuiz.Answer3 = dr["Answer3"].ToString();
                ListOfQuiz.Answer4 = dr["Answer4"].ToString();
                ListOfQuiz.CorrectAnswer = dr["CorrectAnswer"].ToString();
                ListOfQuiz.points = dr["points"].ToString();
                QuizList.Add(ListOfQuiz);
            }

            return QuizList;
        }
    }
}
