$(document).ready(function () {
    ShowBooksData();
});
var bookAuthorArray = [];
var counterAddBook = 0;
function ShowBooksData() {
    var authorIdData = $('#authorIdData').val();
    $.ajax({
        url: '/Author/BooksListAuthor/' + authorIdData,
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, statu, xhr) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<div style="border:double; border-width: thick; padding: 5px;">';
                object += '<dl class="row" >';
                object += '<dt class="col-sm-2">Назва</dt>';
                object += '<dd class = "col-sm-10">' + item.name + '</dd>';
                object += '<dt class="col-sm-2">Жанр</dt>';
                object += '<dd class = "col-sm-10">' + item.genre.name + '</dd>';
                object += '<dt class="col-sm-2">Кількість сторінок</dt>';
                object += '<dd class = "col-sm-10">' + item.countPages + '</dd>';
                object += '</dl>';
                object += '</div>';
            })
                $('#booksData').html(object);
            },
            error: function () {
                alert('Дані не завантажилися')
            }
        })
    }

function OpenModal() {
    $('#bookAddModal').modal('show');
}

    
$(function () {
    $("#btnSubmit").click(function (e) {
        e.preventDefault();
        var selectGenre = document.getElementById("genreId");
        var valueGenre = selectGenre.options[selectGenre.selectedIndex].text;

        var objData = {
            Name: $("#nameBook").val(),
            GenreId: $("#genreId").val(),
            CountPages: $("#countPages").val(),
            AuthorId: $("#authorIdAddBook").val()
        }
        if (objData.Name == '' || objData.CountPages < 1) {
            alert("Не все поля заполнены");
            return;
        }
        bookAuthorArray[counterAddBook] = objData;
        counterAddBook++;

        var addBook = '';
        addBook += '<div style="border:double; border-width: thick; padding: 5px;">';
        addBook += '<dl class="row" >';
        addBook += '<dt class="col-sm-2">Назва</dt>';
        addBook += '<dd class = "col-sm-10">' + objData.Name + '</dd>';
        addBook += '<dt class="col-sm-2">Жанр</dt>';
        addBook += '<dd class = "col-sm-10">' + valueGenre + '</dd>';
        addBook += '<dt class="col-sm-2">Кількість сторінок</dt>';
        addBook += '<dd class = "col-sm-10">' + objData.CountPages + '</dd>';
        addBook += '</dl>';
        addBook += '</div>';
        $('#booksData').append(addBook);

        
    })
})

function ClearTextBox() {
    $("#nameBook").val(''),
    $("#countPages").val('')
}


$(function () {
    $("#addToDB").click(function (e) {
        for (var i = 0; i < counterAddBook; i++) {

            $.ajax({
                url: '/Author/AddBookAuthor',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
                dataType: 'json',
            data: bookAuthorArray[i],
                success: function (response) {
                    alert('Книжка збережена до БД!');
                },
                error: function () {
                    alert('Книжка не додана');
                }
            });
        }
        bookAuthorArray = [];
        counterAddBook = 0;
    })
})