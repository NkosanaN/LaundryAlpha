"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        //alert("Connected to dashboard");
        InvokePendingLaundry();
    }).catch(function (err) {
        return console.error(err.toString());
    })
})

function InvokePendingLaundry()
{
    connection.invoke("SendPendingLaundry")
        .catch(function (error) {
            return console.error(err.toString());
    })
}


connection.on("ReceivedPending", function (pendinglaundry) {
    BindToGrid(pendinglaundry);
})


function BindToGrid(pendinglaundry) {

    //console.log(pendinglaundry)

    $("#tblPendingLaundry tbody").empty();

    var tr;

    $.each(pendinglaundry, function (index, pending) {
        tr = $("<tr/>");
        tr.append(`<td>${(index + 1)}</td>`);
        tr.append(`<td>${(pending.ordHeaderCode)}</td>`);
        tr.append(`<td>${(pending.name)}</td>`);
        tr.append(`<td>${(pending.surname)}</td>`);
        //tr.append(`<td>${(pending.email)}</td>`);
        tr.append(`<td>${(pending.totalLine)}</td>`);
        $("#tblPendingLaundry").append(tr);

    });
}