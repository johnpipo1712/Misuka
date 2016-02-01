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
    this.HidePermission = function () {
        $('.dimdiss').unbind('click').click(function (e) {
            e.stopPropagation();
            $('#modalPopup').modal('hide');
            $("#modalBody").empty();

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
            url: "/Order/Ordering?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#OrderGrid').jqGrid().trigger('reloadGrid');
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
                        var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/Order/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
                        return html;

                    },
                    align: 'left', width: 30, fixed: true
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
            multiselect: true,
            loadComplete: function () {
                $("tr.jqgrow:odd").addClass('odd-row');
            },
        });


    };

    this.OnBack = function () {
        window.location.href = "/Ordering";
    };

};