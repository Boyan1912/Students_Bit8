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
    if (!!data.ErrorMessage) {
        console.error(data.ErrorMessage);
    }
    
    return data.Data;
}

function renderDisciplineInTableCell(result, semester, index, value) {
    if (index === 0) {
        result = result + '<p class="font-italic text-info" style="margin-top:10px;">Disciplines for ' +  semester.Name + '</p>';
    }
    var deleteBtnId = 'delete-btn-' + value.IdDiscipline;
    var addBtnId = 'add-btn-' + value.IdDiscipline;
    var addDisciplineFormId = 'add-form-' + value.IdDiscipline;
    var addDisciplineBtnId = 'add-discipline-btn-' + semester.IdSemester;
    result = result + ('<div class="row">' +
        '<div class="col-3">' + value.Name + '</div>' + 
        '<div class="col-3">' + value.ProfessorName + '</div>' + 
        '<div class="col-3">' + value.Score + '</div>' + 
        '<div class="col-3"><button type="button" id="' + deleteBtnId + '">Delete</button></div>' + 
        '</div>');

    $('#' + deleteBtnId).on('click', function(e) {
        deleteDiscipline(value); // in site.js
    });
    if (index === semester.Disciplines.length - 1) {
        result = result + '<div class="row add-btn-wrapper" style="padding-left:10px;"><button class="col-9 btn btn-success btn-sm" type="button" id="' + addBtnId + '">Add Discipline</button></div>';
        result = result + '<form id="' + addDisciplineFormId + '" style="display:none" >' +
                                    '<div class="form-group">' +
                                        '<label>Name</label>' +
                                        '<input type="text" class="form-control" name="name" placeholder="Discipline name">' +
                                    '</div>' + 
                                    '<div class="form-group">' +
                                        '<label>Professor Name</label>' +
                                        '<input type="text" class="form-control" name="professor" placeholder="Professor name">' +
                                    '</div>' +
                                    '<div class="form-group">' +
                                        '<label>Score</label>' +
                                        '<input type="number" class="form-control" name="score" placeholder="Score">' +
                                    '</div>' +
                                    '<button type="button" id="' + addDisciplineBtnId + '" class="btn btn-primary">Submit</button>' +
                                '</form>';
        $('#' + addBtnId).on('click', function(e) {
            $('#' + addDisciplineFormId).show();
        });
        $('#' + addDisciplineBtnId).on('click', function(e) {
            var discName = $('#' + addDisciplineFormId + ' input[name="name"]').val();
            var profName = $('#' + addDisciplineFormId + ' input[name="professor"]').val();
            var score = $('#' + addDisciplineFormId + ' input[name="score"]').val();
                                
            $.ajax({
                url: "/Disciplines/Create?semesterId=" + semester.IdSemester + "&name=" + discName + "&professor=" + profName + "&score=" + score,
                method: "POST",
                contentType: 'application/json'
            })
            .done(function(res) { 
                if (!!res.ErrorMessage) {
                    alert(res.ErrorMessage);
                }
                else {
                    location.reload(true);
                }    
            })
            .fail(function(err){
                console.error(err.responseText);
                alert('Error creating discipline');  
            });
                                
        });
    }

    return result;
}

function postCreateSemester(studentId, semesterName) {
    var postUrl = "/Semesters/Create?name=" + semesterName;
    if (studentId) {
        postUrl += "&studentId=" + studentId
    }
    $.ajax({
        url: postUrl,
        method: "POST",
        contentType: 'application/json'
    })
    .done(function(res) { 
        if (!!res.ErrorMessage) {
            alert(res.ErrorMessage);
        }
        else {
            location.reload(true);
        }    
    })
    .fail(function(err){
        console.error(err.responseText);
        alert('Error creating semester');  
    });
}