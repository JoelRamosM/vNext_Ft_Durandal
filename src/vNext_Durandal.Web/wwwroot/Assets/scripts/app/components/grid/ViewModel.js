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