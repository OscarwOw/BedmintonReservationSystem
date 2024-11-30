function initializeSiteEvents() {
    const loginButton = document.getElementById('openLogin');
    const registerButton = document.getElementById('openRegister');

    /* modal content */
    const loginContent = document.getElementById('login-content');
    const registerContent = document.getElementById('register-content');

    /* modal content spans */
    const loginContentSpan = document.getElementById('login-content-span');
    const registerContentSpan = document.getElementById('register-content-span');

    /* sign up modal */
    const loginModal = document.getElementById('loginModal');
    const closeButton = loginModal.querySelector('.close');

    /* backdrop */
    const backdrop = document.getElementById('backdrop');

    /* nav buttons */


    /* navigation buttons events */
    if (loginButton) {
        loginButton.addEventListener('click', (event) => {
            event.preventDefault();
            loginModal.style.display = 'block';

            loginContent.classList.add('active');
            loginContentSpan.classList.add('active');

            registerContent.classList.remove('active');
            registerContentSpan.classList.remove('active');

            loginModal.classList.add('show');
            backdrop.classList.add('show')
        });
    }

    if (registerButton) {
        registerButton.addEventListener('click', (event) => {
            event.preventDefault();
            loginModal.style.display = 'block';

            loginContent.classList.remove('active');
            loginContentSpan.classList.remove('active');

            registerContent.classList.add('active');
            registerContentSpan.classList.add('active');

            loginModal.classList.add('show');
            backdrop.classList.add('show')
        });
    }

    /* modal spans events */
    if (loginContentSpan) {
        loginContentSpan.addEventListener('click', (event) => {
            event.preventDefault();

            loginContent.classList.add('active');
            loginContentSpan.classList.add('active');

            registerContent.classList.remove('active');
            registerContentSpan.classList.remove('active');


        });
    }
    if (registerContentSpan) {
        registerContentSpan.addEventListener('click', (event) => {
            event.preventDefault();

            loginContent.classList.remove('active');
            loginContentSpan.classList.remove('active');

            registerContent.classList.add('active');
            registerContentSpan.classList.add('active');

        });
    }

    /* close span */
    if (closeButton) {
        closeButton.addEventListener('click', () => {
            loginModal.style.display = 'none';
            loginModal.classList.remove('show');
            backdrop.classList.remove('show');
            console.log('close button clicked');
        });
    }

    if (backdrop) {
        backdrop.addEventListener('click', () => {
            loginModal.style.display = 'none';
            loginModal.classList.remove('show');
            backdrop.classList.remove('show');
            console.log('backdrop clicked');
        });
    }

    if (loginModal) {
        loginModal.addEventListener('click', (event) => {
            event.stopPropagation();
        });
    }
}

async function performLogout() {
    const token = localStorage.getItem('authToken');
    if (!token) {
        console.warn('No auth token found. Logout skipped.');
        return;
    }

    try {
        const response = await fetch('/api/LoginApi/Logout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(token),
        });

        if (response.ok) {
            console.log('Logout successful');
            localStorage.removeItem('authToken');
            localStorage.removeItem('userId');
            window.location.href = '/Home';
        } else {
            console.error('Logout failed:', await response.text());
        }
    } catch (error) {
        console.error('Error during logout:', error);
    }
}

function attachLogoutEvent() {
    const logoutLink = document.getElementById('logout');

    if (logoutLink) {
        logoutLink.addEventListener('click', async (event) => {
            event.preventDefault(); 
            console.log('Logout link clicked');
            await performLogout(); 
        });
    } else {
        console.warn('Logout link not found.');
    }
}


window.initializeSiteEvents = initializeSiteEvents;
window.attachLogoutEvent = attachLogoutEvent;





document.addEventListener('DOMContentLoaded', () => {
    initializeSiteEvents();
    console.log("loading auth.js");

    // Dynamically load auth.js
    const script = document.createElement('script');
    script.src = '/js/auth.js';
    script.defer = true; 
    document.body.appendChild(script);
});



