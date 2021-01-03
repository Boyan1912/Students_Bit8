function deleteDiscipline(discipline, row) {
    debugger;
    if (!!discipline.Score) {
        alert("Only disciplines without scores can be deleted!")
    }
    else {
        $.ajax({
            url: "/Disciplines/Delete?id=" + discipline.IdDiscipline,
            method: "DELETE",
            contentType: 'application/json'
        })
        .done(function( res ) {
            if (!!res.ErrorMessage) {
                alert(res.ErrorMessage);
            }
            else {
                if (!!row) {
                    row.remove().draw();
                }
                else {
                    location.reload(true);
                }
            }
        })
        .fail(function(err) {
            console.error(err.responseText);
            alert( "Error deleting discipline!");
        })
    }
}