export interface FlowUI {
    messages: {
        text: string,
    }[],
    attributes: {
        name: string,
        value: string,
        type: 'password' | 'text' | 'email',
    },
    meta: {
        label: {
            text: string,
        }
    }
}

export interface Flow {
    id: string,
    type: string,
    expires_at: Date,
    issued_at: Date,
    ui: {
        action: string,
        method: string,
        nodes: FlowUI[],
        messages: {
            text: string,
        }[]
    },
}

export function getFlowInput(flow: Flow, name: string): FlowUI {
    return flow.ui.nodes.filter(node => node.attributes.name === name)[0]
}