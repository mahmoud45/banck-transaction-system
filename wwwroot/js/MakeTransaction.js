$(document).ready(function() {
    $("#Transaction_form").on("submit", function(e){
        e.preventDefault();
        
        var FromAccountId = $("#FromAccountId").val();
        var ToAccountId = $("#ToAccountId").val();
        var Amount = $("#Amount").val();
        
        $.ajax({
            type: "POST",
            url: window.location.pathname,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({FromAccountId: FromAccountId, ToAccountId: ToAccountId, Amount: Amount}),
            success: function(data){
                alert(data.success);
                var username = $("#username").attr("value");
                window.location.pathname ="customers" + "/" + username + "/" + "BankAccounts";
            },
            error: function(err){ $("#msg").html("Error occurs  :(").css("color", "red"); }
        });
    });
    

});
