@Code
    Dim controller = Url.RequestContext.RouteData.Values("controller").ToString().ToLower()
End Code

<div class="container">
    <div class="row nav-pills-container">
        <ul class="nav nav-pills pull-right" role="tablist">
            <li><div class="top-buffer-sm"><span>Views:</span></div></li>
            <li class="@(IIf(controller.StartsWith("liveevent"), "active", ""))"><a href="@Url.Action("Index", "LiveEvent")">Live Events</a></li>
            <li class="@(IIf(controller.StartsWith("environment"), "active", ""))"><a href="@Url.Action("Index", "Environment")">Environments</a></li>
            <li class="@(IIf(controller.StartsWith("map"), "active", ""))"><a href="@Url.Action("Index", "Map")">Map View</a></li>
            <li class="@(IIf(controller.StartsWith("list"), "active", ""))"><a href="@Url.Action("Index", "List")">List View</a></li>
        </ul>
    </div>
</div>
