var TypeMemberClient = new function () {
    this.Init = function () {
        TypeMemberClient.BindData();
        $(window).on("resize", function () {
            var $grid = $("#TypeMemberGrid"),
                newWidth = $grid.closest(".ui-jqgrid").parent().width();
            $grid.jqGrid("setGridWidth", newWidth, true);
        });
        $('#Keyword').bind('keypress', function (e) {
            if (e.which == 13) {
                e.stopPropagation();
                TypeMemberClient.ReloadGrid();
            }

        });
    };

    this.OnSuccessed = function (response) {
        if (!response.IsError && response.Data.Success) {
            window.location.href = "/TypeMember";
            if ($('#TypeMemberId').val() == '0') {
                $('#TypeMemberId').val(response.Data.TypeMemberId);
            }
        }
    };

    this.OnFailed = function (jsonData) {
        Feedback.ShowError(jsonData);
    };

    this.OnAddNew = function () {
        window.location.href = "/TypeMember/Edit";
    };

    this.OnDelete = function (msg) {
        bootbox.confirm(msg, function (result) {
            if (result) {
                var ids = $("#TypeMemberGrid").jqGrid('getGridParam', 'selarrrow');
                if (ids.length > 0) {
                    TypeMemberClient.DeleteNptTypeMembers(ids);
                } else {
                    Feedback.ShowError("Please select at least one TypeMember.");
                }
            }
        });
    };

    this.DeleteTypeMembers = function (ids) {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/TypeMember/DeleteTypeMembers',
                    dataType: 'json',
                    data: { ids: ids.toString() },
                    success: function (response) {
                        Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                        TypeMemberClient.ReloadGrid();
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };
    this.DeleteNptTypeMembers = function (ids) {

        $.ajax({
            type: 'POST',
            url: '/TypeMember/DeleteTypeMembers',
            dataType: 'json',
            data: { ids: ids.toString() },
            success: function (response) {
                Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
                TypeMemberClient.ReloadGrid();

            },
            error: function (response) {
                Feedback.ShowErrorResponse(response);
            }
        });

    };

    this.ReloadGrid = function () {
        $("#TypeMemberGrid").jqGrid().setGridParam({
            url: "/TypeMember/GetTypeMembers?keyword=" + $("#Keyword").val(),
            page: 1
        });
        $('#TypeMemberGrid').jqGrid().trigger('reloadGrid');
    };

    this.BindData = function() {
        $("#TypeMemberGrid").jqGrid({
            url: "/TypeMember/GetTypeMembers?keyword=" + $("#Keyword").val(),
            datatype: 'json',
            mtype: 'Get',
            colNames: colNames,
            colModel: [
                { key: true, hidden: true, name: 'TypeMemberId', index: 'TypeMemberId', editable: false },
                { name: 'Name', index: 'Name', editable: false },
                { name: 'ScoresFrom', index: 'ScoresFrom', editable: false },
                { name: 'ScoresTo', index: 'ScoresTo', editable: false },
                { name: 'PercentDownPayment', index: 'PercentDownPayment', editable: false },
                {
                    name: 'Link',
                    cellattr: function() { return ' title=""'; },
                    formatter: function(cellvalue, options, rowObject) {
                        var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/TypeMember/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
                        //html += '<span style="margin-left: 8px;"><a data-toggle="tooltip" data-placement="left" title="Delete" href="javascript:void(0)"  onclick ="TypeMemberClient.DeleteTypeMembers(\'' + options.rowId + '\')"><img src="/Content/img/common/grid/del_command.gif" class="delete_command"/></a></span>';
                        return html;

                    },
                    align: 'left',
                    width: 60,
                    fixed: true
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
            loadComplete: function() {
                $("tr.jqgrow:odd").addClass('odd-row');
            },
        });


    };

    this.OnBack = function () {
        window.location.href = "/TypeMember";
    };

    this.OnDeleteEdit = function () {
        bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: '/TypeMember/DeleteTypeMembers',
                    dataType: 'json',
                    data: { ids: $("#TypeMemberId").val() },
                    success: function (response) {
                        window.location.href = "/TypeMember";
                    },
                    error: function (response) {
                        Feedback.ShowErrorResponse(response);
                    }
                });
            }
        });
    };

};