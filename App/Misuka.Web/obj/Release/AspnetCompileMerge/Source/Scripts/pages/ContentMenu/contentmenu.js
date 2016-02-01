var ContentMenuClient = new function () {
    this.Init = function () {
        ContentMenuClient.BindData();
        $(window).on("resize", function () {
            var $grid = $("#ContentMenuGrid"),
                newWidth = $grid.closest(".ui-jqgrid").parent().width();
            $grid.jqGrid("setGridWidth", newWidth, true);
        });
        $('#Keyword').bind('keypress', function (e) {
            if (e.which == 13) {
                e.stopPropagation();
                ContentMenuClient.ReloadGrid();
            }

        });
    };

    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            window.location.href = "/ContentMenu";
            if ($('#ContentMenuId').val() == '0') {
                $('#ContentMenuId').val(response.Data.ContentMenuId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };

    this.OnAddNew = function () {
        window.location.href = "/ContentMenu/Edit";
    };

    this.ReloadGrid = function () {
        $("#ContentMenuGrid").jqGrid().setGridParam({
            url: "/ContentMenu/GetContentMenus?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#ContentMenuGrid').jqGrid().trigger('reloadGrid');
    };

    this.BindData = function () {
        $("#ContentMenuGrid").jqGrid({
            url: "/ContentMenu/GetContentMenus?keyword=" + $("#Keyword").val(),
            datatype: 'json',
            mtype: 'Get',
            colNames: colNames,
            colModel: [
                { key: true, hidden: true, name: 'ContentMenuId', index: 'ContentMenuId', editable: false },
                { name: 'Title', index: 'Title', editable: false },
                {
                    name: 'Link', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                        var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/ContentMenu/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
                        return html;

                    },
                    align: 'left', width: 30, fixed: true
                }
            ],
            pager: jQuery('#pager'),
            rowNum: 20,
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
        window.location.href = "/ContentMenu";
    };

};