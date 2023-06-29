import type { PageServerLoad } from "./$types";
import {redirect} from "@sveltejs/kit";
import config from "../../../AppConfig";
import {getAuthFlow} from "../../../authflow/getAuthFlow";
import {getFlowInput} from "../../../authflow/types";

export const load = (async ({url, cookies}) => {
    const flowId = url.searchParams.get("flow")
    
    if (flowId === null) throw redirect(302, config.createLoginFlowUrl)

    const csrfCookie= cookies.getAll().filter(cookie => cookie.name.startsWith("csrf_token"))[0]
    const flow = await getAuthFlow(csrfCookie, flowId, config.getLoginFlowUrl)

    const csrfToken = getFlowInput(flow, "csrf_token").attributes.value;
    
    return {
        flow,
        csrfToken,
        inputs: {
            email: getFlowInput(flow, 'identifier'),
            password: getFlowInput(flow, 'password'),
        },
        messages: flow.ui.messages
    }
    
}) satisfies PageServerLoad;