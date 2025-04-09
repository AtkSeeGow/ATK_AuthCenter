const form = document.getElementById('login-form');
const btn = document.getElementById('login-btn');
const errorMsg = document.getElementById('error-msg');

form.addEventListener('submit', function(e) {
    e.preventDefault();

    btn.classList.add('loading');
    errorMsg.textContent = '';

    form.submit();
});
