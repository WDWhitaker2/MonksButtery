// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function InitFilterTable(searchinput, tableinput) {

    $(document).ready(function () {
        $(searchinput).on("keyup", function () {
            var value = $(this).val().toLowerCase();

            DoFilter(value);

        });

        DoFilter($(searchinput).val());
    });

    function DoFilter(value) {

        $(`${tableinput} tr`).filter(function () {
            var cells = $(this).children("td").not(".ignorefilter");

            var result = false;

            cells.each(function () {
                if (result != true) {
                    result = $(this).text().toLowerCase().indexOf(value) > -1
                }
            });

            $(this).toggle(result);
        });
    }
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

function ConfirmDelete(url, label) {
    PageSpinner.toggle(true);
    $("#DeleteModal #DeleteLabel").html(label);
    $("#DeleteModal #DeleteButton").attr("href", url);
    $("#DeleteModal").modal("show");
    PageSpinner.toggle(false);

}


function UploadFile(selector, isImage = false) {

    var hiddenfield = $(`${selector} .hidden-file-input`);
    var fileSelector = $(`${selector} .custom-file-input`);
    var filelabel = $(`${selector} .custom-file-label`);
    var progress = $(`${selector} .progress`);
    var progressbar = $(`${selector} .progress-bar`);
    var deletebutton = $(`${selector} .custom-delete-file-button`);
    var selectfilebuttoncontainer = $(`${selector} .uploadfilecontainer`);

    progressbar.width('0%');
    progress.show();

    var filenames = [];
    var files = fileSelector.prop('files');
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("Files", files[i]);
        filenames.push(files[i].name);
    }

    if (new XMLHttpRequest().upload) {
        var xhr = new XMLHttpRequest();

        (xhr.upload || xhr).addEventListener('progress', function (e) {
            var done = e.position || e.loaded
            var total = e.totalSize || e.total;
            var donePercent = Math.round(done / total * 100);

            if (donePercent < 100) {
                progressbar.html(donePercent + "%");
                progressbar.width(donePercent + '%');
                progressbar.attr('aria-valuenow', donePercent);
            } else {
                progressbar.html("Saving");
            }

        });

        xhr.addEventListener('load', function (e) {
            UploadComplete(this.responseText, isImage);
        });

        xhr.open('post', '/File/UploadFilesXHR', true);
        xhr.send(formData);
    }
    else {
        $.ajax({
            url: '/File/UploadFilesXHR',
            data: formData,
            processData: false,
            contentType: false,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                UploadComplete(data, isImage);
            }
        });
    }


    function UploadComplete(data, isImage) {

        data = data.replace('"', '');
        data = data.replace(' ', '');

        progressbar.attr('aria-valuenow', 100);
        progressbar.css('width', '100%');
        progressbar.html("Uploaded");
        fileSelector.val('');
        filelabel.html(filenames.join(', '));
        hiddenfield.val(data);

        if (isImage) {
            var FileImage = $(`${selector} .custom-file-image`);

            var IDlist = data.split(",");

            var html = ""
            for (var i = 0; i < IDlist.length; i++) {

                html += `<img src="/File/Get/${IDlist[i]}" />`
            }



            FileImage.html(`<img src="/File/Get/${data}" />`);
            deletebutton.show();
            selectfilebuttoncontainer.hide();
        }

        window.setTimeout(function () {
            progress.hide();
        }, 1000);
    }

}

function RemoveFile(selector) {
    var hiddenfield = $(`${selector} .hidden-file-input`);
    var fileSelector = $(`${selector} .custom-file-input`);
    var filelabel = $(`${selector} .custom-file-label`);
    var FileImage = $(`${selector} .custom-file-image`);
    var deletebutton = $(`${selector} .custom-delete-file-button`);
    var selectfilebuttoncontainer = $(`${selector} .uploadfilecontainer`);

    FileImage.html('');
    hiddenfield.val('');
    fileSelector.val('');
    filelabel.html('');
    deletebutton.hide();
    selectfilebuttoncontainer.show();
}

function ToggleSideNav() {
    $(".sidebar").toggleClass("open");
}

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
    };

    var open = function (options) {
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

    $(modalname).on("submit", "form", function (e) {
        e.preventDefault();
        PageSpinner.toggle(true);
        var formData = $(`${modalname} form`).serializeArray();
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
        return false;
    });

    if (settings?.onclose != null) {
        $(modalname).on("hidden.bs.modal", function () {
            settings.onclose();
        });
    }


    return {
        open: open,
    };



}

function InitAjaxPartial(container, url, options) {

    var settings = {
        InitOnReady: options?.InitOnReady ?? true,
    }

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


    return { refresh: getdata };
}

function scrollToHash() {

    // get hash value
    var hash = window.location.hash;
    // now scroll to element with that id
    $('html, body').animate({ scrollTop: $(hash).offset().top });
}