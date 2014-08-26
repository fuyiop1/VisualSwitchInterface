@ModelType VisualSwitchInterface.Models.SwitchModel

<form class="ajax-form" action="@Request.Url">
    @Html.HiddenFor(Function(x) x.Id)
    @Html.HiddenFor(Function(x) x.CoordX)
    @Html.HiddenFor(Function(x) x.CoordY)
    <div class="form-group">
        @Html.LabelFor(Function(x) x.Name)
        @Html.TextBoxFor(Function(x) x.Name, New With {.class = "form-control", .autocomplete = "off"})
        @Html.ValidationMessageFor(Function(x) x.Name)
    </div>
    <div class="form-group">
        <label>Coordinates</label>
        <p class="form-control-static">@(Model.CoordX), @Model.CoordY</p>
    </div>
</form>