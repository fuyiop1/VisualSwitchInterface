@ModelType IList(Of VisualSwitchInterface.Models.MapModel)

<ol class="breadcrumb row bottom-buffer-sm">
    <li><a href="@Url.Action("Index", "Home")">Home</a></li>
    <li class="active">Map View</li>
</ol>

<div id="viewer">
    <div class="bottom-buffer-sm">
        <a href="@Url.Action("Upload")">Click to upload a map</a>
    </div>

    @If Model.Count > 0 Then
        @<div>
            <div>
                <strong>Available maps:</strong>
            </div>
            @For Each item In Model
                @<p><a href="@Url.Action("ViewMap", New With {.id = item.Id})">@item.Name</a></p>
            Next
        </div>
    Else
        @<div>
            <strong>No maps available.</strong>
        </div>
    End If
</div>