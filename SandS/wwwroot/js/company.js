var dttable

$(document).ready(function () {
    loadData();
})
function loadData()
{
    dttable = $('#tbl').DataTable({
        "ajax": {
            "url": "/admin/company/getall"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "streetaddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            {
                "data": "id",
                "render": function (data) { //data here is id
                    return `
                        <div class="btn-group">
                            <button type="button" class="btn btn-secondary dropdown-toggle"
                                data-bs-toggle="dropdown" aria-expanded="false">
                                Action
                            </button>
                            <ul class="dropdown-menu">
                                <li><a class='dropdown-item' href="/Admin/Upsert?id=${data}">Edit</a></li>
                            </ul>
                        </div>
                            `
                },
                "width": "15%"
            },
        ]
    });
}