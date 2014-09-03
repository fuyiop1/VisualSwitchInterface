@ModelType VisualSwitchInterface.Models.SwitchModel

<form class="ajax-form" action="@Request.Url">
    @Html.HiddenFor(Function(x) x.Id, New With {.autocomplete = "off"})
    @Html.HiddenFor(Function(x) x.MapId, New With {.autocomplete = "off"})
    @Html.HiddenFor(Function(x) x.CoordX, New With {.autocomplete = "off"})
    @Html.HiddenFor(Function(x) x.CoordY, New With {.autocomplete = "off"})
    @Html.HiddenFor(Function(x) x.Width, New With {.autocomplete = "off"})
    @Html.HiddenFor(Function(x) x.Height, New With {.autocomplete = "off"})
    <div class="form-group">
        @Html.LabelFor(Function(x) x.Name)
        @Html.TextBoxFor(Function(x) x.Name, New With {.class = "form-control", .autocomplete = "off"})
        @Html.ValidationMessageFor(Function(x) x.Name)
    </div>
    <div class="form-group">
        <label>Coordinates</label>
        <p class="form-control-static">@(Model.CoordX), @Model.CoordY</p>
        <label>Width</label>
        <p class="form-control-static">@Model.Width</p>
        <label>Height</label>
        <p class="form-control-static">@Model.Height</p>
    </div>
</form>