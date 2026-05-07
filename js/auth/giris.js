// ============================================
// 🔐 GİRİŞ (LOGIN) - API Connected Version
// ============================================
// لازم تضيف في html قبل هاد الملف:
// <script src="../js/api.js"></script>

const button = document.querySelector("button");
const msgEl = document.getElementById("msg");

button.addEventListener("click", async function () {

    const email = document.querySelector("input[placeholder='E-posta']").value.trim();
    const password = document.querySelector("input[placeholder='Şifre']").value.trim();

    if (!email || !password) {
        msgEl.textContent = "Lütfen tüm alanları doldurun!";
        msgEl.style.color = "red";
        return;
    }

    button.disabled = true;
    button.textContent = "Giriş yapılıyor...";

    try {
        await AuthAPI.giris(email, password);
        msgEl.textContent = "Giriş başarılı! Yönlendiriliyorsunuz...";
        msgEl.style.color = "green";
        setTimeout(() => {
            window.location.href = "../pages/randevu.html";
        }, 1000);

    } catch (error) {
        msgEl.textContent = error.message || "Email veya şifre yanlış!";
        msgEl.style.color = "red";
        button.disabled = false;
        button.textContent = "Giriş Yap";
    }
});