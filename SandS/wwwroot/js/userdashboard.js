var dttable
$(document).ready(function () {
    loadData();
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


function loadData() {
    dttable = $('#tblUserLaundry').DataTable({
        "ajax": {
            "url": "/home/getuserdata", "dataSrc": ""
        },
        "columns": [

            //{ "data": "name", "width": "5%" },
            //{ "data": "surname", "width": "5%" },
            { "data": "itemNr", "width": "5%" },
            { "data": "totalLine", "width": "5%" },
        ],
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
}



