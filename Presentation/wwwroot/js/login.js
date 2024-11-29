document.getElementById("loginForm").addEventListener("submit", async function (event) {
    event.preventDefault(); 

    const name = document.getElementById("name").value;
    const password = document.getElementById("password").value;

    const response = await fetch('/api/LoginApi/Authenticate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ name, password }),
    });

    const feedbackDiv = document.getElementById("login-feedback");

    if (response.ok) {
        const result = await response.json();
        feedbackDiv.textContent = result.message;
        feedbackDiv.style.color = "green";
    } else {
        const error = await response.json();
        feedbackDiv.textContent = error.message;
        feedbackDiv.style.color = "red";
    }
});