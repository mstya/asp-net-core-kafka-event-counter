$(function () {
    $("#textarea").keyup(function () {
        console.log(JSON.stringify(this));
        post(PostTextUrl, JSON.stringify(this));
    });

    $("#button").click(function () {
        console.log(JSON.stringify(this));
        post(PostClickUrl, JSON.stringify(this));
    });

    $("#div").mousemove(function () {
        console.log(JSON.stringify(this));
        post(PostMouseMoveEvent, JSON.stringify(this));
    });

    $("#runEvntBtn").click(function() {
        for (var i = 0; i < 100; i++) {
            $("#button").click();
            $("#textarea").keyup();
            $("#div").mousemove();
        };
    });

    function post(url, json) {
        return $.ajax({
            url: url,
            data: { data: json },
            type: "POST"
        });
    }
});