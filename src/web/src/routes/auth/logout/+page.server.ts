import type {PageServerLoad} from "./$types";
import {kratosFetch} from "../../../authflow/kratosFetch";
import {error, redirect} from "@sveltejs/kit";

export const load = (async ({url, cookies}) => {
    
    const result = await kratosFetch<{logout_url: string}>('http://127.0.0.1:4433/self-service/logout/browser', cookies)
    
    if (result.authenticated) {
        throw redirect(302, result.value.logout_url)
    }
    
    throw error(401, "You are not logged in")
    
}) satisfies PageServerLoad;