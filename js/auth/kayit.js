// ============================================
// 📝 KAYIT (REGISTER) - API Connected Version
// ============================================
// لازم تضيف في html قبل هاد الملف:
// <script src="../js/api.js"></script>

const button = document.querySelector("button");
const msgEl = document.getElementById("msg");

button.addEventListener("click", async function () {

    const name = document.querySelector("input[placeholder='Ad']").value.trim();
    const email = document.querySelector("input[placeholder='E-posta']").value.trim();
    const password = document.querySelector("input[placeholder='Şifre']").value.trim();

    // Validation
    if (name.length < 3) {
        msgEl.textContent = "Ad en az 3 karakter olmalı!";
        msgEl.style.color = "red";
        return;
    }

    if (!email.includes("@")) {
        msgEl.textContent = "Geçerli email gir!";
        msgEl.style.color = "red";
        return;
    }

    if (password.length < 4) {
        msgEl.textContent = "Şifre en az 4 karakter olmalı!";
        msgEl.style.color = "red";
        return;
    }

    button.disabled = true;
    button.textContent = "Kaydediliyor...";

    try {
        await AuthAPI.kayit(name, email, password);
        msgEl.textContent = "Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...";
        msgEl.style.color = "green";
        setTimeout(() => {
            window.location.href = "../pages/giris.html";
        }, 1500);

    } catch (error) {
        msgEl.textContent = error.message || "Kayıt sırasında hata oluştu!";
        msgEl.style.color = "red";
        button.disabled = false;
        button.textContent = "Kayıt Ol";
    }
});