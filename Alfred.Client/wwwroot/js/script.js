function receiveMessage(event) {
    if (typeof event.data == 'string' && event.data.length !== 0) {
        window.jwt = event.data;
    } else if (event.data === null) {
        window.location.href = "https://staging.accounts.excelmec.org/auth/login?redirect_to=" + window.location;
    }
}

window.addEventListener("message", receiveMessage);

function getJwt() {
    return window.jwt;
}

window.reload = function () {
    window.location.href = window.location;
}
window.getJwt = function getJwt() {
    return window.jwt;
}


window.apiGet = function (url) {
    return fetch(url).then(res => res.json());
}

async function customFetch(url, method, headers = {}, data = null) {
    console.log(url);
    console.log(method);
    console.log(data);
    // const response = await fetch(url, {
    //     method: 'POST',
    //     mode: 'cors',
    //     headers: {
    //         'Content-Type': 'application/json',
    //         ...headers
    //     },
    //     body: JSON.stringify(data)
    // });
    // return response.json();
    return "success";
}

window.customFetch = customFetch;