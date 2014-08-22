@ModelType VisualSwitchInterface.Models.AccountLoginModel
<div class="login-bg">
    <div class="row">
        <div class="col-lg-4 col-md-6 col-sm-12 col-md-offset-3 col-lg-offset-4">
            <div id="loginWell" class="well vertical-center well-lg">
                <div class="bottom-buffer-sm">
                    <strong class="text-lg">Please Login To Control TV Signage</strong>
                </div>
                @Html.ValidationSummary(True)
                <form action="@Request.Url" method="post">
                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Username)
                        @Html.TextBoxFor(Function(x) x.Username, New With {.class = "form-control", .autocomplete = "off"})
                        @Html.ValidationMessageFor(Function(x) x.Username)
                    </div>
                    <div class="form-group">
                        @Html.LabelFor(Function(x) x.Password)
                        @Html.PasswordFor(Function(x) x.Password, New With {.class = "form-control", .autocomplete = "off"})
                        @Html.ValidationMessageFor(Function(x) x.Password)
                    </div>
                    @If Model.IsLocked Then
                        @<div class="form-group">
                            <span class="text-warning">Please contact 000.000.000 for help.</span>
                        </div>
                    End If
                    <div class="clearfix">
                        <button type="submit" class="btn btn-primary pull-right">Login</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>

@Section scripts
    <script>
        $(function () {
            var performResize = function () {
                $(".vertical-center").each(function () {
                    var $this = $(this);
                    var originalMarginTop = parseInt($this.css("margin-top"));
                    var originalMarginBottom = parseInt($this.css("margin-bottom"));

                    var margin = ($(window).innerHeight() - ($(document.body).outerHeight() - originalMarginTop - originalMarginBottom) - 10) / 2;
                    if (margin <= 0)
                        return;

                    var marginCss = margin + "px";
                    $this.css("margin-top", marginCss).css("margin-bottom", marginCss);
                });
            };

            performResize();

            $(window).resize(function () {
                clearTimeout(global.timerId);
                global.timerId = setTimeout(function () {
                    performResize();
                }, 200);
            });
        });
    </script>
End Section