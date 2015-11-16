define(["knockout", "plugins/router", "plugins/http"], function (ko, router, http) {
    var ctor = function () {
        this.title = ko.observable("Receitas");
        this.receitas = ko.observableArray([]);

    };
    ctor.prototype.activate = function () {

    }
    ctor.prototype.defaultAction = function (id) {
        router.navigate("" + "/" + id);
    }
    return ctor;
});