@ModelType VisualSwitchInterface.Models.SwitchModel

@If Convert.ToBoolean(ViewBag.IsSuccess) Then
    @<script>
        $(function () {
            $("<a />").addClass("absolute ajax-open-modal")
                .attr("href", "@Url.Action("_SwitchClicked", New With {.id = Model.Id})")
                .attr("title", "@Model.Name").data("title", "@Model.Name")
                .css("left", "@Model.CoordX" + "px")
                .css("top", "@Model.CoordY" + "px")
                .append($("<span />").addClass("glyphicon glyphicon-map-marker text-super-danger text-lg"))
                .appendTo($("#viewer .panzoom"));
        });
    </script>
Else
    @Html.EditorFor(Function(x) x)
End If
