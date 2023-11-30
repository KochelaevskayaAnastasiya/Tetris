// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const chekPass = false;

function checkPasswordMatch() {
    var password = $("#pass1").val();
    var confirmPassword = $("#pass2").val();

    if (password != confirmPassword) {
        $("#divCheckPasswordMatch").html("Пароли не совпадают!");
        chekPass = false;
    }
    else {
        $("#divCheckPasswordMatch").html("");
        chekPass = true;
    }
}

$(document).ready(function () {
    $("#txtConfirmPassword").keyup(checkPasswordMatch);
});

const buttonRegist = document.querySelector('#submitRegist');
const login = document.querySelector('#login');
const pass1 = document.querySelector('#pass1');
const pass2 = document.querySelector('#pass2');


function validate_form() {
    valid = true;
    const isIdUnique = login =>
        db.Tetris.findOne({ where: { login } })
            .then(token => token !== null)
            .then(isUnique => isUnique);
    if (isIdUnique) {
        $("#divCheckPasswordMatch").html("Такой логин уже существует!");
        valid = false;
    }
    else {
        $("#divCheckPasswordMatch").html("");
        valid = true;
    }

    return valid;
}