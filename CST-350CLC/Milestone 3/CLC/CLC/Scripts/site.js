$(function () {
    blinkeffect('#txtblnk');
})
function blinkeffect(selector) {
    $(selector).fadeOut('slow', function () {
        $(this).fadeIn('slow', function () {
            blinkeffect(this);
        });
    });
}

$(function () {
    console.log("Page is ready");

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
        console.log("Right click. Preventing context menu.");
    });

    $(document).on("mousedown", "td", function (event) {
        switch (event.which) {
            case 1:
                event.preventDefault();

                var cord = $(this).val();
                console.log("Cell " + cord + "was left clicked");
                break;
            case 3:
                event.preventDefault();
                var cord = $(this).val();
                console.log("Cell " + cord + " was Right clicked");
              //  var buttonNumber = $(this).val();
              //  if (document.getElementById("cellImage " + buttonNumber).src == "/Content/images/flag.png") {
              //      document.getElementById("cellImage " + buttonNumber).src = "";
              //      console.log("Cell already has an image");
              //  }
              //  else {
              //      document.getElementById("cellImage " + buttonNumber).src = "/Content/images/flag.png";
              //      console.log("Cell was given an image");
              //  }

              //  break;
          //  default:
            //    alert('Nothing pressed');
        }
    });
});

              