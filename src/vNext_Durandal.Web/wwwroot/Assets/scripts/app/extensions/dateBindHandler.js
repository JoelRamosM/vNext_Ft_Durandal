ko.bindingHandlers["datepicker"] = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var $element = $(element);
        $element.datepicker();
        var value = valueAccessor();
        ko.utils.registerEventHandler(element, "change", function () {
            value($element.datepicker("getDate"));
        });
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $element.datepicker("destroy");
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var regexDate = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
        var value = ko.utils.unwrapObservable(valueAccessor());
        var $el = $(element);
        var current = $el.datepicker("getDate");
        var input = $el.val();
        if (input.length > 10) {
            var splitDate = input.substring(0, 10).split("/");
            value = new Date(+splitDate[2], splitDate[1] - 1, +splitDate[0]);
        }

        if (!regexDate.test(input))
            $el.datepicker("setDate", new Date());

        if (value - current !== 0)
            $el.datepicker("setDate", value);
    }
};