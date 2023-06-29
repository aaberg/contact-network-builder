import type {Flow} from "./types";

export async function getAuthFlow(csrfCookie: {name: string, value: string}, flowId: string, flowUrl: string) : Promise<Flow> {
    const headers = new Headers();
    
    headers.append("X-CSRF-Token", csrfCookie.value);
    
    const response = await fetch(`${flowUrl}?id=${flowId}`, {
        headers: {
            "Cookie": `${csrfCookie.name}=${csrfCookie.value}`
        },
    });
    return await response.json()
}