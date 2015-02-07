/*!
* Miscelaneous JS
* Copyright 2015 SolutiaIntelligence.
* Author: *
*/

$(document).ready(function () {

    $('button').popover({ trigger: "hover" });
    $('a').popover({ trigger: 'hover' });

    $('#btnFilter').click(function () {
        $('#SearchString').val('');
    });

});