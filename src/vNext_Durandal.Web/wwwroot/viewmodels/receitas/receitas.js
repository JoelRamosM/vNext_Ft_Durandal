define(["knockout", "plugins/router", "plugins/http"], function (ko, router, http) {

    function ctor() {
        this.gridUrl = "api/receitas";
        this.title = ko.observable("Receitas");
        this.receitas = ko.observableArray([]);

        this.collumns = [{ title: "Tipo", prop: "tipo" }, { title: "Valor", prop: "valor", format: "money" }];

    };
    ctor.prototype.activate = function () {

    }
    ctor.prototype.defaultAction = function (id) {
        router.navigate("" + "/" + id);
    }
    return ctor;
});