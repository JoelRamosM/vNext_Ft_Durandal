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

