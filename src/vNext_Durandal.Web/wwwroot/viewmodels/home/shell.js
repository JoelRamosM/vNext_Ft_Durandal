﻿define(["plugins/router", "plugins/http", "durandal/app"], function (router, http, app) {
    var self = this;
    this.routes_structure = ko.observableArray(
    [
        { "route": "", "title": "Home", "moduleId": "viewmodels/home/home", "nav": true },
        { "route": "receitas", "title": "Receitas", "moduleId": "viewmodels/receitas/receitas", "nav": true },
        { "route": "receita#new", "title": "Receita", "moduleId": "viewmodels/receitas/receita" },
        { "route": "receita(/:id)#edit", "title": "Receita", "moduleId": "viewmodels/receitas/receita" }
    ]
    );
    return {
        router: router,
        search: function () {
            //It's really easy to show a message box.
            //You can add custom options too. Also, it returns a promise for the user's response.
            app.showMessage("Search not yet implemented...");
        },
        activate: function () {
            router.map(self.routes_structure()).buildNavigationModel();
            return router.activate();
        }
    };
});