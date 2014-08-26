+function (global, $) {

    global.timerId = null;

    global.init = function () {

        $(".ajax-open-modal").off("click").on("click", function (e) {
            var $this = $(this);
            if ($this.is("a")) {
                e.preventDefault();
            }

            if ($this.attr("clicking")) {
                return;
            }
            $this.attr("clicking", true);
            setTimeout(function () {
                $this.removeAttr("clicking");
            }, 200);

            var options = $this.data("ajax-open-modal-options") || $this.data("options") || {};
            var action = $this.attr("href");
            var $target;
            var hideSaveButton = false;
            var disableSaveButton = false;
            var data;

            if (options.target) {
                $target = $(options.target);
            } else {
                $target = $("#defaultModal");
            }

            if (options.action) {
                action = options.action;
            }
            if (options.hideSaveButton) {
                hideSaveButton = true;
            }
            if (options.disableSaveButton) {
                disableSaveButton = true;
            }
            if (options.dataForm) {
                data = $(options.dataForm).serializeArray();
                data.push({
                    name: "_",
                    value: new Date().getTime()
                });
            } else {
                data = { _: new Date().getTime() };
            }

            if (options.needReplaceValue) {
                var replaceValue = global.findContainer($this, ".input-group").find(":input").val();
                if (!replaceValue || replaceValue === "0") {
                    if (options.message) {
                        global.showNotificationModal({
                            content: options.message
                        });
                    }
                    return;
                } else {
                    action = action.replace("valueplaceholder", replaceValue.toString());
                }
            }

            if (action) {
                $.get(action, data, function (result) {
                    if ($target) {
                        if ($target.attr("role") == "dialog") {
                            var modalBody = $target.find(".modal-body");

                            modalBody.html(result);
                            global.init();

                            $target.find(".modal-title").text($this.data("title"));

                            var $modalFormSubmitBtn = $target.find(".modal-form-submit");
                            if (hideSaveButton) {
                                $modalFormSubmitBtn.addClass("hide");
                            } else {
                                $modalFormSubmitBtn.removeClass("hide");
                            }
                            if (disableSaveButton) {
                                $modalFormSubmitBtn.attr("disabled", "disabled");
                            } else {
                                $modalFormSubmitBtn.removeAttr("disabled");
                            }
                            if (options.saveBtnText) {
                                $modalFormSubmitBtn.text(options.saveBtnText);
                            }

                            $target.find(".modal-content > .always-shown").remove();
                            $target.find(".modal-content .modal-body").removeAttr("style").before($target.find(".always-shown"));

                            $target.modal("show");
                        }
                    }
                }, "html");
            }
        });

        $(".ajax-operation").off("click").on("click", function (e) {
            e.preventDefault();
            var $this = $(this);
            var options = $this.data("ajax-operation-options") || $this.data("options") || {};
            var href = $this.attr("href");
            var $target;

            if (options.target) {
                $target = $(options.target);
            } else {
                $target = $('#operationContainer');
            }

            var operation = function () {
                $.post(href, { _: new Date().getTime() }, function (response) {
                    $target.html(response);
                    global.init();
                });
            };

            if (options.confirmAction) {
                $.post(options.confirmAction, { _: new Date().getTime() }, function (jsonResponse) {
                    if (jsonResponse.isError) {
                        global.showNotificationModal({
                            content: jsonResponse.message,
                            isError: true
                        });
                    } else if (jsonResponse.isWarn) {
                        global.showNotificationModal({
                            type: "yesOrNo",
                            content: jsonResponse.message,
                            yesBtnFunction: operation
                        });
                    } else {
                        operation();
                    }
                });
            } else {
                operation();
            }
        });

        $(".ajax-form").off("submit").on("submit", function (e) {
            e.preventDefault();
            var $this = $(this);
            var options = $this.data("ajax-form-options") || $this.data("options") || {};
            var $target = options.target ? $(options.target) : $("#defaultModal .modal-body");

            var data = $this.serializeArray();
            data.push({
                name: "_",
                value: new Date().getTime()
            });

            $.post($this.attr("action"), data, function (result) {
                if (!options.isReserveContainer) {
                    $target.empty();
                }
                if (options.isPrepend) {
                    $target.prepend(result);
                } else {
                    $target.append(result);
                }

                global.init();

                if ($target.find(".field-validation-error").size() == 0) {
                    var updateSections = $target.find(".update-section");
                    updateSections.each(function () {
                        var $updateSection = $(this);
                        var updateSectionOptions = $updateSection.data("update-section-options") || $updateSection.data("options") || {};
                        var $updateSectionTarget;
                        if (updateSectionOptions.target) {
                            $updateSectionTarget = $(updateSectionOptions.target);
                        }
                        if ($updateSectionTarget) {
                            $updateSectionTarget.empty();
                            $updateSectionTarget.append($updateSection.children());
                            if (!updateSectionOptions.notTriggerEvent) {
                                $updateSectionTarget.find(".cascade-trigger, select").trigger("change");
                            }
                        }
                    });
                    if (options.isClearInputText) {
                        $this.find(":text, textarea").val("");
                    }
                    $this.find("button[data-dismiss='modal']").first().trigger("click");
                    $target.siblings(".modal-header").find("button[data-dismiss='modal']").first().trigger("click");
                }
            }, "html");

        });

        $(".modal-form-submit").off("click").on("click", function () {
            var $this = $(this);
            var $modal = global.findContainer($this, ".modal");
            var $form = $modal.find("form").last();
            $form.submit();
            if (!$form.hasClass("ajax-form")) {
                $modal.modal("hide");
            }
        });

    };

    global.findContainer = function ($sender, selector) {
        var result = null;
        if ($sender) {
            if (selector) {
                var child = $sender;
                for (var i = 0; i < 20; i++) {
                    var temp = child.parent(selector);;
                    if (temp.size() > 0) {
                        result = temp;
                        break;
                    } else {
                        child = child.parent();
                    }
                }
            }
        }
        return result;
    };

    global.showNotificationModal = function (options) {
        if (!options) {
            options = {};
        }

        var $modal = $("#notificationModal");
        var $yesBtn = $modal.find(".modal-yes-btn");
        var $noBtn = $modal.find(".modal-no-btn");
        var $okBtn = $modal.find(".modal-ok-btn");
        var $modalTitle = $modal.find(".modal-title");
        var $modalBody = $modal.find(".modal-body");
        var $yesOrNoBtnGroup = $modal.find(".yes-or-no-btn-group");
        var $okBtnGroup = $modal.find(".ok-btn-group");

        $yesBtn.off("click");
        $noBtn.off("click");
        $okBtn.off("click");
        $modalTitle.html($modalTitle.data("default-title"));
        $modalBody.empty();
        $yesOrNoBtnGroup.addClass("hide");
        $okBtnGroup.addClass("hide");

        if (options.title) {
            $modalTitle.html(options.title);
        }

        var content = options.content || "";
        if (options.isError) {
            content = $("<span />").addClass("text-danger").text(content);
        }

        $modalBody.html(content);

        var $type = options.type && options.type.toString().toLowerCase();

        if ($type === "yesorno") {
            if (options.yesBtnText) {
                $yesBtn.text(options.yesBtnText);
            } else {
                $yesBtn.text("Yes");
            }
            if (options.yesBtnFunction) {
                $yesBtn.on("click", options.yesBtnFunction);
            }
            if (options.noBtnFunction) {
                $noBtn.on("click", options.noBtnFunction);
            }
            $yesBtn.on("click", function () {
                $modal.modal('hide');
            });
            $noBtn.on("click", function () {
                $modal.modal('hide');
            });
            $yesOrNoBtnGroup.removeClass("hide");
            $modal.modal('show');
        } else {
            if (options.okBtnFunction) {
                $okBtn.on("click", options.okBtnFunction);
            }
            $okBtn.on("click", function () {
                $modal.modal('hide');
            });
            $okBtnGroup.removeClass("hide");
            $modal.modal('show');
        }
    };

    global.resizeViewer = function ($viewer) {
        if (!$viewer)
            $viewer = $("#viewer");

        var spaceHeight = $(window).innerHeight() - $(document.body).outerHeight();
        var expectedHeight = parseInt($viewer.css("height"));
        expectedHeight += spaceHeight;

        $viewer.css("height", expectedHeight + "px");
    };

    global.init();

}(window.global = window.global || {}, jQuery);