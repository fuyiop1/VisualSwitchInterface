@Code
    ViewData("Title") = "Index"
End Code

@Html.Partial("_TopNav")

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
            <a href="#" class="btn dropdown-toggle" data-toggle="dropdown">Select Floor <span class="caret"></span></a>
            <ul class="dropdown-menu" role="menu">
                <li role="presentation"><a href="#" role="menuitem" tabindex="-1">First Floor</a></li>
                <li role="presentation"><a href="#" role="menuitem" tabindex="-1">Second Floor</a></li>
                <li role="presentation"><a href="#" role="menuitem" tabindex="-1">Third Floor</a></li>
            </ul>
        </div>
    </div>
    <div class="pull-right">
        <span class="btn-fake">Floor:</span>
    </div>
</div>

<div class="clearfix">
    <div id="viewer" class="relative pull-left center-block">
        @Html.Action("_FloorChanged")
    </div>
</div>

@Section scripts
    <script>
        $(function () {
            global.resizeViewer();

            $(window).off("resize").resize(function () {
                clearTimeout(global.timerId);
                global.timerId = setTimeout(function () {
                    global.resizeViewer();
                }, 200);
            });
        });
    </script>
End Section
