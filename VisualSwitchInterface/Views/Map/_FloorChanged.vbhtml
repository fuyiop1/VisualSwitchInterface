@ModelType VisualSwitchInterface.Models.MapModel
<div class="text-center">
    <span class="text-danger">Click the map to add switch</span>
</div>
<div class="clearfix">
    <div class="pull-left">
        <div class="btn-group-vertical">
            <button type="button" class="btn btn-sm btn-default zoom-in" title="Zoom in"><span class="glyphicon glyphicon-plus"></span></button>
            <button type="button" class="btn btn-sm btn-default zoom-out" title="Zoom out"><span class="glyphicon glyphicon-minus"></span></button>
            <button type="button" class="btn btn-sm btn-default zoom-reset" title="Reset"><span class="glyphicon glyphicon-repeat"></span></button>
        </div>
    </div>
    <div class="pull-right">
        <div class="relative" style="width: 72px;height:100%;">
            <button id="upBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 24px; top: 0;" title="Up"><span class="glyphicon glyphicon-circle-arrow-up"></span></button>
            <button id="leftBtn" type="button" class="btn btn-xs btn-default absolute" style="right:  48px; top: 22px;" title="Left"><span class="glyphicon glyphicon-circle-arrow-left"></span></button>
            <button id="rightBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 0; top: 22px;" title="Right"><span class="glyphicon glyphicon-circle-arrow-right"></span></button>
            <button id="downBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 24px; top: 44px;" title="Down"><span class="glyphicon glyphicon-circle-arrow-down"></span></button>
        </div>
        <span>&nbsp;</span>
    </div>
    <div class="pull-right">
        <div class="panzoom-parent">
            <div class="panzoom relative">
                <img src="@Model.FilePath" alt="" />
                @For Each switchModel In Model.SwitchModels
                    @<a class="absolute ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @(switchModel.CoordX)px; top: @(switchModel.CoordY)px" title="@switchModel.Name" data-title="@switchModel.Name"><span class="glyphicon glyphicon-certificate text-super-danger text-lg"></span></a>
                Next
            </div>
        </div>
    </div>
    <a id="addSwitchBtn" href="#" data-href="@Url.Action("_AddSwitch")" class="hide ajax-open-modal" data-title="Add Switch"></a>
    <script>
        $(function () {
            var $viewer = $("#viewer");
            var $panzoomParent = $viewer.find(".panzoom-parent");

            var imageWidth = parseInt("@Model.Width");
            var imageHeight = parseInt("@Model.Height");
            var panzoomParentWidth = global.findContainer($viewer, ".container").width() - 34 - 72;

            $panzoomParent.width(panzoomParentWidth);
            $panzoomParent.height($viewer.height() - 20);

            var $panzoom = $panzoomParent.find(".panzoom");
            $panzoom.width(imageWidth);
            $panzoom.panzoom({
                $zoomIn: $viewer.find(".zoom-in"),
                $zoomOut: $viewer.find(".zoom-out"),
                $reset: $viewer.find(".zoom-reset"),
                disablePan: true,
                //contain: true,
                startTransform: "scale(1.0)",
                increment: 0.1,
                minScale: 1,
                startTransform: "matrix(1, 0, 0, 1, " + (($panzoomParent.width() - imageWidth) / 2).toString() + ", " + (($panzoomParent.height() - imageHeight) / 2).toString() + ")"
            });

            $panzoom.find("img").on("click", function (e) {
                var offset = $(this).offset();
                var scale = $("#viewer .panzoom").panzoom("getMatrix")[0];
                var coordX = parseInt((e.clientX - offset.left) / scale);
                var coordY = parseInt((e.clientY - offset.top) / scale);
                var $addSwitchBtn = $("#addSwitchBtn");
                $addSwitchBtn.attr("href", $addSwitchBtn.data("href") + "?coordX=" + coordX + "&coordY=" + coordY);
                $addSwitchBtn.trigger("click");
            });

            $("#upBtn").on("click", function () {
                var matrix = $panzoom.panzoom("getMatrix");
                $panzoom.panzoom("setMatrix", [matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], matrix[5] - imageHeight / 10]);
            });

            $("#leftBtn").on("click", function () {
                var matrix = $panzoom.panzoom("getMatrix");
                $panzoom.panzoom("setMatrix", [matrix[0], matrix[1], matrix[2], matrix[3], matrix[4] - imageWidth / 10, matrix[5]]);
            });

            $("#rightBtn").on("click", function () {
                var matrix = $panzoom.panzoom("getMatrix");
                $panzoom.panzoom("setMatrix", [matrix[0], matrix[1], matrix[2], matrix[3], parseInt(matrix[4]) + imageWidth / 10, matrix[5]]);
            });

            $("#downBtn").on("click", function () {
                var matrix = $panzoom.panzoom("getMatrix");
                $panzoom.panzoom("setMatrix", [matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], parseInt(matrix[5]) + imageHeight / 10]);
            });

        });
    </script>
</div>
