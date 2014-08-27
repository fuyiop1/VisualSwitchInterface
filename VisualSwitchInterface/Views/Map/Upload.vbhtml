@ModelType VisualSwitchInterface.Models.MapModel

<ol class="breadcrumb row">
    <li><a href="@Url.Action("Index", "Home")">Home</a></li>
    <li><a href="@Url.Action("Index")">Map View</a></li>
    <li class="active">Upload</li>
</ol>

<div id="viewer">
    <form action="@Url.Action("Upload")" method="post" class="form-horizontal file-upload-form" enctype="multipart/form-data">
        <div class="form-group">
            <label class="col-sm-2 col-xs-12 control-label">Document:</label>
            <div class="col-sm-5 col-xs-12">
                @Html.Partial("_FileInput")
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(Function(x) x.Name, New With {.class = "col-sm-2 col-xs-12 control-label"})
            <div class="col-sm-5 col-xs-12">
                @Html.TextBoxFor(Function(x) x.Name, New With {.class = "form-control", .autocomplete = "off"})
            </div>
            <div class="col-sm-5 col-xs-12">
                <p class="form-control-static">@Html.ValidationMessageFor(Function(x) x.Name)</p>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-5 col-xs-12 col-sm-offset-2">
                <button type="submit" class="btn btn-primary">Save</button>
                <a class="btn btn-link" href="@Url.Action("Index")">Cancel</a>
            </div>
        </div>
    </form>
</div>