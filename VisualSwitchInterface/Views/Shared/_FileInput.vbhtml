<div class="input-file-wrapper">
    <input type="file" name="inputFile" data-name-target="#Name" />
    <button type="button" class="btn btn-info btn-sm">Choose a file</button>
    <span class="chosen-file-text">
        @Html.ValidationMessage("file")
    </span>
    <span class="field-validation-error hide no-file">No files.</span>
</div>