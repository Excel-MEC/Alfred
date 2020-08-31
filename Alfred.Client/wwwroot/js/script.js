function receiveMessage(event) {
    if (typeof event.data == 'string' && event.data.length !== 0) {
        localStorage.setItem("refresh_token", event.data);
    } else if (event.data === null) {
        localStorage.setItem('refresh_token', null);
        localStorage.setItem('token', null);
        window.location.href = "https://staging.accounts.excelmec.org/auth/login?redirect_to=" + window.location;
    }
}

window.addEventListener("message", receiveMessage);

window.reload = function () {
    window.location.href = window.location;
}
window.getJwt = function getJwt() {
    return localStorage.getItem("token");
}
window.setJwt = function setJwt(newToken){
    return localStorage.setItem("token", newToken);
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

