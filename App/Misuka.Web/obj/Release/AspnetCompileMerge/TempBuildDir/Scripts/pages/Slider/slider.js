var SliderClient = new function () {
  this.Init = function () {
    SliderClient.BindData();
    $(window).on("resize", function () {
      var $grid = $("#SliderGrid"),
          newWidth = $grid.closest(".ui-jqgrid").parent().width();
      $grid.jqGrid("setGridWidth", newWidth, true);
    });
    $('#Keyword').bind('keypress', function (e) {
      if (e.which == 13) {
        e.stopPropagation();
        SliderClient.ReloadGrid();
      }

    });
  };

  this.OnSuccessed = function (response) {
    if (!response.IsError && response.Data.Success) {
      window.location.href = "/Slider";
      if ($('#SliderId').val() == '0') {
        $('#SliderId').val(response.Data.SliderId);
      }
    }
  };

  this.OnFailed = function (jsonData) {
    Feedback.ShowError(jsonData);
  };

  this.OnAddNew = function () {
    window.location.href = "/Slider/Edit";
  };

  this.OnDelete = function (msg) {
    bootbox.confirm(msg, function (result) {
      if (result) {
        var ids = $("#SliderGrid").jqGrid('getGridParam', 'selarrrow');
        if (ids.length > 0) {
          SliderClient.DeleteNptSliders(ids);
        } else {
          Feedback.ShowError("Please select at least one Slider.");
        }
      }
    });
  };

  this.DeleteSliders = function (ids) {
    bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
      if (result) {
        $.ajax({
          type: 'POST',
          url: '/Slider/DeleteSliders',
          dataType: 'json',
          data: { ids: ids.toString() },
          success: function (response) {
            Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
            SliderClient.ReloadGrid();
          },
          error: function (response) {
            Feedback.ShowErrorResponse(response);
          }
        });
      }
    });
  };
  this.DeleteNptSliders = function (ids) {

    $.ajax({
      type: 'POST',
      url: '/Slider/DeleteSliders',
      dataType: 'json',
      data: { ids: ids.toString() },
      success: function (response) {
        Feedback.ShowInfoWithFadeOut(messages.DATA_DELETE_SUCCESS);
        SliderClient.ReloadGrid();

      },
      error: function (response) {
        Feedback.ShowErrorResponse(response);
      }
    });

  };

  this.ReloadGrid = function () {
    $("#SliderGrid").jqGrid().setGridParam({
      url: "/Slider/GetSliders?keyword=" + $("#Keyword").val(),
      page: 1
    });
    $('#SliderGrid').jqGrid().trigger('reloadGrid');
  };

  this.BindData = function () {
    $("#SliderGrid").jqGrid({
      url: "/Slider/GetSliders?keyword=" + $("#Keyword").val(),
      datatype: 'json',
      mtype: 'Get',
      colNames: colNames,
      colModel: [
          { key: true, hidden: true, name: 'SliderId', index: 'SliderId', editable: false },
          {
            name: 'ImageUrl', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
                var html = '<img src="' + rowObject.ImageURL + '" class="edit_command"/>';
                return html;
            },
            align: 'left', width: 60, fixed: true
          },
          { name: 'Name', index: 'Name', editable: false },
          { name: 'Description', index: 'Description', editable: false },
           {
             name: 'Link', cellattr: function () { return ' title=""'; }, formatter: function (cellvalue, options, rowObject) {
               var html = '<a data-toggle="tooltip" data-placement="left" title="Edit" href="/Slider/Edit?id=' + options.rowId + '"><img src="/Content/img/common/grid/edit_command.gif" class="edit_command"/></a>';
               html += '<span style="margin-left: 8px;"><a data-toggle="tooltip" data-placement="left" title="Delete" href="javascript:void(0)"  onclick ="SliderClient.DeleteSliders(\'' + options.rowId + '\')"><img src="/Content/img/common/grid/del_command.gif" class="delete_command"/></a></span>';
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
    window.location.href = "/Slider";
  };

  this.OnDeleteEdit = function () {
    bootbox.confirm(messages.DELETE_COMFIRM_MESSAGE, function (result) {
      if (result) {
        $.ajax({
          type: 'POST',
          url: '/Slider/DeleteSliders',
          dataType: 'json',
          data: { ids: $("#SliderId").val() },
          success: function (response) {
            window.location.href = "/Slider";
          },
          error: function (response) {
            Feedback.ShowErrorResponse(response);
          }
        });
      }
    });
  };

};