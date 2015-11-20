var GridDataSource = require("../../models/gridDataSource");
function GridViewModel(params) {
    params = params || {};
    var self = this;
    this.name = ko.observable(params.name);


    this.onRefresh = function () {
        console.log("refreshed!!k");
    };

    this.withDeleteAction = ko.observable(params.withDeleteAction || (params.withDeleteAction == undefined || params.withDeleteAction == null));
    this.withCreateAction = ko.observable(params.withCreateAction || ((params.withCreateAction == undefined || params.withCreateAction == null) && params.createAction));
    this.isMultiSelect = ko.observable(params.isMultiSelect || (params.isMultiSelect == undefined || params.isMultiSelect == null));

    this.defaulActionCallback = params.defaultAction;
    this.createActionCallback = params.createAction;

    this.collumns = ko.observableArray(params.collumns);

    this.dataSource = ko.observable(new GridDataSource({ url: params.url, defaultAction: params.defaulAction, onRefresh: this.onRefresh.bind(this) }));

    this.selectedRows = ko.observableArray([]);
    //TODO: selected rows on current page
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
GridViewModel.prototype.newItem = function () {
    this.createActionCallback && this.createActionCallback();
}
GridViewModel.prototype.defaultAction = function (rowObject) {
    console.log("default action:", rowObject);
    this.defaulActionCallback && this.defaulActionCallback(rowObject["id"]);
}

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
    this.selectedRows.removeAll();
};

module.exports = GridViewModel;