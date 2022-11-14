
BindGrid();

function BindGrid() {
    $(document).ready(function () {
        $("#tbl").DataTable({
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": "GetUserList",
                "type": "POST",
                "datatype": "json"
            },
            //"columnDefs": [{
            //    "targets": [0],
            //    "visible": false,
            //    "searchable": false
            //}],
            "columns": [
                { "data": "fullName", "name": "FullName", "width": "20%" },
                { "data": "email", "name": "Description", "width": "20%" },
                {
                    "render": function (data, type, full, meta) {

                        var str = "";

                        if (full.role=="1") {
                            str = "<span class='badge rounded-pill bg-primary'>Admin</span>";
                        } else {
                            str = "<span class='badge rounded-pill bg-danger'>User</span>";
                        }

                        return str;
                    }
                },
                { "data": "createdDate", "name": "CreatedDate", "autoWidth": true },
                { "data": "createdUserName", "name": "CreatedUserName", "autoWidth": true },
                {
                    render: function (data, type, full) {

                        var actions = "<div class='btn-group'>";
                        actions += "<button type='button' class='btn btn-dark btn-sm dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='false'>Action</button>";
                        actions += "<ul class='dropdown-menu'>";
                        actions += " <li><a class='dropdown-item' onclick=Edit('" + full.id + "')>Edit</a></li>";
                        actions += " <li><a class='dropdown-item text-danger' onclick=Delete('" + full.id + "')> Delete</a></li>";
                        actions += "</ul>";
                        actions += "</div>";

                        return actions;
                    }
                }

            ]
        });

    });

}

function Edit(id) {
    window.location = "Edit/" + id;
}

function Delete(id) {
    if (confirm("Are you sure you want to delete ...?")) {
        var url = "Delete"

        $.post(url, { id: id }, function (data) {
            if (data) {
                oTable = $('#tbl').DataTable();
                oTable.draw();
            } else {
                alert("Something Went Wrong!");
            }
        });  
    } else {
        return false;
    }
} 