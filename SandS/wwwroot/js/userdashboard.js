var dttable
$(document).ready(function () {
    GetCustomerInformation();
    getLoyaltPoint();

})

const previousBtn = document.getElementById("previousBtn");
const nextBtn = document.getElementById("nextBtn");
const finishBtn = document.getElementById("finishBtn");
const content = document.getElementById("content");
const bullets = [...document.querySelectorAll(".bullet")] //get all class with element bullet

const MAX_STEPS = 4;
let currentStep = 1;

//nextBtn.addEventListener('click', () => {

//    const currentBullet = bullets[currentStep - 1];
//    currentBullet.classList.add('completed');
//    currentStep++;
//    previousBtn.disabled = false;

//    if (currentStep == MAX_STEPS) {
//        nextBtn.disabled = true;
//        finishBtn.disabled = false;
//    }
//    content.innerText = `Step Number ${currentStep}`;
//});

//previousBtn.addEventListener('click', () => {

//    previousBullet = bullets[currentStep - 2];
//    previousBullet.classList.remove('completed');
//    currentStep--;

//    nextBtn.disabled = false;
//    finishBtn.disabled = true;

//    if (currentStep == 1) {
//        previousBtn.disabled = true;
//    }
//    content.innerText = `Step Number ${currentStep}`;
//});

//finishBtn.addEventListener('click', () => {
//    location.reload();
//})
function downLoadPdf()
{
    let u = '/customers/debtor/InvoicePdf';
    window.location.href = u;
}


function GetCustomerInformation() {
    var html = '';
    var total = 0;
    $.ajax({
        url: '/home/getuserdata',
        method: 'GET',
        data: {},
        success: function (r) {
            $.each(r, function (i, items) {
             
                html += "<tr><td>" + items.orderLine[i]["items"] +
                        "</td><td>" + items.orderLine[i]["price"] +
                        "</td></tr>";
                total += parseFloat(items.orderLine[i]["price"]);
            });
            $('#total').text("Total  " + total.toFixed(2))
            $('#tblUserLaundry').append(html);
        }
    });
}

function checkDataInTbl() {
    var table = $('#tblLaundry').DataTable();

    if (!table.data().any()) {
        alert('No Row')
    }
}

function getLoyaltPoint() {
    let i = 0;
    for (i; i <= 5; i++) {
        $('.btnLoyalty_' + i).addClass('active');
    }
}


function loadData() {
    dttable = $('#tblUserLaundry').DataTable({
        "ajax": {
            "url": "/home/getuserdata", "dataSrc": ""
        },
        "columns": [

            //{ "data": "name", "width": "5%" },
            //{ "data": "surname", "width": "5%" },
            { "data": "orderLine[0].items" },
            { "data": "orderLine[0].price" },
        ],
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
}



