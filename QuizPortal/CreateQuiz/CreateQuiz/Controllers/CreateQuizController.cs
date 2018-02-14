using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace CreateQuiz.Controllers
{
    public class CreateQuizController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("OpenCreateQuiz")]
        public ActionResult OpenCreateQuiz_Get(string a)
        {
            return View();
        }

        [HttpPost]
        [ActionName("OpenCreateQuiz")]
        public ActionResult OpenCreateQuiz_Post(string id)
        {
            QuizSettings quiz = new QuizSettings();
            TryUpdateModel(quiz);
            if (ModelState.IsValid)
            {
                try
                {
                    QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();

                    if (id == "save")
                        SaveSettings(quiz);

                    else if (id == "ins")
                        SaveInstructions();

                    else if (id == "QandA")
                        SaveQuestionAndAnswers(quiz);

                    else if (id == "UpdateQandA")
                        UpdateQuestionAndAnswers(quiz);

                    else if (id == "GetQuestions")
                        GetQuestions(quiz);

                    return View(quiz);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(quiz);
        }

        public void SaveSettings(QuizSettings quiz)
        {
            if (quiz.Quiztype != null && quiz.Name != null && quiz.QuizGroup != null)
            {
                QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();
                quizbusinesslayer.AddQuizSettings(quiz);
                if (quiz.ErrorMessage != null && quiz.ErrorMessage != string.Empty)
                    ViewBag.NameError = quiz.ErrorMessage;
                else
                    ViewBag.DisableSettings = "Disable";
                    ViewBag.ShowSettingsInformationPanel = "Settings Saved";
                HttpCookie cookie = new HttpCookie("QuizName");
                cookie.Value = quiz.Name;
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                ViewBag.Name = quiz.Name;
            }
            else
            {
                ViewBag.NameError = "Quiz Settings Required Error";
            }
        }

        public void SaveInstructions()
        {
            QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();
            string name = Request.Form["Name"];
            string instructions = Request.Form["instructions"];
            quizbusinesslayer.UpdateInstructions(name, instructions);
            ViewBag.DisableSettings = "Disable";
            ViewBag.ShowInstructionsInformationPanel = "Instructions Saved";
        }

        public void SaveQuestionAndAnswers(QuizSettings quiz)
        {
            quiz.CorrectAnswer = Request.Form["radiog_lite"];
            quiz.Name = Request.Form["Name"];
            if (quiz.Question != null && quiz.Answer1 != null && quiz.Answer2 != null && quiz.Answer3 != null && quiz.Answer4 != null && quiz.CorrectAnswer != null && quiz.Points != null)
            {
                QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();
                quizbusinesslayer.InsertQuestionAnswers(quiz);
                List<QuizSettings> QuizQuestions = quizbusinesslayer.GetQuestionsForUpdate(quiz.Name);
                ViewBag.QuestionCreated = "Created";
                ViewBag.ListOfQues = QuizQuestions;
                ViewBag.DisableSettings = "Disable";
            }
            else
            {
                ViewBag.DisableSettings = "Disable";
                ViewBag.QuestionCreated = "Created";
                ViewBag.QuesAnswerError = "QandA Missing";
            }
        }

        public void UpdateQuestionAndAnswers(QuizSettings quiz)
        {
            QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();
            quiz.CorrectAnswer = Request.Form["radiog_lite"];
            quizbusinesslayer.UpdateQuestionAndANswers(quiz);
            List<QuizSettings> QuizQuestions = quizbusinesslayer.GetQuestionsForUpdate(quiz.Name);
            ViewBag.QuestionCreated = "Created";
            ViewBag.ListOfQues = QuizQuestions;
            ViewBag.DisableSettings = "Disable";
        }

        public void GetQuestions(QuizSettings quiz)
        {
            QuizBusinessLayer quizbusinesslayer = new QuizBusinessLayer();
            quiz.Name = Request.Cookies["QuizName"].Value;
            List<QuizSettings> QuizQuestions = quizbusinesslayer.GetQuestionsForUpdate(quiz.Name);
            ViewBag.QuestionCreated = "Created";
            ViewBag.ListOfQues = QuizQuestions;
            ViewBag.DisableSettings = "Disable";
        }


    }
}
