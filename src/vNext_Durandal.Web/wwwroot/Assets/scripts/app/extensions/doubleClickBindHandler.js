ko.bindingHandlers["doubleClick"] = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var handler = valueAccessor.bind(bindingContext)();
        $(element).on("dblclick", function (e) {
            handler(viewModel, e);
        });
    }
};