@ModelType VisualSwitchInterface.Models.AccountLoginModel
<div class="row">
    <div class="col-lg-4 col-md-6 col-sm-12 col-md-offset-3 col-lg-offset-4">
        <div id="loginWell" class="well well-lg">
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
                <div class="clearfix">
                    <button type="submit" class="btn btn-primary pull-right">Login</button>
                </div>
            </form>
        </div>
    </div>
</div>