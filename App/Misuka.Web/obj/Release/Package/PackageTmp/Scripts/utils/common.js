var ButtonKeys = { "enterKey": 13 };

function InitLeftMenu() {
  $(".navbar-left").superfish({

  });
}

function SetActiveMenu(action, controller) {
  var defaultAction = 'index';
  action || (action = defaultAction);
  action = action.toLowerCase();
  controller = controller.toLowerCase();

  // Active left menu
  $(".navbar-left li").removeClass("active");
  var hrefWithoutAction = '/' + controller;
  var href = hrefWithoutAction + '/' + action;
  var currentMenuLink = $(".navbar-left a").filter(function (i, e) {
    var link = $(e).attr("href").toLowerCase();
    if (link == href || (action == defaultAction && link == hrefWithoutAction)) {
      return true;
    } else {
      var subHrefs = $(e).data("subhref");
      var subLinks = subHrefs ? subHrefs.toLowerCase().split(';') : [];
      return $.grep(subLinks, function (subLink) {
        return subLink == href || (action == defaultAction && subLink == hrefWithoutAction);
      }).length > 0;
    }
  });

  if (currentMenuLink.length) {
    currentMenuLink.addClass("active");
    var currentParentMenuItems = currentMenuLink.parentsUntil(".navbar-left", "li").not(":first");
    currentParentMenuItems.children("a").addClass("active");
  }

  // Show/hide left menu
  switch (controller) {
    case 'setting':
    case 'person':
    case 'logging':
      $("#nav-Misuka").show();
      break;
      
    case 'Misuka':
      $("#nav-Misuka").show();
      
      break;
  }
}

$(document).ready(function () {
  Feedback.AutoRender();
  $('.focus').focus();
  //disable the input that contains file name:
  $('input.upload-filename').attr("disabled", "disabled");

  //hide all elements which contain "hidden" class in order not to display them on the first load
  $('.hidden').hide();

  ////init jquery datepicker for all elements marked with "datepicker" class
  //$('.datepicker').datepicker({
  //    showAnim: '',
  //    dateFormat: 'dd.mm.yy',
  //    showButtonPanel: true,
  //    showOn: 'button',
  //    buttonImage: '/Common/MockWebGrid/images/blank.gif'
  //});



  //init all expandable/collapable fieldsets
  sectionUtil.InitExpandCollapse();
  DialogUtils.BindEnterKey($('form.ajax-form'), close);

  //FlickLoading.Init();
});

var bootBoxCustom = new function () {
  this.ShowError = function (messageObj, panelId) {
    new PNotify({
        title: 'Error',
        text: messageObj,
        type: 'error'
    });
  };

};

var sectionUtil = new function () {
  this.InitExpandCollapse = function () {
    $('.ui-inlinedialog-content span.collapse').each(function () {
      $(this).closest('div.fieldset').find('.fieldset-content').hide();
    });

    $('.ui-inlinedialog-content span.expand').each(function () {
      $(this).closest('div.fieldset').find('.fieldset-content').show();
    });


    $(document).on('click', '.ui-inlinedialog-content h4', function () {
      if ($("span:eq(0)", this).hasClass('expand')) {
        $(this).closest('div.fieldset').find('.fieldset-content').hide();
        $("span:eq(0)", this).removeClass('expand');
        $("span:eq(0)", this).addClass('collapse');
      }
      else {
        $(this).closest('div.fieldset').find('.fieldset-content').show();
        $("span:eq(0)", this).removeClass('collapse');
        $("span:eq(0)", this).addClass('expand');
      }
    });
  };
};

var defaultResponse = new function () {
  this.completeHandler = function (ajaxContext, completehandler) {
    var data = ajaxContext.get_data ? $.parseJSON(ajaxContext.get_data()) : ajaxContext;

    if (completehandler) {
      completehandler(data);
    }

    if (!data.IsError) {
      defaultResponse.closeDialog();
    }
  };

  this.successHandler = function (ajaxContext, formId, panelError, sucessHandler) {
    var data = ajaxContext.get_data ? $.parseJSON(ajaxContext.get_data()) : ajaxContext;
    if (sucessHandler) {
      sucessHandler(data);
    }
    else {
      var formAttributes = getFormAttribute("data-successCallback", formId);
      if (formAttributes && eval(formAttributes))
        eval(formAttributes)(data);
    }

    if ($('#' + panelError) != undefined) {
      MvcUtil.RenderValidationErrorsFromJSON(panelError, data.Object, formId);
    }
    setFormAttribute("data-isSaving", false, formId);
    Feedback.HideLoader();
  };

  this.closeDialog = function () {
    var dialogId = parent.$('#' + window.frameElement.id).parent().attr('id');
    try {
      parent.$('#' + dialogId).dialog('close');
    }
    catch (exception)
    { }
  };

  this.failureHandler = function (ajaxContext, formId, failureHandler) {
    var data = ajaxContext.get_data ? $.parseJSON(ajaxContext.get_data()) : ajaxContext;
    if (failureHandler) {
      failureHandler(data);
    }
    else {
      var formAttributes = getFormAttribute("data-failureHandler", formId);
      if (formAttributes && eval(formAttributes))
        eval(formAttributes)(data);
    }
    setFormAttribute("data-isSaving", false, formId);
    Feedback.HideLoader();
  };

  this.beginHandler = function (ajaxContext) {
    var allForms = $(document).find('form');
    var formId = allForms.length > 0 ? $(allForms[allForms.length - 1]).attr('id') : undefined;
    defaultResponse.beginHandlerWithForm(formId);
  };

  this.beginHandlerWithForm = function (formId) {
    var _saving = getFormAttribute("data-isSaving", formId) || "false";
    if (_saving == "false") {
      Feedback.ShowLoader();
      setFormAttribute("data-isSaving", true, formId);
      return true;
    }
    return false;
  };

  var getFormAttribute = function (attrName, formId) {
    $form = formId == undefined ? $(document).find('form') : $('#' + formId);
    if ($form.length > 0 && $form.attr(attrName))
      return $form.attr(attrName);
  };

  var setFormAttribute = function (attrName, value, formId) {
    $form = formId == undefined ? $(document).find('form') : $('#' + formId);
    if ($form.length > 0)
      $form.attr(attrName, value);
  };

};

var MvcUtil = new function () {
  this.AssignDefaultButton = function (containerID, buttonID) {
    MvcUtil.AssignDefaultButtonForElements($('#' + containerID), $('#' + buttonID));
  };

  this.AssignDefaultButtonForElements = function ($container, $button) {
    $container.find('input, select').not('.not-submit').unbind('keypress');
    $container.find('input, select').not('.not-submit').bind('keypress', function (e) {
      if (e.which == ButtonKeys.enterKey) {
        $button.click();
        return false;
      }
      return true;
    });
  };

  this.RenderError = function (container, error, removeValidateErrorClass) {
    if (removeValidateErrorClass)
      $('.input-validation-error').removeClass('input-validation-error');
    var errorPanel;
    var length = $('.error-panel').length;
    if (length > 0)
      errorPanel = $('.error-panel')[length - 1];
    else
      errorPanel = $('#' + container);

    $(errorPanel).hide();
    var errorList = $(errorPanel).find('ul')[0];
    if (!errorList) {
      $(errorPanel).append('<ul/>');
      errorList = $(errorPanel).find('ul')[0];
    }
    var $errorList = $(errorList);
    $errorList.html(error);
    $(errorPanel).show();
  };

  this.RenderValidationErrors = function (container, jsonContext) {
    var data = jsonContext.get_data ? $.parseJSON(jsonContext.get_data()) : jsonContext;
    MvcUtil.RenderValidationErrorsFromJSON(container, data.Object);
  };

  this.RenderValidationErrorsFromJSON = function (container, errors, formId) {
    $('.input-validation-error').removeClass('input-validation-error');
    var errorPanel;
    var length = $('.error-panel').length;
    if (length > 0)
      errorPanel = $('.error-panel')[length - 1];
    else
      errorPanel = $('#' + container);

    $(errorPanel).hide();

    if (errors == null || errors.length == 0) {
      return;
    }

    var errorList = $(errorPanel).find('ul')[0]; // $('#' + container).find('ul')[0];
    if (!errorList) {
      $(errorPanel).append('<ul/>');
      errorList = $(errorPanel).find('ul')[0];
    }

    var $form = formId !== undefined ? $('#' + formId) : null;
    var $errorList = $(errorList);
    var html = '';
    $errorList.html(html);

    for (var i = 0; i < errors.length; i++) {
      //append error message to container
      html += '<li>' + errors[i].Value + '</li>';

      //highlight the error field
      var selector = '*[name="' + errors[i].Key + '"]';
      var $element = $form ? $($form.find(selector)) : $(selector);
      if ($element) {
        $element.addClass('input-validation-error');
      }

      //set the error message to inline validation span
      var $inlineValidationMessage = $('#' + errors[i].Key + '_validationMessage');
      if ($inlineValidationMessage) $inlineValidationMessage.attr('class', 'field-validation-error');
    }
    new PNotify({
        title: 'Notice Error',
        text: html,
        type: 'error'
    });
   // $errorList.html(html);
   // $(errorPanel).show();

    $("body, html").scrollTop(0);
  };

  this.RenderValidationJsonErrorWithoutSummary = function (container, errors, formId) {
    $('.input-validation-error').removeClass('input-validation-error');

    if (errors == null || errors.length == 0 || typeof (formId) == 'undefined') {
      return;
    }

    var $form = $('#' + formId);

    for (var i = 0; i < errors.length; i++) {

      //highlight the error field
      var selector = '*[name="' + errors[i].Key + '"]';
      var $element = $($form.find(selector));
      if ($element) {
        $element.addClass('input-validation-error');
      }

      //set the error message to inline validation span
      var $inlineValidationMessage = $('#' + errors[i].Key + '_validationMessage');
      if ($inlineValidationMessage) $inlineValidationMessage.html(errors[i].Value).attr('class', 'field-validation-error');
    }
  };
};

var Feedback = new function () {
  var playAnimate;
  this.HideLoader = function ($loaderContainer) {
    clearInterval(playAnimate);
    if ($('.loading-panel').length > 0)
      $('.loading-panel').dialog("close");
  };

  this.ShowLoader = function (noFadeOut, $loaderContainer) {

    $('.loading-panel').dialog({
      autoOpen: true,
      resizable: false,
      modal: true,
      closeOnEscape: true,
      draggable: false,
      width: 300,
      height: 180,
      minHeight: 100,
      position: 'center',
      open: function (event, ui) {
        $(".loading-panel").closest(".ui-dialog").find(".ui-dialog-titlebar").hide();
        FlickLoading.Init();
        playAnimate = setInterval(FlickLoading.PlayAnimation, 1200);
      }
    });
  };
  this.ShowWarning = function (warningMessage) {
      new PNotify({
          title: 'Information',
          text: warningMessage,
          type: 'success'
      });
  };

  this.ShowInfo = function (infoMessage) {
      new PNotify({
          title: 'Information',
          text: infoMessage,
          type: 'success'
      });
  };

  this.ShowInfoWithFadeOut = function (infoMessage) {
      new PNotify({
          title: 'Information',
          text: infoMessage,
          type: 'success'
      });

  };

  this.ShowError = function (messageObj, panelId) {
      new PNotify({
          title: 'Error',
          text: messageObj,
          type: 'error'
      });
  };
  this.ShowErrorResponse = function (response) {
      var html = $(response.responseText);
      var title = $(html)[1];
      var error = $(title).text();
      if (error == "Security Exception") {
          error = "You don't have permission on this function.";
      }
      new PNotify({
          title: 'Error',
          text: error,
          type: 'error'
      });
  };

  this.HideInfo = function () {
    $('.info-panel').find('ul').html('');
    $('.info-panel').hide();
  };

  this.HideError = function () {
    $('.error-panel').find('ul').html('');
    $('.error-panel').hide();
  };

  this.AutoRender = function () {
    if ($('#info-msg-server').html() != null && $('#info-msg-server').html().length > 0) {
      Feedback.ShowInfo($('#info-msg-server').html());
      $('#info-msg-server').remove();
    }

    if ($('#error-msg-server').html() != null && $('#error-msg-server').html().length > 0) {
      Feedback.ShowError($('#error-msg-server').html());
      $('#error-msg-server').remove();
    }

    if ($('#warning-msg-server').html() != null && $('#warning-msg-server').html().length > 0) {
      Feedback.ShowWarning($('#warning-msg-server').html());
      $('#warning-msg-server').remove();
    }
  };

  //Show loader of the current active dialog
  this.ShowCurrentDialogLoader = function () {
    var currentDialog = $('.ui-inlinedialog-content');
    if (currentDialog.length > 0) {
      currentDialog = $(currentDialog[currentDialog.length - 1]);

      var $loadingPanel = currentDialog.find('.loading-panel');
      Feedback.ShowLoader(null, $loadingPanel);
    }
  };

  this.HideCurrentDialogLoader = function () {
    Feedback.HideLoader($('.loading-panel:visible').last());
  };

};

// all these have alreday set up in np_desktop_form_init.js
String.prototype.trim = function () {
  return this.replace(/^\s+|\s+$/g, "");
};
String.prototype.ltrim = function () {
  return this.replace(/^\s+/, "");
};
String.prototype.rtrim = function () {
  return this.replace(/\s+$/, "");
};
String.prototype.format = function (args) {
  var str = this;
  return str.replace(String.prototype.format.regex, function (item) {
    var intVal = parseInt(item.substring(1, item.length - 1));
    var replace;
    if (intVal >= 0) {
      replace = args[intVal];
    } else if (intVal === -1) {
      replace = "{";
    } else if (intVal === -2) {
      replace = "}";
    } else {
      replace = "";
    }
    return replace;
  });
};
String.prototype.format.regex = new RegExp("{-?[0-9]+}", "g");

var DateType = { "DateAndTime": 1, "Date": 2, "Time": 3, "MonthYearOnly": 4 };
(function ($) {
  $.fn.validateDate = function (options) {
    var isEmpty = function (str) {
      str = str.replace(/\_/gi, '');
      str = str.replace(/\./gi, '');
      str = str.replace(/\:/gi, '');
      str = str.replace(/\//gi, '');
      if (str.length > 0)
        return false;
      return true;
    };

    var validationMsg = (options && options.message) || '*';
    var allowBlank = (options && options.allowBlank) || true;
    var dateType = (options && options.type) || DateType.Date;
    var dateformat = (options && options.dateformat) || 'dd.mm.yyyy';

    var curDate = new Date();
    var year = curDate.getFullYear();;
    var month = curDate.getMonth();
    var day = 1;//curDate.getDate();
    var minute = curDate.getMinutes();
    var hour = curDate.getHours();

    var re = /^(\d{1,2}).(\d{1,2}).(\d{4})$/; //default use format dd.mm.yyyy
    if (dateType == DateType.DateAndTime)
      re = /^(\d{1,2}).(\d{1,2}).(\d{4}) (\d{1,2}):(\d{2})$/;
    else if (dateType == DateType.Time) {
      re = /^(\d{1,2}):(\d{2})$/;
    }
    else if (dateType == DateType.MonthYearOnly) {
      re = /^(\d{1,2}).(\d{4})$/;
    }

    $(this).removeClass("input-validation-error");
    if ($(this).parent().find("span.field-validation-error").length > 0)
      $(this).parent().find("span.field-validation-error").remove();

    var value = $(this).val();
    var valid = true;
    if (value != '' && !isEmpty(value)) {
      var regs;
      if (regs = value.match(re)) {
        if (dateType == DateType.DateAndTime) {
          if (dateformat == "mm/dd/yy") {
            year = regs[3];
            month = regs[1];
            day = regs[2];
          }
          else {
            year = regs[3];
            month = regs[2];
            day = regs[1];
          }
          hour = regs[4];
          minute = regs[5];
        }
        else if (dateType == DateType.Date) {
          if (dateformat == "mm/dd/yy") {
            year = regs[3];
            month = regs[1];
            day = regs[2];
          }
          else {
            year = regs[3];
            month = regs[2];
            day = regs[1];
          }
        }
        else if (dateType == DateType.MonthYearOnly) {
          if (regs.length == 3) {
            year = regs[2];
            month = regs[1];
          }
        }
        else if (dateType == DateType.Time) {
          hour = regs[1];
          minute = regs[2];
        }

        var date = new Date(year, month - 1, day, hour, minute);
        if (date.getFullYear() != year || date.getMonth() != (month - 1) || date.getDate() != day || date.getHours() != hour || date.getMinutes() != minute)
          valid = false;
      }
      else {
        valid = false;
      }
    } else if (!allowBlank) {
      valid = false;
    }
    if (!valid) {
      $(this).addClass("input-validation-error");
      $('<span/>').addClass("field-validation-error")
              .appendTo($(this).parent('div'))
              .text(validationMsg);
      return false;
    }
    return true;
  };

})(jQuery);

var TabControl = new function () {
  this.RegisterTabstrip = function (selector, defaultTabId) {
    var tabs = [];
    var activeTab;
    var callbackFunction = arguments[2] || function () { };
    $.each($(selector).children().find('[rel]'), function (index, tab) {
      var $tab = $(tab);
      var $page = $($tab.attr('rel'));
      var $curActiveTab = $tab.closest("li.active");
      var deactiveTabId = $('a', $curActiveTab).attr('rel');
      $page.hide();
      $curActiveTab.removeClass('active');

      $tab.click(function () {
        $.each(tabs, function (i, t) {
          t.header.closest("li").removeClass('active');
          t.page.hide();
        });
        $tab.closest("li").addClass('active');
        $page.show();
        callbackFunction($tab.attr('rel'), deactiveTabId);
      });

      if (defaultTabId) {
        if ($tab.attr("rel") == defaultTabId) {
          activeTab = ({ page: $page, header: $tab });
        }
      }
      tabs.push({ page: $page, header: $tab });
    });
    if (!defaultTabId && tabs.length) activeTab = tabs[0];

    if (activeTab) {
      activeTab.page.show();
      activeTab.header.closest("li").addClass('active');
    }
  };
};

var UserSelectionTag = new function () {
  var MAX_LENGTH = 19;
  var _selectionName = "";

  function getBriefName(name) {
    var result = '';
    if (name) {
      result = name.length > (MAX_LENGTH + 3) ? name.substring(0, MAX_LENGTH).trim() + "..." : name;
    }

    return result;
  };

  this.Focus = function (selectionName) {
    _selectionName = selectionName;
  };

  this.AddTag = function (id, name, li) {
    id = id.toLowerCase();
    var selection = $("#" + _selectionName);
    var tagList = $("#" + _selectionName + '-tags');
    var autocomplete = $("#" + _selectionName + '-autoComplete');
    var autocomplete_input = $("#AutoCompleteUserIdentityId");
    var enableMultipleSelection = tagList.data("enableMultipleSelection").toLowerCase();

    // Just add if id doesn't exist
    if (selection.val().toLowerCase().indexOf(id) < 0) {
      // If single selection and selected value exists then replace instead of adding more
      if (enableMultipleSelection != "true" && selection.val().length > 0) {
        UserSelectionTag.RemoveTag(_selectionName, selection.val());
      }

      // Add to tag list
      var addedElement = String.format('<li class="item" rel="{0}" title="{1}">\
                                                    <span class="value">{2}</span>\
                                                    <a href="javascript:void(0);" class="remove" onclick="UserSelectionTag.RemoveTag(\'{3}\', \'{0}\');">X</a>\
                                                </li>', id, name, getBriefName(name), _selectionName);

      tagList.find(".tagFieldset").append(addedElement);
      UserSelectionTag.ShowTagList(tagList);

      // Add to selection
      if (selection.val() != null && selection.val().length > 0) {
        selection.val(selection.val() + ",");
      }

      selection.val(selection.val() + id);
    }

    // Clear autocomplete data
    autocomplete.val('');
    autocomplete_input.val('');
  };

  this.AddTags = function (users) {
    $.each(users, function (index, user) {
      UserSelectionTag.AddTag(user.UserIdentityId, user.FullName);
    });
  };

  this.RemoveTag = function (selectionName, id) {
    id = id.toLowerCase();
    var selection = $("#" + selectionName);
    var tagList = $("#" + selectionName + '-tags');

    if (selection.val().toLowerCase().indexOf(id) >= 0) {
      // Remove from the tag list
      tagList.find("li[rel='" + id + "']").remove();
      UserSelectionTag.ShowTagList(tagList);

      // Remove from selection
      if (selection.val().toLowerCase().indexOf("," + id) >= 0)
        selection.val(selection.val().replace("," + id, ""));
      else
        selection.val(selection.val().replace(id, ""));
    }
  };

  this.ShowTagList = function (tagList) {
    if (tagList.find("ul li").length == 0)
      tagList.hide();
    else
      tagList.show();
  };
};

/*************************************************************************/
// Use for set height of left & right sides when using Spliter page
// *************************************************************************/
var Utils = new function () {
  this.SetAutoHeight = function () {
    if ($.browser.msie) {
      $('div.guiSplitter_Left .content').css("min-height", 'auto');
      $('div.guiSplitter_Right .content').css("min-height", 'auto');
      $('div.guiSplitter_Divider').css("min-height", 'auto');
    }
    else {
      $('div.guiSplitter_Left .content').css("min-height", '');
      $('div.guiSplitter_Right .content').css("min-height", '');
      $('div.guiSplitter_Divider').css("min-height", '');
    }

    var marginTop = parseFloat($('body').css("margin-top").replace("px", ""));
    var distance = 0;

    var windowHeight = $(window).height() - marginTop || 0;

    var rightH = $('div.guiSplitter_Right').height();
    var leftH = $('div.guiSplitter_Left').height();
    var cRight = $('div.guiSplitter_Right .content').height();
    var cLeft = $('div.guiSplitter_Left .content').height();
    var panelMarginTopL = parseFloat($('div.guiSplitter_Left .content').css("margin-top").replace("px", "")) || 0;
    var panelMarginBottomL = parseFloat($('div.guiSplitter_Left .content').css("margin-bottom").replace("px", "")) || 0;
    var panelPaddingTopL = parseFloat($('div.guiSplitter_Left .content').css("padding-top").replace("px", "")) || 0;
    var panelPaddingBottomL = parseFloat($('div.guiSplitter_Left .content').css("padding-bottom").replace("px", "")) || 0;
    var panelMarginTopR = parseFloat($('div.guiSplitter_Right .content').css("margin-top").replace("px", "")) || 0;
    var panelMarginBottomR = parseFloat($('div.guiSplitter_Right .content').css("margin-bottom").replace("px", "")) || 0;
    var panelPaddingTopR = parseFloat($('div.guiSplitter_Right .content').css("padding-top").replace("px", "")) || 0;
    var panelPaddingBottomR = parseFloat($('div.guiSplitter_Right .content').css("padding-bottom").replace("px", "")) || 0;

    if (rightH < windowHeight && leftH < windowHeight) {
      distance = panelMarginTopL + panelMarginBottomL + panelPaddingBottomL + panelPaddingTopL + 4; //4 for border-width
      cLeft = windowHeight - distance;
      $('div.guiSplitter_Left .content').css("min-height", cLeft);
    }

    if (cRight < cLeft) {
      distance = panelMarginTopL + panelMarginBottomL + panelPaddingBottomL + panelPaddingTopL + 4; //4 for border-width
      $('div.guiSplitter_Divider').css("height", cLeft + distance);
      $('div.guiSplitter_Right .content').css("min-height", cLeft);
      $('div.guiSplitter_Left').css("height", "auto");

      if (cLeft + distance < leftH)
        $('div.guiSplitter_Left .content').css("min-height", leftH - distance);
    }
    else if (cRight > cLeft) {
      distance = panelMarginTopR + panelMarginBottomR + panelPaddingBottomR + panelPaddingTopR + 4; //4 for border-width
      $('div.guiSplitter_Divider').css("height", rightH);
      $('div.guiSplitter_Left').css("height", "auto");
      $('div.guiSplitter_Left .content').css("min-height", rightH - distance);
    }
  };
};

var CookieUtil = new function () {
  this.GetCookie = function (name) {
    /// <summary>Get the cookie with the approriate name</summary>
    /// <param name="name" type="String">The cookie name</param>
    /// <returns type="String">The value of the specific name stored to cookie</returns>
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
      x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
      y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
      x = x.trim();
      if (x === name) {
        return unescape(y);
      }
    }
    return null;
  };

  this.SetCookie = function (name, value, daysToExpire) {
    /// <summary>Store the value to cookies</summary>
    /// <param name="name" type="String">The cookie name</param>
    /// <param name="value" type="String">The cookie value</param>
    /// <param name="daysToExpire" type="Number">Number of day to cookie expire</param>
    /// <returns>None</returns>
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + daysToExpire);
    var currentDomain = document.domain;
    var cValue = escape(value) + ((daysToExpire == null) ? "" : "; expires=" + exdate.toUTCString());
    //add domain to get cookie from subdomain, virtual directory
    cValue = cValue + "; domain=" + currentDomain + "; path=/";
    document.cookie = name + "=" + cValue;
  };

};

var UrlUtil = new function () {

  ///<summary>
  /// Converts absolute url (if any) to relative url with assuming that desktop is always set up at webroot
  ///<summary>
  /// <param name="absoluteUrl">An url which is either absolute or relative</param>
  /// <returns>Relative url</returns>
  this.ConvertToRelativeUrl = function (absoluteUrl) {
    return absoluteUrl.toLowerCase().replace(/(https?:\/\/[^\/]*)(\/.*)/g, '$2');
  };
};

var ImgUtil = new function () {
  /// <summary>
  /// Use this method to scale a picture fit to the box, specified by <c>max_width</c> and <c>max_height</c>, and keep its proportions.
  /// For example: <img id="tooltipImage" src="/images/some-image.jpg' style="visibility:hidden" onload="scaleImage(this, 310, 240)" />"
  /// </summary>
  /// <param name="objImage">The js object of <img> tag</param>
  /// <param name="max_width" type="int"></param>
  /// <param name="max_height" type="int"></param>
  /// <returns>None</returns>
  this.ScaleImage = function (objImage, max_width, max_height) {
    var img = $(objImage);
    $(objImage).width('auto').height('auto');
    var height = img.height();
    var width = img.width();
    if (width != 0) { //don't know why but on IE sometimes, it cannot take the width of image and we'll get divide by zero exception
      var ratio = height / width;

      if (width < max_width && height < max_height) {
        if (ratio < (max_height / max_width)) width = max_width;
        else width = max_height / ratio;
      } else {
        if (height > max_height) {
          ratio = max_height / height;
          width = width * ratio;
        }
        if (width > max_width) {
          ratio = max_width / width;
          width = width * ratio;
        }
      }

      img.width(width);
    }

    img.css('visibility', 'visible');
  };
};

//this method use for ajax upload form callback
function uploadHandler(form, callbacks) {
  //function uploadHandler(form, evt, callbacks) {
  $('#' + form.target).data('handlers', callbacks);
}

//This class to handle action of Html.AjaxUploadForm when you want to use upload controls inside an ajax form
var AjaxUploadForm = new function () {
    this.BindingAsynAction = function ($forms) {
    //Call this function on document ready to init for ajax upload control
      $forms.each(function (index) {
        
      if ($(this).attr('enctype') == 'multipart/form-data') {
       
          var frame = 'f_' + $(this).attr('id');
          if ($('#' + frame).length == 0) {
              $(document.body).append('<iframe id="' + frame + '" name="' + frame + '" style="position:absolute;top:-1000px;left:-1000px"></iframe>');
          }
          $(this).attr('target', frame);
          
          $('#' + frame).load(function () {
            var contents = '';
            if ($(this).contents().find('body pre').length > 0)
              contents = $(this).contents().find('body pre').html();
            else
              contents = $(this).contents().find('body').html();

            var result = contents ? $.parseJSON(contents) : null;
            var handlers = $(this).data('handlers');
            if (handlers && handlers.onsuccess) {
                console.log(frame);
               handlers.onsuccess(result);
            }
            if (handlers && handlers.oncomplete) {
              handlers.oncomplete(result);
            }
          });
        
      } else {
          $(this).submit(function (event) {
              console.log(frame);
              return false;
        });
      }
    });
  };

  //User this to remove temporary frames used for upload. 
  //In case you use ajaxUploadForm in a dialog. You should call this method to remove iframe after the dialog is closed
  //Otherwise you may get trouble with the next upload time. It will forward to the upload result page which is supposed to be hidden inside the iframe
  this.RemoveIframe = function (formIds) {
    if (formIds == null) return;
    for (var i = 0; i < formIds.length; i++) {
      var frame = 'f_' + formIds[i];
      if ($('#' + frame).length == 0) continue;
      $('#' + frame).empty().remove();
    }
  };

};

function setFileName(source, target) {
  var path = source.value;
  if (path != '' & path.indexOf('fakepath') != 0) {
    $('#' + target).val(path.replace("C:\\fakepath\\", ""));
  }
};

(function ($) {
  m = {
    '\b': '\\b',
    '\t': '\\t',
    '\n': '\\n',
    '\f': '\\f',
    '\r': '\\r',
    '"': '\\"',
    '\\': '\\\\'
  },
$.toJSON = function (value, whitelist) {
  var a,          // The array holding the partial texts.
  i,          // The loop counter.
  k,          // The member key.
  l,          // Length.
  r = /["\\\x00-\x1f\x7f-\x9f]/g,
  v;          // The member value.

  switch (typeof value) {
    case 'string':
      return r.test(value) ?
            '"' + value.replace(r, function (a) {
              var c = m[a];
              if (c) {
                return c;
              }
              c = a.charCodeAt();
              return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16);
            }) + '"' :
            '"' + value + '"';

    case 'number':
      return isFinite(value) ? String(value) : 'null';

    case 'boolean':
    case 'null':
      return String(value);

    case 'object':
      if (!value) {
        return 'null';
      }
      if (typeof value.toJSON === 'function') {
        return $.toJSON(value.toJSON());
      }
      a = [];
      if (typeof value.length === 'number' &&
                !(value.propertyIsEnumerable('length'))) {
        l = value.length;
        for (i = 0; i < l; i += 1) {
          a.push($.toJSON(value[i], whitelist) || 'null');
        }
        return '[' + a.join(',') + ']';
      }
      if (whitelist) {
        l = whitelist.length;
        for (i = 0; i < l; i += 1) {
          k = whitelist[i];
          if (typeof k === 'string') {
            v = $.toJSON(value[k], whitelist);
            if (v) {
              a.push($.toJSON(k) + ':' + v);
            }
          }
        }
      } else {
        for (k in value) {
          if (typeof k === 'string') {
            v = $.toJSON(value[k], whitelist);
            if (v) {
              a.push($.toJSON(k) + ':' + v);
            }
          }
        }
      }
      return '{' + a.join(',') + '}';
  }
};

})(jQuery);

(function ($) {
  $.removeFirstChar = function (value, char) {
    if (value.indexOf(char) == 0) {
      value = value.substring(1);
      return value.trim();
    }
    return value;
  };

})(jQuery);




var common = new function () {
    this.UploadFileAnsy = function(classLoad,controlId, urlPost) {
        $('#' + controlId).on('change', function (e) {
            common.run_waitMe(classLoad);
            var files = e.target.files;
            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();
                    for (var x = 0; x < files.length; x++) {
                        data.append("file" + x, files[x]);
                    }

                    $.ajax({
                        type: "POST",
                        url: urlPost,
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            common.stop_waitMe(classLoad);
                            $('#imgbgcover').load('/Account/ImageCover');
                        },
                        error: function(xhr, status, p3, p4) {
                            var err = "Error " + " " + status + " " + p3 + " " + p4;
                            if (xhr.responseText && xhr.responseText[0] == "{")
                                err = JSON.parse(xhr.responseText).Message;
                              common.stop_waitMe(classLoad);
                              bootBoxCustom.ShowError("The file must be less than 5MB");
                        }
                    });
                } else {
                    common.stop_waitMe(classLoad);
                    alert("This browser doesn't support HTML5 file uploads!");
                }
            }
        });
    };
    this.submitperventDefault = function (namefrom) {
         $('form[name="' + namefrom + '"]').submit(function (event) {
            event.preventDefault();
        });
    };
    this.getFormDataTypeJson = function (formId) {

        var elems = $('#' + formId).serializeArray();
        
        var data = {};
        $.each(elems, function (i, obj) {
            data[obj.name] = obj.value;
        });

        return data;
    };

    this.getListFromDataTypeJson = function (formId) {
        var elems = $('#' + formId).serializeArray();
        
        var dataList = {};
        var index = 0;
        var objIndexold = '';
        var dataobject = {};
        $.each(elems, function (i, obj) {
            var objtmp = obj.name.split('.');
            var objName = objtmp[1];
            if (i == 0) {
                objIndexold = objtmp[0];
            }
            if (objIndexold == objtmp[0]) {
                dataobject[objName] = obj.value;
            } else {
                dataList[index] = dataobject;
                index++;
                objIndexold = objtmp[0];
                dataobject = {};
            }
            if (i == elems.length - 1) {
                index++;
                dataobject[index] = dataobject;
            }
        });
        return dataList;
    };
    this.checkImg = function (url) {
        var ext = url.split('.');
        var fileext = ext[ext.length - 1].toLowerCase();
        if (fileext.indexOf('png') != -1 || fileext.indexOf('jpg') != -1 || fileext.indexOf('jpeg') != -1) {
   
            return true;
        }
        return false;
    };
    
    
   
    this.checkFileSize = function(btnSubmit, idfile) {
        $("#" + btnSubmit).click(function() {
            var input = document.getElementById(idfile);
            // check for browser support (may need to be modified)
            if (input.files && input.files.length == 1) {
                if (input.files[0].size > 5242880) {
                    bootBoxCustom.ShowError("The file must be less than 5MB");
                    return false;
                }
            }

            return true;
        });
    };
   
    this.LayoutGo2Top = function () {

        var handle = function () {
            var currentWindowPosition = $(window).scrollTop(); // current vertical position
            if (currentWindowPosition > 300) {
                $(".c-layout-go2top").show();
            } else {
                $(".c-layout-go2top").hide();
            }
        };


        handle(); // call headerFix() when the page was loaded

        if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
            $(window).bind("touchend touchcancel touchleave", function (e) {
                handle();
            });
        } else {
            $(window).scroll(function () {
                handle();
            });
        }

        $(".c-layout-go2top").on('click', function (e) {
            e.preventDefault();
            $("html, body").animate({
                scrollTop: 0
            }, 600);
        });

    
    };
    this.run_waitMe = function(classname) {
        $('.' + classname).waitMe({
            effect: 'win8_linear',
            text: '',
            bg: 'rgba(255,255,255,0.7)',
            color: '#000',
            sizeW: '',
            sizeH: '',
            source: 'img.svg'
        });
    };
    this.stop_waitMe = function (classname) {
        $('.' + classname).waitMe('hide');
    };
    this.setWidthOfBrower = function (width) {
        var nVer = navigator.appVersion;
        var nAgt = navigator.userAgent;
        var browserName = navigator.appName;
        var fullVersion = '' + parseFloat(navigator.appVersion);
        var majorVersion = parseInt(navigator.appVersion, 10);
        var nameOffset, verOffset, ix;

        // In Opera, the true version is after "Opera" or after "Version"
        if ((verOffset = nAgt.indexOf("Opera")) != -1) {
            browserName = "Opera";
            fullVersion = nAgt.substring(verOffset + 6);
            if ((verOffset = nAgt.indexOf("Version")) != -1)
                fullVersion = nAgt.substring(verOffset + 8);
        }
            // In MSIE, the true version is after "MSIE" in userAgent
        else if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
            browserName = "Microsoft Internet Explorer";
            fullVersion = nAgt.substring(verOffset + 5);
        }
            // In Chrome, the true version is after "Chrome" 
        else if ((verOffset = nAgt.indexOf("Chrome")) != -1) {
            browserName = "Chrome";
            fullVersion = nAgt.substring(verOffset + 7);
        }
            // In Safari, the true version is after "Safari" or after "Version" 
        else if ((verOffset = nAgt.indexOf("Safari")) != -1) {
            width = 'auto';
            browserName = "Safari";
            fullVersion = nAgt.substring(verOffset + 7);
            if ((verOffset = nAgt.indexOf("Version")) != -1)
                fullVersion = nAgt.substring(verOffset + 8);
        }
            // In Firefox, the true version is after "Firefox" 
        else if ((verOffset = nAgt.indexOf("Firefox")) != -1) {
            browserName = "Firefox";
            fullVersion = nAgt.substring(verOffset + 8);
        }
            // In most other browsers, "name/version" is at the end of userAgent 
        else if ((nameOffset = nAgt.lastIndexOf(' ') + 1) < (verOffset = nAgt.lastIndexOf('/'))) {
            width = 'auto';
            browserName = nAgt.substring(nameOffset, verOffset);
            fullVersion = nAgt.substring(verOffset + 1);
            if (browserName.toLowerCase() == browserName.toUpperCase()) {
                browserName = navigator.appName;
            }
        }

        return width;
    };
    this.setNavigationMenuBackEnd = function () {
        var path = window.location.pathname;
        var search = window.location.search;
        var flag = false;
        if (search != '') {
            path = path + window.location.search;
            flag = true;
        }

        $("#side-menu a").each(function () {
            var href = $(this).attr('href');
            if (flag) {
                if (path == href) {
                    console.log(href);
                    $(this).closest('li').addClass('active');
                    $(this).closest('ul').addClass('in');
                    $(this).parent().parent().parent().closest('li').addClass('active');
                    $(this).parent().parent().parent().parent().closest('li').addClass('active');
                }
            } else {
                if (path.substring(0, href.length) === href) {
                    $(this).closest('li').addClass('active');
                    $(this).closest('ul').addClass('in');
                    $(this).parent().parent().parent().closest('li').addClass('active');
                    $(this).parent().parent().parent().parent().closest('li').addClass('active');
                }
            }
            
        });
    };
    this.setNavigationMenuFrontEnd = function () {
        var path = window.location.pathname;
        var flag = false;
      
        $(".c-menu-type-classic a").each(function () {
            var href = $(this).attr('href');
            if (flag) {
                if (path == href) {
                    $(this).closest('li').addClass('c-active');
                    $(this).parent().parent().closest('li').addClass('c-active');
                }
            } else {
                if (path.substring(0, href.length) === href) {
                    $(this).closest('li').addClass('c-active');
                    $(this).parent().parent().closest('li').addClass('c-active');
                }
            }

        });
    };
    this.kendoGridRefresh = function (gridId) {
        $('#' + gridId).data('kendoGrid').dataSource.read();
        $('#' + gridId).data('kendoGrid').refresh();
    };
    // END
    
};



