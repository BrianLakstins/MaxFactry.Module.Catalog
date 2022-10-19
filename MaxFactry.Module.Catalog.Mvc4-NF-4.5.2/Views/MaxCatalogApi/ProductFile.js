var psProductFileServer = "http://localhost:44445";
var psProductFileFromEmail = "my@test.com";
var psProductFileFromName = "Test Sender";
var psProductFileSubject = "This is a test";
function pfProductFilePopup(lsId) {
    $.ajax({
        url: psProductFileServer + "/MaxCatalogApi/productfileemailpopcontainer",

    }).done(function (data) {
        $("body").append(data);
        pfProductFileForm(lsId);
    })

}

function pfProductFileForm(lsId) {
    $.ajax({
        url: psProductFileServer + "/MaxCatalogApi/productfileemailform/" + lsId,
        xhrFields: {
            withCredentials: true
        }
    }).done(function (data) {
        $("#udProductFilePopUpContainer .modal-content").html(data);

        $('#udProductFileForm').submit(function (e) {
            e.preventDefault();
            pfProductFileProcess(this, lsId);

            return false;
        });

        $("#udProductFilePopUpContainer").modal({ keyboard: false });
    })

}

function pfProductFileProcess(loForm, lsId) {
    var lsEmail = $('#udProductFileEmail').val();
    var loEmailInfo = { ToEmail: lsEmail, FromEmail: psProductFileFromEmail, FromName: psProductFileFromName, Subject: psProductFileSubject };

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: psProductFileServer + "/MaxCatalogApi/productfileemailcheck/" + lsId,
        data: JSON.stringify(loEmailInfo),
        xhrFields: {
            withCredentials: true
        }
    }).done(function (data) {
        if (data.length == 0) {
            $("#udProductFilePopUpContainer").modal("hide");
            var lsEmail = $('#udProductFileEmail').val();
            var loEmailInfo = { ToEmail: lsEmail, FromEmail: psProductFileFromEmail, FromName: psProductFileFromName, Subject: psProductFileSubject };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: psProductFileServer + "/MaxCatalogApi/productfileemail/" + lsId,
                data: JSON.stringify(loEmailInfo),
                xhrFields: {
                    withCredentials: true
                }
            }).done(function (data) {
                if (data.length != 0) {
                    alert(data);
                }
            })
        }
        else {
            alert(data);
        }
    })
}