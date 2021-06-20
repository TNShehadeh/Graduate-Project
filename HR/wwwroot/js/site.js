function AdministrationButton() {
    var x = document.getElementById("Administration");
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else {
        x.className = x.className.replace(" w3-show", "");
    }
}
function EmployeeInformationButton() {
    var x = document.getElementById("EmployeeInformation");
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else {
        x.className = x.className.replace(" w3-show", "");
    }
}
function StaticInformationButton() {
    var x = document.getElementById("StaticInformation");
    if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
    } else {
        x.className = x.className.replace(" w3-show", "");
    }
}
function w3_open() {
    document.getElementById("Sidebar").style.display = "block";
}
function w3_close() {
    document.getElementById("Sidebar").style.display = "none";
} 