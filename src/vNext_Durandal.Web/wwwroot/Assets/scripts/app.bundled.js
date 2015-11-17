(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){

require("./extensions/formatterBindHandler.js");
require("./components/register-all.js");

},{"./components/register-all.js":5,"./extensions/formatterBindHandler.js":6}],2:[function(require,module,exports){
ko.components.register("grid", {
    viewModel: require("./ViewModel.js"),
    template: require("./Template.html")
});
},{"./Template.html":3,"./ViewModel.js":4}],3:[function(require,module,exports){
module.exports = "﻿\r\n<button class=\"btn btn-danger\" type=\"button\" data-bind=\"enable: selectedRows().length , click: deleteItem\">\r\n    <span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span> Excluir\r\n</button>\r\n<button class=\"btn btn-primary\" type=\"button\" role=\"presentation\" data-bind=\"visible:selectedRows().length\">\r\n    Selecionados <span class=\"badge\" data-bind=\"text: selectedRows().length\"></span>\r\n</button>\r\n<div class=\"table-responsive\">\r\n    <table class=\"table table-bordered\">\r\n        <thead>\r\n            <tr>\r\n                <td class=\"col-sm-1 text-center\"><input type=\"checkbox\" data-bind=\"checked: checkAll\" /></td>\r\n                <!--ko foreach: collumns-->\r\n                <th data-bind=\"text:title\"></th>\r\n                <!--/ko-->\r\n            </tr>\r\n        </thead>\r\n        <tbody data-bind=\"foreach: dataSource().dataSet\">\r\n            <tr>\r\n                <td class=\"col-sm-1 text-center\"><input type=\"checkbox\" data-bind=\"value:$data.id, checked:$parent.selectedRows\" /></td>\r\n                <!--ko foreach: $parent.collumns-->\r\n                <td data-bind=\"text: $parent[$data.prop], formatter: $data.format\"></td>\r\n                <!--/ko-->\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <nav>\r\n        <ul class=\"pagination\">\r\n            <li data-bind=\"css:{'disabled':!prevPageEnable()}\"><a aria-label=\"Previous\" data-bind=\"click:prev\"><span aria-hidden=\"true\">&laquo;</span></a></li>\r\n            <!--ko foreach: pages-->\r\n            <li data-bind=\"css:{'active': $data.current}\"><a data-bind=\"text:$data.value, click: $parent.goToPage.bind($parent,$data.value)\"> <span class=\"sr-only\">(current)</span></a></li>\r\n            <!--/ko-->\r\n            <li data-bind=\"css:{'disabled':!nextPageEnable()}\"><a aria-label=\"Next\" data-bind=\"click:next\"><span aria-hidden=\"true\">&raquo;</span></a></li>\r\n        </ul>\r\n    </nav>\r\n</div>";

},{}],4:[function(require,module,exports){
var GridDataSource = require("../../models/gridDataSource");
function GridViewModel(params) {
    params = params || {};
    var self = this;
    this.name = ko.observable(params.name);

    this.collumns = ko.observableArray(params.collumns);

    this.dataSource = ko.observable(new GridDataSource({ url: params.url, defaultAction: params.defaulAction }));

    this.selectedRows = ko.observableArray([]);


    this.pages = ko.computed(function () {
        var result = [];
        var totalPages = this.dataSource().gridRequest().totalPages();
        var currentPage = this.dataSource().gridRequest().currentPage();
        for (var i = 0; i < totalPages; i++)
            result.push({ value: i + 1, current: i + 1 === currentPage });

        return result;
    }, this);

    this.prevPageEnable = ko.computed(function () {
        return this.dataSource().gridRequest().currentPage() > 1;
    }, this);

    this.nextPageEnable = ko.computed(function () {
        var gridRequest = this.dataSource().gridRequest();
        return gridRequest.currentPage() < gridRequest.totalPages();
    }, this);

    this.checkAll = ko.computed({
        read: function () {
            return this.selectedRows().length === this.dataSource().dataSet().length;
        },
        write: function (value) {
            if (value) {
                this.dataSource().dataSet().forEach(function (item) {
                    self.selectedRows.push(item.id);
                });
            } else {
                this.selectedRows.removeAll();
            }
        },
        owner: this
    });

    this.dataSource().refresh();
};

GridViewModel.prototype.goToPage = function (pageNumber) {
    this.dataSource().goto(pageNumber);
};

GridViewModel.prototype.next = function () {
    this.dataSource().next();
};
GridViewModel.prototype.prev = function () {
    this.dataSource().prev();
};

GridViewModel.prototype.deleteItem = function () {
    this.dataSource().delete(this.selectedRows(), function (response) { console.log(response); });
};

module.exports = GridViewModel;
},{"../../models/gridDataSource":8}],5:[function(require,module,exports){
require("./grid/Register");
ko.applyBindings({});
},{"./grid/Register":2}],6:[function(require,module,exports){
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
        if (/function()/.test(format)) {
            var formatterFunction = eval("(" + format + ")");
            $(element).text(formatterFunction(text));
        }
    }

};
},{}],7:[function(require,module,exports){
function request(method, url, params, done, fail, aways) {
    var xhr = $.ajax({ method: method, url: url, data: params })
                  .done(done)
                  .fail(fail)
                  .always(aways);
    return xhr;
};
module.exports = {

    get: function (url, params, done, fail, aways) {
        return request("GET", url, params, done, fail, aways);
    },
    _delete: function (url, params, done, fail, aways) {
        return request("DELETE", url, params, done, fail, aways);
    }

};
},{}],8:[function(require,module,exports){
var GridRequest = require("./gridRequest");
var ajaxRequest = require("./ajax-request");
function GridDataSource(data) {
    data = data || {};
    var self = this;
    this.url = ko.observable(data.url || "");
    this.urlNotInformed = ko.computed(function () {
        return !this.url();
    }, this);


    this.gridRequest = ko.observable(new GridRequest());

    this.dataSet = ko.computed(function () {
        return this.gridRequest().data();
    }, this);

    this.selected = ko.observableArray();
};

GridDataSource.prototype.next = function () {
    this.gridRequest().currentPage(this.gridRequest().currentPage() + 1);
    this.refresh();
};
GridDataSource.prototype.prev = function () {
    this.gridRequest().currentPage(this.gridRequest().currentPage() - 1);
    this.refresh();
};

GridDataSource.prototype.goto = function (page) {
    this.gridRequest().currentPage(page);
    this.refresh();
};

GridDataSource.prototype.delete = function (ids, onDelete) {
    ajaxRequest._delete("api/receitas", {ids:ids}, onDelete);
};

GridDataSource.prototype.refresh = function () {
    var self = this;
    var params = $.param(ko.toJS(this.gridRequest()));
    ajaxRequest.get("api/receitas", params,
        function (response) {
            self.gridRequest(new GridRequest(response));
        });
};
module.exports = GridDataSource;


},{"./ajax-request":7,"./gridRequest":9}],9:[function(require,module,exports){
function GridRequest(data) {
    data = data || {};
    this.pageLength = ko.observable(data.pageLength || 10);
    this.query = ko.observable(data.query || "");
    this.currentPage = ko.observable(data.currentPage || 1);
    this.totalPages = ko.observable(data.totalPages || 1);
    this.data = ko.observableArray(data.data || []);
}
module.exports = GridRequest;
},{}]},{},[1]);
