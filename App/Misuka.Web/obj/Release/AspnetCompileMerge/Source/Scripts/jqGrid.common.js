var JqgridUtil = new function() {
  this.HandleResponse = function(gridName, res, errorContainer) {
    var response = JSON.parse(res.responseText);
    if (response.IsError) {
      bootbox.alert(response.ErrorMessage);
      //   $("#" + errorContainer).html(response.ErrorMessage);
    } else {
      //  $("#" + errorContainer).html('');
      $("#" + gridName).trigger("reloadGrid");
    }
    return [true, '', ''];
  };

  this.HandleDeleteResponse = function(gridName, res) {
    var response = JSON.parse(res.responseText);
    if (response.IsError) {
      bootbox.alert(response.ErrorMessage);
      //   $("#" + errorContainer).html(response.ErrorMessage);
    } else {
      //  $("#" + errorContainer).html('');
      $("#" + gridName).trigger("reloadGrid");
    }
    return [true, '', ''];
  };
};


