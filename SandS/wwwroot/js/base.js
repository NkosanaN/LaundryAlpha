////function getSelectedRows() {
////    var data = [];
////    var rows = $(table.$('input[type="checkbox"]').map(function () {
////        return $(this).prop("checked") ? $(this).closest('tr') : null;
////    }));

////    if (rows.length == 0) {
////        return false;
////    }
////    $.each(rows, function () {
////        var obj = {};
////        let cRow = $(this).closest("tr");
////        obj["ProductName"] = cRow.find("td:eq(0)").html();
////        obj["ListPrice"] = cRow.find("td:eq(1)").html();
////        data.push(obj);
////    });
////    return data;
////}