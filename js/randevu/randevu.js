// ============================================
// 📅 RANDEVU - API Connected Version
// ============================================
// لازم تضيف في html قبل هاد الملف:
// <script src="../js/api.js"></script>

// ============================================
// 🔒 AUTH CHECK
// ============================================

if (!AuthAPI.isLoggedIn()) {
    alert("Önce giriş yap!");
    window.location.href = "../pages/giris.html";
}

// ============================================
// 📌 DOM ELEMENTS
// ============================================

const btnRandevuAl = document.querySelector("button");
const list = document.getElementById("list");
const bolumSelect = document.getElementById("bolum");
const doktorSelect = document.getElementById("doktor");
const saatContainer = document.getElementById("saatler");
const msgEl = document.getElementById("msg");

let secilenSaat = "";
let secilenDoktorId = null;

// ============================================
// 🏥 LOAD DEPARTMENTS FROM API
// ============================================

async function loadBolumler() {
    try {
        const bolumler = await DoktorAPI.getBolumler();

        bolumSelect.innerHTML = '<option value="">Bölüm Seç</option>';

        bolumler.forEach(b => {
            const option = document.createElement("option");
            // API'den gelen field isimlerini rفقاتك ile kontrol et
            option.value = b.id || b.Id;
            option.textContent = b.name || b.Name || b.bolumAdi;
            bolumSelect.appendChild(option);
        });

    } catch (error) {
        console.error("Bölümler yüklenemedi:", error);
        // Fallback static data eğer API çalışmıyorsa
        const fallback = ["Kardiyoloji", "Dahiliye", "Ortopedi", "Nöroloji"];
        fallback.forEach(b => {
            const option = document.createElement("option");
            option.value = b;
            option.textContent = b;
            bolumSelect.appendChild(option);
        });
    }
}

// ============================================
// 👨‍⚕️ LOAD DOCTORS BY DEPARTMENT
// ============================================

bolumSelect.addEventListener("change", async function () {
    const bolumId = this.value;

    doktorSelect.innerHTML = '<option value="">Doktor Seç</option>';
    saatContainer.innerHTML = "";
    secilenSaat = "";
    secilenDoktorId = null;

    if (!bolumId) return;

    try {
        const doktorlar = await DoktorAPI.getDoktorlar(bolumId);

        doktorlar.forEach(d => {
            const option = document.createElement("option");
            option.value = d.id || d.Id;
            option.textContent = d.name || d.Name || d.ad;
            doktorSelect.appendChild(option);
        });

    } catch (error) {
        console.error("Doktorlar yüklenemedi:", error);
        msgEl.textContent = "Doktorlar yüklenirken hata oluştu.";
    }
});

// ============================================
// ⏰ LOAD AVAILABLE TIME SLOTS
// ============================================

doktorSelect.addEventListener("change", async function () {
    secilenDoktorId = this.value;
    secilenSaat = "";
    saatContainer.innerHTML = "";

    if (!secilenDoktorId) return;

    // Bugünün tarihi
    const bugun = new Date().toISOString().split("T")[0];

    try {
        const saatler = await DoktorAPI.getMüsaitSaatler(secilenDoktorId, bugun);

        if (!saatler || saatler.length === 0) {
            saatContainer.innerHTML = "<p>Bu gün için müsait saat yok.</p>";
            return;
        }

        saatler.forEach(saat => {
            const btn = document.createElement("button");
            btn.classList.add("time-btn");
            btn.textContent = saat.time || saat;
            btn.style.margin = "5px";

            btn.onclick = function () {
                // Tüm butonlardan seçimi kaldır
                document.querySelectorAll(".time-btn").forEach(b => b.classList.remove("selected"));
                btn.classList.add("selected");
                secilenSaat = saat.time || saat;
            };

            saatContainer.appendChild(btn);
        });

    } catch (error) {
        console.error("Saatler yüklenemedi:", error);
        // Fallback saatler
        const fallbackSaatler = ["09:00", "10:00", "11:00", "14:00", "15:00", "16:00"];
        fallbackSaatler.forEach(saat => {
            const btn = document.createElement("button");
            btn.classList.add("time-btn");
            btn.textContent = saat;
            btn.style.margin = "5px";
            btn.onclick = function () {
                document.querySelectorAll(".time-btn").forEach(b => b.classList.remove("selected"));
                btn.classList.add("selected");
                secilenSaat = saat;
            };
            saatContainer.appendChild(btn);
        });
    }
});

// ============================================
// ➕ CREATE APPOINTMENT
// ============================================

btnRandevuAl.addEventListener("click", async function () {
    const hasta = document.getElementById("hasta")?.value || localStorage.getItem("userName") || "";

    if (!secilenDoktorId || !secilenSaat) {
        msgEl.textContent = "Lütfen doktor ve saat seçin!";
        msgEl.style.color = "red";
        return;
    }

    btnRandevuAl.disabled = true;
    btnRandevuAl.textContent = "Kaydediliyor...";

    try {
        const bugun = new Date().toISOString().split("T")[0];
        const result = await RandevuAPI.olustur(secilenDoktorId, bugun, secilenSaat);

        msgEl.textContent = "Randevu başarıyla alındı! ✅";
        msgEl.style.color = "green";

        // Listeye ekle
        const newItem = {
            id: result.id || Date.now(),
            hasta: hasta,
            doktor: doktorSelect.options[doktorSelect.selectedIndex]?.text || "",
            saat: secilenSaat
        };
        const li = createAppointmentEl(newItem);
        list.appendChild(li);

        // Reset
        secilenSaat = "";
        secilenDoktorId = null;
        saatContainer.innerHTML = "";
        doktorSelect.value = "";

    } catch (error) {
        msgEl.textContent = error.message || "Randevu alınamadı!";
        msgEl.style.color = "red";

    } finally {
        btnRandevuAl.disabled = false;
        btnRandevuAl.textContent = "Randevu Al";
    }
});

// ============================================
// 📋 LOAD MY APPOINTMENTS
// ============================================

async function loadRandevularim() {
    try {
        const randevular = await RandevuAPI.getMyRandevular();
        list.innerHTML = "";

        randevular.forEach(item => {
            const li = createAppointmentEl(item);
            list.appendChild(li);
        });

    } catch (error) {
        console.error("Randevular yüklenemedi:", error);
    }
}

// ============================================
// 🧱 CREATE APPOINTMENT ELEMENT
// ============================================

function createAppointmentEl(item) {
    const li = document.createElement("li");
    li.innerHTML = `
        <div class="appointment-card">
            <span>👤 ${item.hasta || item.patientName || ""}</span>
            <span>🩺 ${item.doktor || item.doctorName || ""}</span>
            <span>⏰ ${item.saat || item.time || ""}</span>
            <button onclick="iptalEt('${item.id}')">❌ İptal</button>
        </div>
    `;
    return li;
}

// ============================================
// ❌ CANCEL APPOINTMENT
// ============================================

async function iptalEt(id) {
    if (!confirm("Bu randevuyu iptal etmek istiyor musunuz?")) return;

    try {
        await RandevuAPI.iptalEt(id);
        await loadRandevularim(); // Yenile
    } catch (error) {
        alert("İptal edilemedi: " + error.message);
    }
}

// ============================================
// 🚀 INIT
// ============================================

loadBolumler();
loadRandevularim();