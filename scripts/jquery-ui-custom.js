(function ($, undefined) {
    $.widget("ui.combobox", {
        options: {
            source: null
        },
        _create: function () {
            var that = this;
            var wasOpen = false;
            var oldInput = this.element;
            var input = oldInput.clone();

            oldInput.hide();
            input.attr("id", oldInput.attr("id") + "_combobox");
            wrapper = this.wrapper = $("<span>").addClass("ui-combobox").insertAfter(oldInput);

            // set the value from oldInput to input
            this._on(this.element, {
                change: function () {
                    input.val(oldInput.val());
                }
            });

            this._initSource();

            input.appendTo(wrapper)
                .attr("title", "")
                .addClass("ui-state-default ui-combobox-input")
                .autocomplete({
                    delay: this.options.delay,
                    minLength: 0,
                    source: this.source,
                    // set the value from input to oldInput
                    change: function (event, ui) {
                        oldInput.val(input.val());
                    }
                })
                .addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("ui-autocomplete")._renderitem = function (ul, item) {
                return $("<li>").append("<a>" + item.label + "</a>").appendto(ul);
            };

            $("<a>").attr("tabIndex", -1)
                .attr("title", "Show All Items")
                .tooltip()
                .appendTo(wrapper)
                .button({
                    icons: {
                        primary: "ui-icon-triangle-1-s"
                    },
                    text: false
                }).removeClass("ui-corner-all")
                .addClass("ui-corner-right ui-combobox-toggle")
                .mousedown(function () {
                    wasOpen = input.autocomplete("widget").is(":visible");
                }).click(function () {
                    input.focus();

                    // close if already visible
                    if (wasOpen) {
                        return;
                    }

                    // pass empty string as value to search for, displaying all results
                    input.autocomplete("search", "");
                });

                input.tooltip({
                    tooltipClass: "ui-state-highlight"
                });
            },

        _setOption: function (key, value) {
            this._super(key, value);
            if (key === "source") {
                this._initSource();
            }
        },

        _initSource: function () {
            var array, url,
			        that = this;
            if ($.isArray(this.options.source)) {
                array = this.options.source;
                this.source = function (request, response) {
                    response($.ui.autocomplete.filter(array, request.term));
                };
            } else if (typeof this.options.source === "string") {
                url = this.options.source;
                this.source = function (request, response) {
                    if (that.xhr) {
                        that.xhr.abort();
                    }
                    that.xhr = $.ajax({
                        url: url,
                        data: request,
                        dataType: "json",
                        success: function (data) {
                            response(data);
                        },
                        error: function () {
                            response([]);
                        }
                    });
                };
            } else {
                this.source = this.options.source;
            }
        },

        _destroy: function () {
            this.wrapper.remove();
            this.element.show();
        }
    });
})(jQuery);