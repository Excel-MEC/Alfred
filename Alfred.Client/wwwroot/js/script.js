function receiveMessage(event) {
    if (typeof event.data == 'string' && event.data.length !== 0) {
        localStorage.setItem("refresh_token", event.data);
    } else if (event.data === null) {
        localStorage.setItem('refresh_token', null);
        localStorage.setItem('access_token', null);
        window.location.href = "https://staging.accounts.excelmec.org/auth/login?redirect_to=" + window.location;
    }
}

window.addEventListener("message", receiveMessage);

window.reload = function () {
    window.location.href = window.location;
}
window.getJwt = function getJwt() {
    return getCookie("access_token");
}
window.setJwt = function setJwt(newToken){
    var d = new Date();
    d.setTime(d.getTime() + 780000);
    var expires = "expires="+ d.toUTCString();
    document.cookie = "access_token" + "=" + newToken + ";" + expires + ";path=/";
}
window.getRefreshToken = function getRefreshToken(){
    return localStorage.getItem("refresh_token");
}

window.saveAsFile = function (filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
