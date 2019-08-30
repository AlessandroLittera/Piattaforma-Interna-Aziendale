// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var tableDeleteFunction = function ($table, dataTable) {

    var _dataTable = dataTable;
    var _$table = $($table);

    var deleteUrl = _$table.attr("data-delete-url");

    _$table.find('.btn-delete').on("click", function () {

        var $this = $(this);

        var success = confirm("Sicuro di voler eliminare questo utente?");
        var id = $this.attr("data-id");

        if (success) {
            $.ajax({
                url: deleteUrl,
                type: 'POST',
                data: {
                    "id": id
                },
                success: (function () {

                    _dataTable
                        .row($this.closest('tr'))
                        .remove()
                        .draw();
                }),

            })

        }


    })

}

$(function () {

    // Inizializzazione Chosen
    $('.chosen').chosen();

    // Inizializzazione Datatable
    var $allTablesInPage = $('table');
    $allTablesInPage.each(function () {
        var _$table = $(this);
        var dataTable = _$table.DataTable();
        dataTable.on('draw.dt', function () {
            tableDeleteFunction(_$table, dataTable);
            $("a[isDisabled]").remove();
            $("button[isDisabled]").remove();
        });
        tableDeleteFunction(_$table, dataTable);
    });
    
    $("a[isDisabled]").remove();
    $("button[isDisabled]").remove();
    

});