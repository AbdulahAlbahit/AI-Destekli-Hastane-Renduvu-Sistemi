const container = document.getElementById("doktor-listesi");

let allDoctors = [];

// ============================================
// Load Doctors
// ============================================

async function loadDoctors() {

  const bolumId = localStorage.getItem("selectedBolumId");

  try {

    const data = await DoktorAPI.getDoktorlar(bolumId);

    allDoctors = data;

    renderDoctors(allDoctors);

  } catch(err) {

    console.warn("API hatası:", err);

    // ============================================
    // TEMP DOCTORS
    // ============================================

    allDoctors = [

      {
        id:1,
        ad:"Dr. Ahmet Yılmaz",
        bolum:"Kardiyoloji"
      },

      {
        id:2,
        ad:"Dr. Ayşe Demir",
        bolum:"Nöroloji"
      },

      {
        id:3,
        ad:"Dr. Mehmet Kaya",
        bolum:"Ortopedi"
      },

      {
        id:4,
        ad:"Dr. Fatma Çelik",
        bolum:"Dermatoloji"
      },

      {
        id:5,
        ad:"Dr. Ali Özkan",
        bolum:"Genel Cerrahi"
      },

      {
        id:6,
        ad:"Dr. Zeynep Şahin",
        bolum:"Pediatri"
      },

      {
        id:7,
        ad:"Dr. Mustafa Arslan",
        bolum:"Üroloji"
      },

      {
        id:8,
        ad:"Dr. Emine Kurt",
        bolum:"Kadın Doğum"
      },

      {
        id:9,
        ad:"Dr. Hasan Acar",
        bolum:"Dahiliye"
      },

      {
        id:10,
        ad:"Dr. Elif Yıldız",
        bolum:"Göz Hastalıkları"
      },

      {
        id:11,
        ad:"Dr. İbrahim Koç",
        bolum:"Psikiyatri"
      },

      {
        id:12,
        ad:"Dr. Selin Aydın",
        bolum:"Nefroloji"
      },

      {
        id:13,
        ad:"Dr. Murat Turan",
        bolum:"Kulak Burun Boğaz"
      },

      {
        id:14,
        ad:"Dr. Gizem Karaca",
        bolum:"Endokrinoloji"
      },

      {
        id:15,
        ad:"Dr. Okan Yavuz",
        bolum:"Onkoloji"
      },

      {
        id:16,
        ad:"Dr. Eda Polat",
        bolum:"Enfeksiyon"
      },

      {
        id:17,
        ad:"Dr. Yusuf Tekin",
        bolum:"Fizyoterapi"
      },

      {
        id:18,
        ad:"Dr. Burcu Erdem",
        bolum:"Diş Hekimliği"
      },

      {
        id:19,
        ad:"Dr. Cem Kaplan",
        bolum:"Plastik Cerrahi"
      },

      {
        id:20,
        ad:"Dr. Derya Taş",
        bolum:"Acil Tıp"
      }

    ];

    renderDoctors(allDoctors);

  }

}

// ============================================
// Render Doctors
// ============================================

function renderDoctors(list) {

  const label = document.getElementById("sectionLabel");

  label.textContent = `${list.length} Doktor`;

  if(list.length === 0){

    container.innerHTML = `
    
      <div class="no-results">
        <h3>Doktor bulunamadı</h3>
      </div>

    `;

    return;

  }

  container.innerHTML = list.map((d,i) => {

    const id =
      d.id || d.Id;

    const ad =
      d.ad || d.name || d.fullName || "Doktor";

    const bolum =
      d.bolum || d.departmentName || "Bölüm";



    // ============================================
    // IMAGE NAME — use shared utility from api.js
    // ============================================

   const imageName = (typeof getImageName === 'function')
     ? getImageName(ad)
     : ad.toLowerCase().replace(/\s+/g, '-');


    return `

      <div class="doctor-card" style="animation-delay:${i * .06}s">

        <div class="doctor-avatar">

          <img
            src="../images/doctors/${imageName}.jpg"

            alt="${ad}"

            onerror="
              this.src='../images/doctors/default.jpg'
            "
          >

        </div>

        <h3>${ad}</h3>

        <div class="doctor-department">
          ${bolum}
        </div>

        <div class="doctor-info">
          Uzman doktor kadromuz ile güvenli sağlık hizmeti sunuyoruz.
        </div>

        <div class="doctor-bottom">

          <span class="doctor-status">
            Müsait
          </span>

          <button
            class="doctor-btn"
            onclick="sec(${id})"
          >
            Randevu
          </button>

        </div>

      </div>

    `;

  }).join("");

}

// ============================================
// Filter
// ============================================

function filterDoctors(){

  const q = document.getElementById("searchInput")
    .value
    .trim()
    .toLowerCase();

  const filtered = allDoctors.filter(d => {

    const name = (d.ad || d.name || d.fullName || "")
      .toString()
      .toLowerCase();

    const bolum = (d.bolum || d.departmentName || "")
      .toString()
      .toLowerCase();

    return (
      name.includes(q) ||
      bolum.includes(q)
    );

  });

  renderDoctors(filtered);

}

// ============================================
// Select Doctor
// ============================================

function sec(id){

  localStorage.setItem("selectedDoctorId", id);

  window.location.href = "randevu.html";

}

// ============================================
// Navbar Effect
// ============================================

window.addEventListener("scroll", () => {

  document.getElementById("navbar")
    .classList.toggle("scrolled", window.scrollY > 20);

});

// ============================================
// Init
// ============================================

loadDoctors();