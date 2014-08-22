<!DOCTYPE html>
<html>
<head>
    @Code
        Dim title As String = ViewData("Title")
        If String.IsNullOrEmpty(title) Then
            title = "Visual Switch Interface"
        End If
    End Code
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@title</title>
    @Styles.Render("~/Content/bootstrap")
    @Styles.Render("~/Content/css")

    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <header class="bottom-buffer-sm">
        <div class="container">
            <div class="row">
                <div id="headerLeft" class="col-xs-8 col-sm-4">
                </div>
                <div id="headerMiddle" class="hidden-xs col-sm-4">
                    <div><span class="text-lg">&nbsp;&nbsp;&nbsp;&nbsp;Switching App</span></div>
                    <div><span>Powered By:</span></div>
                </div>
                <div id="headerRight" class="hidden-xs col-xs-4 col-sm-4">
                </div>
            </div>
        </div>
    </header>

    <div class="container">
        @RenderBody()
    </div>

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
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/VisualSwitchInterface")
    @RenderSection("scripts", required:=False)
</body>
</html>
