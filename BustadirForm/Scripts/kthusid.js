﻿// http://learn.jquery.com/plugins/basic-plugin-creation/
// the semi-colon before function invocation is a safety net against concatenated
// scripts and/or other plugins which may not be closed properly.
; (function ($) {
    $.getJSONorXML = function (action, parameters, type, onSuccess, onError) {
        var contentType = type == "json" ? "application/json" : "";
        contentType = type == "xml" ? "text/xml" : contentType;
        var charset = "utf-8";
        var requestType = "GET";
        var url = "https://localhost";
        url += action;
        if (parameters != null) {
            var c = 0;
            $.each(parameters, function (k, v) {
                if (c == 0) {
                    url += "?" + k + "=" + v;
                }
                else {
                    url += "&" + k + "=" + v;
                }
                c++;
            });
            url += "&";
        }
        else {
            url += "?";
        }
        url += "type=" + type;
        //console.log(url);
        $.ajax({
            url: url,
            type: requestType,
            // http://en.wikipedia.org/wiki/List_of_HTTP_header_fields
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Accept": contentType + "; charset=" + charset
            }
        })
        .done(function (data) {
            onSuccess(data);
        })
        .fail(function () {
            onError();
        })
        .always(function () {
            return this;
        });
    };
}(jQuery));

; (function ($) {
    $.showError = function (error, url) {
        var errorString = "<h3 style='color: red;'>Url: " + url + "</h3><br />";
        errorString += error['responseText'];
        $('#modalDebug .modal-body').html(errorString);
        $('#modalDebug').modal({
            keyboard: false
        }).modal('show');
        console.log("url: " + url);
        console.log(error);
        //                        alert('Failed on jQuery.get()\nPlease contact the system administrator for more help.\nSee browser console for more details.');
    };
})(jQuery);

; (function ($) {
    $.showModal = function (title, body, footer, modalParams) {
        this.setContent = function (body) {
            $('#modalDebug .modal-body').html(body);
        };
        this.setTitle = function (title) {
            $('#modalDebug .modal-header').html(title);
        };

        this.setTitle(title);
        if (!body) {
            this.setContent("<div class='row text-center'><img style='display: inline-block; vertical-align: middle; float: none;' src='/Content/images/ajax-loader.gif' /></div>");
        }
        else {
            this.setContent(body);
        }
        if (footer && footer.length > 0) {
            $('#modalDebug .modal-footer').html(footer);
        }
        this.modal = $('#modalDebug').modal(modalParams);

        $('.modal')
           .on('hidden.bs.modal', function (e) {
               //alert("modal hidden");
               //console.log(e);
               //$('.modal').die();
               //$.event.trigger({
               //    type: "modalDebugClosed",
               //    message: "",
               //    time: new Date()
               //}, { 'data': null });
           });

        this.modal.modal('show');
        return this;
    };
})(jQuery);

function loadList(divId, url, id) {
    $(divId).html("<p class='text-center' style='padding-top:25px;'><img src='/Content/images/ajax-loader.gif' /></p>");
    var data2send = id !== null ? { id: id } : {};
    $.get(url, data2send, function (data) {
        var canLoadList = true;
        //console.log(data);
        $(divId).html(data);
        $('.modal').on('hidden.bs.modal', function (e) {
            //console.log(e);
            if (canLoadList) {
                loadList(divId, url, id);
            }
            // Make sure we can load the list again
            canLoadList = true;
            //$('.modal').die();
        });
        // Don't load list on ESCAPE key
        $('.modal').keydown(function (e) {
            //console.log(e.keyCode);
            canLoadList = e.keyCode !== 27; // ESC-key
            //console.log('canLoadList: ' + canLoadList + ', keyCode: ' + e.keyCode);
        });
        $('.close-modal').click(function (e) {
            canLoadList = false;
            //$('.modal').die();
        });

    })
    .fail(function (error) {
        $.showError(error, url);
    });
}

; (function ($) {
    $.loadData = function (_url, _sortName, _sortType, _filterName, _page, _pageSize, successCallback, errorCallback) {
        console.log(_url + " - " + _sortName + " - " + _sortType);
        //Ajax Call Get All Titles
        $.ajax({
            type: "GET",
            url: _url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                sortName: _sortName,
                sortType: _sortType, // asc || desc
                //currentFilter: _currentFilter,
                filterName: _filterName,
                page: _page,
                pageSize: _pageSize,
                type: "json"
            },
            success: function (result) {
                successCallback(result);
            },
            error: function (error) {
                $.showError(error, _url);
                if (errorCallback) {
                    errorCallback(error);
                }
            }
        });
    };
    $.getDataAsJson = function (_url, _data, successCallback, errorCallback, showError) {
        _data = $.extend(_data, {
            type: "json"
        });
        $.ajax({
            type: "GET",
            url: _url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: _data,
            success: function (result) {
                successCallback(result);
            },
            error: function (error) {
                if (showError && showError === true) {
                    $.showError(error, _url);
                }
                if (errorCallback) {
                    errorCallback(error);
                }
            }
        });
    }
})(jQuery);

; (function ($) {
    $.modalCRUD = function (_modalCrudId, controllerName, _data, imgAjaxLoader, btnValues, pleaseWaitText) {
        var isRegistered = false;
        var modalCRUD = $(_modalCrudId);
        var url;
        var divTitle, divBody, divFooter;
        var thisModal = this;
        //thisModal.onModalClosed = function (callback) {
        //    callback();
        //};
        var modelName;

        $("body").on("click", ".create-action, .edit-action, .details-action, .delete-action", function (e) {
            var self = $(this);
            var parent = self.parent();
            var id = parent.find('input[name=__id]').val(); //modalCRUD.attr("id");
            var title = parent.find('input[name=__title]').val();
            var form = $(parent);
            modelName = self.data("model-name");
            if (!modelName) {
                console.log("WARNING: ModelName has not been assigned on EditDetailsDelete2Data in _EditDetailsDeletePartial2.cshtml!");
            }
            var data2send = id !== null ? { id: id } : {};
            data2send = $.extend(data2send, _data);
            //console.log(data2send);
            //console.log(id);
            //console.log(title);
            //console.log(data2send);
            var hasCreateAction = self.hasClass("create-action");
            var hasEditAction = self.hasClass("edit-action");
            var hasDetailsAction = self.hasClass("details-action");
            var hasDeleteAction = self.hasClass("delete-action");

            var customControllerName = self.data("controller-name");
            if (!customControllerName) {
                console.log("WARNING: ControllerName has not been assigned on EditDetailsDelete2Data in _EditDetailsDeletePartial2.cshtml!");
            }
            controllerName = (customControllerName && customControllerName.length > 0) ? customControllerName : controllerName;

            url = "/" + controllerName;
            divTitle = modalCRUD.find(".modal-title");
            divBody = modalCRUD.find(".modal-body");
            divFooter = modalCRUD.find(".modal-footer");
            //var saveAction = modalCRUD.find(".save-action");
            //var closeAction = modalCRUD.find(".close-action");

            //console.log(saveButtonClass);
            var callbackCloseFunc = function () {
                $('.modal').modal('hide');
            };
            var callbackSaveFunc = function () {
                modalCRUD.find('form').submit();
            };
            if (!isRegistered) {
                $(document).keydown(function (e) {
                    //console.log(e.target);
                    //console.log(e.keyCode);
                    if ($(e.target).is('input') && e.keyCode == 13) {
                        callbackSaveFunc();
                        e.preventDefault();
                        return false;
                    }
                });
                isRegistered = true;
            }

            var saveButtonClass = "";
            var saveButtonText = "";

            var actionName = "/" + self.data("action-name");

            // Create
            if (hasCreateAction) {
                saveButtonClass = 'btn-primary';
                saveButtonText = btnValues["create"]["btnText"];
                url += actionName !== "/" ? actionName : btnValues["create"]["urlPostfix"];
            } // Edit
            else if (hasEditAction) {
                saveButtonClass = 'btn-primary';
                saveButtonText = btnValues["edit"]["btnText"];
                url += actionName !== "/" ? actionName : btnValues["edit"]["urlPostfix"];
                //url += btnValues["edit"]["urlPostfix"];
            } // Details
            else if (hasDetailsAction) {
                saveButtonClass = 'btn-info';
                saveButtonText = btnValues["details"]["btnText"];
                url += btnValues["details"]["urlPostfix"];
                callbackSaveFunc = function () {
                    window.location = url + "/" + id;
                }
            } // Delete
            else if (hasDeleteAction) {
                saveButtonClass = 'btn-danger';
                saveButtonText = btnValues["delete"]["btnText"];
                url += actionName !== "/" ? actionName : btnValues["delete"]["urlPostfix"];
                //url += btnValues["delete"]["urlPostfix"];
            }

            var saveBtn = SetupSaveButton(saveButtonClass, saveButtonText, callbackSaveFunc);
            var closeBtn = SetupCloseButton(btnValues["close"]["btnText"], callbackCloseFunc);
            divFooter.html("");
            divFooter.append(saveBtn);
            divFooter.append(closeBtn);
            divTitle.html(title);
            divBody.html("<p class='text-center' style='padding-top:25px;'>" + pleaseWaitText + "<br /><img src='" + imgAjaxLoader + "' /></p>");
            $.get(url, data2send, function (data) {
                //console.log(data);
                divBody.html(data);
                //setTimeout(function () {
                //    saveBtn.focus();
                //}, 500);
            });

            if (hasDeleteAction) {
                if (confirm(btnValues["delete"]["btnText"] + "?")) {
                    var data = form.serialize();
                    if (data.length == 0) {
                        data = $('form').filter(":visible").serialize();
                    }
                    console.log(url);
                    console.log(data);
                    triggerSubmit(url, data, false);
                    return false;
                }
                else {
                    return false;
                }
            }
            else {
                $.event.trigger({
                    type: "modalOpened",
                    message: "",
                    time: new Date()
                }, { 'data': null, 'modelName': modelName });
                // open the modal
                $('#modalCRUD').modal('show');
            }
        });

        $('.modal')
            .on('hidden.bs.modal', function (e) {
                //console.log(e);
                //$('.modal').die();
                $.event.trigger({
                    type: "modalClosed",
                    message: "",
                    time: new Date()
                }, { 'data': null, 'modelName': modelName });
            })
            .on('shown.bs.modal', function (e) {
                modalCRUD.find(".save-action").focus();
            })
            .keydown(function (e) { // Don't load list on ESCAPE key
                //console.log(e.keyCode);
                canLoadList = e.keyCode !== 27; // ESC-key
            })
            .modal({
                backdrop: false,
                keyboard: true,
                show: false
            });

        function SetupSaveButton(className, divHtml, callback) {
            var saveBtn = $("<button class='btn save-action'></button>");
            saveBtn
                .addClass(className)
                .html(divHtml)
                .click(function () {
                    callback();
                });
            return saveBtn;
        }

        function SetupCloseButton(divHtml, callback) {
            var closeBtn = $("<button class='btn btn-default close-action'></button>");
            closeBtn
                .text(divHtml)
                .click(function () {
                    callback();
                    triggerOnModalClosed(null, modelName, "");
                });

            return closeBtn;
        }

        function triggerOnModalClosed(_data, _modelName, _message) {
            $.event.trigger({
                type: "modalClosed",
                message: _message,
                time: new Date()
            }, { 'data': _data, 'modelName': _modelName });
        }

        $(document).on('submit', _modalCrudId + ' form', function (e) {
            e.preventDefault();
            var data = $(this).serialize();
            triggerSubmit(url, data, false);
            //$.post(url, data, function (d, s, xhr) {
            //    console.log(d);
            //    console.log(s);
            //    console.log(xhr);
            //    console.log(xhr.responseJSON);
            //    var json = xhr.responseJSON;
            //    if (json) { // has json object data
            //        modalCRUD.modal('hide');
            //        //triggerOnModalClosed(json, modelName, "");
            //    } else { // Model State is Invalid (required fields shown now)
            //        divBody.html(d);
            //    }
            //})
            //.fail(function (error) {
            //    console.log(error);
            //});
        });

        function triggerSubmit(_url, _data, _addRequestVerificationToken) {
            if (_addRequestVerificationToken) {
                _data = addRequestVerificationToken(_data);
            }
            $.post(url, _data, function (d, s, xhr) {
                console.log(d);
                console.log(s);
                console.log(xhr);
                console.log(xhr.responseJSON);
                var json = xhr.responseJSON;
                if (json) { // has json object data
                    modalCRUD.modal('hide');
                    triggerOnModalClosed(json, modelName, "");
                    //$.event.trigger({
                    //    type: "modalClosed",
                    //    message: "",
                    //    time: new Date()
                    //}, { 'data': json, 'modelName': modelName });
                } else { // Model State is Invalid (required fields shown now)
                    divBody.html(d);
                }
            })
            .fail(function (error) {
                console.log(error);
            });
        }
    };
}(jQuery));

function addRequestVerificationToken(data) {
    return $.extend(data, {
        __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
    });
    //data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
};

var AjaxPageHandler = function (_targetId, _baseUrl, _startPage) {
    var self = this;
    self.targetId = _targetId;
    self.baseUrl = _baseUrl;
    self.startPage = _startPage;
    self.app = $.sammy(self.targetId, function () {
        this.get(/\#\/(.*)/, function (context) {
            var args = this.params.splat;
            if (args.length === 1) {
                var page = this.params.splat[0];
                var url = self.baseUrl + '/' + page;
                self.setActive($('a[href="#/' + page + '"]').parent());
                console.log(url);
                $.get(url, null, function (data, s, xhr) {
                    $(self.targetId).html(data);
                    console.log(xhr);
                });
            }
        });
    });
    $('.nav-sidebar > li').click(function (e) {
        var obj = $(this);
        self.setActive(obj);
    });
    $(self.targetId).on('submit', 'form', function (e) {
        e.preventDefault();
        $.post(e.target.action, $(this).serialize(), function (d, s, xhr) {
            //if (!xhr.responseJSON) {
            //    // Model State is Invalid (required fields shown now)
            //    $(self.targetId).html(d);
            //}
            $(self.targetId).html(d);
        })
        .fail(function (error) {
            console.log(error);
        });
    });
    self.setActive = function (obj) {
        //console.log(obj);
        var activeElement = obj.parent().find('li.active');
        if (activeElement != obj) {
            activeElement.removeClass('active');
            obj.addClass('active');
        }
    };
    self.app.run(self.startPage);
    return self;
};



$('.show-hide').click(function (e) {
    $("#" + $(this).data('name')).toggle("slow", function () {
        // on animation done...
    });
});


function replaceAll(find, replace, str) {
    return str.replace(new RegExp(find, 'g'), replace);
}

; (function ($) {
    $.navbarContextMenu = function (title, items) {

        var contextMenu = $("#context-dropdown-menu");
        contextMenu.find(".dropdown-toggle").html(title);
        var menuItems = contextMenu.find(".dropdown-menu").empty();

        for (var i = 0; i < items.length; i++) {
            menuItems.append("<li>" + items[i] + "</li>");
        }

        return contextMenu;
    };
})(jQuery);


var dateSeparator = "/";

function navigate(url) {
    window.location = url;
};

function println(o, n) {
    if (n) {
        console.log(n + ":");
    }
    console.log(o);
}

function setModalHeight(pct) {
    if (!pct || pct < 0.1 || pct > 0.99) {
        pct = 0.95;
    }
    var modalHeight = pct * $(window).height();

    $('.modal-content').css('height', modalHeight);
    $('.modal-body').css('height', modalHeight * pct * 0.8632);
    //$('.modal-footer').css('height', '50px');//modalHeight * 0.8632);
};

function activateDatePicker() {
    $(".datefield").kendoDatePicker({
        format: "d"
    });
};

function AddInputCodeFilter(source, list, compareAction, setTargetValue) {
    $(source).keyup(function (e) {
        var y = $(this).val();
        var source = list.filter(function (x, idx, obs) {
            return compareAction(x, y);
        });
        var subscription = source.subscribe(
        function (x) {
            setTargetValue(x);
        },
        function (err) {
            console.log('Error: %s', err);
        },
        function () {
            //console.log('Completed');
        });
    });
}

function AddDropdownFilter(source, list, compareAction, setTargetValue) {
    $(source).change(function () {
        var y = $(this).val();
        var source = list.filter(function (x, idx, obs) {
            return compareAction(x, y);
        });
        var subscription = source.subscribe(
        function (x) {
            setTargetValue(x);
        },
        function (err) {
            console.log('Error: %s', err);
        },
        function () {
            //console.log('Completed');
        });
    });
}

var GridHandler = function (_gridId, _searchId) {
    var self = this;
    self.getTime = function () {
        return new Date().getTime();
    };
    self.lastSearchTime = self.getTime();
    self.searchInterval = 250;
    self.rowIndex = 0;
    self.colIndex = 0;
    self.gridId = _gridId;
    self.grid = $(_gridId).data("kendoGrid");
    self.Grid = function () {
        return self.grid;
    };
    self.searchInput = _searchId ? $(_searchId) : null;
    self.focusSearch = function () {
        if (self.searchInput != null) {
            self.searchInput.focus();
        }
        //else {
        //    console.log(self.gridId + " doesn't have any searchId (search input) set. Can't call function .focus()!");
        //}
    };
    self.searchHasFocus = function () {
        if (self.searchInput != null) {
            return self.searchInput.is(":focus");
        }
        //else {
        //    console.log(self.gridId + " doesn't have any searchId (search input) set. Can't call function .focus()!");
        //}
        return false;
    };
    self.gridHasFocus = function () {
        return $($(self.gridId + ' :focus')[0]).attr("role") == "grid";
    };
    self.changeRow = function () {
        self.row = self.grid.tbody.find(">tr:not(.k-grouping-row)").eq(self.rowIndex);
        self.grid.select(self.row);
    };
    self.setPage = function (page) {
        self.grid.dataSource.page(page);
    };
    self.getPage = function () {
        return self.grid.dataSource.page();
    };
    self.getTotalPages = function () {
        return Math.floor(self.grid.dataSource.total() / self.grid.dataSource.pageSize()) + 1;
    }
    self.previous = function () {
        if (self.rowIndex <= 0) {
            if (self.getPage() > 1) {
                self.setPage(self.getPage() - 1);
                self.onPageChange = function () {
                    self.last();
                };
            }
            return false;
        }
        self.rowIndex--;
        self.changeRow();
    };
    self.next = function () {
        if (self.rowIndex >= self.grid.dataSource._data.length - 1) {
            //console.log(self.getPage() + " /" +self.getTotalPages());
            if (self.getPage() <= self.getTotalPages()) {
                self.setPage(self.getPage() + 1);
                self.onPageChange = function () {
                    self.first();
                };
            }
            return false;
        }
        self.rowIndex++;
        self.changeRow();
    };
    self.first = function () {
        self.rowIndex = 0;
        self.changeRow();
    };
    self.last = function () {
        //self.rowIndex = self.grid.dataSource.pageSize() - 1;
        self.rowIndex = self.grid.dataSource._data.length - 1;
        self.changeRow();
    };
    self.setDataSource = function (dataSource) {
        if (dataSource != null) {
            self.grid.dataSource = dataSource;
            self.reload();
        }
    };
    self.onPageChange = undefined;
    self.firstPage = function () {
        self.grid.dataSource.page(1);
        setTimeout(function () {
            self.first();
        }, 100);
    };
    self.lastPage = function () {
        var lastPage = Math.ceil(self.grid.dataSource.total() / self.grid.dataSource.pageSize());
        //var lastPage = self.grid.totalPages();
        self.grid.dataSource.page(lastPage);
        setTimeout(function () {
            self.last();
        }, 100);
    };
    self.select = function (sender) {
        self.row = $(sender).closest("tr");
        if (self.row) {
            var ri = $("tr", self.grid.tbody).index(self.row) - 1;
            var ci = $("td", self.row).index(sender) - 1;
            self.rowIndex = ri >= 0 ? ri : self.rowIndex;
            self.colIndex = ci >= 0 ? ci : self.colIndex;
        }
    };
    self.onChange = function (arg) {
        //println("onChange");
    };
    self.onDataBound = function (arg) {
        //console.log("Grid data bound");
        self.select();
        if (self.onPageChange != null) {
            self.onPageChange();
            self.onPageChange = null;
        }
    };
    self.onDataBinding = function () {
        //console.log("Grid data binding");
    };
    self.onDoubleClick = function (callback) {
        // handled in user code...
        if (callback && typeof callback == "function") {
            callback();
        }
    };
    self.onItemOpen = function (item) {
        // to be handled in user code...
        //alert("opening item");
        //alert(JSON.stringify(item));
    };
    self.onItemDelete = function (item) {
        // to be handled in user code...
        //alert("deleting item");
        //alert(JSON.stringify(item));
    };
    self.onSearching = function (event, sender) {
        // can be handled in user code...
    };
    self.onKeydown = function (e) {
        console.log(e.keyCode);
        // Focus table
        if (self.searchHasFocus() && e.keyCode == 9) {
            self.first();
        }
        if (self.gridHasFocus()) {
            if (e.keyCode == 38) {
                // Key up
                e.preventDefault();
                self.previous();
            }
            if (e.keyCode == 40) {
                // Key down
                e.preventDefault();
                self.next();
            }
            if (e.keyCode == 33) {
                // Page up
                e.preventDefault();
                self.firstPage();
            }
            if (e.keyCode == 34) {
                // Page down
                e.preventDefault();
                self.lastPage();
            }
            if (e.keyCode == 46) {
                // Delete
                e.preventDefault();
                var item = self.currentItem();
                if (item) {
                    self.onItemDelete(item);
                }
            }
            if (e.keyCode == 13 || e.keyCode == 79) {
                // Open details ([ENTER] or 'o')
                e.preventDefault();
                var item = self.currentItem();
                if (item) {
                    self.onItemOpen(item);
                }
            }
        }
    };
    //self.onItemOpen = function (callback) {
    //    callback();
    //};
    self.search = function (json) {
        if (self.getTime() - self.lastSearchTime > self.searchInterval) {
            self.lastSearchTime = self.getTime();
            if (json.value) {
                self.filter(json);
            } else {
                self.filter();
            }
        }
    };
    self.filter = function (json) {
        if (json) {
            self.grid.dataSource.filter({ field: "filter", operator: json.operator, value: json.value });
        }
        else {
            self.reload();//self.grid.dataSource.filter({});
        }
    };
    self.refresh = function () {
        self.reload();
    }
    self.reload = function () {
        self.grid.dataSource.filter({});
        //self.grid.dataSource.read();
        self.grid.refresh();
    };
    $(self.grid.tbody).on("click", "td", function (e) {
        self.select(this);
    });
    $(self.grid.tbody).on("dblclick", "tr.k-state-selected", function () {
        self.onDoubleClick(self.currentItem());
    });
    //$(document).keydown(function (e) {
    //    self.onKeydown(e);
    //});
    $(".k-grid table").on("keydown", function (e) {
        self.onKeydown(e);
    });
    if (self.searchInput != null) {
        self.searchInput.keyup(function (e) {
            self.onSearching(e, $(this));
        });
        self.onSearching = function (e, sender) {
            self.search({ operator: "eq", value: sender.val() });
            if (e.keyCode == 46) {
                sender.val("");
            };
        };
    };
    self.currentItem = function () {
        return self.grid.dataItem(self.grid.select());
    }
    return self;
};

function UpdateVisibleGrids()
{
    var grids = $(document.body).find('.k-grid').filter(":visible");
    $.each(grids, function (i, grid) {
        $(grid).data("kendoGrid").dataSource.filter({});
    });
}

function MathRound(number, decimals) {
    return parseFloat(Math.round(number * 100) / 100).toFixed(decimals ? decimals : 0);
}

function ParseInputDate(id, eventType, toFormat, fromFormat) {
    eventType = eventType ? eventType : "blur";
    $(document.body).off(eventType, id).on(eventType, id, function () {
        var input = $(this);
        fromFormat = fromFormat ? fromFormat : "DDMMYYYY";
        toFormat = toFormat ? toFormat : "DD-MM-YYYY";
        var date = moment(input.val(), fromFormat);
        input.val(date.format(toFormat));
    });
}

function ParseInputDecimal(id, toFormat, fromFormat, eventType) {
    eventType = eventType ? eventType : "blur";
    $(document.body).off(eventType, id).on(eventType, id, function () {
    //$(document.body).filter(":visible").find(id).on(eventType, function () {
        //alert("OK!");
        var input = $(this);
        fromFormat = fromFormat ? fromFormat : ".";
        toFormat = toFormat ? toFormat : ",";
        var decimalValue = input.val().replace(fromFormat, toFormat);
        decimalValue = ParseDecimal(decimalValue, toFormat);
        input.val(decimalValue);
    });
}

function ParseInputPercentage(id, eventType, toFormat, fromFormat) {
    eventType = eventType ? eventType : "blur";
    $(document.body).off(eventType, id).on(eventType, id, function () {
        var input = $(this);
        fromFormat = fromFormat ? fromFormat : ".";
        toFormat = toFormat ? toFormat : ",";
        var decimalValue = input.val().replace(fromFormat, toFormat);
        //alert(decimalValue); alert(decimalValue <= 100);
        decimalValue = ParseDecimal(decimalValue, toFormat);
        //alert(typeof decimalValue);
        //decimalValue = decimalValue <= 100 ? decimalValue : 100.0;
        //decimalValue = decimalValue >= 0.0 ? decimalValue : 0.0;
        input.val(decimalValue);
    });
}

function ParseDecimal(decimalValue, separator) {
    decimalValue = decimalValue.replace(",", ".");
    var strlen = decimalValue.length;
    if (decimalValue.substring(strlen - 1, strlen) == separator) {
        decimalValue = decimalValue.substring(0, strlen - 1);
    }
    var splitValues = decimalValue.split(separator);
    if (splitValues.length == 1) {
        decimalValue = parseFloat(splitValues[0])
    }
    if (splitValues.length == 2) {
        var value1 = parseFloat(splitValues[0]);
        var value2 = parseFloat(splitValues[1]);
        if (isNaN(value1)) {
            decimalValue = value2;
        }
        else if (isNaN(value2)) {
            decimalValue = value1;
        }
        else {
            decimalValue = value1 + separator + value2;
        }
    }
    return decimalValue;//.toFixed(2);
}

function parseDate(dateStr, toIsoString) {
    var date = kendo.parseDate(dateStr);
    if (toIsoString) {
        date = date.toISOString();
    }
    return date;
}

function resetForm(formId) {
    $(formId)[0].reset();
}