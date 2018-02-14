$(document).ready(function () {

    if (EndQuiz == "EndQuiz") {
        document.getElementById("Next").style.visibility = "hidden";
        document.getElementById("EndQuiz").style.visibility = "Visible";
    }

    if (DisplayScore == "true") {
        document.getElementById("QuizDiv").style.visibility = "hidden";
        document.getElementById("showResult").style.visibility = "Visible";
    }

    debugger;
    if (SelectedAnswer !== "") {
        var radioid = "#" + SelectedAnswer;
        $(radioid).prop("checked", true);
    }
})

