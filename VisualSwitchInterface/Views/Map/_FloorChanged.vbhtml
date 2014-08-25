@ModelType VisualSwitchInterface.Models.MapModel

<img class="map-img" src="@Model.FilePath" alt="" />
@For Each switchModel In Model.SwitchModels
    Dim coorXPercent = (switchModel.CoordX * 100).ToString + "%"
    Dim coorYPercent = (switchModel.CoordY * 100).ToString + "%"
    @<a class="absolute ajax-open-modal" href="@Url.Action("_SwitchClicked", New With {.id = switchModel.Id})" style="left: @coorXPercent; top: @coorYPercent" title="@switchModel.Name" data-title="@switchModel.Name"><span class="glyphicon glyphicon-map-marker text-super-danger text-lg"></span></a>
Next