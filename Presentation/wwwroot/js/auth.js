console.log("auth.js loaded successfully");


//fillingLinks();




//function fillingLinks() {
//    const token = localStorage.getItem('authToken');



//    document.querySelectorAll('a[data-auth]').forEach(link => {
//        console.log("link is: ", link);

//        link.addEventListener('click', async (event) => {
//            event.preventDefault();
//            console.log("anchor clicked");
//            const url = link.getAttribute('href');

//            console.log("url: ", url);
//            try {
//                console.log('Token:', token);
//                const response = await fetch(url, {
//                    method: 'GET',
//                    headers: { Authorization: `Bearer ${localStorage.getItem('authToken') }` }
//                });

//                if (response.ok) {
//                    const html = await response.text();
//                    const parser = new DOMParser();
//                    const newDoc = parser.parseFromString(html, 'text/html');
//                    document.body.innerHTML = newDoc.body.innerHTML;
//                    history.pushState(null, '', url);

//                    initializeSiteEvents();
//                    fillingLinks();
//                    window.attachLogoutEvent();
//                    window.attachLoginEvent();

//                    initializeCalendar();
//                } else {
//                    console.error('Navigation failed:', await response.json());
//                    alert('Failed to navigate to the requested page. Please check your authentication.');
//                }
//            } catch (error) {
//                console.error('Error during navigation:', error);
//            }
//        });
//    });

//    const originalFetch = window.fetch;
//    window.fetch = function (url, options = {}) {
//        options.headers = {
//            ...options.headers,
//            Authorization: `Bearer ${localStorage.getItem('authToken') }`
//        };
//        return originalFetch(url, options);
//    };
//}

//function initializeCalendar() {
//    if (typeof VanillaCalendar === 'undefined') {
//        console.error('VanillaCalendar is not defined. Make sure the script is loaded.');
//        return;
//    }
//    //src="https://cdn.jsdelivr.net/npm/vanilla-calendar-pro@2.9.6/build/vanilla-calendar.min.js"
//    const calendarContainer = document.getElementById('calendar');
//    if (calendarContainer) {
//        const calendar = new VanillaCalendar('#calendar', {
//            type: 'default',
//            settings: {
//                lang: 'en',
//            },
//        });
//        calendar.init();
//        console.log('Calendar initialized successfully.');
//    } else {
//        console.log('Calendar container not found.');
//    }
//}
//function initializeCalendar() {
//    const calendarContainer = document.getElementById('calendar');
//    if (!calendarContainer) {
//        console.error('Calendar container not found.');
//        return;
//    }

//    const calendar = new VanillaCalendar('#calendar', {
//        type: 'default',
//        settings: {
//            lang: 'en',
//        },
//    });

//    calendar.init();
//    console.log('Calendar initialized successfully.');
//}


//function reloadVanillaCalendarScript() {
//    // Remove existing script if necessary
//    const existingScript = document.querySelector('script[src*="vanilla-calendar.min.js"]');
//    if (existingScript) existingScript.remove();

//    // Add the script dynamically
//    const script = document.createElement('script');
//    script.src = "https://cdn.jsdelivr.net/npm/vanilla-calendar-pro@2.9.6/build/vanilla-calendar.min.js";
//    script.onload = () => initializeCalendar(); // Ensure calendar initializes after the script is loaded
//    document.head.appendChild(script);
//}



