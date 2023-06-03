$(document).bind("contextmenu", function (e) {
    e.preventDefault();
});
$(document).on("mousedown", "td", function (event) {
    switch (event.buttons) {
        case 1:
            var form = $(event.target).closest('form');
            form.attr('action', '/Game/activateCell');
            form.submit();
            console.log("Cell was left clicked to reveal");
            break;
        case 2:
            var form = $(event.target).closest('form');
            form.attr('action', '/Game/activateFlag');
            form.submit();
            console.log("Cell was flagged");
            break;
        default:
            var form = $(event.target).closest('form');
            form.attr('action', '/Game/deactivateFlag');
            form.submit();
            console.log("Cells flag was removed");

    }
});
              