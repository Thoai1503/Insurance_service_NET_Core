const cbx = document.getElementById("cbx");

function Status() {
    var id = $("#userid").val();
    var status = $("#cbx").val();
    $.ajax({
        url: "~/customer/updatestatus",
        type = "POST",
        contentType: 'application/x-www-form-urlencoded',
        data: { "active": status },
        success: function (response) {
            if (response.success) {
                window.location("~/adminarea/customer");
            }
        }
    })
}