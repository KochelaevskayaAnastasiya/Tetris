// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const buttonRegist = document.querySelector('#submitRegist');
const login = document.querySelector('#login');
const pass1 = document.querySelector('#pass1');
const pass2 = document.querySelector('#pass2');

function validate_form() {
    localStorage.setItem("login", login);
    localStorage.setItem("pass1", pass1);
    localStorage.setItem("pass2", pass2);
    return true;
}
function codeAddress() {
    divCheckPasswordMatch.innerHTML = localStorage.getItem("warn2");
    divCheckPasswordMatch.innerHTML = localStorage.getItem("warn");
    document.getElementById("login").innerHTML = localStorage.getItem("login");;
    document.getElementById("pass1").innerHTML = localStorage.getItem("pass1");;
    document.getElementById("pass2").innerHTML = localStorage.getItem("pass2");;
}

$('body').on('click', '.password-checkbox-reg', function () {
    if ($(this).is(':checked')) {
        $('#pass1').attr('type', 'text');
        $('#pass2').attr('type', 'text');
    } else {
        $('#pass1').attr('type', 'password');
        $('#pass2').attr('type', 'password');
    }
}); 

$('body').on('click', '.password-checkbox-auto', function () {
    if ($(this).is(':checked')) {
        $('#pass').attr('type', 'text');
    } else {
        $('#pass').attr('type', 'password');
    }
});

function checkPasswordMatch() {
    var password = $("#pass1").val();
    var confirmPassword = $("#pass2").val();

    if (password != confirmPassword) {
        $("#divCheckPasswordMatch").html("Пароли не совпадают!");
        localStorage.setItem("warn", "Пароли не совпадают!");
    }
    else {
        $("#divCheckPasswordMatch").html("");
        localStorage.removeItem("warn");
    }
}

function checLogin(b) {
    if (b) {
        localStorage.setItem("warn2", "Такой логин уже существует");
    }
    else {
        localStorage.removeItem("warn2");
    }
}

$(document).ready(function () {
    $("#txtConfirmPassword").keyup(checkPasswordMatch);
});

let val_str;

$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');

        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-dismiss="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');

        });
    });
});
$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal4"]').click(function (event) {
        var url = $(this).data('url');
        
            $.get(url).done(function (data) {
                placeholderElement.html(data);
                placeholderElement.find('.modal').modal('show');
            }); 
    });
    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid1"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            }
        });
    });
    placeholderElement.on('click', '[data-dismiss="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');

        });
    });
});
let kkk_2;
let w;
let h;
let index;
let index1;
let iddd;
let hw;
$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal2"]').click(function (event) {
        var url = $(this).data('url');
        kkk_2 = $(this).data('kkk');
        kkk_2 = this.value;
        index = kkk_2.indexOf("x");
        index1 = kkk_2.indexOf("</td>");
        w = kkk_2.substring(index - 2, index);
        h = kkk_2.substring(index + 1, index + 3); 
        iddd = kkk_2.substring(16, index1); 
        hw = w + h+iddd;
        if (kkk_2 != "4" && kkk_2.indexOf("Размер")<0) {
            $.get(url).done(function (data) {
                placeholderElement.html(data);
                placeholderElement.find('.modal').modal('show');
            });
        }
    });
    placeholderElement.on('click', '[data-save="modalf"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

           
                placeholderElement.find('.modal').modal('hide');
                location.reload();
            
        });
    });
    placeholderElement.on('click', '[data-dismiss="modal"]', function (event) {
        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');

        });
    });
});


$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal3"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
    placeholderElement.on('click', '[data-dismiss="modal"]', function (event) {
        placeholderElement.find('.modal').modal('hide');
    });
});


let id_level = 0;


$("#button_chen").on("click", function () {
    id_level = $(this).attr('level-id');
});

function saveGlass(kkk){

}
$("#btn-modal-2").on("click", function () {
    id_glass = $(this).attr('glass-id');
});


