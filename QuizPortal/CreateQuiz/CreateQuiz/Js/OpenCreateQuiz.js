
var showEditQues = "";
function SaveAndNextButton() {
    $("#inst").hide();
    document.getElementById("QuesandAns").style.visibility = "visible"
    var name = $("#Name").val();
    name = "Quiz:" + name
    $("#QuizName").text(name);
    $("#Question").val("");
    $("#Answer1").val("");
    $("#Answer2").val("");
    $("#Answer3").val("");
    $("#Answer4").val("");
    $("#CorrectAnswer").val("");
    $("#Points").val("");
    document.getElementById("CreateQues").style.visibility = "visible";
    document.getElementById("UpdateQues").style.visibility = "hidden";
};



function EditQuestion(questions, ans1,ans2,ans3,ans4,crctans,points,qesId) {
    debugger;
    var name = $("#Name").val();
    name = "Quiz:" + name
    $("#QuizName").text(name);
    $("#Question").val(questions);
    $("#Answer1").val(ans1);
    $("#Answer2").val(ans2);
    $("#Answer3").val(ans3);
    $("#Answer4").val(ans4);
    $("#hdnQuesID").val(qesId);
    var radioid = "#" + crctans;
    $(radioid).prop("checked", true);
    $("#Points").val(points);
    document.getElementById("CreateQues").style.visibility = "hidden";
    document.getElementById("UpdateQues").style.visibility = "visible";

}
function SaveQuesAndRetainSamePage() {
    $("#inst").hide();
    document.getElementById("QuesandAns").style.visibility = "visible";
    var name = $("#Name").val();
    name = "Quiz:" + name
    $("#QuizName").text(name);
    $.ajax({
        url: '/CreateQuiz/OpenCreateQuiz',
        type: 'POST',
        data:{id :"QandA"},
        success: function (data) { },
        error: function (data) { }
    });
}

function PublishTheQuiz() {
    debugger;
    alert("Quiz Got published Successfully!");
    $.ajax({
        url: '/CreateQuiz/Index',
        type: 'POST',
        success: function (data) { },
        error: function (data) { }
    });
};

function HideErrorPanel() {
    $('#errorpanel').hide();
    debugger;
    if (showEditQues == "Y")
    {
        document.getElementById("QuesForUpdate").style.visibility = "visible";
        $.ajax({
            url: '/CreateQuiz/OpenCreateQuiz',
            type: 'POST',
            data:{id :"GetQuestions"},
            success: function (data) {
            },
            error: function (data) { }
        });
    }
}

function HideInformationPanel() {
    $('#informationPanel').hide();
}

$(document).ready(function () {
    debugger;
    $('#errorpanel').hide();
    $('#informationPanel').hide();
    $('#errorpanel').css({ 'right': '0px', 'left': '' }).animate({
        'right': '0px'
    });

    debugger;
    if (QuizSettingsErrors != null && QuizSettingsErrors != "") {
        AddQuizSettingsError();
    }

    if (QuestionCreated != null && QuestionCreated != "") {
        debugger;
        document.getElementById("PublishQuiz").disabled = false;
        $("#inst").hide();
        document.getElementById("QuesandAns").style.visibility = "visible"
        var name = $("#Name").val();
        name = "Quiz:" + name
        $("#QuizName").text(name);
        document.getElementById("QuesForUpdate").style.visibility = "visible"       
    }

    if (DisableSettings != null && DisableSettings == "Disable") {
        document.getElementById("SaveSettings").disabled = true;
        document.getElementById("SaveInstr").disabled = false;
        document.getElementById("newQues").disabled = false;
        if (ShowSettingsInformationPanel == "Settings Saved") {
            var informationpanel = document.getElementById("informationPanel");
            informationpanel.innerHTML = informationpanel.innerHTML + "<br/>" + "Quiz Settings Saved Successfully" + "<br>";
            $('#informationPanel').show();
        }
        $('#temp').addClass("disabledbutton");
    }
    else {
        document.getElementById("newQues").disabled = true;
    }

    if (ShowInstructionsInformationPanel == "Instructions Saved") {
        document.getElementById("newQues").disabled = false;
        var informationpanel = document.getElementById("informationPanel");
        informationpanel.innerHTML = informationpanel.innerHTML + "<br/>" + "Quiz Instructions Saved Successfully" + "<br>";
        $('#informationPanel').show();
    }

    debugger;
    if ($("#hdnQuesID").val() == "") {
        $("#hdnQuesID").val("0");
    }

    if (QandAMissingError == "QandA Missing") {
        debugger;
        AddQuestionAnswerError();
    }
});

function showErrorPanel() {
    var namelabl = document.getElementById("QuizNameErrorMsg");
    namelabl.style.visibility = "visible";
    $("#QuizNameErrorMsg").text(someStringValue);
    $('#errorpanel').show();
}

function AddQuestionAnswerError() {
    debugger;
    var div = document.getElementById("errorpanel");

    if ($("#Question").val() == "") {
        div.innerHTML = div.innerHTML + "<br/>" + "Please Enter Question" + "<br>";      
    }
    if ($("#Answer1").val() == "") {
        div.innerHTML = div.innerHTML + "Please Enter Option 1" + "<br/>";
    }
    if ($("#Answer2").val() == "") {
        div.innerHTML = div.innerHTML + "Please Enter Option 2" + "<br/>";
    }
    if ($("#Answer3").val() == "") {
        div.innerHTML = div.innerHTML + "Please Enter Option 3" + "<br/>";
    }
    if ($("#Answer4").val() == "") {
        div.innerHTML = div.innerHTML + "Please Enter Option 4" + "<br/>";
    }
    if ($("#CorrectAnswer").val() == "") {
        div.innerHTML = div.innerHTML + "Please Like Correect Answer" + "<br/>";
    }
    if ($("#Points").val() == "") {
        div.innerHTML = div.innerHTML + "Please Enter Points" + "<br/>";
    }
    $('#errorpanel').show();

    showEditQues = "Y";
    document.getElementById("QuesForUpdate").style.visibility = "hidden";
}


function AddQuizSettingsError() {
    debugger;
    var div = document.getElementById("errorpanel");

    if (QuizSettingsErrors == "Quiz Settings Required Error") {

        if ($("#Quiztype").val() == "") {
            div.innerHTML = div.innerHTML + "<br/>" + "Please Select Quiz Type" + "<br>";
        }

        if ($("#QuizGroup").val() == "") {
            div.innerHTML = div.innerHTML + "<br/>" + "Please Select Quiz QuizGroup" + "<br>";
        }

        if ($("#Name").val() == "") {
            div.innerHTML = div.innerHTML + "<br/>" + "Please Enter Quiz Name" + "<br>";
        }

        $('#errorpanel').show();
    }
}