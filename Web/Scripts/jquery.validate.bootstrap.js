$.validator.setDefaults({
    focusInvalid: false,
    unhighlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass('has-error').addClass('has-info');
    },
    highlight: function (element, errorClass, validClass) {
        $(element).closest('.form-group').removeClass('has-info').addClass('has-error');

        $($.validator.unobtrusive).bind('onError', function (data, error) {
            $(error).addClass('help-block');
            var container = $("[data-valmsg-summary=true]");
            var cssClass = "alert alert-danger";
            $.each(container.find("ul li"), function () {
                if ($(this).is(":visible")) {
                    if (!container.hasClass(cssClass))
                        container.addClass(cssClass).show();
                    return false;
                }
            });
        });
    }
});