var ExchangeRateClient = new function () {
    this.Init = function () {
        ExchangeRateClient.BindData();
        $(window).on("resize", function () {
            var $grid = $("#ExchangeRateGrid"),
                newWidth = $grid.closest(".ui-jqgrid").parent().width();
            $grid.jqGrid("setGridWidth", newWidth, true);
        });
        $('#Keyword').bind('keypress', function (e) {
            if (e.which == 13) {
                e.stopPropagation();
                ExchangeRateClient.ReloadGrid();
            }

        });
    };

    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            window.location.href = "/ExchangeRate";
            if ($('#ExchangeRateId').val() == '0') {
                $('#ExchangeRateId').val(response.Data.ExchangeRateId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };

    this.OnAddNew = function () {
        window.location.href = "/ExchangeRate/Edit";
    };

    this.OnDelete = function (msg) {
        bootbox.confirm(msg, function (result) {
            if (result) {
                var ids = $("#ExchangeRateGrid").jqGrid('getGridParam', 'selarrrow');
                if (ids.length > 0) {
                    ExchangeRateClient.DeleteNptExchangeRates(ids);
                } else {
                    Feedback.ShowError("Please select at least one ExchangeRate.");
                }
            }
        });
    };

    this.DeleteExchangeRates = function (ids) {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/ExchangeRate/DeleteExchangeRates',
                    dataType: 'json',
                    data: { ids: ids.toString() },
                    success: function (response) {
                        Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                        ExchangeRateClient.ReloadGrid();
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };
    this.DeleteNptExchangeRates = function (ids) {

        $.ajax({
            type: 'POST',
            url: '/ExchangeRate/DeleteExchangeRates',
            dataType: 'json',
            data: { ids: ids.toString() },
            success: function (response) {
                Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                ExchangeRateClient.ReloadGrid();

            },
            error: function (response) {
                Feedback.ShowErrorResponse(response);
            }
        });

    };

    this.ReloadGrid = function () {
        $("#ExchangeRateGrid").jqGrid().setGridParam({
            url: "/ExchangeRate/GetExchangeRates?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#ExchangeRateGrid').jqGrid().trigger('reloadGrid');
    };

    this.BindData = function () {
        $("#ExchangeRateGrid").jqGrid({
            url: "/ExchangeRate/GetExchangeRates?keyword=" + $("#Keyword").val(),
            datatype: 'json',
            mtype: 'Get',
            colNames: colNames,
            colModel: [
                { key: true, hidden: true, name: 'ExchangeRateId', index: 'ExchangeRateId', editable: false },
              
                { name: 'Name', index: 'Name', editable: false },
                { name: 'Price', index: 'Price', editable: false },
                 {
                     name: 'Link', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                         var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/ExchangeRate/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
                         html += '<span style="margin-left: 8px;"><a data-toggle="tooltip" data-placement="left" title="Delete" href="javascript:void(0)"  onclick ="ExchangeRateClient.DeleteExchangeRates(\'' + options.rowId + '\')"><img src="/Content/img/common/grid/del_command.gif" class="delete_command"/></a></span>';
                         return html;

                     },
                     align: 'left', width: 60, fixed: true
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
        window.location.href = "/ExchangeRate";
    };

    this.OnDeleteEdit = function () {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/ExchangeRate/DeleteExchangeRates',
                    dataType: 'json',
                    data: { ids: $("#ExchangeRateId").val() },
                    success: function (response) {
                        window.location.href = "/ExchangeRate";
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };

};