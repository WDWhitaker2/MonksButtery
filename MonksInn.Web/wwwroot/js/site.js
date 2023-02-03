
var LoginModal = InitAjaxModal("#LoginModal", "/Account/LoginPartial");
var RegisterModal = InitAjaxModal("#RegisterModal", "/Account/Register");
var ForgottenPasswordModal = InitAjaxModal("#PasswordForgetModal", "/Account/ForgotPassword");


function AddAlert(text, type = 'success') {

    var fadeoutdelay = 5000;
    var fadeoutduration = 5000;
    var fadeinduration = 100;

    var html = $(`<div class="alert alert-${type}" role="alert">${text}</div>`);
    $(".alert-container").append(html);


    var timeout = setTimeout(function () {
        html.fadeTo(fadeoutduration, 0, function () {
            html.slideUp(fadeinduration, function () {
                $(this).alert('close');
            });
        });

    }, fadeoutdelay);


    html.on("mouseover", function (e) {
        window.clearTimeout(timeout);
        html.stop().fadeTo(fadeinduration, 100);
    });

    html.on("mouseout", function (e) {
        timeout = setTimeout(function () {
            html.fadeTo(fadeoutduration, 0, function () {
                html.slideUp(fadeinduration, function () {
                    $(this).alert('close');
                });
            });

        }, fadeoutdelay);
    });


    html.on("click", function (e) {
        html.fadeTo(fadeinduration, 0, function () {
            html.slideUp(fadeinduration, function () {
                $(this).alert('close');
            });
        });
    });

}

function InitAjaxModal(modalname, url, options) {

    var settings = {
        onsubmitcomplete: options?.onsubmitcomplete,
        onclose: options?.onclose,
        innerTarget: options?.innerTarget || ".modal-content",
        refreshOnPost: options?.refreshOnPost || false,
    };


    $(modalname).on("submit", "form", function (e) {
        e.preventDefault();
        submit();
        return false;
    });

    if (settings?.onclose != null) {
        $(modalname).on("hidden.bs.modal", function () {
            settings.onclose();
        });
    }

    var open = function (options) {

        if (settings.refreshOnPost == true) {
            $(`${modalname}`).modal("show");
        }
        else {

            $(".modal").modal("hide");
            PageSpinner.toggle(true);

            var urlparams = "";
            if (options?.getParameters != null) {
                urlparams = "?" + jQuery.param(options.getParameters);
            }

            $(`${modalname} ${settings.innerTarget}`).load(`${url}${urlparams}`, function (data) {
                $(`${modalname}`).modal("show");
                PageSpinner.toggle(false);
            });
        }
    }

    var submit = function () {

        PageSpinner.toggle(true);
        var formData = getFormData();

        $.ajax({
            url: url,
            method: 'post',
            data: formData,
            complete: function (data) {
                PageSpinner.toggle(false);
                $(`${modalname} ${settings.innerTarget}`).html(data.responseText);
                if (settings.onsubmitcomplete != null) {
                    settings.onsubmitcomplete();
                }
            }
        });


    }

    var reset = function () {
        PageSpinner.toggle(true);
        var formData = [];

        $.ajax({
            url: url,
            method: 'post',
            data: formData,
            complete: function (data) {
                $(`${modalname} ${settings.innerTarget}`).html(data.responseText);
                PageSpinner.toggle(false);
                if (settings.onsubmitcomplete != null) {
                    settings.onsubmitcomplete();
                }
            }
        });
    }

    var getFormData = function () {
        var formData = $(`${modalname} form`).serializeArray();
        return formData;
    }

    return {
        open: open,
        submit: submit,
        getFormData: getFormData,
        reset:reset,
    };
}



function InitAjaxPartial(container, url, options) {

    var settings = {
        InitOnReady: options?.InitOnReady ?? true,
    }

    var getdata = function (options) {

        settings = {
            formdata: options?.formdata,
        };

        PageSpinner.toggle(true);

        var fullurl = url;
        if (settings.formdata != null) {
            fullurl += "?" + settings.formdata
        }

        $.get(fullurl, function (data) {
            $(container).html(data);
            PageSpinner.toggle(false);

        });
    }

    if (settings.InitOnReady) {
        $(document).ready(function () {
            getdata();
        });
    }


    return { refresh: getdata };
}

function InitAjaxForm(container, url, options) {
    var settings = {
        InitOnReady: options?.InitOnReady ?? true,
        onsubmitcomplete: options.onsubmitcomplete,
    };

    var getdata = function () {
        PageSpinner.toggle(true);
        $.get(url, function (data) {
            $(container).html(data);
            PageSpinner.toggle(false);

        });
    }

    if (settings.InitOnReady) {
        $(document).ready(function () {
            getdata();
        });
    }

    $(container).on("submit", "form", function (e) {
        e.preventDefault();
        PageSpinner.toggle(true);
        var formData = $(`${container} form`).serializeArray();
        $.ajax({
            url: url,
            method: 'post',
            data: formData,
            complete: function (data) {
                $(`${container}`).html(data.responseText);
                PageSpinner.toggle(false);
                if (settings.onsubmitcomplete != null) {
                    settings.onsubmitcomplete();
                }
            }
        });
        return false;
    });
}

function InitGetRequest(url, oncomplete) {

    PageSpinner.toggle(true);
    $.get(url, function (data) {
        $(container).html(data);
        PageSpinner.toggle(false);
        if (oncomplete != null) {
            oncomplete(data);
        }
    });
}

var PageSpinner = (function () {

    var spinnersCount = 0;

    var toggle = function (show, force = false) {
        if (show) {
            spinnersCount++;
            $(".full-page-spinner").addClass("in");
        }
        else {
            spinnersCount--;
            if (force || spinnersCount <= 0) {
                spinnersCount = 0;
                $(".full-page-spinner").removeClass("in");
            }

        }
    }

    return {
        toggle: toggle,
        spinnercount: spinnersCount
    };

})();

function ToggleSideNav() {
    $("#navbarlinks").toggleClass("open");
}



var FadeShow = (function () {

    function InitFader() {

        $(document).ready(function (e) {




        });
    }

    return {
        init: InitFader
    };
})();


//var AjaxHelper = (function () {

//    function InitAjaxPartial(container, url, options) {
//        var settings = {
//            initOnReady: options?.initOnReady ?? true,
//            onGetComplete: options.onGetComplete,
//        };

//        function Fetch (options) {

//            var getdatasettings = {
//                formdata: options?.formdata,
//            };

//            var fullurl = url;
//            if (getdatasettings.formdata != null) {
//                fullurl += "?" + getdatasettings.formdata
//            }

//            PageSpinner.toggle(true);
//            $.get(fullurl, function (data) {
//                $(container).html(data);
//                PageSpinner.toggle(false);

//                if (settings.onGetComplete != null) {
//                    settings.onGetComplete();
//                }

//            });
//        }

//        if (settings.InitOnReady) {
//            $(document).ready(function () {
//                getdata();
//            });
//        }

//        return {
//            Fetch,
//        }
//    }

//    function InitAjaxForm(container, url, options) {

//        var settings = {
//            onSubmitComplete: options.onSubmitComplete,
//        };

//        var Partial = InitAjaxPartial(container, url, options);

//        function Submit() {
//            PageSpinner.toggle(true);
//            var formData = $(`${container} form`).serializeArray();
//            $.ajax({
//                url: url,
//                method: 'post',
//                data: formData,
//                complete: function (data) {
//                    $(`${container}`).html(data.responseText);
//                    PageSpinner.toggle(false);
//                    if (settings.onSubmitComplete != null) {
//                        settings.onSubmitComplete();
//                    }
//                }
//            });
//        }

//        function Fetch(options) {
//            Partial.Fetch(options);
//        }

//        $(container).on("submit", "form", function (e) {
//            e.preventDefault();
//            Submit();
//            return false;
//        });

//        return {
//            Fetch,
//            Submit,
//        }
//    }

//    function InitAjaxModal(container, url, options) {
//        var settings = {
//            onclose: options?.onclose,
//            innerTarget: options?.innerTarget || ".modal-content",
//            fetchOnSubmit: options?.fetchOnSubmit || false,
//        };

//        option.onSubmitComplete = function () {
//            $(`${container} ${settings.innerTarget}`).html(data.responseText);
//            if (settings.onsubmitcomplete != null) {
//                settings.onsubmitcomplete();
//            }

//        }

        

//        var Form = InitAjaxForm(`${container} ${settings.innerTarget}`, url, options)

//        function Fetch() {
//            Form.Fetch()
//        }

//        function Fetch() {
//            Form.Fetch()
//        }

//    }


//    return {
//        InitAjaxPartial,
//        InitAjaxForm,
//        InitAjaxModal,
//    }
//})();