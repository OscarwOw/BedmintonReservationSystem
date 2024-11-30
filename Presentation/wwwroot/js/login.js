
window.attachLoginEvent = function () {
    document.getElementById("loginForm").addEventListener("submit", async function (event) {
        event.preventDefault();


        const authToken = Math.floor(Math.random() * 2147483647) 

        const name = document.getElementById("name").value;
        const password = document.getElementById("password").value;

        const response = await fetch('/api/LoginApi/Authenticate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ name, password, authToken }),
        });

        const feedbackDiv = document.getElementById("login-feedback");

        if (response.ok) {
            const result = await response.json();
            feedbackDiv.textContent = result.message;
            feedbackDiv.style.color = "green";

            console.log("user id: ", result.id);
            console.log("authToken: ", result.authToken);

            localStorage.setItem("userId", result.id); 
            localStorage.setItem("authToken", result.authToken); 

            const homeLink = document.querySelector('a[href="/Home"]');
            if (homeLink) {
                homeLink.click(); 
            }
        } else {
            const error = await response.json();
            feedbackDiv.textContent = error.message;
            feedbackDiv.style.color = "red";
        }
    });
}

document.addEventListener('DOMContentLoaded', () => {
    attachLoginEvent(); 
});