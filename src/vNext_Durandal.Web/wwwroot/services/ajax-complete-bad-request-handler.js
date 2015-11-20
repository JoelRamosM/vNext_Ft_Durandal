require("jquery", function ($) {
    $(document).ajaxComplete(function (ev, xhr, settings) {
        if(xhr.status==400)

    });
});