var Buttons = { "SaveAndClose": 0, "Save": 1, "Close": 2, "None": 3 };
var ButtonKeys = { "enterKey": 13, "ESCKey": 27 };
var LocalizedStrings = { "Save": "Save", "Close": "Close" };



/*----------------------------------- INLINE DIALOG -------------------------------
Use CloseDialog function to close dialog
-----------------------------------------------------------------------------------*/
if (!InlineDialog) {
  var InlineDialog = function (options) {
    var url;
    var successHandler;
    var failureHandler;
    var title;
    var enableButtons;
    var width;
    var height;
    var $dialog;
    var dialogId;
    var dlgbuttons = {};
    var localizedLabel;
    var onClosed;
    var onLoaded;
    var customButtons;
    var hintText = '';

    var init = function (options) {
      url = options['url'];
      successHandler = options['successHandler'];
      failureHandler = options['failureHandler'];
      title = options['title'];
      enableButtons = options['buttons'];
      width = DialogUtils.ConvertWidthToPixel(options['width']);
      height = DialogUtils.ConvertHeightToPixel(options['height']);
      localizedLabel = options['localizedStrings'] || LocalizedStrings;
      onClosed = options["onClosed"];
      onLoaded = options["onLoaded"];
      customButtons = options["customButtons"];
      hintText = options['hintText'];

      if (enableButtons != undefined) {
        if (enableButtons == Buttons.SaveAndClose) {
          dlgbuttons[localizedLabel.Close] = function () { close(); };
          dlgbuttons[localizedLabel.Save] = function () { save(); };
        } else if (enableButtons == Buttons.Save) {
          dlgbuttons[localizedLabel.Save] = function () { save(); };
        } else if (enableButtons == Buttons.Close) {
          dlgbuttons[localizedLabel.Close] = function () { close(); };
        }
      }

      if (customButtons != undefined) {
        jQuery.each(customButtons, function (i, val) {
          if ((typeof val) == "object") {
            if (val.click) {
              dlgbuttons[i] = val.click;
            }
          } else
            dlgbuttons[i] = val;
        });
      }

      dialogId = Math.random().toString(16).replace(".", "") + (new Date()).valueOf().toString(16);
      var dialogContainer = $('<div>').attr('id', dialogId);
      dialogContainer.html("");
      $dialog = dialogContainer.dialog({
        autoOpen: false,
        title: title,
        resizable: false,
        modal: false,
        closeOnEscape: true,
        draggable: false,
        width: width || 'auto',
        height: height || 'auto',
        minHeight: 0,
        position: 'center',
        buttons: dlgbuttons,
        open: function (event, ui) {
          $(this).load(url, function () {
            $('#' + dialogId).find("div").first().addClass("ui-inlinedialog-content");
            var $form = $('#' + dialogId).find('form');
            DialogUtils.InitParameters($form, $(this), close, height, width);
            DialogUtils.ProcessResultCallback($form, successHandler, failureHandler);
            DialogUtils.BindingESCKey($form, close);
            if (hintText)
              $($form).find('input.focus').val(hintText);

            //hide all elements which contain "hidden" class in order not to display them on the first load
            $($form).find('.hidden').hide();

            //used for MVC client validation
            var requiredScripts = [];
            //if (!Function.createCallback) requiredScripts.push('/Common/Javascript/Framework/MicrosoftAjax.js');
            //if (!window.Sys || !Sys.Mvc || !Sys.Mvc.InsertionMode) requiredScripts.push('/Common/Javascript/Framework/MicrosoftMvcAjax.js');
            //if (!window.Sys || !Sys.Mvc || !Sys.Mvc.$create_Validation) requiredScripts.push('/Common/Javascript/Framework/MicrosoftMvcValidation.js');

            if (!Function.createCallback) requiredScripts.push('/Scripts/jquery.unobtrusive-ajax.min.js');
            if (!window.Sys || !Sys.Mvc || !Sys.Mvc.InsertionMode) requiredScripts.push('/Scripts/jquery.validate.min.js');
            if (!window.Sys || !Sys.Mvc || !Sys.Mvc.$create_Validation) requiredScripts.push('/Scripts/jquery.validate.unobtrusive.min.js');
            if (!window.Sys || !Sys.Mvc || !Sys.Mvc.$create_Validation) requiredScripts.push('/Scripts/jQueryFixes.js');
            for (var i = 0; i < requiredScripts.length; i++) {
              $.getScript(requiredScripts[i]);
            }

            //fire onLoaded event
            if (onLoaded) onLoaded(event, ui);
          });
        }
      });

      var buttonClasses = {};

      if (customButtons != undefined) {
        jQuery.each(customButtons, function (i, val) {
          if ((typeof val) == "object" && val.className) {
            buttonClasses[i] = val.className;
          }
        });
      }

      //Add default classname for "Save" button:
      if (!buttonClasses[localizedLabel.Save]) buttonClasses[localizedLabel.Save] = 'save';
      var buttonTexts = $dialog.closest(".ui-dialog").find(".ui-dialog-buttonpane > .ui-dialog-buttonset > button");
      jQuery.each(buttonClasses, function (i, val) {
        buttonTexts.each(function () {
          if (typeof val == 'string' && $(this).text() == i) $(this).addClass(val);
        });
      });

      $dialog.bind("dialogclose", function (event, ui) {
        $dialog.remove();
        $('#ui-datepicker-div').css("display", "none");
        DialogUtils.SetDialogContainerStatus(false, $dialog);
        if (onClosed)
          onClosed(event, ui);
      });

      $('<div class="dlg-container"></div>')
          .css({
            zIndex: 400000 + $('.dlg-container').length
          })
          .appendTo("body");
      var container = $.getLastElementByClass('.dlg-container');
      if ($(container).find('input.none').length <= 0)
        $(container).append('<input class="none" type="input" style="width:0; height:0; border:0;position:absolute; top:-100px;"/>');
      $(container).append($dialog.parent());
      $dialog.dialog('open');

    }(options);

    this.DialogContainer = function () {
      return $dialog;
    };

    this.CloseDialog = function () {
      close();
    };

    var close = function () {
      $dialog.dialog('close');
      return false;
    };

    var save = function () {
      var $form = $('#' + dialogId).find('form');
      if ($form.length > 0) $form.submit();
    };

    return this;
  };
};

/*----------------------------------- IFRAME DIALOG -------------------------------
User CloseDialog() function to close this dialog
-----------------------------------------------------------------------------------*/

if (!IFrameDialog) {
  var IFrameDialog = function(options) {
    var url;
    var width;
    var height;
    var title;
    var successHandler;
    var failureHandler;
    var enableButtons;
    var $dialog;
    var localizedLabel;
    var dlgbuttons = {};
    var onClosed;
    var onLoaded;
    var crossSite = false;
    var customButtons;

    var init = function(options) {
      url = options['url'];
      successHandler = options['successHandler'];
      failureHandler = options['failureHandler'];
      title = options['title'];
      enableButtons = options['buttons'];
      width = DialogUtils.ConvertWidthToPixel(options['width']);
      height = DialogUtils.ConvertHeightToPixel(options['height']);
      onClosed = options["onClosed"];
      crossSite = options["crossSite"];
      localizedLabel = options['localizedStrings'] || LocalizedStrings;
      customButtons = options["customButtons"];
      onLoaded = options["onLoaded"];

      if (enableButtons != undefined) {
        if (enableButtons == Buttons.SaveAndClose) {
        } else if (enableButtons == Buttons.Save) {
        } else if (enableButtons == Buttons.Close) {
          dlgbuttons[localizedLabel.Close] = function() { close(); };
        }
      }

      if (customButtons != undefined) {
        jQuery.each(customButtons, function(i, val) {
          dlgbuttons[i] = function() {
            executeFunction(val);
          };
        });
      }

      $dialog = jQuery.FrameDialog
        .create({
          url: url,
          title: title,
          width: width || 'auto',
          height: height || 'auto',
          autoOpen: false,
          draggable: false,
          resizable: false,
          closeOnEscape: true,
          position: 'center',
          buttons: dlgbuttons,
          OnLoaded: function(event, ui) {
            if (crossSite == undefined || !crossSite) {
              $dialog.children('iframe').contents().find("div").first().addClass("ui-inlinedialog-content");
              var $form = $dialog.children('iframe').contents().find('form');
              DialogUtils.InitParameters($form, $dialog, close, height, width);
              DialogUtils.ProcessResultCallback($dialog.children('iframe').contents().find('form'), successHandler, failureHandler);
              DialogUtils.BindingESCKey($dialog.children('iframe').contents().find('body'), close);
            }
          }
        })
        .bind('dialogbeforeclose', function(event, ui) {
          if (crossSite == undefined || !crossSite) {
            var iframeName = $dialog.attr("id") + '-VIEW';
            var frame = $(frames[iframeName]);
            if (frame[0].OnClosingDialog) frame[0].OnClosingDialog();
          }
        })
        .bind('dialogclose', function(event, ui) {
          DialogUtils.SetDialogContainerStatus(false, $dialog);
          if (onClosed) onClosed(event, ui);
        });

      // calculate max z-index
      var maxZIndex = 400000 + $('.dlg-container').length;
      $('.ui-dialog').each(function() {
        var currentZIndex = parseInt($(this).css('z-index'), 10) || 0;
        if (currentZIndex > maxZIndex) {
          maxZIndex = currentZIndex;
        }
      });
      $('<div class="dlg-container"></div>')
        .css({
          zIndex: maxZIndex + 1
        })
        .appendTo("body");
      var container = $.getLastElementByClass('.dlg-container');
      if ($(container).find('input.none').length <= 0)
        $(container).append('<input class="none" type="input" style="width:0; height:0; border:0;position:absolute; top:-100px;"/>');
      $(container).append($dialog.parent());
      $dialog.dialog('open');
      if (onLoaded) {
        onLoaded($dialog.get(0));
      }
    }(options);

    this.CloseDialog = function() {
      close();
    };

    this.GetDialogId = function() {
      return $dialog.attr("id") + '-VIEW';
    };

    var close = function() {
      $dialog.dialog('close');
      return false;
    };

    var save = function() {
      $dialog.children('iframe').contents().find('form').submit();
    };

    var executeFunction = function(val) {
      if (val != undefined) {
        var func = $.getFunctionName(val);
        eval('$dialog.children(\'iframe\')[0].contentWindow.' + func);
      }
    };
    return this;
  };
};

(function ($) {
  $.fn.hasVerticalScrollBar = function () {
    return this.get(0).scrollHeight > this.get(0).clientHeight;
  };

  $.fn.hasHorizontalScrollBar = function () {
    return this.get(0).scrollWidth > this.get(0).clientWidth;
  };

  $.fn.getCssHeight = function () {
    var height = parseFloat(this.css("height").replace("px", ""));
    if (!isNaN(height))
      return 0;
  };

  $.getFunctionName = function (val) {
    if (val != undefined) {
      var func = val.toString();
      var idx = func.indexOf("{");
      var lastIdx = func.lastIndexOf("}");
      if (idx >= 0 && lastIdx >= 0)
        func = func.substring(idx + 1, lastIdx).trim();
      return func;
    }
    return;
  };

  $.getLastElementByClass = function (name) {
    if ($(name).length > 0)
      return $(name)[$(name).length - 1];
  };

})(jQuery);

var DialogUtils = new function () {
  var menuBarHeight = 37; //include top menu
  this.BindEnterKey = function ($form, closeFunc) {
    if ($form != null) {
      $form.each(function (index) {
        //Bind submit action when users focus on any inputs or select boxes and press enter key
        //Except when those inputs or select boxes specify a class "not-submit"
        $(this).find('input, select').not('.not-submit').unbind('keypress');
        $(this).find('input, select').not('.not-submit').bind('keypress', function (e) {
          if (e.which == ButtonKeys.enterKey) {
            $(this).closest("form").submit();
            return false;
          }
          return true;
        });

        $(this).find('textarea').each(function (index) {
          $(this).bind('keypress', function (e) {
            if (e.which == ButtonKeys.enterKey) e.stopPropagation();
          });
        });

        $(this).find('a.submit').unbind('click');
        $(this).find('a.submit').bind('click', function (e) {
          $(this).closest("form").submit();
          //return false;
        });

        $(this).find('a.cancel').unbind('click');
        $(this).find('a.cancel').bind('click', function (e) {

          closeFunc();
        });
      });

      if ($form.hasClass("ajax-form")) {
        $form.submit(function (event) {
          event.preventDefault();
        });
      }
    }
  };

  this.SetDialogContainerStatus = function (show, $dialog) {
    var container = $.getLastElementByClass('.dlg-container');
    if (show) {
      if ($(container).parent('.main-body').length > 0) {//Inline dialog or the first of iframe dialog                
        $('body').css('overflow', 'hidden');
        if ($('.dlg-container').length > 1) {
          $($('.dlg-container')[$('.dlg-container').length - 2]).css("overflow", "hidden");
          DialogUtils.SetElementStatus(true, $('.dlg-container')[$('.dlg-container').length - 2]);
        }
        $(container).css('display', 'block');
      }
      else {
        $(container).css('display', 'block');
      }
    }
    else {
      var parentDlg = $(container).parent();
      $(container).remove();
      container = $.getLastElementByClass('.dlg-container');
      if (container != undefined) {
        $(container).css("overflow", "auto");
        DialogUtils.SetElementStatus(false, container);
        if ($(container).find('.focus').length > 0) {
          $(container).find('.focus').focus();
          $(container).find('.focus').unbind("keyup").keyup(function () {
            return false;
          });
        }
        else {
          $(container).find('input.none').focus();
          $(container).find('input.none').unbind("keyup").keyup(function () {
            return false;
          });
        }
      } else { //closed on child iframe dialogs
        var autoFocusElement = $(parentDlg).find('.focus').first();
        if (autoFocusElement.length) {
          autoFocusElement.focus();
        } else {
          $(parentDlg).find('input').first().focus();
        }
      }
      if ($('.main-body').length > 0) {
        if ($('.dlg-container').length == 0)
          $('body').css('overflow', 'auto');
      }
    }

  };

  this.SetElementStatus = function (disableStatus, container) {
    $($(container).children('div').children('div')[1]).children('div').children('div .content').children('form').find('input, select, textarea, a').each(function () {
      var $input = $(this);
      if (disableStatus) {
        if (!$input.prop('disabled')) {
          $input.prop('disabled', disableStatus);
          $input.attr("data-auto-disable", true);
        }
      }
      else {
        if ($input.attr("data-auto-disable") == "true") {
          $input.removeAttr('disabled');
          $input.removeAttr('data-auto-disable');
        }
      }
    });
  };

  this.BindingESCKey = function ($element, closeFunc) {
    $element.unbind('keyup');
    $element.keyup(function (e) {
      if (e.keyCode === ButtonKeys.ESCKey) { //ESC key  
        closeFunc();
      }
    });

    var container = $.getLastElementByClass('.dlg-container');
    $(container).unbind('keyup');
    $(container).keyup(function (e) {
      if (e.keyCode === ButtonKeys.ESCKey) {//ESC key                  
        closeFunc();
      }
    });
  };

  this.InitParameters = function ($form, $dialog, closeFunc, inputHeight, inputWidth) {
    DialogUtils.SetDialogContainerStatus(true, $dialog);
    DialogUtils.CalculateSize($dialog, inputHeight, inputWidth);
    DialogUtils.ResizeToolbar($dialog, inputHeight, inputWidth);
    DialogUtils.BindEnterKey($form, closeFunc);

    if ($form.find('.focus').length > 0)
      $form.find('.focus').focus();
    else
      $($.getLastElementByClass('.dlg-container')).find('input.none').focus();

    //removing this in order to avoid re-calculating dialog height when users navigate to other modules then comming back to the current module
    //Refer to bug #11059: CMS - Publishing - Edit content popup should be standard when user click to other module tab and back to it
    //        $(window).bind('resize', function () {
    //            DialogUtils.ResizeToolbar($dialog, inputHeight, inputWidth);
    //        });

    // We dont need to detect scrolling event to resize toolbar, that causes the toolbar alway re-position and make scrollbar has infinite height
    //        $($.getLastElementByClass('.dlg-container')).scroll(function () {
    //            DialogUtils.ResizeToolbar($dialog, inputHeight, inputWidth);
    //        });

    //Set tab index automatically
    var tabindex = 1;
    $form.find('input, select, textarea, a, button').each(function () {
      var $input = $(this);
      if ($input.is(':visible')) {
        $input.attr('tabindex', tabindex);
        tabindex++;
      }
    });

    if ($dialog.parent().find('.ui-dialog-buttonpane').length > 0) {
      var buttons = $dialog.parent().find('.ui-dialog-buttonpane').children();
      for (var i = buttons.length; i >= 0; i--) {
        $(buttons[i]).attr("tabindex", tabindex);
        tabindex++;
      }
    }
  };

  this.ProcessResultCallback = function ($element, successHandler, failureHandler) {
    Feedback.HideLoader();
    if (successHandler != undefined)
      $element.attr("data-successCallback", $.getFunctionName(successHandler));
    if (failureHandler != undefined)
      $element.attr("data-failureHandler", $.getFunctionName(failureHandler));
    $element.attr("data-isSaving", false);
  };

  this.CalculateSize = function ($dialog, inputHeight, inputWidth) {
    $dialog.dialog('option', 'position', $dialog.dialog('option', 'position'));
    
    // Increase parent width to recover the parent padding
    $dialog.parent().width($dialog.parent().width() + 8);

    //Calculate dialog size if not specified with, height
    if (inputHeight == undefined) {
      if ($dialog.find('.ui-inlinedialog-content .content').length > 0)
        $dialog.height($dialog.height() + 41);
    }

    if ($dialog.children('iframe').contents().find('.ui-inlinedialog-content .content').length > 0) {
      if (inputHeight == undefined) {
        if ($dialog.children('iframe').contents().find('form').find('.task-bar').length > 0) {
          $dialog.children('iframe').height($dialog.children('iframe').contents().find('.ui-inlinedialog-content').height() + 65);
          $dialog.height($dialog.children('iframe').contents().find('.ui-inlinedialog-content').height() + 65);
        }
        else {
          $dialog.children('iframe').height($dialog.children('iframe').contents().find('.ui-inlinedialog-content').height() + 41);
          $dialog.height($dialog.children('iframe').contents().find('.ui-inlinedialog-content').height() + 41);
        }
      }

      if (inputWidth == undefined) {
        $dialog.children('iframe').width($dialog.children('iframe').contents().find('.ui-inlinedialog-content .content').width() + 24);
      }
      
      var content = $dialog.children('iframe').contents().find('.ui-inlinedialog-content .content');
      //if (content.find('.fieldset').length == 1) { // Set height for the first fieldset
      //  content.find('.fieldset').height($dialog.children('iframe').height() - 135 - $dialog.children('iframe').contents().find('.ui-inlinedialog-content .header').height());
      //}
      //else if (content.find('.fieldset').length > 1) { // Set height and allow to scroll for content
      content.height($dialog.children('iframe').height() - 70 - $dialog.children('iframe').contents().find('.ui-inlinedialog-content .content-header').height()).css('overflow-y', 'hidden');
      //}
    }

    //Apply for in-line dialog
    var fieldSetCount = $dialog.find('.ui-inlinedialog-content .content .fieldset').length;
    if (fieldSetCount > 0) {
      if (fieldSetCount == 1) {
        var heightMinus = ($dialog.find('.ui-inlinedialog-content .header').height() || 0) + 32 + 34; //minus for button pannel, padding            
        $dialog.find('.ui-inlinedialog-content .content .fieldset').height($dialog.height() - heightMinus);
      }
      else {
        $dialog.find('.ui-inlinedialog-content .content').height($dialog.height() - $dialog.find('.ui-inlinedialog-content .header').height() - 34).css('overflow-y', 'hidden');
      }
    }
  };

  this.StyleCustomButton = function (event, customButtons) {
    if (customButtons != undefined) {
      if ($(event.target).parent().find('.ui-dialog-buttonpane').length > 0) {
        var buttons = $(event.target).parent().find('.ui-dialog-buttonpane').children();
        jQuery.each(customButtons, function (id, val) {
          for (var i = 0; i < buttons.length; i++) {
            if ($(buttons[i]).html() == id) {
              $(buttons[i]).addClass("dlg-custom-button");
              break;
            }
          }
        });
      }
    }
  };

  this.ResizeToolbar = function ($dialog, inputHeight, inputWidth) {
    var $container = $($.getLastElementByClass('.dlg-container'));
    $dialog.dialog('option', 'position', $dialog.dialog('option', 'position'));
    if ($dialog.parent().find('.ui-dialog-buttonpane').length > 0) {
      if ($(window).height() > ($dialog.height() + menuBarHeight + ($container.hasHorizontalScrollBar() ? 18 : 0))) {
        $dialog.parent().find('.ui-dialog-buttonpane').css('left', 1);
      }
      else {
        $dialog.parent().find('.ui-dialog-buttonpane').css('position', 'fixed');
        $dialog.parent().find('.ui-dialog-buttonpane').css('bottom', $container.hasHorizontalScrollBar() ? "17px" : "0px");
        var left = parseFloat($dialog.parent().css("left").replace("px", ""));
        if (!isNaN(left))
          $dialog.parent().find('.ui-dialog-buttonpane').css("left", left - $container.scrollLeft() + 2);
      }
      $dialog.parent().find('.ui-dialog-buttonpane').width($dialog.width() - 10);
    }
    else if ($dialog.children('iframe').contents().find('form').find('.task-bar').length > 0) {
      if ($container.hasVerticalScrollBar() || $container.height() < ($dialog.height() + menuBarHeight + ($container.hasHorizontalScrollBar() ? 18 : 0))) {
        if ($.browser.msie && $.browser.version > 7) {
          $dialog.children('iframe').contents().find('form').find('.task-bar').css("top", $dialog.height() - menuBarHeight - ($container.hasHorizontalScrollBar() ? 46 : 30) + 25);
        } else {
          $dialog.children('iframe').contents().find('form').find('.task-bar').css("top", $(".ui-dialog:visible").parent().scrollTop() + $(window).height() - menuBarHeight - ($container.hasHorizontalScrollBar() ? 46 : 30));
        }
      }
      else {
        $dialog.children('iframe').contents().find('form').find('.task-bar').css("top", "auto");
      }
    }
  };

  this.ConvertWidthToPixel = function (pixelOrPercentage) {
    if (pixelOrPercentage == undefined) return undefined;
    if (pixelOrPercentage.toString().indexOf('%') > 0) return parseFloat(pixelOrPercentage) * $(window).width() / 100;
    if (pixelOrPercentage.toString().indexOf('px') > 0) return parseInt(pixelOrPercentage);
    return pixelOrPercentage;
  };

  this.ConvertHeightToPixel = function (pixelOrPercentage) {
    if (pixelOrPercentage == undefined) return undefined;
    if (pixelOrPercentage.toString().indexOf('%') > 0) {
      var height = parseFloat(pixelOrPercentage) * $(window).height() / 100;
      return Math.min(height, $(window).height() - 50);
    }
    if (pixelOrPercentage.toString().indexOf('px') > 0) return parseInt(pixelOrPercentage);
    return pixelOrPercentage;
  };
}