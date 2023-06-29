<script>
    import '../app.css'

    export let data
</script>

<div class="drawer drawer-mobile">
    <input id="my-drawer-2" type="checkbox" class="drawer-toggle" />
    <div class="drawer-content flex flex-col items-center justify-center bg-base-300">
        <!-- Page content here -->
        <label for="my-drawer-2" class="btn btn-primary drawer-button lg:hidden">Open drawer</label>

        <slot />    
      
    </div> 
    <div class="drawer-side">
        <label for="my-drawer-2" class="drawer-overlay"></label>
        <div class="flex flex-col w-80 bg-base-100 text-base-content">
            
            <div class="flex flex-row">
                <div class="avatar h-24 ml-8">
                    <div class="w-16 mask mask-squircle">
                        <img src={data.gravatar} alt="Gravatar" />
                    </div>
                </div>
                <div class="grid"></div>
            </div>

            <ul class="menu p-4 ">
            
                <!-- Sidebar content here -->
                <li><a>Actions</a></li>
                <li><a>Contacts</a></li>
                <li><a>Settings</a></li>
            </ul>
        </div>
        
    
    </div>
</div>