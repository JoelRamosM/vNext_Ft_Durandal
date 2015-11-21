define(["knockout", "models/ModelBase"], function (ko, ModelBase) {

    function receitaModel(data) {
        data = data || {};
        ModelBase.bind(this, data).call();
        this.observacao = ko.observable(data.observacao);
        this.valor = ko.observable(data.valor || 0).extend({ currencyFormatted: 2 });
        this.data = ko.observable(data.data ? new Date(data.data) : new Date());
    }
    return receitaModel;
});