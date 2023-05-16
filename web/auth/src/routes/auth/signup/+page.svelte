<script lang="ts">
    import AuthInput from "../../../components/AuthInput.svelte"

    export let data
    
    // data.flow.ui.nodes.forEach(node => {
    //     console.log(node)
    // })
</script>

<div class="hero min-h-screen bg-base-200">
    <div class="hero-content flex-col lg:flex-row-reverse">
        <div class="prose text-center lg:text-left">
            <h1>Sign up now!</h1>
            <p class="py-6">The Contact Network builder is provided by Aaberg Corp ©</p>
            <p>Already have an account? <a href="login">Login</a></p>
        </div>
        <div class="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
            <!--            <figure><img src="/padlock.png" alt="padlock"/></figure>-->
            <form action={data.flow.ui.action} method={data.flow.ui.method} enctype="application/x-www-form-urlencoded" name="password">
                <div class="card-body">
                    
                    <input type="hidden" name="method" value="password" />
                    <input type="hidden" name="csrf_token" value={data.csrfToken}   />
                    <AuthInput input={data.inputs.firstName} />
                    <AuthInput input={data.inputs.lastName} />
                    
                    <AuthInput input={data.inputs.email} />
                    <AuthInput input={data.inputs.password} />
                    
                    <div class="form-control mt-6">
                        <button name="password" value="password" type="submit" class="btn btn-primary">Sign up</button>
                    </div>
                    {#if data.messages}
                        {#each data.messages as message}
                            <div class="alert alert-error">
                                <div class="alert-description">{message.text}</div>
                            </div>
                        {/each}
                    {/if}
                </div>
            </form>
        </div>
    </div>
</div>