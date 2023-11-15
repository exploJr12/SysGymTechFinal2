document.addEventListener("DOMContentLoaded", function () {
    var trigger = document.querySelector(".hamburger");
    var overlay = document.querySelector(".overlay");
    var isClosed = false;

    trigger.addEventListener("click", function () {
        hamburger_cross();
    });

    function hamburger_cross() {
        if (isClosed == true) {
            overlay.style.display = "none";
            trigger.classList.remove("is-open");
            trigger.classList.add("is-closed");
            isClosed = false;
        } else {
            overlay.style.display = "block";
            trigger.classList.remove("is-closed");
            trigger.classList.add("is-open");
            isClosed = true;
        }
    }

    document.querySelector('[data-toggle="offcanvas"]').addEventListener("click", function () {
        document.getElementById("wrapper").classList.toggle("toggled");
    });
});