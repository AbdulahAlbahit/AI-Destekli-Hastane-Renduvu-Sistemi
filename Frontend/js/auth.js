/* ============================================
   MedRandevu — Auth & Common Utilities
   ============================================ */

const TOKEN_KEY = 'medrandevu_token';

// Force Flatpickr inputs to use Western numerals globally
if (typeof flatpickr !== 'undefined') {
  flatpickr.setDefaults({
    onReady: function(selectedDates, dateStr, instance) {
      const elementsToFix = [];
      if (instance.yearElements) elementsToFix.push(...instance.yearElements);
      if (instance.hourElement) elementsToFix.push(instance.hourElement);
      if (instance.minuteElement) elementsToFix.push(instance.minuteElement);
      if (instance.secondElement) elementsToFix.push(instance.secondElement);

      elementsToFix.forEach(el => {
        el.type = 'text'; // Chromium ignores lang on type="number", so we force text
        el.setAttribute('inputmode', 'numeric');
        el.setAttribute('lang', 'en-US');
        el.style.direction = 'ltr';
        el.style.fontFamily = 'sans-serif';
      });
    }
  });
}

/* ── Auth Helpers ── */
function isLoggedIn() {
  return !!localStorage.getItem(TOKEN_KEY);
}

function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

function saveToken(token) {
  localStorage.setItem(TOKEN_KEY, token);
}

function logout() {
  localStorage.removeItem(TOKEN_KEY);
  window.location.href = 'index.html';
}

/** Redirect to login if not authenticated (call on protected pages) */
function requireAuth() {
  if (!isLoggedIn()) {
    window.location.href = 'index.html';
  }
}

/** Redirect to dashboard if already authenticated (call on login/register pages) */
function redirectIfLoggedIn() {
  if (isLoggedIn()) {
    window.location.href = 'dashboard.html';
  }
}

/**
 * Decode JWT token payload (without validation — just for display)
 */
function decodeToken() {
  const token = getToken();
  if (!token) return null;
  try {
    const payload = token.split('.')[1];
    const decoded = JSON.parse(atob(payload));
    return decoded;
  } catch {
    return null;
  }
}

/* ── Toast Notifications ── */
let toastContainer = null;

function ensureToastContainer() {
  if (!toastContainer) {
    toastContainer = document.createElement('div');
    toastContainer.className = 'toast-container';
    toastContainer.id = 'toast-container';
    document.body.appendChild(toastContainer);
  }
}

function showToast(message, type = 'success', duration = 4000) {
  ensureToastContainer();

  const icons = {
    success: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>`,
    danger: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/></svg>`,
    warning: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"/><line x1="12" y1="9" x2="12" y2="13"/><line x1="12" y1="17" x2="12.01" y2="17"/></svg>`,
    info: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><path d="M12 16v-4"/><path d="M12 8h.01"/></svg>`,
  };

  const titles = {
    success: 'Başarılı',
    danger: 'Hata',
    warning: 'Uyarı',
    info: 'Bilgi',
  };

  const toast = document.createElement('div');
  toast.className = `toast toast-${type}`;
  toast.innerHTML = `
    <span class="toast-icon" style="color: var(--clr-${type === 'info' ? 'info' : type})">${icons[type]}</span>
    <div class="toast-content">
      <div class="toast-title">${titles[type]}</div>
      <div class="toast-message">${message}</div>
    </div>
    <button class="toast-close" onclick="this.parentElement.remove()">✕</button>
  `;

  toastContainer.appendChild(toast);

  setTimeout(() => {
    toast.style.opacity = '0';
    toast.style.transform = 'translateX(100%)';
    toast.style.transition = 'all 300ms ease';
    setTimeout(() => toast.remove(), 300);
  }, duration);
}

/* ── Modal Helpers ── */
function openModal(modalId) {
  const backdrop = document.getElementById(modalId);
  if (backdrop) {
    backdrop.classList.add('is-open');
    document.body.style.overflow = 'hidden';
  }
}

function closeModal(modalId) {
  const backdrop = document.getElementById(modalId);
  if (backdrop) {
    backdrop.classList.remove('is-open');
    document.body.style.overflow = '';
  }
}

// Close modal on backdrop click
document.addEventListener('click', (e) => {
  if (e.target.classList.contains('modal-backdrop') && e.target.classList.contains('is-open')) {
    e.target.classList.remove('is-open');
    document.body.style.overflow = '';
  }
});

// Close modal on Escape key
document.addEventListener('keydown', (e) => {
  if (e.key === 'Escape') {
    const openModals = document.querySelectorAll('.modal-backdrop.is-open');
    openModals.forEach((modal) => {
      modal.classList.remove('is-open');
    });
    document.body.style.overflow = '';
  }
});

/* ── Loading State Helpers ── */
function setButtonLoading(btn, isLoading) {
  if (isLoading) {
    btn.classList.add('is-loading');
    btn.dataset.originalText = btn.innerHTML;
    btn.innerHTML = `<span class="btn-spinner"></span> Yükleniyor...`;
  } else {
    btn.classList.remove('is-loading');
    btn.innerHTML = btn.dataset.originalText || btn.innerHTML;
  }
}

/* ── Sidebar Active Link ── */
function initSidebar() {
  const currentPage = window.location.pathname.split('/').pop() || 'dashboard.html';
  const links = document.querySelectorAll('.sidebar-link');

  links.forEach((link) => {
    const href = link.getAttribute('href');
    if (href === currentPage) {
      link.classList.add('active');
    }
  });

  // Logout button
  const logoutBtn = document.getElementById('logout-btn');
  if (logoutBtn) {
    logoutBtn.addEventListener('click', (e) => {
      e.preventDefault();
      e.stopPropagation(); // Prevent opening profile modal
      openLogoutModal();
    });
  }

  // Profile Modal logic
  const sidebarUser = document.querySelector('.sidebar-user');
  if (sidebarUser) {
    sidebarUser.style.cursor = 'pointer';
    sidebarUser.addEventListener('click', async (e) => {
      if (e.target.closest('#logout-btn')) return; // Ignore logout clicks
      await openProfileModal();
    });
  }
}

function openLogoutModal() {
  let modal = document.getElementById('logout-modal');
  if (!modal) {
    modal = document.createElement('div');
    modal.className = 'modal-backdrop';
    modal.id = 'logout-modal';
    modal.innerHTML = `
      <div class="modal" style="max-width: 400px; text-align: center;">
        <div class="modal-body" style="padding-top: 2rem;">
          <h3 style="margin-bottom: 1rem;">Çıkış Yap</h3>
          <p style="color: var(--clr-text-light); margin-bottom: 2rem;">Hesabınızdan çıkış yapmak istediğinize emin misiniz?</p>
          <div style="display: flex; gap: 1rem; justify-content: center;">
            <button class="btn btn-ghost" onclick="closeModal('logout-modal')">İptal</button>
            <button class="btn btn-danger" onclick="logout()" style="background: var(--clr-danger); color: white; border: none;">Evet, Çıkış Yap</button>
          </div>
        </div>
      </div>
    `;
    document.body.appendChild(modal);
  }
  openModal('logout-modal');
}

async function openProfileModal() {
  let modal = document.getElementById('profile-modal');
  
  if (!modal) {
    // Inject HTML dynamically
    modal = document.createElement('div');
    modal.className = 'modal-backdrop';
    modal.id = 'profile-modal';
    modal.innerHTML = `
      <div class="modal">
        <div class="modal-header">
          <div class="modal-title">Profil Bilgileri</div>
          <button class="modal-close" onclick="closeModal('profile-modal')">✕</button>
        </div>
        <div class="modal-body">
          <form id="profile-form">
            <div class="form-group">
              <label class="form-label">Ad Soyad</label>
              <input type="text" class="form-control" id="profile-name" disabled>
            </div>
            <div class="form-group">
              <label class="form-label">Telefon Numarası</label>
              <input type="tel" class="form-control" id="profile-phone" placeholder="05XXXXXXXXX" maxlength="11" pattern="[0-9]{10,11}">
            </div>
            <div class="form-group">
              <label class="form-label">Doğum Tarihi</label>
              <input type="date" class="form-control" id="profile-dob" lang="en-US" style="direction: ltr; font-family: sans-serif;">
            </div>
            <div class="form-group">
              <label class="form-label">Cinsiyet</label>
              <select class="form-control" id="profile-gender">
                <option value="Erkek">Erkek</option>
                <option value="Kadın">Kadın</option>
              </select>
            </div>
            <button type="submit" class="btn btn-primary" style="width: 100%; margin-top: 1rem;" id="profile-save-btn">Kaydet</button>
          </form>
        </div>
      </div>
    `;
    document.body.appendChild(modal);

    // Initialize Flatpickr for DOB
    const dobEl = document.getElementById('profile-dob');
    if (dobEl && typeof flatpickr !== 'undefined') {
      const eighteenYearsAgo = new Date();
      eighteenYearsAgo.setFullYear(new Date().getFullYear() - 18);
      
      flatpickr(dobEl, {
        locale: "tr",
        dateFormat: "Y-m-d",
        maxDate: eighteenYearsAgo,
        disableMobile: "true"
      });
    }

    // Phone input — only digits and max 11
    const profilePhone = document.getElementById('profile-phone');
    if (profilePhone) {
      profilePhone.addEventListener('input', (e) => {
        e.target.value = e.target.value.replace(/\D/g, '').substring(0, 11);
      });
    }

    document.getElementById('profile-form').addEventListener('submit', async (e) => {
      e.preventDefault();
      const btn = document.getElementById('profile-save-btn');
      setButtonLoading(btn, true);

      try {
        const payload = {
          patientName: document.getElementById('profile-name').value,
          phone: document.getElementById('profile-phone').value,
          dateOfBirth: document.getElementById('profile-dob').value,
          gender: document.getElementById('profile-gender').value
        };

        const res = await PatientAPI.updateProfile(payload);
        if (res.ok) {
          showToast('Profil başarıyla güncellendi.', 'success');
          closeModal('profile-modal');
        } else {
          showToast(res.data?.message || 'Güncelleme başarısız.', 'danger');
        }
      } catch (err) {
        showToast(err.message, 'danger');
      } finally {
        setButtonLoading(btn, false);
      }
    });
  }

  // Fetch current profile data
  try {
    const res = await PatientAPI.getProfile();
    if (res.ok && res.data) {
      document.getElementById('profile-name').value = res.data.patientName || '';
      document.getElementById('profile-name').disabled = true; // Mevcut ise düzenlenemesin
      document.getElementById('profile-phone').value = res.data.phone || '';
      document.getElementById('profile-dob').value = res.data.dateOfBirth || '';
      document.getElementById('profile-gender').value = res.data.gender || 'Erkek';
    } else {
      // Hasta profili yoksa alanları boş bırak, ismin girilmesine izin ver
      document.getElementById('profile-name').value = '';
      document.getElementById('profile-name').disabled = false;
      document.getElementById('profile-phone').value = '';
      document.getElementById('profile-dob').value = '';
      document.getElementById('profile-gender').value = 'Erkek';
      if (res.status !== 404) {
        showToast('Profil bilgileri alınamadı, ancak yeni profil oluşturabilirsiniz.', 'info');
      }
    }
    openModal('profile-modal');
  } catch (err) {
    showToast('Profil bilgileri yüklenirken hata oluştu.', 'danger');
    openModal('profile-modal'); // Hata olsa da açsın
  }
}

/* ── Date/Time Formatting ── */
function formatDate(dateStr) {
  if (!dateStr) return '—';
  const d = new Date(dateStr);
  return d.toLocaleDateString('tr-TR', { day: '2-digit', month: 'long', year: 'numeric' });
}

function formatTime(timeStr) {
  if (!timeStr) return '—';
  // Handle "HH:mm:ss" or "HH:mm"
  return timeStr.substring(0, 5);
}

function getStatusBadge(status) {
  const map = {
    'Beklemede': 'warning',
    'Onaylandı': 'success',
    'İptal': 'danger',
    'Tamamlandı': 'info',
  };
  const type = map[status] || 'primary';
  return `<span class="badge badge-${type}">${status}</span>`;
}
