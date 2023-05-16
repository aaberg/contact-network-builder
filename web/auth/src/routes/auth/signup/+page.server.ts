import type {PageServerLoad} from './$types';
import config from "../../../AppConfig";
import {redirect} from "@sveltejs/kit";
import {getAuthFlow} from "../../../authflow/getAuthFlow";
import { getFlowInput } from '../../../authflow/types';

export const load = (async ({url, cookies}) => {
    const flowId = url.searchParams.get("flow")
    
    if (flowId === null) throw redirect(302, config.createRegisterFlowUrl)

    const csrfCookie= cookies.getAll().filter(cookie => cookie.name.startsWith("csrf_token"))[0]
    const flow = await getAuthFlow(csrfCookie, flowId, config.getRegistrationFlowUrl)
    
    flow.ui.nodes.forEach(node => {
        console.log(node)
    })
    
    //const csrfToken = flow.ui.nodes.filter(node => node.attributes.name === "csrf_token")[0].attributes.value;
    const csrfToken = getFlowInput(flow, "csrf_token").attributes.value;
    
    return {
        flow,
        csrfToken,
        inputs: {
            email: flow.ui.nodes.filter(node => node.attributes.name === 'traits.email')[0],
            password: flow.ui.nodes.filter(node => node.attributes.name === 'password')[0],
            firstName: flow.ui.nodes.filter(node => node.attributes.name === 'traits.name.first')[0],
            lastName: flow.ui.nodes.filter(node => node.attributes.name === 'traits.name.last')[0],
        },
        messages: flow.ui.messages
    }
}) satisfies PageServerLoad;