ko.extenders.currencyFormatted = function (target, decimals) {
    target.formatted = ko.computed({
        read: function () {
            var val = target();
            var parts = (Math.abs(val) + "").split(".");
            var inteiro = parts[0];
            var decimal = ((parts[1] || 0) + Array(decimals + 1).join("0")).substr(0, decimals);
            var formattedValue = "R$ " + inteiro.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") + "," + decimal;
            return formattedValue;
        },
        write: function (newValue) {
            var current = target();
            var valueToWrite = formatToDouble(newValue, decimals);
            if (valueToWrite !== current)
                target(valueToWrite);
            else if (newValue !== current)
                target.notifySubscribers(valueToWrite);


        }
    }); return target;
};
function formatToDouble(str, decimals) {
    var roundingMultiplier = Math.pow(10, decimals);
    var newValueAsNum = str.replace(/[^0-9,]/g, "").replace(",", ".");
    //se caracter diferente encontrado
    if (isNaN(newValueAsNum))
        newValueAsNum = 0;
    var valueToWrite = Math.round(newValueAsNum * roundingMultiplier) / roundingMultiplier;
    return valueToWrite;
}