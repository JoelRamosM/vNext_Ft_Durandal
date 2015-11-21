define(["jquery", "services/toastService"], function ($, toastService) {
    $(document).ajaxComplete(function (ev, xhr, settings) {
        if (xhr.status === 400) {
            var errorResponse = xhr.responseJSON;
            var message = "<ul>";
            errorResponse.errors.forEach(function (error) {
                message += "<li><strong>" + error.title + "</strong> - " + error.message + "</li>";
            });
            message += "</ul>";
            toastService.showError(message, errorResponse.title, 2000);
        }
        if (xhr.status === 500) {
            toastService.showError("Ops! Desculpe ocorreu um erro inesperados. =(", "Erro Inesperado", 2000);
        }
        if (xhr.status === 404) {
            toastService.showWarning("Ops! Não consegui encontrar. =(", "Não Encontrado", 2000);
        }
    });
});