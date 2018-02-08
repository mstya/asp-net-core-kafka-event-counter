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

    function post(url, json) {
        return $.ajax({
            url: url,
            data: { data: json },
            type: "POST"
        });
    }
});