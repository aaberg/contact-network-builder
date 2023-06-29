<script lang="ts">
    
    import type { FlowUI } from "../authflow/types";
    export let input: FlowUI
</script>

<div class="form-control">
    <label class="label">
        <span class="label-text" for={input.attributes.name}>{input.meta.label.text}</span>
    </label>
    <input type={input.attributes.type} id={input.attributes.name} name={input.attributes.name} placeholder={input.meta.label.text} class={input.messages.length > 0 ? 'input input-bordered input-error' : 'input input-bordered'} value={input.attributes.value ?? ''} />
    
    {#if input.messages.length > 0}
        <div>
            <span class="label-text-alt">{input.messages[0].text}</span>
        </div>
    {/if}
</div>