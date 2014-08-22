@ModelType VisualSwitchInterface.Models.AccountLoginModel
<div class="row login-bg">
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

@Section scripts
    <script>
        $(function () {
            var performResize = function () {
                $(".vertical-center").each(function () {
                    var $this = $(this);
                    var originalMarginTop = parseInt($this.css("margin-top"));
                    var originalMarginBottom = parseInt($this.css("margin-bottom"));

                    var spaceHeight = $(window).innerHeight() - ($(document.body).outerHeight() - originalMarginTop - originalMarginBottom);
                    if (spaceHeight <= 0)
                        return;

                    var marginTopCss = ((1 - 0.618) * spaceHeight) + "px";
                    var marginBottomCss = (0.618 * spaceHeight) + "px";

                    $this.css("margin-top", marginTopCss).css("margin-bottom", marginBottomCss);
                });
            };

            performResize();

            $(window).off("resize").resize(function () {
                clearTimeout(global.timerId);
                global.timerId = setTimeout(function () {
                    performResize();
                }, 200);
            });
        });
    </script>
End Section