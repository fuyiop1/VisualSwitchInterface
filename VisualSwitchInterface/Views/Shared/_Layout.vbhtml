<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title")</title>
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/bootstrap")

    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    @RenderBody()

    <div class="modal fade" id="defaultModal" tabindex="-1" role="dialog" aria-labelledby="defaultModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="defaultModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary modal-form-submit" data-text="Save">Save</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="notificationModal" tabindex="-1" role="dialog" aria-labelledby="notificationModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="notificationModalLabel" data-default-title="Notification">Notification</h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <div class="yes-or-no-btn-group hide">
                        <button type="button" class="btn btn-default modal-no-btn">No</button>
                        <button type="button" class="btn btn-primary modal-yes-btn">Yes</button>
                    </div>
                    <div class="ok-btn-group hide">
                        <button type="button" class="btn btn-primary modal-ok-btn">OK</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @*@Scripts.Render("~/bundles/jqueryval")*@
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/VisualSwitchInterface")
    @RenderSection("scripts", required:=False)
</body>
</html>
