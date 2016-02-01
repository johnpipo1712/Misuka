$.validator.methods.range = function(value, element, param) {
  var globalizedValue = value.replace(",", ".");
  return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
};

$.validator.methods.number = function(value, element) {
  return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
};

/**
(function($) {
  $.extend($.validator, {
    prototype: {
      check: function(element) {
        element = this.validationTargetFor(this.clean(element));

        if ($(element).attr("data-val") != "true") {
          //No need to validate
          alert('d');
          return true;
        }
        var rules = $(element).rules();
        var dependencyMismatch = false;
        for (var method in rules) {
          var rule = { method: method, parameters: rules[method] };
          try {
            var result = $.validator.methods[method].call(this, element.value.replace(/\r/g, ""), element, rule.parameters);

            // if a method indicates that the field is optional and therefore valid,
            // don't mark it as valid when there are no other rules
            if (result == "dependency-mismatch") {
              dependencyMismatch = true;
              continue;
            }
            dependencyMismatch = false;

            if (result == "pending") {
              this.toHide = this.toHide.not(this.errorsFor(element));
              return;
            }

            if (!result) {
              this.formatAndAdd(element, rule);
              return false;
            }
          } catch(e) {
            this.settings.debug && window.console && console.log("exception occured when checking element " + element.id
              + ", check the '" + rule.method + "' method", e);
            throw e;
          }
        }
        if (dependencyMismatch)
          return;
        if (this.objectLength(rules))
          this.successList.push(element);
        return true;
      }
    }
  });
})(jQuery);*/