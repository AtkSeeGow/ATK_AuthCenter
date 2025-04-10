const form = document.getElementById('login-form');
const btn = document.getElementById('login-btn');
const message = document.getElementById('message');

form.addEventListener('submit', function(e) {
    e.preventDefault();

    btn.classList.add('loading');
    message.textContent = '';

    form.submit();
});
