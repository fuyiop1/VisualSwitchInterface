@Code
    ViewData("Title") = "Index"
End Code

@Html.Partial("_TopNav")

<div class="row">
    <a class="ajax-open-modal" href="@Url.Action("_MapClicked")">
        <img class="map-img" src="~/Content/Images/VisualSwitchInterface/sample-floor.png" alt="" />
    </a>
</div>
