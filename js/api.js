// =====================================
// API BASE
// =====================================

const API_BASE = "http://localhost:5247/api";


// =====================================
// TOKEN MANAGEMENT
// =====================================

function getToken() {
  return localStorage.getItem("token");
}

function setToken(token) {
  localStorage.setItem("token", token);
}

function removeToken() {
  localStorage.removeItem("token");
}


// =====================================
// CURRENT USER
// =====================================

function setUser(user) {
  localStorage.setItem("user", JSON.stringify(user));
}

function getUser() {
  return JSON.parse(localStorage.getItem("user"));
}

function logout() {
  removeToken();
  localStorage.removeItem("user");
  localStorage.removeItem("userName");
  window.location.href = "../pages/giris.html";
}


// =====================================
// GENERIC API REQUEST
// =====================================

async function apiRequest(endpoint, method = "GET", body = null) {

  const options = {
    method,
    headers: {
      "Content-Type": "application/json",
    },
  };

  const token = getToken();

  if (token) {
    options.headers["Authorization"] = `Bearer ${token}`;
  }

  if (body) {
    options.body = JSON.stringify(body);
  }

  const response = await fetch(`${API_BASE}${endpoint}`, options);

  let data;
  try {
    data = await response.json();
  } catch {
    data = null;
  }

  if (!response.ok) {
    throw new Error(data?.message || "Bir hata oluştu");
  }

  return data;
}

// =====================================
// AUTH API
// =====================================

const AuthAPI = {

  async kayit(nameOrObj, email, password) {
    // Kabul eder: kayit({name,email,password}) VEYA kayit(name, email, password)
    const userData = (typeof nameOrObj === 'object')
      ? nameOrObj
      : { name: nameOrObj, email, password };
    return await apiRequest("/auth/register", "POST", userData);
  },

  async giris(emailOrObj, password) {
    // Kabul eder: giris({email,password}) VEYA giris(email, password)
    const loginData = (typeof emailOrObj === 'object')
      ? emailOrObj
      : { email: emailOrObj, password };
    const data = await apiRequest("/auth/login", "POST", loginData);
    if (data?.token) {
      setToken(data.token);
      if (data.user) setUser(data.user);
      if (data.user?.name || data.user?.fullName) {
        localStorage.setItem("userName", data.user.name || data.user.fullName);
      }
    }
    return data;
  },

  isLoggedIn() {
    return !!getToken();
  },

  cikis() {
    logout();
  },

};

// =====================================
// DEPARTMENTS API
// =====================================

const DepartmentAPI = {

  async getAll() {
    return await apiRequest("/departments");
  },

};


// =====================================
// DOCTORS API
// =====================================

const DoctorAPI = {

  async getAll() {
    return await apiRequest("/doctors");
  },

  async getByDepartment(departmentId) {
    return await apiRequest(`/doctors?departmentId=${departmentId}`);
  },

  async getAvailableSlots(doctorId, date) {
    return await apiRequest(`/doctors/${doctorId}/available-slots?date=${date}`);
  },

};

// =====================================
// APPOINTMENTS API
// =====================================

const AppointmentAPI = {

  async create(data) {
    return await apiRequest("/appointments", "POST", data);
  },

  async getMine() {
    return await apiRequest("/appointments/my");
  },

  async cancel(id) {
    return await apiRequest(`/appointments/${id}`, "DELETE");
  },

};


// =====================================
// ADMIN PANEL API
// =====================================

const AdminAPI = {

  async getUsers() {
    return await apiRequest("/admin/users");
  },

  async getDoctors() {
    return await apiRequest("/admin/doctors");
  },

  async getAppointments() {
    return await apiRequest("/admin/appointments");
  },

  async deleteUser(id) {
    return await apiRequest(`/admin/users/${id}`, "DELETE");
  },

  async addDoctor(data) {
    return await apiRequest("/admin/doctors", "POST", data);
  },

  async deleteDoctor(id) {
    return await apiRequest(`/admin/doctors/${id}`, "DELETE");
  },

};


// =====================================
// ALIASES — للتوافق مع randevu.html
// =====================================

const DoktorAPI = {
  getBolumler:      ()                    => DepartmentAPI.getAll(),
  getDoktorlar:     (bolumId)             => DoctorAPI.getByDepartment(bolumId),
  getMüsaitSaatler: (doctorId, date)      => DoctorAPI.getAvailableSlots(doctorId, date),
};

const RandevuAPI = {
  olustur:         (doctorId, date, time) => AppointmentAPI.create({ doctorId, date, time }),
  getMyRandevular: ()                     => AppointmentAPI.getMine(),
  iptalEt:         (id)                   => AppointmentAPI.cancel(id),
};


// =====================================
// SHARED IMAGE NAME UTILITY
// =====================================

function getImageName(name) {
  if (!name) return 'default';
  return name
    .replace(/İ/g, 'i')          // Turkish capital dotted-I → i (MUST be before toLowerCase)
    .replace(/I/g, 'i')          // Regular capital I
    .toLowerCase()
    .replace(/prof\. dr\. /g, 'dr-')
    .replace(/doç\. dr\. /g,    'dr-')
    .replace(/dr\. /g,           'dr-')
    .replace(/ı/g, 'i')
    .replace(/ğ/g, 'g')
    .replace(/ü/g, 'u')
    .replace(/ş/g, 's')
    .replace(/ö/g, 'o')
    .replace(/ç/g, 'c')
    .replace(/\s+/g, '-')
    .replace(/-+/g, '-')         // clean up double dashes
    .replace(/^-|-$/g, '');      // trim dashes
}


// =====================================
// SHARED NAVBAR BUILDER
// =====================================
// activePage: 'home' | 'bolumler' | 'doktorlar' | 'randevu' | 'randevularim' | 'panel' | 'giris' | 'kayit'
// isSubPage : true  → links use '../pages/'  (from a page in /pages/)
//             false → links use 'pages/'     (from root index.html)

function buildNavbar(activePage, isSubPage = true) {
  const navEl = document.getElementById('navLinks')
             || document.querySelector('.navbar-links');
  if (!navEl) return;

  const isLogged  = AuthAPI.isLoggedIn();
  const userName  = localStorage.getItem('userName') || 'Kullanıcı';
  const root      = isSubPage ? '../' : '';
  const pgs       = isSubPage ? '' : 'pages/';

  const active = (key) =>
    key === activePage
      ? 'style="color:var(--primary);font-weight:700;"'
      : '';

  let html = '';

  if (!isLogged) {
    // ✅ غير مسجل → يشوف فقط Giriş + Kayıt
    html = `
      <a href="${pgs}giris.html"  ${active('giris')} >Giriş Yap</a>
      <a href="${pgs}kayit.html" class="btn-nav" ${active('kayit')}>Kayıt Ol</a>
    `;
  } else {
    // ✅ مسجل دخول → يشوف الصفحات الداخلية فقط
    html = `
      <a href="${root}index.html"         ${active('home')}       >Ana Sayfa</a>
      <a href="${pgs}randevu.html"        ${active('randevu')}    >Randevu Al</a>
      <a href="${pgs}randevularim.html"   ${active('randevularim')}>📋 Randevularım</a>
      <span style="font-weight:700;color:var(--primary);padding:8px 12px;font-size:0.9rem;">👤 ${userName}</span>
      <button onclick="AuthAPI.cikis()"
        style="background:var(--danger,#ef4444);color:white;border:none;padding:9px 18px;border-radius:10px;font-weight:700;cursor:pointer;font-family:inherit;font-size:0.9rem;">
        Çıkış
      </button>
    `;
  }

  navEl.innerHTML = html;
}
