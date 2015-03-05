
    $(function () {
        $.ajaxSetup({ cache: false });

        $("a[data-modal]").on("click", function (e) {
            // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
            //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

            $('#myModalContent').load(this.href, function () {
                $('#myModal').modal({
                    /*backdrop: 'static',*/
                    keyboard: true
                }, 'show');
                bindForm(this);
            });
            return false;
        });
    });

function bindForm(dialog) {
    $('form', dialog).submit(function () {

        $.ajax({
            type: this.method,
            url: this.action,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    window.location.href = result.url;
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        return false;
    });
}
