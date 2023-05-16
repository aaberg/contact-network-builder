<script lang="ts">
    export let data;
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>

{#if data.authenticated}
    <p>Authenticated as: {data.identity.traits.name.first} {data.identity.traits.name.last}</p>
    <div class="avatar">
        <div class="w-16 mask mask-heart">
            <img src={data.gravatar} alt="Gravatar" />
        </div>
    </div>
    
    <p><a href="auth/logout" class="link">Logout</a></p>
    
{:else}
    <p><a href="auth/login" class="link">Login</a></p>
{/if}


