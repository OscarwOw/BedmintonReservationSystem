console.log("auth.js loaded successfully");


fillingLinks();




function fillingLinks() {
    const token = localStorage.getItem('authToken');



    document.querySelectorAll('a[data-auth]').forEach(link => {
        console.log("link is: ", link);

        link.addEventListener('click', async (event) => {
            event.preventDefault();
            console.log("anchor clicked");
            const url = link.getAttribute('href');

            console.log("url: ", url);
            try {
                console.log('Token:', token);
                const response = await fetch(url, {
                    method: 'GET',
                    headers: { Authorization: `Bearer ${localStorage.getItem('authToken') }` }
                });

                if (response.ok) {
                    const html = await response.text();
                    const parser = new DOMParser();
                    const newDoc = parser.parseFromString(html, 'text/html');
                    document.body.innerHTML = newDoc.body.innerHTML;
                    history.pushState(null, '', url);

                    initializeSiteEvents();
                    fillingLinks();
                    window.attachLoginEvent();
                } else {
                    console.error('Navigation failed:', await response.json());
                    alert('Failed to navigate to the requested page. Please check your authentication.');
                }
            } catch (error) {
                console.error('Error during navigation:', error);
            }
        });
    });

    const originalFetch = window.fetch;
    window.fetch = function (url, options = {}) {
        options.headers = {
            ...options.headers,
            Authorization: `Bearer ${localStorage.getItem('authToken') }`
        };
        return originalFetch(url, options);
    };
}


