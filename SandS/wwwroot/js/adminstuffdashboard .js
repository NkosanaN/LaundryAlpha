var table;
$(document).ready(function () {
    //table = $('table.display').DataTable({
    //    "bPaginate": false,
    //    "bFilter": false,
    //    "bInfo": false
    //});
    GetInformation();

})


function movetoComplete() {
    window.location.href ='/admin/category/updatestatuestocomplete?ordHeaderCode=' + JSON.stringify(getSelectedOrder());
}

function getSelectedOrder() {
    var data = [];
    var rows = $($('input[type="checkbox"]').map(function () {
        return $(this).prop("checked") ? $(this).closest('tr') : null;
    }));

    if (rows.length == 0) {
        return false;
    }

    $.each(rows, function () {
        let cRow = $(this).closest("tr");
        cRow.find("td:eq(1)").html();
        data.push(cRow.find("td:eq(1)").html());
    });
    return data;
}


function GetInformation() {
    var pendingTbl = '';
    var completedTbl = '';
    var total = 0;
    $.ajax({
        url: '/home/getuserdata',
        method: 'GET',
        data: {},
        success: function (r) {
            $.each(r, function (i, items) {


                if (items.orderLine[i]["isCompleted"] === false)
                {
                    pendingTbl += "<tr><td>" + "<input type='checkbox' style='width: 25px; height: 20px'/></td>" +
                        "<td hidden>" + items["ordHeaderCode"] +
                        "</td><td>" + items["firstName"] +
                        "</td><td>" + items.orderLine[i]["items"] +
                        "</td><td>" + parseFloat(items.orderLine[i]["price"]).toFixed(2) +
                        "</td></tr>";
                }
                if (items.orderLine[i]["isCompleted"] === true)
                {
                    completedTbl += "<tr><td>" + items["firstName"] +
                        "<td>" + items.orderLine[i]["items"] +
                        "</td><td>" + parseFloat(items.orderLine[i]["price"]).toFixed(2) +
                        "</td></tr>";
                }
                //total += parseFloat(items.orderLine[i]["price"]);
            });
            //$('#total').text("Total  " + total.toFixed(2))
            $('#tblPendingx').append(pendingTbl);
            $('#tblCompletedx').append(completedTbl);

            
        }
    });
}