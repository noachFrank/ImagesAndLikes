$(() => {
    console.log("working")
    const id = $("#image-id").val();
    $("#like-button").on("click", function () {
        console.log("liking");
        $("#like-button").attr("disabled", true)

        $.post("/home/like", { id }, function () {
            console.log("liked");


        });
    });

    setInterval(() => {
        $.get("/home/getbyid", { id }, function (image) {
            console.log("getting likes count");
            console.log(id);
            console.log(image.likes);

            $("#likes-count").text(image.likes);
        })
    }, 1000);


});