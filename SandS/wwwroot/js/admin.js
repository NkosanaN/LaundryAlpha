var totalL = 0.00, totalB = 0.00;
var table;
$(document).ready(function () {
    $('#isCollection').prop('checked', true);
});

$('#isCollection').on('change', function () {
    if ($(this).is(':checked'))
        $('.Address').addClass('d-none');
    else
        $('.Address').removeClass('d-none');
});

$('#adminviewsummary').on('click', function () {
    AdmincreateSummaryTable(AdmingetSelectedRows());

})
$('#adminModalToggle').on('show.bs.modal', function (e) {


    $('#AdminformBackdrop').modal('hide');
    $('#staticBackdrop').modal('hide');

    $('#summaryTable > tbody > tr .name').html($('#inputName').val());
    $('#summaryTable > tbody > tr .surname').html($('#Surname').val());
    $('#summaryTable > tbody > tr .email').html($('#inputEmail').val());
    $('#summaryTable > tbody > tr .address').html($('#inputAddress').val());
    $('#summaryTable > tbody > tr .phoneNr').html($('#MobilePhoneNumber').val());

    AdmincreateSummaryTable(AdmingetSelectedRows());
});

function AdmincreateSummaryTable(data) {
    var html = '';

    $('#adminsummaryViewTbl tbody').empty();

    console.log("Data =>" + data);
   
    $.each(data, function (i, items) {
        html += "<tr><td>" + items.ProductName
            + "</td><td style='text-align: right'>" + items.ListPrice
            + "</td></tr>"
    });
    html += "<tr class='alert-primary text-dark'><th>Total</th>"
        + "<th style='text-align: right'>" + parseFloat(totalL + totalB).toFixed(2)
        + "</th></tr>";

    $('#adminsummaryViewTbl').append(html);
}
function AdmingetSelectedRows() {
    var data = [];
    var rows = $(table.$('input[type="checkbox"]').map(function () {
        return $(this).prop("checked") ? $(this).closest('tr') : null;
    }));

    if (rows.length == 0)
    {
        bootbox.alert("Oops Add Items to Basket");
        return false;
    }
  //  rows = rows.length + 1;

    $.each(rows, function () {
        var obj = {};
        let cRow = $(this).closest("tr");
        obj["ProductName"] = cRow.find("td:eq(0)").html();
        obj["ListPrice"] = cRow.find("td:eq(1)").html();
        data.push(obj);
    });
    return data;
}

function getSelectedRows() {
    var data = [];
    var rows = $(table.$('input[type="checkbox"]').map(function () {
        return $(this).prop("checked") ? $(this).closest('tr') : null;
    }));

    if (rows.length == 0) {
        return false;
    }

    $.each(rows, function () {
        var obj = {};
        let cRow = $(this).closest("tr");
        obj["ProductName"] = cRow.find("td:eq(0)").html();
        obj["ListPrice"] = parseFloat(cRow.find("td:eq(1)").html()).toFixed(0);
        data.push(obj);
    });
    return data;
}

$('#generateReceipt').on('click', function () {

    let v = $('#Catergory Option:Selected').text();
    console.log("Selected " + v);

    var custumerobj = {}
    custumerobj["FirstName"] = $('#inputName').val();
    custumerobj["LastName"] = $('#Surname').val();
    custumerobj["Email"] = $('#inputEmail').val();
    custumerobj["MobilePhoneNumber"] = $('#MobilePhoneNumber').val();
    custumerobj["Category"] = $('#Catergory Option:Selected').text();
    custumerobj["isCollected"] = $('#isCollection').is(':checked');
    custumerobj["StreetAddress1"] = $('#inputAddress').val() === "" ? "Mamelodi" : ('#inputAddress').val();


    let u = '/admin/category/generatereceipt?customerinfo=' + JSON.stringify(custumerobj) + '&selectedlines=' + JSON.stringify(getSelectedRows());
    window.location.href = u;
});

$('#adminviewsummary').on('click', function () {
    /* $('#testButton').attr('data-target', '#testModal2');*/
    return false;
})

$('#Adminback').on('show.bs.modal', function (e) {
    $('#adminModalToggle').modal('hide');
});

