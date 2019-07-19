$(document).ready(function () {
    $('.edit').click(function () {
        var id = this.id;
        var idSplit = id.split('-');
        $('#hndEditId').val(idSplit[0]);
        $.ajax({
            url: '../Home/GetDataToEdit',
            type: 'POST',
            data: { id: idSplit[0] },
            success: function (data) {
                $('#firstName').empty().val(data.FirstName);
                $('#lastName').empty().val(data.LastName);
            },
            error: function (er) {
                alert(er);
            }
        });
    });

    $('#updatebtn').click(function () {
        var id = $('#hndEditId').val();
        var fname = $('#firstName').val();
        var lname = $('#lastName').val();
        if (fname !== "" && lname !== "") {
            $.ajax({
                url: '../Home/UpdateData',
                type: 'POST',
                data: { id: id, fname: fname, lname: lname },
                success: function (data) {
                    $('#firstName').val('');
                    $('#lastName').val('');
                    location.reload();
                },
                error: function (er) {
                    alert(er);
                }
            });
        }
    });

    $('#add').click(function () {
        var fname = $('#firstName').val();
        var lname = $('#lastName').val();
        if (fname !== "" && lname !== "") {
            $.ajax({
                url: '../Home/InsertIntoPerson',
                type: 'POST',
                data: { fname: fname, lname: lname },
                success: function (data) {
                    $('#firstName').val('');
                    $('#lastName').val('');
                    location.reload();
                },
                error: function (er) {
                    alert(er);
                }
            });
        }
    });

    $('.dlt').click(function () {
        var id = this.id;
        var idSplit = id.split('-');
        $('#hndEditId').val(idSplit[0]);
        $.ajax({
            url: '../Home/Delete',
            type: 'POST',
            data: { id: idSplit[0] },
            success: function (data) {
                location.reload();
            },
            error: function (er) {
                alert(er);
            }
        });
    });

    $('#refresh').click(function () {
        location.reload();
    });
});