// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#menu-toggle").on('click', (function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    }));

    $("#formButton").click(function () {
        $("#form1").toggle();
    });

});