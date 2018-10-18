function sendsms()
{
    alert("sending");
    var mydata = new Object();
    mydata.mobile = $("#txtmobileno").val();
    mydata.msg = $("#txtmsg").val();

    $.ajax({
        type: 'POST',
        url: 'http://localhost:51727/api/sendsms/',
        data: mydata,
        dataType: 'json',
        headers: {
            "Authorization": btoa("jay" + ":" + "pass")
        },
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (result) {
            var token = result;
        },
        //complete: function (jqXHR, textStatus) {
        //},
        error: function (req, status, error) {
            alert(error);
        }
    });
    alert("Complete");
}