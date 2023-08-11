<div class="navbar bg-base-200">
  <div class="flex-1">
    <label for="side-drawer" class="btn btn-square btn-ghost lg:hidden">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        class="inline-block w-6 h-6 stroke-current"
        ><path
          stroke-linecap="round"
          stroke-linejoin="round"
          stroke-width="2"
          d="M4 6h16M4 12h16M4 18h16"
        /></svg
      >
    </label>
  </div>
  <div class="flex-none">
    <div />
    <div class="form-control mr-5">
      <label>
        <svg
          class="w-4 h-4 absolute translate-y-1/2 translate-x-3"
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
          />
        </svg>

        <input
          id="search"
          type="text"
          placeholder="Search"
          class="input input-bordered input-sm w-full pl-10 md:w-auto"
        />
      </label>
    </div>
    <div class="dropdown dropdown-end">
      <button class="btn btn-ghost btn-circle avatar">
        <div class="w-10 rounded-full">
          <img src="/avatar.jpg" alt="avatar" />
        </div>
      </button>
      <!-- <ul tabindex="0" class="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
          <li>
            <a class="justify-between">
              Profile
              <span class="badge">New</span>
            </a>
          </li>
          <li><a>Settings</a></li>
          <li><a>Logout</a></li>
        </ul> -->
    </div>
  </div>
</div>

<div class="drawer lg:drawer-open">
  <input id="side-drawer" type="checkbox" class="drawer-toggle" />
  <div class="drawer-content p-5">
    <slot />

    <!-- Page content here -->
  </div>
  <div class="drawer-side">
    <label for="side-drawer" class="drawer-overlay" />
    <ul class="menu p-4 w-80 h-full bg-base-200 text-base-content">
      <!-- Sidebar content here -->
      <li><a>Sidebar Item 1</a></li>
      <li><a>Sidebar Item 2</a></li>
    </ul>
  </div>
</div>
