@ModelType VisualSwitchInterface.Models.MapModel

@Code
    Dim otherMaps As IList(Of VisualSwitchInterface.Models.MapModel) = ViewBag.otherMaps
End Code

<script>
    var mapSwitches = [];
</script>

<ol class="breadcrumb row bottom-buffer-sm">
    <li><a href="@Url.Action("Index", "Home")">Home</a></li>
    <li><a href="@Url.Action("Index")">Map View</a></li>
    <li class="active">Floor View - @Model.Name</li>
</ol>

<div class="clearfix bottom-buffer-sm">
    <div class="pull-left">
        <div class="text-lg">
            <strong>Floor View</strong>
        </div>
        <div>
            <span>Select the Room from the Floor you want to control.</span>
        </div>
    </div>
    <div class="pull-right">
        <div class="dropdown">
            <a href="#" class="btn dropdown-toggle" data-toggle="dropdown">@Model.Name <span class="caret"></span></a>
            <ul class="dropdown-menu dropdown-menu-right" role="menu">
                @If otherMaps.Count > 0 Then
                    For Each otherMap In otherMaps
                    @<li role="presentation"><a href="@Url.Action("ViewMap", New With {.id = otherMap.Id})" role="menuitem" tabindex="-1">@otherMap.Name</a></li>
                    Next
                Else
                    @<li role="presentation"><a class="text-curor" href="#" role="menuitem" tabindex="-1"><span class="text-warning">No other floors</span></a></li>
                End If
            </ul>
        </div>
    </div>
    <div class="pull-right">
        <span class="btn-fake">Floor:</span>
    </div>
</div>

<div id="viewer">
    @*<div class="text-center">
            <span class="text-danger">Click the empty area of the map to add a new switch or click the polygon to control the switch.</span>
        </div>*@
    <div class="clearfix">
        <div class="pull-left">
            <div class="btn-group-vertical bottom-buffer">
                <button type="button" class="btn btn-sm btn-default zoom-in" title="Zoom in"><span class="glyphicon glyphicon-plus"></span></button>
                <button type="button" class="btn btn-sm btn-default zoom-out" title="Zoom out"><span class="glyphicon glyphicon-minus"></span></button>
                <button type="button" class="btn btn-sm btn-default zoom-reset" title="Reset"><span class="glyphicon glyphicon-repeat"></span></button>
            </div>

            <div>
                <button id="rehook" type="button" class="btn btn-sm btn-primary" title="Draw polygon"><span class="glyphicon glyphicon-pencil"></span></button>
            </div>

            <div class="bottom-buffer">
                <button id="unhook" type="button" class="btn btn-sm btn-danger hide" title="Cancel"><span class="glyphicon glyphicon-off"></span></button>
            </div>

            <div class="bottom-buffer">
                <button id="apply" type="button" class="btn btn-sm btn-success hide" title="Apply"><span class="glyphicon glyphicon-ok"></span></button>
            </div>

        </div>
        <div class="pull-right">
            <div class="relative" style="width: 72px">
                <button id="upBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 24px; top: 0;" title="Up"><span class="glyphicon glyphicon-circle-arrow-up"></span></button>
                <button id="leftBtn" type="button" class="btn btn-xs btn-default absolute" style="right:  48px; top: 22px;" title="Left"><span class="glyphicon glyphicon-circle-arrow-left"></span></button>
                <button id="rightBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 0; top: 22px;" title="Right"><span class="glyphicon glyphicon-circle-arrow-right"></span></button>
                <button id="downBtn" type="button" class="btn btn-xs btn-default absolute" style="right: 24px; top: 44px;" title="Down"><span class="glyphicon glyphicon-circle-arrow-down"></span></button>
                <div style="padding: 80px 5px 0;">
                    <div class="form-group">
                        <label>X:</label>
                        <input id="x" type="text" class="form-control" autocomplete="off" readonly />
                    </div>
                    <div class="form-group">
                        <label>Y:</label>
                        <input id="y" type="text" class="form-control" autocomplete="off" readonly />
                    </div>
                    <div class="form-group">
                        <label>Width:</label>
                        <input id="w" type="text" class="form-control" autocomplete="off" readonly />
                    </div>
                    <div class="form-group">
                        <label>Height:</label>
                        <input id="h" type="text" class="form-control" autocomplete="off" readonly />
                    </div>
                </div>
            </div>
        </div>
        <div class="pull-right">
            <div class="panzoom-parent">
                <div class="panzoom relative">
                    <img id="map" src="@Model.FilePath" alt="" />
                    @For Each switchModel In Model.SwitchModels
                        @*@<a class="map-anchor ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @(switchModel.CoordX)px; top: @(switchModel.CoordY)px" title="@switchModel.Name" data-title="@switchModel.Name"><span class="glyphicon glyphicon-asterisk text-super-danger text-lg"></span></a>*@
                        @<a class="map-anchor ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @(switchModel.CoordX)px; top: @(switchModel.CoordY)px; width: @(switchModel.Width)px; height: @(switchModel.Height)px" title="@switchModel.Name" data-title="@switchModel.Name">@switchModel.Name</a>
                        @<script>
                            mapSwitches.push({
                                x: @switchModel.CoordX,
                                y: @switchModel.CoordY,
                                w: @switchModel.Width,
                                h: @switchModel.Height,
                            });
                        </script>
                    Next
                </div>
            </div>
        </div>
        @*<a id="addSwitchBtn" href="#" data-href="@Url.Action("_AddSwitch", New With {.mapId = Model.Id})" class="hide ajax-open-modal" data-title="Add Switch"></a>*@
        <a id="addSwitchBtn" href="#" data-href="@Url.Action("_AddNewSwitch", New With {.mapId = Model.Id})" class="hide ajax-open-modal" data-title="Add Switch"></a>
    </div>
</div>

@Section scripts
    @Scripts.Render("~/bundles/jqueryPanzoom")
    <script>
        $(function () {
            function initPanzoom() {
                var $viewer = $("#viewer");
                var $panzoomParent = $viewer.find(".panzoom-parent");

                var imageWidth = parseInt("@Model.Width");
                var imageHeight = parseInt("@Model.Height");
                var panzoomParentWidth = global.findContainer($viewer, ".container").width() - 34 - 72;

                $panzoomParent.width(panzoomParentWidth);
                //$panzoomParent.height($viewer.height() - 20);
                $panzoomParent.height($viewer.height());

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

                //$panzoom.find("img").on("click", function (e) {
                //    var offset = $(this).offset();
                //    var scale = $("#viewer .panzoom").panzoom("getMatrix")[0];
                //    var coordX = parseInt((e.clientX - offset.left) / scale);
                //    var coordY = parseInt((e.clientY - offset.top) / scale);
                //    var $addSwitchBtn = $("#addSwitchBtn");
                //    $addSwitchBtn.attr("href", $addSwitchBtn.data("href") + "&coordX=" + coordX + "&coordY=" + coordY);
                //    $addSwitchBtn.trigger("click");
                //});

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
            };

            var jcropApi;

            function initJcrop() {
                $("#rehook").on("click", function () {
                    $('#map').Jcrop({
                        touchSupport: true,
                        onChange: processCoords,
                        onSelect: jcropSelected,
                        onRelease: clearCoords
                    }, function () {
                        jcropApi = this;
                    });

                    $(this).addClass("hide");
                    $("#unhook").removeClass("hide");
                });

                $("#unhook").on("click", function () {
                    jcropApi.release();
                    jcropApi.destroy();

                    $(this).addClass("hide");
                    $("#apply").addClass("hide");
                    $("#rehook").removeClass("hide");
                });

                $("#apply").on("click", function () {
                    var x = $('#x').val();
                    var y = $('#y').val();
                    var w = $('#w').val();
                    var h = $('#h').val();

                    var $addSwitchBtn = $("#addSwitchBtn");
                    $addSwitchBtn.attr("href", $addSwitchBtn.data("href") + "&coordX=" + x + "&coordY=" + y + "&width=" + w + "&height=" + h);
                    $addSwitchBtn.trigger("click");
                });
            };

            function processCoords(c) {
                $('#x').val(parseInt(c.x));
                $('#y').val(parseInt(c.y));
                //$('#x2').val(c.x2);
                //$('#y2').val(c.y2);
                $('#w').val(parseInt(c.w));
                $('#h').val(parseInt(c.h));
            };

            function clearCoords() {
                $('#x, #y, #w, #h').val('');
                $("#apply").addClass("hide");
            };

            function jcropSelected(e) {
                var coords = processOverlap(e, mapSwitches);
                if (coords) {
                    //coords.w = coords.x2 - coords.x;
                    //coords.h = coords.y2 - coords.y;
                    //processCoords(coords);
                    jcropApi.animateTo([coords.x, coords.y, coords.x2, coords.y2]);
                    $("#apply").removeClass("hide");
                }else {
                    jcropApi.release();
                }
            }

            function processOverlap(coords, others){
                var result = {
                    x: parseInt(coords.x),
                    y: parseInt(coords.y),
                    x2: parseInt(coords.x2),
                    y2: parseInt(coords.y2),
                };

                var item = {};
                for (var i = 0; i < others.length; i++) {
                    item = others[i];
                    item.x2 = item.x + item.w;
                    item.y2 = item.y + item.h;

                    if (Math.abs(result.x2 + result.x - item.x2 - item.x) < result.x2 - result.x + item.x2 - item.x
                        && Math.abs(result.y2 + result.y - item.y2 - item.y) < result.y2 - result.y + item.y2 - item.y) {
                        if (result.x >= item.x && result.x <= item.x2 && result.y >= item.y && result.y <= item.y2
                            && result.x2 >= item.x && result.x2 <= item.x2 && result.y2 >= item.y && result.y2 <= item.y2) {
                            return null;
                        }else if (result.x >= item.x && result.x <= item.x2 && result.x2 >= item.x && result.x2 <= item.x2) {
                            if (result.y > item.y) {
                                result.y = item.y2;
                            }else {
                                result.y2 = item.y;
                            }
                        }else{
                            if (result.x < item.x) {
                                result.x2 = item.x;
                            }else {
                                result.x = item.x2;
                            }
                        }
                    }
                }

                return result;
            }

            initPanzoom();
            initJcrop();
        });
    </script>
End Section

