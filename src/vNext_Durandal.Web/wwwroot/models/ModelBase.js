define(["knockout"], function (ko) {
    function ModelBase(data) {
        this.id = ko.observable(data.id || 0);
    };

    return ModelBase;
});