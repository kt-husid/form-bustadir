function dynamicAddRemoveElements(newAmount, currentAmount, targetId, templateId, newElementIdPrefix, callbackOnAdd) {
    var newAmount = parseInt(newAmount);
    var target = $(targetId);
    var diff = newAmount - currentAmount;
    var from = currentAmount;
    var to = currentAmount == 0 ? diff : currentAmount + diff;
    //alert(from + " / " + to);
    var template = $(templateId).clone();
    for (var i = from; i < to; i++) {
        var newId = newElementIdPrefix + i;
        template.attr('id', newId).attr("type", "text");
        //callbackOnAdd(template);
        target.after(template);
        console.log(template);
    }
    if (diff < 0) {
        for (var i = from; i >= to; i--) {
            var newId = "#" + newElementIdPrefix + i;
            //console.log("remove: " + templateId);
            $(newId).remove();
        }
    }
    return newAmount;
};
//function EnableToggleFor(triggerId, target, callback, yes, no) {
//    $(triggerId).on('click', function () {
//        var self = $(this);
//        var value = $(target);
//        var currentValue = value.val().toLowerCase() === 'true';
//        if (currentValue) {
//            self.text(no ? no : "Nei");
//        } else {
//            self.text(yes ? yes : "Ja");
//        }
//        var newValue = currentValue ? false : true;
//        self.toggleClass("btn-primary", newValue);
//        value.val(newValue);
//        callback(newValue);
//    });
//};
function EnableDisableToggleFor(triggerId, target, callback) {
    $(triggerId).on('click', function () {
        var self = $(this);
        var value = $(target);
        var currentValue = value.val().toLowerCase() === 'true';
        var toggleValue = self.data('value');
        self.parent().find('.btn').each(function (k, v) {
            console.log($(v));
            $(v).toggleClass("btn-primary").toggleClass("btn-default").toggleClass('active');
        });
        var newValue = currentValue ? false : true;
        value.val(newValue);
        callback(newValue);
    });
};
var AppWizard = function (startIndex, hideBackOnEnd, hideForwardOnStart, hideForwardOnEnd) {
    var self = this;
    var currentIndex = -1;
    var currentTabId = "tab1";
    var backButton = $("#back");
    var forwardButton = $("#forward");
    var navItem = $(".wizard-nav-item");
    var forwardAction = function () {
        self.goTo(currentIndex + 1);
    };
    self.getCurrentIndex = function () {
        return currentIndex;
    };
    self.goTo = function (index, fadeSpeed) {
        //alert("index: " + index);
        //var event = { 'type': 'onWizardBeforeNavigate', 'index': index };
        var event = $.Event('onWizardBeforeNavigate', { 'index': index, 'currentIndex': currentIndex });

        //alert(event.isDefaultPrevented());
        $(document).trigger(event);
        if (event.isDefaultPrevented() === false) {
            fadeSpeed = fadeSpeed ? fadeSpeed : 400;
            index = parseInt(index);
            var total = $("#navigation").find('li').length;
            if (index >= 0 && index < total && index != currentIndex) {
                var current = index + 1;
                var percent = (current / total) * 100;
                var pctStr = MathRound(percent) + '%';
                $('#wizard').find('.progress-bar').css({ width: pctStr }).text(pctStr);
                var content = $('.wizard-content');
                //console.log(index + " / " + total + ", pct: " + percent + ", pctStr: " + pctStr);
                //console.log("currentTabId: " + currentTabId + ", new tab id: " + tabId);
                if (currentIndex < 0) {
                    content.find('#tab' + index).fadeIn(fadeSpeed, function () {
                        // done
                    });
                }
                else {
                    content.find('#tab' + currentIndex).fadeOut(fadeSpeed, function () {
                        content.find('#tab' + index).fadeIn("normal", function () {
                            // done
                        });
                    });
                }
                currentIndex = index;
            }
            if (index == 0) {
                $(document).trigger("onWizardStart");
                backButton.find("a").hide();
                if (hideForwardOnStart) {
                    forwardButton.find("a").hide();
                }
            }
            else {
                $(document).trigger({ 'type': 'onWizardNavigate', 'index': index, 'currentIndex': currentIndex });
                backButton.find("a").show();
            }
            if (index == total - 1) {
                $(document).trigger("onWizardEnd");
                if (hideForwardOnEnd) {
                    forwardButton.find("a").hide();
                }
                if (hideBackOnEnd) {
                    backButton.find("a").hide();
                }
            }
            else {
                $(document).trigger({ 'type': 'onWizardNavigate', 'index': index, 'currentIndex': currentIndex });
                forwardButton.find("a").show();
            }
        }
    };
    self.setForwardAction = function (action) {
        forwardButton.unbind("click").click(function () {
            if (action) {
                action();
            }
        })
    };
    self.resetForwardAction = function () {
        forwardButton.unbind("click").click(function () {
            forwardAction();
        })
    };
    self.setForwardText = function (txt) {
        forwardButton.find("a").text(txt);
    };
    self.setBackText = function (text) {
        backButton.find("a").text(text);
    };
    self.setForwardEnabled = function (state) {
        forwardButton.find("a").attr('disabled', !state);
        //forwardButton.attr('disabled', !state);
    };
    self.setBackEnabled = function (state) {
        backButton.find("a").attr('disabled', !state);
        //backButton.attr('disabled', !state);
    };
    self.setForwardActive = function (state) {
        //forwardButton.find("a").attr('disabled', !state);
        if (state) {
            forwardButton.show();
        } else {
            forwardButton.hide();
        }
    };
    self.setBackActive = function (state) {
        if (state) {
            backButton.show();
        } else {
            backButton.hide();
        }
    };
    backButton.click(function () {
        self.goTo(currentIndex - 1);
    });
    forwardButton.click(function () {
        self.goTo(currentIndex + 1);
    });
    navItem.click(function (e) {
        var isEnabled = $(this).data("wizard-enabled");
        console.log(isEnabled);
        if (isEnabled == undefined || (undefined != undefined && isEnabled.toLowerCase() == 'true')) {
            self.goTo($(this).data("wizard-id"));
        }
    });
    if (startIndex) {
        self.goTo(startIndex);
    }
};
function GetCountryInfo(countryCode, callback) {
    $.getJSON("http://restcountries.eu/rest/v1/callingcode/" + countryCode, function (result) {
        var obj = result[0];
        console.log(obj);
        if (callback) {
            callback(obj);
        }
    });
};
function applySSNRule() {
    $(".ssn-number").kendoMaskedTextBox({
        mask: "000000-0000",
        unmaskOnPost: true,
        clearPromptChar: true
    });
};
function applyCurrencyRule() {
    $(".currency").kendoNumericTextBox({
        //culture: "fo",
        format: "c0",
        decimals: 0,
        min: 0,
        max: 99999999,
        spinners: false
    });
};
function applyNumericPositiveValueRule() {
    $(".numeric-positive-value").kendoNumericTextBox({
        //culture: "fo",
        format: "#",
        decimals: 0,
        min: 0,
        spinners: false
    });
};

function FixArrayData(data) {
    var newData = data;
    var itemsToRemove = [];
    $.each(data, function (i, v) {
        var val = v.value.trim();
        var name = v.name.trim();
        if (val) {
            //itemsToRemove.push(name);
            if (!_.isObject(val) && !_.isArray(val)) {
                if (val.toLowerCase() == 'on') {
                    newData[i].value = true;
                }
                else if (val.toLowerCase() == 'off') {
                    newData[i].value = false;
                }
            }
        }
    });
    $.each(itemsToRemove, function (i, v) {
        newData = _.without(newData, _.findWhere(newData, { name: v }));
    });
    return newData;
};

function validate(callbackSuccess, callbackError) {
    //alert("?");
    var isValid = validator.validate();
    if (isValid) {
        if (callbackSuccess) {
            callbackSuccess();
        }
    }
    else {
        var errors = validator.errors();
        var ul = $("<ul />");
        $(errors).each(function (k, v) {
            //$("#errors").append(this);
            //console.log(k);
            //console.log(v);
            ul.append($("<li />").text(this));
            //console.log($(this));
        });
        $("#errors").html(ul);
        console.log(ul);
        if (callbackError) {
            callbackError();
        }
    }
    return isValid;
};

function SendMail(data) {
    var url = "/Tilmelding/SendMail";
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (data) {
            console.log(data);
        }
    });
};

/*!
 * jQuery toDictionary() plugin
 *
 * Version 1.2 (11 Apr 2011)
 *
 * Copyright (c) 2011 Robert Koritnik
 * Licensed under the terms of the MIT license
 * http://www.opensource.org/licenses/mit-license.php
 */

(function ($) {

    // #region String.prototype.format
    // add String prototype format function if it doesn't yet exist
    if ($.isFunction(String.prototype.format) === false) {
        String.prototype.format = function () {
            var s = this;
            var i = arguments.length;
            while (i--) {
                s = s.replace(new RegExp("\\{" + i + "\\}", "gim"), arguments[i]);
            }
            return s;
        };
    }
    // #endregion

    // #region Date.prototype.toISOString
    // add Date prototype toISOString function if it doesn't yet exist
    if ($.isFunction(Date.prototype.toISOString) === false) {
        Date.prototype.toISOString = function () {
            var pad = function (n, places) {
                n = n.toString();
                for (var i = n.length; i < places; i++) {
                    n = "0" + n;
                }
                return n;
            };
            var d = this;
            return "{0}-{1}-{2}T{3}:{4}:{5}.{6}Z".format(
                d.getUTCFullYear(),
                pad(d.getUTCMonth() + 1, 2),
                pad(d.getUTCDate(), 2),
                pad(d.getUTCHours(), 2),
                pad(d.getUTCMinutes(), 2),
                pad(d.getUTCSeconds(), 2),
                pad(d.getUTCMilliseconds(), 3)
            );
        };
    }
    // #endregion

    var _flatten = function (input, output, prefix, includeNulls) {
        if ($.isPlainObject(input)) {
            for (var p in input) {
                if (includeNulls === true || typeof (input[p]) !== "undefined" && input[p] !== null) {
                    _flatten(input[p], output, prefix.length > 0 ? prefix + "." + p : p, includeNulls);
                }
            }
        }
        else {
            if ($.isArray(input)) {
                $.each(input, function (index, value) {
                    _flatten(value, output, "{0}[{1}]".format(prefix, index));
                });
                return;
            }
            if (!$.isFunction(input)) {
                if (input instanceof Date) {
                    output.push({ name: prefix, value: input.toISOString() });
                }
                else {
                    var val = typeof (input);
                    switch (val) {
                        case "boolean":
                        case "number":
                            val = input;
                            break;
                        case "object":
                            // this property is null, because non-null objects are evaluated in first if branch
                            if (includeNulls !== true) {
                                return;
                            }
                        default:
                            val = input || "";
                    }
                    output.push({ name: prefix, value: val });
                }
            }
        }
    };

    $.extend({
        toDictionary: function (data, prefix, includeNulls) {
            /// <summary>Flattens an arbitrary JSON object to a dictionary that Asp.net MVC default model binder understands.</summary>
            /// <param name="data" type="Object">Can either be a JSON object or a function that returns one.</data>
            /// <param name="prefix" type="String" Optional="true">Provide this parameter when you want the output names to be prefixed by something (ie. when flattening simple values).</param>
            /// <param name="includeNulls" type="Boolean" Optional="true">Set this to 'true' when you want null valued properties to be included in result (default is 'false').</param>

            // get data first if provided parameter is a function
            data = $.isFunction(data) ? data.call() : data;

            // is second argument "prefix" or "includeNulls"
            if (arguments.length === 2 && typeof (prefix) === "boolean") {
                includeNulls = prefix;
                prefix = "";
            }

            // set "includeNulls" default
            includeNulls = typeof (includeNulls) === "boolean" ? includeNulls : false;

            var result = [];
            _flatten(data, result, prefix || "", includeNulls);

            return result;
        }
    });
})(jQuery);