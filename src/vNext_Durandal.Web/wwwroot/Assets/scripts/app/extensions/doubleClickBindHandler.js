ko.bindingHandlers["doubleClick"] = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var handler = ko.unwrap(valueAccessor());
        $(element).on("dblClick", function (e) {
            handler.call(viewModel, bindingContext.$data, e);
        });
    }
};