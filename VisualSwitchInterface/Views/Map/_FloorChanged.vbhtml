@ModelType VisualSwitchInterface.Models.MapModel
<div class="clearfix">
    <div class="pull-left">
        <div class="btn-group-vertical">
            <button type="button" class="btn btn-default zoom-in" title="Zoom in"><span class="glyphicon glyphicon-plus"></span></button>
            <button type="button" class="btn btn-default zoom-out" title="Zoom out"><span class="glyphicon glyphicon-minus"></span></button>
            <button type="button" class="btn btn-default zoom-reset" title="Reset"><span class="glyphicon glyphicon-repeat"></span></button>
        </div>
    </div>
    <div class="pull-right">
        <div class="panzoom-parent">
            <div class="panzoom relative">
                <img src="@Model.FilePath" alt="" />
                @For Each switchModel In Model.SwitchModels
                    @<a class="absolute ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @(switchModel.CoordX)px; top: @(switchModel.CoordY)px" title="@switchModel.Name" data-title="@switchModel.Name"><span class="glyphicon glyphicon-map-marker text-super-danger text-lg"></span></a>
                Next
            </div>
        </div>
    </div>
    <a id="addSwitchBtn" href="#" data-href="@Url.Action("_AddSwitch")" class="hide ajax-open-modal" data-title="Add Switch"></a>
    <script>
        setTimeout(function () {
            var $viewer = $("#viewer");
            var $panzoomParent = $viewer.find(".panzoom-parent");

            var imageWidth = parseInt("@Model.Width");
            var imageHeight = parseInt("@Model.Height");
            var panzoomParentWidth = global.findContainer($viewer, ".container").width() - 40;

            $panzoomParent.width(panzoomParentWidth);
            $panzoomParent.height($viewer.height());

            var $panzoom = $panzoomParent.find(".panzoom");
            $panzoom.width(imageWidth);
            $panzoom.panzoom({
                $zoomIn: $viewer.find(".zoom-in"),
                $zoomOut: $viewer.find(".zoom-out"),
                $reset: $viewer.find(".zoom-reset"),
                //contain: true,
                startTransform: "scale(1.0)",
                increment: 0.1,
                minScale: 1,
                startTransform: "matrix(1, 0, 0, 1, " + (($panzoomParent.width() - imageWidth) / 2).toString() + ", " + (($panzoomParent.height() - imageHeight) /2).toString() + ")"
            });

            $panzoom.find("img").on("dblclick", function (e) {
                var offset = $(this).offset();
                var scale = $("#viewer .panzoom").panzoom("getMatrix")[0];
                var coordX = parseInt((e.clientX - offset.left) / scale);
                var coordY = parseInt((e.clientY - offset.top) / scale);
                var $addSwitchBtn = $("#addSwitchBtn");
                $addSwitchBtn.attr("href", $addSwitchBtn.data("href") + "?coordX=" + coordX + "&coordY=" + coordY);
                $addSwitchBtn.trigger("click");
            });

        }, 10);
    </script>
</div>
