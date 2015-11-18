(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){

require("./extensions/formatterBindHandler.js");
require("./components/register-all.js");

},{"./components/register-all.js":8,"./extensions/formatterBindHandler.js":9}],2:[function(require,module,exports){
ko.components.register("grid", {
    viewModel: require("./ViewModel.js"),
    template: require("./Template.html")
});
},{"./Template.html":3,"./ViewModel.js":4}],3:[function(require,module,exports){
module.exports = "﻿\r\n<button class=\"btn btn-danger\" type=\"button\" data-bind=\"visible:withDeleteAction, enable: selectedRows().length , click: deleteItem\">\r\n    <span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span> Excluir\r\n    <span class=\"badge\" data-bind=\"visible:selectedRows().length, text: selectedRows().length\"></span>\r\n</button>\r\n<!--<button class=\"btn btn-primary disabled\" type=\"button\" role=\"presentation\" data-bind=\"visible:selectedRows().length\">\r\n    Selecionados <span class=\"badge\" data-bind=\"text: selectedRows().length\"></span>\r\n</button>-->\r\n<div class=\"table-responsive\">\r\n    <table class=\"table table-bordered\">\r\n        <thead>\r\n            <tr>\r\n                <td class=\"col-sm-1 text-center\"><input type=\"checkbox\" data-bind=\"checked: checkAll\" /></td>\r\n                <!--ko foreach: collumns-->\r\n                <th data-bind=\"text:title\"></th>\r\n                <!--/ko-->\r\n            </tr>\r\n        </thead>\r\n        <tbody data-bind=\"foreach: dataSource().dataSet\">\r\n            <tr>\r\n                <td class=\"col-sm-1 text-center\"><input type=\"checkbox\" data-bind=\"value:$data.id, checked:$parent.selectedRows\" /></td>\r\n                <!--ko foreach: $parent.collumns-->\r\n                <td data-bind=\"text: $parent[$data.prop], formatter: $data.format\"></td>\r\n                <!--/ko-->\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <page-control params=\"{totalPages: totalPages, currentPage: currentPage, onGoTo: goToPage.bind($data), onNext: next.bind($data), onPrev:prev.bind($data)}\"></page-control>\r\n</div>";

},{}],4:[function(require,module,exports){
var GridDataSource = require("../../models/gridDataSource");
function GridViewModel(params) {
    params = params || {};
    var self = this;
    this.name = ko.observable(params.name);

    this.onRefresh = function () {
        console.log("refreshed!!k");
    };

    this.withDeleteAction = ko.observable(params.withDeleteAction || (params.withDeleteAction == undefined || params.withDeleteAction == null));

    this.collumns = ko.observableArray(params.collumns);

    this.dataSource = ko.observable(new GridDataSource({ url: params.url, defaultAction: params.defaulAction, onRefresh: this.onRefresh.bind(this) }));

    this.selectedRows = ko.observableArray([]);

    this.totalPages = ko.computed(function () {
        return this.dataSource().gridRequest().totalPages();
    }, this);

    this.currentPage = ko.computed(function () {
        return this.dataSource().gridRequest().currentPage();
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



GridViewModel.prototype.refresh = function () {
    this.dataSource().refresh();
}

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
    this.dataSource().delete(this.selectedRows(), this.refresh.bind(this));
};

module.exports = GridViewModel;
},{"../../models/gridDataSource":11}],5:[function(require,module,exports){
ko.components.register("page-control", {
    viewModel: require("./ViewModel.js"),
    template: require("./Template.html")
});
},{"./Template.html":6,"./ViewModel.js":7}],6:[function(require,module,exports){
module.exports = "﻿<nav data-bind=\"visible: totalPages()>1\">\r\n    <ul class=\"pagination\">\r\n        <li data-bind=\"css:{'disabled':!prevPageEnable()}\"><a aria-label=\"Previous\" data-bind=\"click:prev\"><span aria-hidden=\"true\">&laquo;</span></a></li>\r\n        <!--<li data-bind=\"css:{'disabled':!nextPageEnable()},visible: ellipisisPrev\"><span>...</span></li>-->\r\n        <!--ko foreach: pages-->\r\n        <li data-bind=\"css:{'active': $data.current}\"><a data-bind=\"text:$data.value, click: $parent.goTo.bind($parent,$data.value)\"> <span class=\"sr-only\"></span></a></li>\r\n        <!--/ko-->\r\n        <li data-bind=\"css:{'disabled':!nextPageEnable()},visible: ellipisisNext\"><span>...</span></li>\r\n        <li data-bind=\"css:{'disabled':!nextPageEnable()}\"><a aria-label=\"Next\" data-bind=\"click:next\"><span aria-hidden=\"true\">&raquo;</span></a></li>\r\n    </ul>\r\n</nav>";

},{}],7:[function(require,module,exports){
function PageControlViewModel(params) {
    this.totalPages = ko.isObservable(params.totalPages) ? params.totalPages : ko.observable(params.totalPages);
    this.currentPage = ko.isObservable(params.currentPage) ? params.currentPage : ko.observable(params.currentPage);
    this.controlSize = ko.observable(params.controlSize || 5);

    this.pages = ko.computed(function () {
        var result = [];

        if (this.totalPages() <= this.controlSize())
            for (var i = 0; i < this.totalPages() ; i++)
                result.push({ value: i + 1, current: (i + 1) === this.currentPage() });

        else if (this.currentPage() > this.controlSize())
            for (var i = (this.currentPage() - this.controlSize()) + 1 ; i <= this.currentPage() ; i++)
                result.push({ value: i, current: (i) === this.currentPage() });
        else
            for (var i = 0; i < this.controlSize() ; i++)
                result.push({ value: i + 1, current: (i + 1) === this.currentPage() });

        return result;
    }, this);

    this.prevPageEnable = ko.computed(function () {
        return this.currentPage() > 1;
    }, this);

    this.nextPageEnable = ko.computed(function () {
        return this.currentPage() < this.totalPages();
    }, this);

    this.ellipisisNext = ko.computed(function () {
        return (this.totalPages() > this.controlSize()) && (this.totalPages() - this.currentPage()) > 0;
    }, this);

    this.ellipisisPrev = ko.computed(function () {
        return (this.currentPage() - this.controlSize()) > 0;
    }, this);

    this.onNextCallback = params.onNext;
    this.onPrevCallback = params.onPrev;
    this.onGoToCallback = params.onGoTo;
    this.onChangeCallback = params.onChange;

    this.onChange = new ko.subscribable();
    this.onGoTo = new ko.subscribable();
    this.onNext = new ko.subscribable();
    this.onPrev = new ko.subscribable();

    this.onChange.subscribe(function (page) {
        this.onChangeCallback && this.onChangeCallback(page);
    }, this);
    this.onNext.subscribe(function (page) {
        this.onChange.notifySubscribers(page);
        this.onNextCallback && this.onNextCallback(page);
    }, this);
    this.onPrev.subscribe(function (page) {
        this.onChange.notifySubscribers(page);
        this.onPrevCallback && this.onPrevCallback(page);
    }, this);
    this.onGoTo.subscribe(function (page) {
        this.onChange.notifySubscribers(page);
        this.onGoToCallback && this.onGoToCallback(page);
    }, this);


};

PageControlViewModel.prototype.next = function (page) {
    if (!this.nextPageEnable()) return;
    this.onNext.notifySubscribers(page);
};
PageControlViewModel.prototype.prev = function (page) {
    if (!this.prevPageEnable()) return;
    this.onPrev.notifySubscribers(page);
};


PageControlViewModel.prototype.goTo = function (page) {
    this.onGoTo.notifySubscribers(page);
};

module.exports = PageControlViewModel;
},{}],8:[function(require,module,exports){
require("./page-control/Register");
require("./grid/Register");
ko.applyBindings({});
},{"./grid/Register":2,"./page-control/Register":5}],9:[function(require,module,exports){
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
},{}],10:[function(require,module,exports){
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
},{}],11:[function(require,module,exports){
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

    this.onRefresh = new ko.subscribable();

    this.onRefresh.subscribe(function () {
        data.onRefresh(this.gridRequest().data());
    }, this);
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
    ajaxRequest._delete("api/receitas", { ids: ids }, onDelete);
};

GridDataSource.prototype.refresh = function () {
    var self = this;
    var params = $.param(ko.toJS(this.gridRequest()));
    ajaxRequest.get(this.url(), params,
        function (response) {
            self.gridRequest(new GridRequest(response));
            self.onRefresh.notifySubscribers();
        });
};
module.exports = GridDataSource;


},{"./ajax-request":10,"./gridRequest":12}],12:[function(require,module,exports){
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
