@ModelType VisualSwitchInterface.Models.SwitchModel

@If Convert.ToBoolean(ViewBag.IsSuccess) Then
    @<script>
        $(function () {
            $("#unhook").trigger("click");

            $("<a />").addClass("map-anchor ajax-open-modal")
                .attr("href", "@Url.Action("_SwitchClicked", New With {.id = Model.Id})")
                .attr("title", "@Model.Name").data("title", "@Model.Name")
                .css("left", "@Model.CoordX" + "px")
                .css("top", "@Model.CoordY" + "px")
                .css("width", "@Model.Width" + "px")
                .css("height", "@Model.Height" + "px")
                .text("@Model.Name")
                .appendTo($("#viewer .panzoom"));
        });
    </script>
Else
    @Html.EditorFor(Function(x) x)
End If
