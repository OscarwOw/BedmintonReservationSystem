
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

            const form = document.createElement("form");
            form.action = "/Home";
            form.method = "post";

            const antiForgeryTokenInput = document.createElement("input");
            antiForgeryTokenInput.type = "hidden";
            antiForgeryTokenInput.name = "__RequestVerificationToken";
            antiForgeryTokenInput.value = document.querySelector('input[name="__RequestVerificationToken"]').value;
            form.appendChild(antiForgeryTokenInput);

            const authTokenInput = document.createElement("input");
            authTokenInput.type = "hidden";
            authTokenInput.name = "token";
            authTokenInput.value = result.authToken;
            form.appendChild(authTokenInput);

            document.body.appendChild(form);
            form.submit();
        } else {
            const error = await response.json();
            feedbackDiv.textContent = error.message;
            feedbackDiv.style.color = "red";
        }
    });
}



window.attachRegisterEvent = function () {
    document.getElementById("registerForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        const name = document.getElementById("register-username").value;
        const password = document.getElementById("register-password").value;
        const repeatPassword = document.getElementById("register-repeat-password").value;

        const feedbackDiv = document.getElementById("register-feedback");

        if (!name || !password || !repeatPassword) {
            feedbackDiv.textContent = "All fields are required.";
            feedbackDiv.style.color = "red";
            return;
        }

        if (password !== repeatPassword) {
            feedbackDiv.textContent = "Passwords do not match.";
            feedbackDiv.style.color = "red";
            return;
        }

        console.log("sending request to /api/LoginApi/Register");
        try {
            const response = await fetch('/api/LoginApi/Register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: name,
                    password: password,
                    repeatPassword: repeatPassword
                }),
            });

            if (response.ok) {
                feedbackDiv.textContent = "Registration successful!";
                feedbackDiv.style.color = "green";


                //document.getElementById("login-content").classList.add("active");
                //document.getElementById("register-content").classList.remove("active");

                window.location.href = '/home';
            } else {
                const error = await response.json();
                feedbackDiv.textContent = error.message || "Registration failed.";
                feedbackDiv.style.color = "red";
            }
        } catch (error) {
            feedbackDiv.textContent = "Error during registration. Please try again.";
            feedbackDiv.style.color = "red";
            console.error("Error during registration:", error);
        }
    });
};






document.addEventListener('DOMContentLoaded', () => {
    attachLoginEvent();
    attachRegisterEvent();
});