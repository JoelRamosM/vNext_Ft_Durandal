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