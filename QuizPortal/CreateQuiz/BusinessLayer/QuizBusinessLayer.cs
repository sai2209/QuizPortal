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
    public class QuizBusinessLayer
    {
        public void AddQuizSettings(QuizSettings quizSet)
        {
            {
                string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);

                SqlCommand cmnd = new SqlCommand("CheckQuizName", con);
                cmnd.CommandType = CommandType.StoredProcedure;
                cmnd.Parameters.Add("@Name", SqlDbType.VarChar).Value = quizSet.Name;
                con.Open();
                int count = int.Parse(cmnd.ExecuteScalar().ToString());
                con.Close();

                if (count == 0)
                {
                    quizSet.ErrorMessage = "";
                    SqlCommand cmd = new SqlCommand("spSaveQuizSettings", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Quiztype", SqlDbType.VarChar).Value = quizSet.Quiztype;
                    cmd.Parameters.Add("@QuizGroup", SqlDbType.VarChar).Value = quizSet.QuizGroup;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = quizSet.Name;
                    cmd.Parameters.Add("@Shuffleanswers", SqlDbType.VarChar).Value = quizSet.Shuffleanswers;
                    cmd.Parameters.Add("@Timelimit", SqlDbType.VarChar).Value = quizSet.Timelimit;
                    cmd.Parameters.Add("@Minut", SqlDbType.VarChar).Value = quizSet.Minut;
                    cmd.Parameters.Add("@Studentsseequiz", SqlDbType.VarChar).Value = quizSet.Studentsseequiz;
                    cmd.Parameters.Add("@Showcrctans", SqlDbType.VarChar).Value = quizSet.Showcrctans;
                    cmd.Parameters.Add("@Allowmultattempt", SqlDbType.VarChar).Value = quizSet.Allowmultattempt;
                    cmd.Parameters.Add("@Showoneques", SqlDbType.VarChar).Value = quizSet.Showoneques;
                    cmd.Parameters.Add("@RestQuiz", SqlDbType.VarChar).Value = quizSet.RestQuiz;
                 
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    quizSet.ErrorMessage = "Quiz Name already Exists. Please create Quiz with other name";
                }


            }
        }
        public void UpdateInstructions(string name, string instructions)
        {
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand("UpdateInstructions", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@Instructions", SqlDbType.VarChar).Value = instructions;



            con.Open();
            cmd.ExecuteNonQuery();
        }
        public void InsertQuestionAnswers(QuizSettings quizSet)
        {
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand getQuestionId = new SqlCommand("GetQuestionCount", con);
            getQuestionId.CommandType = CommandType.StoredProcedure;

            getQuestionId.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = quizSet.Name;

            SqlParameter iCount = getQuestionId.Parameters.Add("@Count", SqlDbType.Int);
            iCount.Direction = ParameterDirection.Output;
            con.Open();
            getQuestionId.ExecuteNonQuery();
            con.Close();

            int QuesID = int.Parse(iCount.Value.ToString())+1;

            SqlCommand cmd = new SqlCommand("SaveQuestionAndAnswers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = quizSet.Name;
            cmd.Parameters.Add("@QuestionType", SqlDbType.VarChar).Value = quizSet.Quiztype;
            cmd.Parameters.Add("@Question", SqlDbType.VarChar).Value = quizSet.Question;
            cmd.Parameters.Add("@Answer1", SqlDbType.VarChar).Value = quizSet.Answer1;
            cmd.Parameters.Add("@Answer2", SqlDbType.VarChar).Value = quizSet.Answer2;
            cmd.Parameters.Add("@Answer3", SqlDbType.VarChar).Value = quizSet.Answer3;
            cmd.Parameters.Add("@Answer4", SqlDbType.VarChar).Value = quizSet.Answer4;
            cmd.Parameters.Add("@CorrectAnswer", SqlDbType.VarChar).Value = quizSet.CorrectAnswer;
            cmd.Parameters.Add("@points", SqlDbType.VarChar).Value = quizSet.Points;
            cmd.Parameters.Add("@QuesID", SqlDbType.VarChar).Value = QuesID.ToString();


            con.Open();
            cmd.ExecuteNonQuery();
        }
        public List<QuizSettings> GetQuestionsForUpdate(string QuizName)
        {
            List<QuizSettings> Questions = new List<QuizSettings>();
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand cmnd = new SqlCommand("GetQuestions", con);
            cmnd.CommandType = CommandType.StoredProcedure;
            SqlParameter ParamCheckname = new SqlParameter();
            ParamCheckname.ParameterName = "@QuizName";
            ParamCheckname.Value = QuizName;
            cmnd.Parameters.Add(ParamCheckname);
            con.Open();
            SqlDataReader dr = cmnd.ExecuteReader();
            while (dr.Read())
            {
                QuizSettings QuestionandAnsw = new QuizSettings();
                QuestionandAnsw.Question = dr["Question"].ToString();
                QuestionandAnsw.QuizID = int.Parse(dr["QuesID"].ToString());
                QuestionandAnsw.Name = dr["QuizName"].ToString();
                QuestionandAnsw.Answer1 = dr["Answer1"].ToString();
                QuestionandAnsw.Answer2 = dr["Answer2"].ToString();
                QuestionandAnsw.Answer3 = dr["Answer3"].ToString();
                QuestionandAnsw.Answer4 = dr["Answer4"].ToString();
                QuestionandAnsw.CorrectAnswer = dr["CorrectAnswer"].ToString();
                QuestionandAnsw.Points = dr["points"].ToString();
                Questions.Add(QuestionandAnsw);
            }
            return Questions;
        }
        public void UpdateQuestionAndANswers(QuizSettings quizSet)
        {
            string connString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand getQuestionId = new SqlCommand("UpdateQandA", con);

            getQuestionId.CommandType = CommandType.StoredProcedure;

            getQuestionId.Parameters.Add("@QuizName", SqlDbType.VarChar).Value = quizSet.Name;
            getQuestionId.Parameters.Add("@Question", SqlDbType.VarChar).Value = quizSet.Question;
            getQuestionId.Parameters.Add("@Answer1", SqlDbType.VarChar).Value = quizSet.Answer1;
            getQuestionId.Parameters.Add("@Answer2", SqlDbType.VarChar).Value = quizSet.Answer2;
            getQuestionId.Parameters.Add("@Answer3", SqlDbType.VarChar).Value = quizSet.Answer3;
            getQuestionId.Parameters.Add("@Answer4", SqlDbType.VarChar).Value = quizSet.Answer4;
            getQuestionId.Parameters.Add("@CorrectAnswer", SqlDbType.VarChar).Value = quizSet.CorrectAnswer;
            getQuestionId.Parameters.Add("@points", SqlDbType.VarChar).Value = quizSet.Points;
            getQuestionId.Parameters.Add("@QuesID", SqlDbType.VarChar).Value = quizSet.QuesID;

            con.Open();
            getQuestionId.ExecuteNonQuery();
        }
    }
}
