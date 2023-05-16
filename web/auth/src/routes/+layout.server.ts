import type {LayoutServerLoad} from "./$types";
import {kratosFetch} from "../authflow/kratosFetch";
import md5 from "md5";

export const load = (async ({url, cookies}) => {
    
    const result = await kratosFetch<{identity: {id: string, traits: {name: {first: string, last: string}, email: string}}}>('http://127.0.0.1:4433/sessions/whoami', cookies)
    
    return {
        authenticated: result.authenticated,
        identity: result.value?.identity,
        gravatar: result.authenticated ? `https://gravatar.com/avatar/${md5(result.value?.identity?.traits?.email)}?d=retro&r=g` : '',
    }
}) satisfies LayoutServerLoad