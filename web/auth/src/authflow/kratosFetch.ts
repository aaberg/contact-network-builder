import type {Cookies} from "@sveltejs/kit";
import {log} from "../log";

export async function kratosFetch<Type>(url: string, cookies: Cookies) : Promise<{value: Type, authenticated: boolean}>{
    const csrfCookie= cookies.getAll().filter(cookie => cookie.name.startsWith("csrf_token"))[0]
    const sessionCookieValue = cookies.get("ory_kratos_session")
    
    if (sessionCookieValue == undefined) {
        return {
            value: null as Type,
            authenticated: false
        }
    }

    const headers = new Headers();
    headers.append("csrfCookie.value", csrfCookie.value)
    headers.append("cookie", `ory_kratos_session=${sessionCookieValue}`)

    const response = await fetch(url, {
        headers: headers
    })
    
    if (response.status !== 200) {
        log.warn(`kratos responded with status code ${response.status}`, response)
        return {
            value: null as Type,
            authenticated: false
        }
    }
    
    const value: Type = await response.json();
    
    return {
        value,
        authenticated: response.status === 200,
    }
}