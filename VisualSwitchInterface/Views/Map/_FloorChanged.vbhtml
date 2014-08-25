﻿@ModelType VisualSwitchInterface.Models.MapModel
<div class="clearfix">
    <div class="pull-left">
        <div class="panzoom-parent">
            <div class="panzoom relative">
                <img src="@Model.FilePath" alt="" />
                @For Each switchModel In Model.SwitchModels
                    Dim coorXPercent = (switchModel.CoordX * 100).ToString + "%"
                    Dim coorYPercent = (switchModel.CoordY * 100).ToString + "%"
                    @<a class="absolute ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @coorXPercent; top: @coorYPercent" title="@switchModel.Name" data-title="@switchModel.Name"><span class="glyphicon glyphicon-map-marker text-super-danger text-lg"></span></a>
                Next
            </div>
        </div>
    </div>
    <div class="pull-left">
        <div class="btn-group-vertical">
            <button type="button" class="btn btn-default zoom-in" title="Zoom in"><span class="glyphicon glyphicon-plus"></span></button>
            <button type="button" class="btn btn-default zoom-out" title="Zoom out"><span class="glyphicon glyphicon-minus"></span></button>
            <button type="button" class="btn btn-default zoom-reset" title="Reset"><span class="glyphicon glyphicon-repeat"></span></button>
        </div>
    </div>
    <script>
        setTimeout(function () {
            var $viewer = $("#viewer");
            var $panzoomParent = $viewer.find(".panzoom-parent");
            var imageWidth = parseInt("@Model.Width");
            var imageHeight = parseInt("@Model.Height");
            var maxWidth = global.findContainer($viewer, ".container").width() - 40;

            var panzoomParentWidth;
            if (imageWidth > maxWidth) {
                panzoomParentWidth = maxWidth;
            } else {
                panzoomParentWidth = imageWidth;
            }

            $panzoomParent.css("max-width", maxWidth + "px");
            $panzoomParent.css("height", $viewer.height() + "px");

            var $panzoom = $panzoomParent.find(".panzoom");
            $panzoom.panzoom({
                $zoomIn: $viewer.find(".zoom-in"),
                $zoomOut: $viewer.find(".zoom-out"),
                $reset: $viewer.find(".zoom-reset"),
                //contain: "invert",
                startTransform: 'scale(1.0)',
                increment: 0.1,
                minScale: 1,
            }).panzoom("zoomIn");

        }, 0);
    </script>
</div>
