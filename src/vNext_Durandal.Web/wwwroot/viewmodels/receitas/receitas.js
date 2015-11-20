define(["knockout", "plugins/router", "plugins/http"], function (ko, router, http) {

    function ReceitaViewModel() {
        this.gridUrl = "api/receitas";
        this.title = ko.observable("Receitas");
        this.receitas = ko.observableArray([]);
        this.collumns = [{ title: "Tipo", prop: "tipo" }, { title: "Valor", prop: "valor", format: "money" }];

        this.gridAPI = ko.observable();
        this.crudBarAPI = ko.observable();


    };

    ReceitaViewModel.prototype.attached = function() {
        this.crudBarAPI().disableDeleteAction();
        this.gridAPI().selectedRows.subscribe(function(data) {
            this.crudBarAPI().setSelectedBadge(data.length);
            if (!data.length)
                this.crudBarAPI().disableDeleteAction();
            else
                this.crudBarAPI().enableDeleteAction();

        }, this);
    };
    
    ReceitaViewModel.prototype.onNew = function () {
        router.navigate("receita#new");
    };

    ReceitaViewModel.prototype.onEdit = function (id) {
        if (id)
            router.navigate("receita/" + id + "#edit");
    };

    ReceitaViewModel.prototype.onDelete = function () {
        var that = this;
        http.remove("api/receitas", { ids: this.gridAPI().selectedRows() }).then(function (response) {
            that.gridAPI().refresh();
            that.gridAPI().selectedRows.removeAll();
        });
    };

    ReceitaViewModel.prototype.defaultAction = function (id) {
        this.onEdit(id);
    };

    return ReceitaViewModel;
});