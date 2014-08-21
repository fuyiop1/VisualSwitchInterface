+function (global, $) {

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
            var options = $this.data("options") || {};
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

    global.init();

}(window.global = window.global || {}, jQuery);