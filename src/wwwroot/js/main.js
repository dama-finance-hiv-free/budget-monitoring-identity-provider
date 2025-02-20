// @ts-nocheck
/**
 * Handle password toggle
 * */

<<<<<<< HEAD
document.addEventListener("DOMContentLoaded", () => {
    const passwordToggleBtn = document.querySelector("#password-toggle");

    if (passwordToggleBtn) {
        passwordToggleBtn.addEventListener("click", (e) => {
            const passwordInput = document.querySelector(".form__input_password");
            const passwordIcons = Array.from(
                document.querySelectorAll(".password_icon")
            );

            if (passwordToggleBtn.dataset.icon === "show") {
                // show password and change icon to hidden icon
                passwordInput.type = "text";
                passwordIcons.map((input) => {
                    if (input.dataset.name === "show") {
                        input.classList.add("hidden");
                    } else {
                        input.classList.remove("hidden");
                    }
                });

                passwordToggleBtn.dataset.icon = "hide";
            } else {
                // show password and change icon to hidden icon
                passwordInput.type = "password";
                passwordIcons.map((input) => {
                    if (input.dataset.name === "hide") {
                        input.classList.add("hidden");
                    } else {
                        input.classList.remove("hidden");
                    }
                });

                passwordToggleBtn.dataset.icon = "show";
            }
        });
    }
});
=======
document.addEventListener('DOMContentLoaded', () => {

    const passwordToggleBtn = document.querySelector('#password-toggle');
    const formInputs = Array.from(document.querySelectorAll('.form__input'));

    const toggleFloatLabel = (inputEl, labelEl) => {
        if (inputEl.value !== '') {
            labelEl.classList.add('label_float_active');
        } else {
            labelEl.classList.remove('label_float_active');
        }
    };

    formInputs.map(input => {
        const nextLabel = input.nextElementSibling;
        toggleFloatLabel(input, nextLabel);
        input.addEventListener('blur', () => toggleFloatLabel(input, nextLabel));
    });

    if (passwordToggleBtn) {
        passwordToggleBtn.addEventListener('click', e => {
            const passwordInput = document.querySelector(".form__input_password");
            const passwordIcons = Array.from(document.querySelectorAll(".password_icon"));

            if (passwordToggleBtn.dataset.icon === 'show') {
                // show password and change icon to hidden icon
                passwordInput.type = 'text';
                passwordIcons.map(input => {
                    if (input.dataset.name === 'show') {
                        input.classList.add('hidden');
                    } else {
                        input.classList.remove('hidden');
                    }
                });

                passwordToggleBtn.dataset.icon = 'hide';

            } else {
                // show password and change icon to hidden icon
                passwordInput.type = 'password';
                passwordIcons.map(input => {
                    if (input.dataset.name === 'hide') {
                        input.classList.add('hidden');
                    } else {
                        input.classList.remove('hidden');
                    }
                });

                passwordToggleBtn.dataset.icon = 'show';
            }
        });
    }
});
>>>>>>> 8766606485d03c0655033f05c0c9ee42a42314e2
