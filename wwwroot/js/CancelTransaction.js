$(document).ready(function() {
    $("a").on("click", function(e){
        e.preventDefault();
        if(jQuery.inArray("cancel", $(this).attr("class").split(" ")) != -1)
        {
            var transactionsafter_cancellation_url = window.location.href;
            var cancel_transaction_url = $(this).prop('href');
            $.ajax({
                type: "POST",
                url: cancel_transaction_url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data){
                    if("success" in data)
                    {
                        alert(data.success)
                        window.location.href = transactionsafter_cancellation_url;
                    }
                    else{
                        alert(data.fail)
                    }
                },
                error: function(err){ $("#msg").html("Error occurs  :(").css("color", "red"); }
            });
        }
        else{
            window.location.href = $(this).prop("href");
        }
    });
});
