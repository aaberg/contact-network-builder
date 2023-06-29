import type {PageServerLoad} from "./$types";

export const load = (async({url, cookies}) => {

    // const csrfCookie= cookies.getAll().filter(cookie => cookie.name.startsWith("csrf_token"))[0]
    // const session = cookies.get("ory_kratos_session")
    //
    // if (session == undefined) {
    //     return {}
    // }
    //
    // const headers = new Headers();
    // headers.append("csrfCookie.value", csrfCookie.value)
    // headers.append("cookie", `ory_kratos_session=${session}`)
    //
    // const response = await fetch("http://127.0.0.1:4433/sessions/whoami", {
    //     headers: headers
    // })
    //
    // console.log(await response.json())
    
    
    
    return {}
    
}) satisfies PageServerLoad;