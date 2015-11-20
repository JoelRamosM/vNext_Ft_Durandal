define(["knockout", "plugins/router", "plugins/http"], function (ko, router, http) {

    function ReceitaViewModel() {
        this.gridUrl = "api/receitas";
        this.title = ko.observable("Receitas");
        this.receitas = ko.observableArray([]);

        this.collumns = [{ title: "Tipo", prop: "tipo" }, { title: "Valor", prop: "valor", format: "money" }];

    };

    ReceitaViewModel.prototype.activate = function () {

    }

    ReceitaViewModel.prototype.newAction = function () {
        router.navigate("receita#new");
    }

    ReceitaViewModel.prototype.defaultAction = function (id) {
        router.navigate("receita/" + id + "#edit");
    }
    return ReceitaViewModel;
});