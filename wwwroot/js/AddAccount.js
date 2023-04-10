$(document).ready(function() {
    $("#add-account").on("click", function(e){
        e.preventDefault();
        $.ajax({
            type: "POST",
            url: window.location.href + "/AddAccount",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data){
                if("error" in data)
                {
                    alert(data.error)
                }
                else{
                    var accountTable = $('#accounts-table tbody');
                    accountTable.empty();

                    data.forEach(function(account){
                        accountTable.append('<tr><td>' + account.id + '</td><td>'
                        + account.currentBalance + '</td><td>' + account.creationDate + '</td><td>'
                        + '<a class="btn btn-outline-primary" href=' + window.location.pathname + "/" + account.id + "/make-transaction>"
                        + "Make Transaction" + '</a>'
                        + '<a class="btn btn-outline-primary" href=' + window.location.pathname + "/" + account.id + "/transactions>"
                        + "Transaction" + '</a>'
                        + '<a class="btn btn-outline-primary delete-account" href=' + window.location.pathname + "/" + account.id + "/Delete>"
                        + "Delete Account" + '</a>'
                        );
                    });
                }
            },
            error: function(err){ $("#msg").html("Error occurs  :(").css("color", "red"); }
        });
    });

    $(document).on("click", ".delete-account", function(event){
        event.preventDefault();
        
        $.ajax({
            type: "POST",
            url: $(this).prop("href"),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data){
                if("error" in data)
                {
                    alert(data.error)
                }
                else{
                    var accountTable = $('#accounts-table tbody');
                    accountTable.empty();

                    data.forEach(function(account){
                        accountTable.append('<tr><td>' + account.id + '</td><td>'
                        + account.currentBalance + '</td><td>' + account.creationDate + '</td><td>'
                        + '<a class="btn btn-outline-primary" href=' + window.location.pathname + "/" + account.id + "/make-transaction>"
                        + "Make Transaction" + '</a>'
                        + '<a class="btn btn-outline-primary" href=' + window.location.pathname + "/" + account.id + "/transactions>"
                        + "Transaction" + '</a>'
                        + '<a class="btn btn-outline-primary delete-account" href=' + window.location.pathname + "/" + account.id + "/Delete>"
                        + "Delete Account" + '</a></tr>'
                        );
                    });
                }
            },
            error: function(err){ $("#msg").html("Error occurs  :(").css("color", "red"); }
        })
    });
});
