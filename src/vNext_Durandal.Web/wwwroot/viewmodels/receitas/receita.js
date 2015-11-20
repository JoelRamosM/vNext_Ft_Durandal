define(["knockout", "plugins/http", "plugins/router", "models/Receita"], function (ko, http, router, Receita) {

    function ReceitaViewModel() {
        this.title = ko.observable();
        this.model = ko.observable(new Receita());
    };

    ReceitaViewModel.prototype.save = function () {
        var promisse;
        if (this.model().id())
            promisse = http.put("api/receitas", this.model());
        else
            promisse = http.post("api/receitas", this.model());

        promisse.then(function () {
            router.navigate("receitas");
        })
    };

    ReceitaViewModel.prototype.cancel = function () {

    };

    ReceitaViewModel.prototype.activate = function (id) {
        var self = this;
        if (id) {
            http.get("api/receitas/" + id).then(function (response) {
                self.model(new Receita(response));
            });

        } else {
            this.title("Novo");
        }
    };

    ReceitaViewModel.prototype.attached = function () {
        $("#data").datepicker();
    };
    return ReceitaViewModel;
});