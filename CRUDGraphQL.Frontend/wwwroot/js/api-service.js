const baseURL = "https://localhost:44369/api"

async function asyncGet(url) {
    return fetch(`${baseURL}${url}`).then(async (response) => await response.json())
}