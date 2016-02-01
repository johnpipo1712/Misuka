var OrderClient = new function () {
    this.Init = function () {
        OrderClient.BindData();
        $(window).on("resize", function () {
            var $grid = $("#OrderGrid"),
                newWidth = $grid.closest(".ui-jqgrid").parent().width();
            $grid.jqGrid("setGridWidth", newWidth, true);
        });
        $('#Keyword').bind('keypress', function (e) {
            if (e.which == 13) {
                e.stopPropagation();
                OrderClient.ReloadGrid();
            }

        });
    };
  
    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            Feedback.ShowInfoWithFadeOut(messages.DATA_SAVED_SUCCESS);
            if ($('#OrderId').val() == '0') {
                $('#OrderId').val(response.Data.OrderId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };



    this.ReloadGrid = function () {
        $("#OrderGrid").jqGrid().setGridParam({
            url: "/Ordering/GetOrders?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#OrderGrid').jqGrid().trigger('reloadGrid');
    };
    this.ShowEdit = function (id) {
        $('#modalPopup').modal('show');
        $("#modalBody").empty();
        $("#modalBody").load("Ordering/Edit?id=" + id);

    };
    this.ShowOrder = function (id) {
        $('#modalPopup').modal('show');
        $("#modalBody").empty();
        $("#modalBody").load("Ordering/EditOrderingFollowingOrder?id=" + id);

    };
    this.ShowUSD = function (id) {
        $('#modalPopup').modal('show');
        $("#modalBody").empty();
        $("#modalBody").load("Ordering/EditOrderingFollowingUSD?id=" + id);

    };
    this.ShowDone = function (id) {
        $('#modalPopup').modal('show');
        $("#modalBody").empty();
        $("#modalBody").load("Ordering/EditOrderingFollowingDone?id=" + id);

    };
    this.BindData = function () {
        $("#OrderGrid").jqGrid({
            url: "/Ordering/GetOrders?keyword=" + $("#Keyword").val(),
            datatype: 'json',
            mtype: 'Get',
            colNames: colNames,
            colModel: [
                { key: true, hidden: true, name: 'OrderId', index: 'OrderId', editable: false },
                { name: 'OrderingCode', index: 'OrderingCode', editable: false },
                { name: 'Price', index: 'Price', editable: false },
                {
                    name: 'Action', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                        var html = '';
                        html += '<a data-toggle="tooltip" data-placement="left" title="Order" onclick="OrderClient.ShowOrder(\'' + options.rowId + '\')" href="javascript:;"><img src="/Content/img/common/grid/report.png" class="edit_command"/></a>';

                        html += '<a data-toggle="tooltip" data-placement="left" title="USD" onclick="OrderClient.ShowUSD(\'' + options.rowId + '\')" href="javascript:;"><img src="/Content/img/common/grid/report.png" class="edit_command"/></a>';

                        html += '<a data-toggle="tooltip" data-placement="left" title="Done" onclick="OrderClient.ShowDone(\'' + options.rowId + '\')" href="javascript:;"><img src="/Content/img/common/grid/report.png" class="edit_command"/></a>';

                        return html;

                    },
                    align: 'left', width: 90, fixed: true
                }
            ],
            pager: jQuery('#pager'),
            rowNum: 50,
            rowList: [50, 100],
            height: 'auto',
            viewrecords: true,
            emptyrecords: messages.NO_DATA_DISPLAY,
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            multiselect: false,
            loadComplete: function () {
                $("tr.jqgrow:odd").addClass('odd-row');
            },
        });


    };

    this.OnBack = function () {
        window.location.href = "/Order";
    };

};