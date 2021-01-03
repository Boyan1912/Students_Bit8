function deleteDiscipline(discipline, row) {
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

function editDiscipline(discipline, btn) {
    var editMode = btn.html() === 'Edit'
    var input = btn.prev('input');
    if (editMode) {
        var oldVal = $(input).val();
        btn.html('Done');
        $(input).removeAttr('readonly')
                    .attr('value', '')
                    .focus();
    }
    else {
        $.ajax({
            url: "/Disciplines/Edit?id=" + discipline.IdDiscipline + "&professor=" + $(input).val(),
            method: "PUT",
            contentType: 'application/json'
        })
        .done(function( res ) {
            if (!!res.ErrorMessage) {
                alert(res.ErrorMessage);
                $(input).val(oldVal);
            }
            else {
                            
            }
            })
        .fail(function(err) {
            console.error(err.responseText);
            alert( "Error editing discipline!");
            $(input).val(oldVal);
            })
        .always(function() {
            btn.html('Edit');
            $(input).attr('readonly', 'readonly');
        })
    }
}

function onDatatableDataLoad (data) {
    debugger;
    if (!!data.ErrorMessage) {
        console.error(data.ErrorMessage);
    }
    
    return data.Data;
}



