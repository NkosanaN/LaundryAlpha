var totalL = 0.00, totalB = 0.00, totalC = 0.00;
var table;
$(document).ready(function () {
    table = $('table.display').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false
    });
});
//table.clear().draw();
function clearFields() {
    $('#inputName').val('');
    $('#Surname').val('');
    $('#inputEmail').val('');
    $('#inputAddress').val('');
    $('#inputAddress2').val('');
    $('#Catergory').prop('selectedIndex', 0);
    $('#tblBedding').addClass('d-none');
    $('#tblLaundry').addClass('d-none');
    $('input[type="checkbox"]').prop('checked', false);
    totalL = 0.00, totalB = 0.00; totalC = 0.00;
    $('#totall').html('0.00');
    $('#totalb').html('0.00');
    $('#totalc').html('0.00');
}

$("input:checkbox[name='row-check']").on('change', function () {

    let selectOption = $(this).attr('id');
    let selected = $(this).parents('tr:eq(0)');
    let price = parseInt($(selected).find('td:eq(1)').text());
    let serviceName = localStorage.getItem('getServiceName');

    if ($("#" + selectOption).is(':checked')) {

        if (serviceName == 'Laundry') {
            totalL += price;
            $('#totall').html(totalL.toFixed(2));
        }
        else if (serviceName == 'HouseHolds') {
            totalB += price;
            $('#totalb').html(totalB.toFixed(2));
        }
        else if (serviceName == 'Comforters') {
            totalC += price;
            $('#totalc').html(totalC.toFixed(2));
        }
    }
    else
    {
        if (serviceName == 'Laundry') {
            totalL -= price;
            $('#totall').html(totalL.toFixed(2));
        }
        else if (serviceName == 'HouseHolds') {
            totalB -= price;
            $('#totalb').html(totalB.toFixed(2));
        }
        else if (serviceName == 'Comforters') {
            totalC -= price;
            $('#totalc').html(totalC.toFixed(2));
        }
    }
});
$('#Catergory').on('change', function () {
    let selected = $(this).val();
    $('#tblLaundry').show();
    localStorage.setItem("getServiceName", selected);
    console.log(selected)
    switch (selected) {

        case 'Comforters':
            $('#tblComforters').removeClass('d-none');
            $('#tblLaundry').addClass('d-none');
            $('#tblBedding').addClass('d-none');
            break;
        case 'Laundry':
            $('#tblLaundry').removeClass('d-none');
            $('#tblBedding').addClass('d-none');
            $('#tblComforters').addClass('d-none');
            break;
        case 'HouseHolds':
            $('#tblBedding').removeClass('d-none');
            $('#tblLaundry').addClass('d-none');
            $('#tblComforters').addClass('d-none');
            break;

        default:
    }
});

//$('#adminModalToggle').on('show.bs.modal', function (e) {

//    $('#AdminformBackdrop').modal('hide');
//    $('#staticBackdrop').modal('hide');

//    $('#summaryTable > tbody > tr .name').html($('#inputName').val());
//    $('#summaryTable > tbody > tr .surname').html($('#Surname').val());
//    $('#summaryTable > tbody > tr .email').html($('#inputEmail').val());
//    $('#summaryTable > tbody > tr .address').html($('#inputAddress').val());
//    $('#summaryTable > tbody > tr .phoneNr').html($('#MobilePhoneNumber').val());

//    createSummaryTable(AdmingetSelectedRows());
//});

//function createSummaryTable(data) {
//    var html = '';

//    $('#adminsummaryViewTbl tbody').empty();
//    console.log("Data =>" + data);

//    $.each(data, function (i, items) {
//        html += "<tr><td>" + items.ProductName
//            + "</td><td style='text-align: right'>" + items.ListPrice
//            + "</td></tr>"
//    });
//    html += "<tr><th>Total</th>"
//        + "<th style='text-align: right'>" + (totalL + totalB)
//        + "</th></tr>";

//    console.log(html);

//    $('#adminsummaryViewTbl').append(html);
//}

//function getSelectedRows() {
//    var data = [];
//    var rows = $(table.$('input[type="checkbox"]').map(function () {
//        return $(this).prop("checked") ? $(this).closest('tr') : null;
//    }));

//    if (rows.length == 0) {
//        return false;
//    }

//    $.each(rows, function () {
//        var obj = {};
//        let cRow = $(this).closest("tr");
//        obj["ProductName"] = cRow.find("td:eq(0)").html();
//        obj["ListPrice"] = cRow.find("td:eq(1)").html();
//        data.push(obj);
//    });
//    return data;
//}
//function getCustomerInfo()
//{
//    var custumerobj = {}
//    custumerobj["FirstName"] = $('#inputName').val();
//    custumerobj["LastName"] = $('#Surname').val();
//    custumerobj["Email"] = $('#inputEmail').val();
//    custumerobj["StreetAddress1"] = $('#inputAddress').val();

//    return JSON.stringify(custumerobj);
//}

$('#cart').on('click', function (e) {
    $('#exampleModalToggle2').modal('hide');
    $('#formBackdrop').modal('show');

});


$('#save').on('click', function () {
    var custumerobj = {}
    custumerobj["FirstName"] = $('#inputName').val();
    custumerobj["LastName"] = $('#Surname').val();
    custumerobj["Email"] = $('#inputEmail').val();
    custumerobj["StreetAddress1"] = $('#inputAddress').val();

    $.ajax({
        url: 'customer/postpo',
        method: 'POST',
        data: { 'customerinfo' : JSON.stringify(custumerobj),'selectedlines': JSON.stringify(getSelectedRows()) },
        success: function (r) {
            $('#exampleModalToggle2').modal('hide');
            clearFields()
            Swal.fire({
                title: 'Saved!',
                text: 'Successful created order with Reference No : 1234',
                icon: 'success',
                confirmButtonText: 'Cool'
            })
        }
    });

})