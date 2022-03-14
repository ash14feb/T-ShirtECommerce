
$(document).ready(function () {
    $("#HomepageProducts").hide();
    $("#loader").show();
    LoadHomeProducts();
    HandleModals();
  
});

function CallGetSuccess(data) {
    var obj = JSON.parse(data);
    var outputhtml = ''
    $("#divproducts").html('');
    for (var i = 0; i < obj.Data.length; i++) {
        var objprod = obj.Data[i];
        var gender = "F";
        if (objprod.Gender == 1) {
            gender = "M"
        }
        outputhtml += "<div class='col-md-6 col-lg-4 col-xl-3 prdcard' >";
        outputhtml += "<div id='product-1' class='single-product'>";
        outputhtml += '<div onclick=\'callprodmodal(' + objprod.Id + ')\' class=\'part-1\' style=\' background-size:contain; background-repeat:no-repeat; background-image:url("../Content/ProdImages/' + objprod.Image + '") \'>';
        outputhtml += "<ul>";
        outputhtml += "<li><a href='#'onclick='calleditmodal(" + objprod.Id + ");'><i class='fas fa-edit'></i></a></li>";
        outputhtml += "<li><a href='#' onclick='calldeletemodal(" + objprod.Id + ");'><i class='fas fa-trash'></i></a></li>";
        outputhtml += "</ul>";
        outputhtml += " </div>";
        outputhtml += "<div class='part-2' style='text-align:'>";
        outputhtml += "<!--<h3 class='product-title'>Here Product Title</h3>-->";
        outputhtml += "<button type='button' class='btn btnprodinfo bgorange'>" + objprod.Size + "</button>";
        outputhtml += "<button type='button' class='btn btnprodinfo bgorange'>$" + objprod.Price + "</button>";
        outputhtml += "<button type='button' class='btn btnprodinfo bgorange'>" + gender + "</button>";
        outputhtml += "<!--<h4 class='product-old-price'>" + objprod.Price + "</h4>";
        outputhtml += "<h4 class='product-price'>$" + objprod.Price + "</h4>-->";
        outputhtml += "</div>";
        outputhtml += "</div >";
        outputhtml += "</div >";
    }

    outputhtml += "<div class='col-md-6 col-lg-4 col-xl-3' >";
    outputhtml += "<div class='single-product'>";
    outputhtml += "<div class='part-1'>";
    outputhtml += "   <button type='button' data-toggle='modal' onclick='callAddProdmodal()' class='btn btn-primary btn-lg bigaddbutton'>+ Add</button>";
    outputhtml += "  </div";
    outputhtml += "</div>";
    outputhtml += "</div>";

    $("#divproducts").append(outputhtml);
    $("#HomepageProducts").show();
    $("#loader").hide();
}
function CallGetFailure(data) {
    alert("SOmething went wrong!");
}
function LoadHomeProducts() {
    callGetAjax("/Home/GetAllProducts?Id=0", CallGetSuccess, CallGetFailure)
}



function ConfirmDelete() {
    var prod = {};
    prod.Id = $('#hdnDeleteId').val();
    callPostAjax("/Home/DeleteProd", prod, succ, err)

}
function succ() {
    $('#deleteModalin').modal('hide');
    LoadHomeProducts();
}
function err() {
    alert('Something went wrong!');
}


function calldeletemodal(id) {
    deleteclick = true;
    $('#hdnDeleteId').val(id);
    $('#deleteModalin').modal('show');
    $('#ShowProductModal').modal('hide');
}

function calleditmodal(id) {

    editclick = true;
    resetcontrols();
    $('#prodimage').removeAttr("required");
    $("#prodimage").css("border", "1px solid  #ced4da");
    $('#hdnEditId').val(id);
    callGetAjax("/Home/GetAllProducts?Id=" + id, CallGetSuccessProd, CallGetFailureProd)
    $('#AddEditProdModal').modal('show');
    $('#ShowProductModal').modal('hide');

}

function callprodmodal(id) {
    if (editclick || deleteclick) return;

    $('#hdnProdId').val(id);
    $('#ShowProductModal').modal('show');
    callGetAjax("/Home/GetAllProducts?Id=" + id, CallGetSuccessProdSingle, CallGetFailureProdSingle)
}
var CallGetSuccessProdSingle = function (result) {
    var obj = JSON.parse(result);
    $("#lblSize").html(obj.Data[0].Size.trim());
    $("#lblstyle").html(obj.Data[0].Style.trim());
    $("#lblprice").html("$" + obj.Data[0].Price);
    $("#lblmade").html(obj.Data[0].Made.trim());
    $("#lbldescription").html(obj.Data[0].Description.trim());
    $("#lblcolour").html(obj.Data[0].Colour.trim());
    var obj = JSON.parse(result);
    $("#drpSize option").each(function () {
        if ($(this).text() == obj.Data[0].Size.trim()) {
            $(this).attr('selected', 'selected');
        }
    });
    $("#drpstyle option").each(function () {
        if ($(this).text() == obj.Data[0].Style.trim()) {
            $(this).attr('selected', 'selected');
        }
    });
    document.getElementById("imgbigprev").style.display = "block";
    var output = document.getElementById('imgbigprev');
    output.src = "../Content/ProdImages/" + obj.Data[0].Image;

    if (obj.Data[0].Gender == 1) {

        $("#lblGender").html("Male");
    } else {
        $("#lblGender").html("Female");
    }

};

var CallGetFailureProdSingle = function () {
    alert("Something went wrong!!");
}
function callproddelete() {
    $('#ShowProductModal').modal('hide');
    calldeletemodal($('#hdnProdId').val())
}
function callprodedit() {
    $('#ShowProductModal').modal('hide');
    calleditmodal($('#hdnProdId').val());
}
function callAddProdmodal() {
    resetcontrols();
    clearFields();
    $("#modaltitle").html("Add New T-Shirt");
    $('#prodimage').attr("required", "true");
    $("#btnAddUpdate").html("Add");
    $('#AddEditProdModal').modal('show');
}
var CallGetSuccessProd = function (result) {
    $("#btnAddUpdate").html("Update");
    $("#modaltitle").html("Edit a T-Shirt");
    var obj = JSON.parse(result);
    $("#drpSize option").each(function () {
        if ($(this).text() == obj.Data[0].Size.trim()) {
            $(this).attr('selected', 'selected');
        }
    });
    $("#drpstyle option").each(function () {
        if ($(this).text() == obj.Data[0].Style.trim()) {
            $(this).attr('selected', 'selected');
        }
    });

    document.getElementById("imgpreview").style.display = "block";
    var output = document.getElementById('imgpreview');
    output.src = "../Content/ProdImages/" + obj.Data[0].Image;
    $('#txtprice').val(obj.Data[0].Price)
    $('#txtcolour').val(obj.Data[0].Colour)
    $('#txtmade').val(obj.Data[0].Made)
    if (obj.Data[0].Gender == 1) {
        $('#btnradio1').prop('checked', true);
    } else {
        $('#btnradio2').prop('checked', true);
    }
};
var CallGetFailureProd = function () {
    alert("Something went wrong!!");
}
var loadFile = function (event) {
    document.getElementById("imgpreview").style.display = "block";
    var output = document.getElementById('imgpreview');

    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};
function clearFields() {
    $("#drpSize").val("");
    $('#txtprice').val("");
    $('#txtcolour').val("");
    $('#txtmade').val("");
    $('#drpstyle').val("");
    $('#txtdescription').val("");
    $('#btnradio1').prop('checked', false);
    $('#btnradio2').prop('checked', false);
    $('#imgpreview').hide();
    $("#prodimage").val("")
}



function callsubmit() {
    $("#AddEditProdModal").submit();
}
var editclick = false;
var deleteclick = false;
function form_validate(attr_id) {
    var result = true;
    $('#' + attr_id).validator('validate');
    $('#' + attr_id + ' .form-group').each(function () {
        if ($(this).hasClass('has-error')) {
            result = false;
            return false;
        }
    });
    return result;
}


function resetcontrols() {

    $("#radiogender").css("border", "1px solid  #ced4da");
    $("#drpSize").css("border", "1px solid  #ced4da");
    $("#prodimage").css("border", "1px solid  #ced4da");
    $("#txtprice").css("border", "1px solid  #ced4da");
}

function validatefields(isedit) {
    var res = true;
    if ($("#drpSize").val() == null) {

        $("#drpSize").css("border", "1px solid red");
        res = false;
    }
    if ($("#txtprice").val() == "") {
        $("#txtprice").css("border", "1px solid red");
        res = false;
    }

    var $filePhoto = $("#prodimage")[0];
    if ($filePhoto.files.length == 0 && isedit == false) {
        $("#prodimage").css("border", "1px solid red");
        res = false;
    }

    if ($('#radiogender input:radio:checked').val() == 1 || $('#radiogender input:radio:checked').val() == 2) {

    }
    else {
        res = false;
        $('#radiogender').attr("required", true);
        $("#radiogender").css("border", "1px solid red");
    }

    if (res) {
        resetcontrols();
    }
    return res;
}

function SaveProduct() {
    var isedit = false;
    if ($("#btnAddUpdate").html().trim() == "Update") {
        isedit = true;
    }
    if (!validatefields(isedit)) {
        return false;
    }

    var formData = new FormData();
    var prod = {};
    if (isedit) {
        prod.Id = $('#hdnEditId').val();
    }
    prod.Size = $("#drpSize").val();
    prod.Price = $('#txtprice').val();
    prod.colour = $('#txtcolour').val();
    prod.made = $('#txtmade').val();
    prod.image = "image";
    prod.style = $('#drpstyle').val();
    prod.description = $('#txtdescription').val();
    prod.gender = $('#radiogender input:radio:checked').val();

    var $filePhoto = $("#prodimage")[0];
    if ($filePhoto.files.length > 0) {
        $.each($filePhoto.files, function (i, fileInfo) {
            formData.append("Image", fileInfo);
        });
    }
    formData.append("Result", JSON.stringify(prod));
    callPostAjaxWithFile("/Home/SaveImage", formData, onSuccessSubmissionsave, onErrorSubmissionsave);



}

var onSuccessSubmissionsave = function (result) {
    $('#AddEditProdModal').modal('hide');
    LoadHomeProducts();
};

var onErrorSubmissionsave = function () {
    alert("Something went wrong!!")
}

function HandleModals()
{
    $('#AddEditProdModal').on('hidden.bs.modal', function () {
        editclick = false;
        deleteclick = false;
    })
    $('#deleteModalin').on('hidden.bs.modal', function () {
        editclick = false;
        deleteclick = false;
    })
}