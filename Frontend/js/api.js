/* ============================================
   MedRandevu — API Service Layer
   ============================================ */

// Vercel üzerinden proxy yaparak Somee'ye (HTTP) güvenli istek atmak için /api kullanıyoruz.
const API_BASE_URL = '/api';

/**
 * Centralized fetch wrapper that auto-attaches JWT token
 * and handles common error cases.
 */
async function apiFetch(endpoint, options = {}) {
  const token = localStorage.getItem('medrandevu_token');

  const headers = {
    'Content-Type': 'application/json',
    ...options.headers,
  };

  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }

  try {
    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
      ...options,
      headers,
    });

    // Handle 401 — redirect to login
    if (response.status === 401) {
      localStorage.removeItem('medrandevu_token');
      window.location.href = 'login.html';
      throw new Error('Oturum süresi doldu. Lütfen tekrar giriş yapın.');
    }

    // Handle no-content responses
    if (response.status === 204 || response.headers.get('content-length') === '0') {
      return { ok: response.ok, status: response.status, data: null };
    }

    // Try to parse JSON
    let data;
    const contentType = response.headers.get('content-type');
    if (contentType && contentType.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    return { ok: response.ok, status: response.status, data };
  } catch (error) {
    if (error.message.includes('Failed to fetch') || error.message.includes('NetworkError')) {
      throw new Error('Sunucuya bağlanılamadı. Backend çalışıyor mu?');
    }
    throw error;
  }
}

/* ── Auth API ── */
const AuthAPI = {
  async login(tc, password) {
    return apiFetch('/User', {
      method: 'POST',
      body: JSON.stringify({ tc, password }),
    });
  },

  async register(registrationData) {
    return apiFetch('/Sign', {
      method: 'POST',
      body: JSON.stringify(registrationData),
    });
  },
};

/* ── Appointment API ── */
const AppointmentAPI = {
  async getMyAppointment() {
    return apiFetch('/Appointment');
  },

  async getAllAppointments() {
    return apiFetch('/Appointment/GetAllAppointment');
  },

  async getById(id) {
    return apiFetch(`/Appointment/${id}`);
  },

  async create(appointmentData) {
    return apiFetch('/Appointment', {
      method: 'POST',
      body: JSON.stringify(appointmentData),
    });
  },

  async update(id, appointmentData) {
    return apiFetch(`/Appointment?id=${id}`, {
      method: 'PUT',
      body: JSON.stringify(appointmentData),
    });
  },

  async remove(appointmentId) {
    return apiFetch(`/Appointment?appointmentId=${appointmentId}`, {
      method: 'DELETE',
    });
  },

  async checkAvailability(date, time) {
    return apiFetch(`/Appointment/zamanMakinizma/${date}/${time}`);
  },
};

/* ── Department API ── */
const DepartmentAPI = {
  async getAll() {
    return apiFetch('/Department');
  },
};

/* ── Clinic API ── */
const ClinicAPI = {
  async getAll() {
    return apiFetch('/Clinic');
  },

  async getByDepId(depId) {
    return apiFetch(`/Clinic/Depid:${depId}`);
  },

  async getById(id) {
    return apiFetch(`/Clinic/Id:${id}`);
  },
};

/* ── Doctor API ── */
const DoctorAPI = {
  async getAll() {
    return apiFetch('/Doctor');
  },

  async getById(id) {
    return apiFetch(`/Doctor/iD:${id}`);
  },

  async getByClinicId(clinicId) {
    return apiFetch(`/Doctor/ClinicId:${clinicId}`);
  },
};

/* ── AI API ── */
const AiAPI = {
  async analyze(complaint) {
    return apiFetch('/Ai/analyze', {
      method: 'POST',
      body: JSON.stringify(complaint),
    });
  },
};

/* ── Patient API ── */
const PatientAPI = {
  async getProfile() {
    return apiFetch('/api/Patient/profile');
  },

  async updateProfile(profileData) {
    return apiFetch('/api/Patient/profile', {
      method: 'PUT',
      body: JSON.stringify(profileData),
    });
  },
};
