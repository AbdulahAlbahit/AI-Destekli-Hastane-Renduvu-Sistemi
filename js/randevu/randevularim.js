import { apiGet, apiDelete } from "../api.js";
import { getUser } from "../utils/depolama.js";

const liste = document.getElementById("randevuList");

let allRandevular = [];

// ============================================================
// Load Appointments
// ============================================================

async function loadRandevular() {

  try {

    const user = getUser();

    // ========================================================
    // API REQUEST
    // ========================================================

    const data = await apiGet(
      `randevu/hasta/${user.id}`
    );

    // ========================================================
    // Normalize Backend Fields
    // ========================================================

    allRandevular = data.map(r => ({

      id:
        r.id ||
        r.randevuId ||
        "",

      doktor:
        r.doktorAdi ||
        r.doctorName ||
        "Doktor",

      bolum:
        r.bolum ||
        r.department ||
        "Bölüm",

      tarih:
        r.tarih ||
        r.date ||
        "",

      saat:
        r.saat ||
        r.time ||
        "",

      durum:
        r.durum ||
        r.status ||
        "upcoming"

    }));

    renderRandevular(allRandevular);

  } catch(error) {

    console.warn(
      "Randevular alınamadı:",
      error
    );

    // ========================================================
    // Demo Data
    // ========================================================

    allRandevular = [

      {
        id: 1,
        doktor: "Dr. Ahmet Yılmaz",
        bolum: "Kardiyoloji",
        tarih: "2026-05-15",
        saat: "10:00",
        durum: "upcoming"
      },

      {
        id: 2,
        doktor: "Dr. Ayşe Demir",
        bolum: "Nöroloji",
        tarih: "2026-04-10",
        saat: "14:30",
        durum: "completed"
      },

      {
        id: 3,
        doktor: "Dr. Mehmet Kaya",
        bolum: "Ortopedi",
        tarih: "2026-03-01",
        saat: "09:00",
        durum: "cancelled"
      }

    ];

    renderRandevular(allRandevular);

  }

}

// ============================================================
// Render Appointments
// ============================================================

function renderRandevular(list) {

  liste.innerHTML = "";

  if(list.length === 0){

    liste.innerHTML = `

      <div class="empty-state">

        <h3>
          Henüz randevunuz yok
        </h3>

        <p>
          İlk randevunuzu oluşturabilirsiniz.
        </p>

      </div>

    `;

    return;

  }

  list.forEach((randevu, index) => {

    const card = document.createElement("div");

    card.className =
      `randevu-card ${randevu.durum}`;

    card.style.animationDelay =
      `${index * 0.05}s`;

    card.innerHTML = `

      <div class="randevu-top">

        <div>

          <h3>
            🩺 ${randevu.doktor}
          </h3>

          <p class="bolum">
            ${randevu.bolum}
          </p>

        </div>

        <span class="
          durum
          ${getDurumClass(randevu.durum)}
        ">
          ${getDurumText(randevu.durum)}
        </span>

      </div>

      <div class="randevu-info">

        <p>
          📅 ${formatDate(randevu.tarih)}
        </p>

        <p>
          🕐 ${randevu.saat}
        </p>

      </div>

      ${
        randevu.durum === "upcoming"
        ?
        `
          <button
            class="iptal-btn"
            onclick="iptalEt(${randevu.id})"
          >
            ❌ Randevuyu İptal Et
          </button>
        `
        :
        ""
      }

    `;

    liste.appendChild(card);

  });

}

// ============================================================
// Status Text
// ============================================================

function getDurumText(status){

  switch(status){

    case "upcoming":
      return "Yaklaşan";

    case "completed":
      return "Tamamlandı";

    case "cancelled":
      return "İptal Edildi";

    default:
      return "Bekliyor";

  }

}

// ============================================================
// Status Class
// ============================================================

function getDurumClass(status){

  switch(status){

    case "upcoming":
      return "success";

    case "completed":
      return "info";

    case "cancelled":
      return "danger";

    default:
      return "";

  }

}

// ============================================================
// Format Date
// ============================================================

function formatDate(date){

  if(!date) return "—";

  return new Date(date)
  .toLocaleDateString("tr-TR", {

    year: "numeric",
    month: "short",
    day: "numeric"

  });

}

// ============================================================
// Cancel Appointment
// ============================================================

window.iptalEt = async function(id){

  const onay = confirm(
    "Randevu iptal edilsin mi?"
  );

  if(!onay) return;

  try {

    // ========================================================
    // API DELETE
    // ========================================================

    await apiDelete(
      `randevu/${id}`
    );

    // ========================================================
    // Update Local State
    // ========================================================

    const updated = allRandevular.map(r => {

      if(r.id === id){

        return {

          ...r,

          durum: "cancelled"

        };

      }

      return r;

    });

    allRandevular = updated;

    renderRandevular(allRandevular);

  } catch(error) {

    alert(
      "Randevu iptal edilemedi"
    );

    console.error(error);

  }

};

// ============================================================
// Init
// ============================================================

loadRandevular();