
/*AJAX POST AND GET CALLS*/
function callGetAjax(url, successCallback, errorCallback) {
    $.ajax({
        type: "GET",
        url: url,
        async: true,
        success: function (result) {
                successCallback(result);
        },
        error: function (err) {
            if (typeof errorCallback === 'function')
                errorCallback(err);
        }
    });
}

function callPostAjax(url, data, successCallback, errorCallback) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (result) {
                if (typeof successCallback === 'function')
                    successCallback(result);
        },
        error: function (err) {
            if (typeof errorCallback === 'function')
                errorCallback(err);
        }
    });
}

function callPostAjaxWithFile(url, data, successCallback, errorCallback) {
    $.ajax({
        type: "POST",
        contentType: false,
        processData: false,
        url: url,
        data: data,
        success: function (result) {
            successCallback(result);
        },
        error: function (err) {
            if (typeof errorCallback === 'function')
                errorCallback(err);
        }
    });
}