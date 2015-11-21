ko.bindingHandlers["formatter"] = {
    init: function () {

    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var format = ko.unwrap(valueAccessor());
        var text = ko.unwrap(allBindings.get("text"));

        if (format === "money") {
            var parts = (Math.abs(text) + "").split(".");
            var inteiro = parts[0];
            var decimal = ((parts[1] || 0) + Array(2 + 1).join("0")).substr(0, 2);
            var nvalue = "R$ " + inteiro.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "," + decimal;
            $(element).text(nvalue);
            return;
        }
        if (format === "date") {
            var nvalue = new Date(text).toLocaleDateString();
            $(element).text(nvalue);
            return;
        }
        if (/function()/.test(format)) {
            var formatterFunction = eval("(" + format + ")");
            $(element).text(formatterFunction(text));
        }
    }

};