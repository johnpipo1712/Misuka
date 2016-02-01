var WebSiteLinkClient = new function () {
    this.Init = function () {
        WebSiteLinkClient.BindData();
        $(window).on("resize", function () {
            var $grid = $("#WebSiteLinkGrid"),
                newWidth = $grid.closest(".ui-jqgrid").parent().width();
            $grid.jqGrid("setGridWidth", newWidth, true);
        });
        $('#Keyword').bind('keypress', function (e) {
            if (e.which == 13) {
                e.stopPropagation();
                WebSiteLinkClient.ReloadGrid();
            }

        });
    };

    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            window.location.href = "/WebSiteLink";
            if ($('#WebSiteLinkId').val() == '0') {
                $('#WebSiteLinkId').val(response.Data.WebSiteLinkId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };

    this.OnAddNew = function () {
        window.location.href = "/WebSiteLink/Edit";
    };

    this.OnDelete = function (msg) {
        bootbox.confirm(msg, function (result) {
            if (result) {
                var ids = $("#WebSiteLinkGrid").jqGrid('getGridParam', 'selarrrow');
                if (ids.length > 0) {
                    WebSiteLinkClient.DeleteNptWebSiteLinks(ids);
                } else {
                    Feedback.ShowError("Please select at least one WebSiteLink.");
                }
            }
        });
    };

    this.DeleteWebSiteLinks = function (ids) {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/WebSiteLink/DeleteWebSiteLinks',
                    dataType: 'json',
                    data: { ids: ids.toString() },
                    success: function (response) {
                        Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                        WebSiteLinkClient.ReloadGrid();
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };
    this.DeleteNptWebSiteLinks = function (ids) {

        $.ajax({
            type: 'POST',
            url: '/WebSiteLink/DeleteWebSiteLinks',
            dataType: 'json',
            data: { ids: ids.toString() },
            success: function (response) {
                Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                WebSiteLinkClient.ReloadGrid();

            },
            error: function (response) {
                Feedback.ShowErrorResponse(response);
            }
        });

    };

    this.ReloadGrid = function () {
        $("#WebSiteLinkGrid").jqGrid().setGridParam({
            url: "/WebSiteLink/GetWebSiteLinks?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#WebSiteLinkGrid').jqGrid().trigger('reloadGrid');
    };

    this.BindData = function () {
        $("#WebSiteLinkGrid").jqGrid({
            url: "/WebSiteLink/GetWebSiteLinks?keyword=" + $("#Keyword").val(),
            datatype: 'json',
            mtype: 'Get',
            colNames: colNames,
            colModel: [
                { key: true, hidden: true, name: 'WebSiteLinkId', index: 'WebSiteLinkId', editable: false },
                {
                    name: 'ImageUrl', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                        var html = '<img src="' + rowObject.ImageUrl + '" class="edit_command"/>';
                        return html;

                    },
                    align: 'left', width: 60, fixed: true
                },
                { name: 'Name', index: 'Name', editable: false },
                { name: 'Link', index: 'Link', editable: false },
                 {
                     name: 'Link', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                         var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/WebSiteLink/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
                         html += '<span style="margin-left: 8px;"><a data-toggle="tooltip" data-placement="left" title="Delete" href="javascript:void(0)"  onclick ="WebSiteLinkClient.DeleteWebSiteLinks(\'' + options.rowId + '\')"><img src="/Content/img/common/grid/del_command.gif" class="delete_command"/></a></span>';
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
        window.location.href = "/WebSiteLink";
    };

    this.OnDeleteEdit = function () {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/WebSiteLink/DeleteWebSiteLinks',
                    dataType: 'json',
                    data: { ids: $("#WebSiteLinkId").val() },
                    success: function (response) {
                        window.location.href = "/WebSiteLink";
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };

};

