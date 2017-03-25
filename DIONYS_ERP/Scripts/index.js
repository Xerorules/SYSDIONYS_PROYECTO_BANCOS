
$(function notify() {

    var clickButton = document.getElementById("<%= btnSubmit.ClientID %>");
    clickButton.click();

});


$(function notify2() {
    $(".btnSubmit").on("click", function () {
        $.notify({
            title: '<strong></strong>',
            icon: 'glyphicon glyphicon-saved',
            message: " REALIZADO CORRECTAMENTE!"
        }, {
            type: 'success',
            animate: {
                enter: 'animated fadeInRight',
                exit: 'animated fadeOutRight'
            },
            placement: {
                from: "top",
                align: "right"
            },
            offset: 2,
            spacing: 10,
            z_index: 1031,
        });
    });
});

