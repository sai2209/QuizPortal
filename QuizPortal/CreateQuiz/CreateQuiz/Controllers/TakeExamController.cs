using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace CreateQuiz.Controllers
{
    public class TakeExamController : Controller
    {
        //
        // GET: /TakeExam/
        List<GetQuizList> lstQuizList =  new List<GetQuizList>();

        public ActionResult SelectQuiz()
        {
            QuizListBusinessLayer quizlistbusinesslayer = new QuizListBusinessLayer();
           List<GetQuizList> lstQuizList  = quizlistbusinesslayer.GetQuizList();
           List<SelectListItem> items = new List<SelectListItem>();
           items.Add(new SelectListItem
           {
               Text = "Select Quiz",
               Value = "0",
               Selected = true
           });
           for (int i = 0; i < lstQuizList.Count; i++)
           {
               items.Add(new SelectListItem
               {
                   Text = lstQuizList[i].QuizList,
                   Value = lstQuizList[i].QuizList,
               });
           }
           ViewBag.Quiz = items;

           return View();
        }


        public ActionResult GetQuestions1(string id)
        {
            GetQuizList quizlist = new GetQuizList();

            if (Request.Form["hdnIndex"] == null || Request.Form["hdnIndex"] == "")
            {
                try
                {
                    QuizListBusinessLayer quizlistbusinesslayer = new QuizListBusinessLayer();
                    string Quiz = Request.Form["Quiz"];
                    lstQuizList = quizlistbusinesslayer.GetQuestionAndAnswers(Quiz);
                    Session["ListOfQues"] = lstQuizList;
                    if (Request.Form["hdnIndex"] == null || Request.Form["hdnIndex"] == "")
                        ViewBag.Index = "0";
                    else
                        ViewBag.Index = int.Parse(Request.Form["hdnIndex"]) + 1;
                    ViewBag.AllQuestions = lstQuizList[int.Parse(ViewBag.Index)];
                }
                catch (Exception ex)  
                {
                    ModelState.AddModelError("", "Please Select Quiz");
                }
            }
            else
            {
                List<GetQuizList> objlist = (List<GetQuizList>)Session["ListOfQues"];
                string SelectedAns = Request.Form["radiog_lite"];
                objlist[int.Parse(Request.Form["hdnIndex"])].SelectedAnswer = SelectedAns;
                Session["ListOfQues"] = objlist;
                int index = 0;

                if (id == "Prev")
                     index = int.Parse(Request.Form["hdnIndex"]) - 1;
                if (index == -1)
                    index = 0;

                else if (id == "Next")
                    index = int.Parse(Request.Form["hdnIndex"]) + 1;

                if (index < objlist.Count)
                {
                    try
                    {
                        ViewBag.AllQuestions = objlist[index];
                        ViewBag.Index = index;
                        if (objlist[index].SelectedAnswer != null)
                            ViewBag.SelectedAnswer = objlist[index].SelectedAnswer;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    int score = 0;
                    int totalscore = 0;
                    ViewBag.TotalQues = objlist.Count;
                    int UnansweredQuestions = 0;
                    int correctAnswers = 0;
                    foreach (var obj in objlist)
                    {
                        totalscore = totalscore + int.Parse(obj.points);
                        if (obj.CorrectAnswer == obj.SelectedAnswer)
                        {
                            score = score + int.Parse(obj.points);
                            correctAnswers = correctAnswers + 1;
                        }
                        else if (obj.SelectedAnswer == null)
                            UnansweredQuestions = UnansweredQuestions + 1;
                           
                    }
                    ViewBag.CorrectAnsw = correctAnswers;
                    ViewBag.UnansweredQues = UnansweredQuestions;
                    ViewBag.WrongAnsw = objlist.Count - correctAnswers - UnansweredQuestions;
                    ViewBag.Score = score;
                    ViewBag.TotalScore = totalscore;
                    ViewBag.DisplayScore = "true";
                }

                if (index == objlist.Count - 1)
                {
                    ViewBag.ShowEndQuiz = "EndQuiz";
                }
            }

            return View();

        }

    }
}
