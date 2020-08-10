function receiveMessage(event) {
    if (typeof event.data == 'string' && event.data.length !== 0) {
        window.jwt = event.data;
    } else if (event.data === null) {
        window.location.href = "https://staging.accounts.excelmec.org/auth/login?redirect_to=" + window.location;
    }
}
window.addEventListener("message", receiveMessage);
function getJwt(){
    return window.jwt;
}

window.reload = function(){
    window.location.href = window.location;
}
window.getJwt = getJwt;
window.customFetch = async function(url){
    var res = await fetch(url).then(res => res.json());
    return res;
}