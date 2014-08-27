+function (fileUpload, $) {

    fileUpload.init = function () {
        $(".input-file-wrapper input[type='file']").on("change", function () {
            var $this = $(this);

            var files = this.files;
            var fileName;

            var getPureFileName = function (targetFileName) {
                var seperateIndex = targetFileName.lastIndexOf("/");
                if (seperateIndex == -1) {
                    seperateIndex = targetFileName.lastIndexOf("\\");
                }
                if (seperateIndex > -1) {
                    targetFileName = targetFileName.slice(seperateIndex + 1);
                }
                return targetFileName;
            };

            if (files && files.length > 1) {
                var fileNames = [];
                for (var i = 0; i < files.length; i++) {
                    fileNames.push(getPureFileName(files.item(i).name));
                }
                fileName = fileNames.join("; ");
            } else {
                fileName = getPureFileName($this.val());
            }

            $this.siblings(".chosen-file-text").html(fileName);
            $this.siblings(".no-file").addClass("hide");

            var nameTarget = $this.data("name-target");
            if (nameTarget) {
                if ($(nameTarget).val()) {
                    return;
                }
                if (fileName.lastIndexOf(".") > -1) {
                    fileName = fileName.slice(0, fileName.lastIndexOf("."));
                }
                $(nameTarget).val(fileName);
            }
        });

        $(".file-upload-form").on("submit", function () {
            var $this = $(this);
            if (!$this.find("input[type='file']").val()) {
                $this.find(".chosen-file-text").empty();
                $this.find(".input-file-wrapper").find(".no-file").removeClass("hide");
                return false;
            }
            var loadingWrapper = $this.siblings(".loading-wrapper");
            if (loadingWrapper.size() > 0) {
                $this.addClass("hide");
                $this.siblings(".loading-wrapper").removeClass("hide");
            }
            return true;
        });
    }

    fileUpload.init();

}(window.fileUpload = window.fileUpload || {}, jQuery);