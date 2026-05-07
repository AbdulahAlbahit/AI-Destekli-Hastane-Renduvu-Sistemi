// =====================================
// التحقق من المستخدم الحالي
// =====================================

const user = getUser();


// إذا لم يسجل دخول
if (!user) {

  window.location.href = "../pages/giris.html";

}


// فقط الأدمن يستطيع الدخول
if (user.role !== "Admin") {

  alert("Bu sayfaya erişim yetkiniz yok");

  window.location.href = "../index.html";

}



// =====================================
// إظهار وإخفاء الأقسام
// =====================================

function showSection(sectionId) {

  // جميع الأقسام
  const sections = document.querySelectorAll(".section");


  // إخفاء الكل
  sections.forEach(section => {

    section.classList.add("hidden");

  });


  // إظهار القسم المطلوب
  document
    .getElementById(sectionId)
    .classList.remove("hidden");

}



// =====================================
// تحميل الإحصائيات
// =====================================

async function loadDashboardStats() {

  try {

    // جلب البيانات من API
    const users = await AdminAPI.getUsers();

    const doctors = await AdminAPI.getDoctors();

    const appointments = await AdminAPI.getAppointments();


    // عرض الأرقام
    document.getElementById("totalUsers").innerText =
      users.length;


    document.getElementById("totalDoctors").innerText =
      doctors.length;


    document.getElementById("totalAppointments").innerText =
      appointments.length;

  }

  catch(error) {

    console.error(error);

  }

}



// =====================================
// تحميل المستخدمين
// =====================================

async function loadUsers() {

  try {

    const users = await AdminAPI.getUsers();

    const table = document.getElementById("usersTable");


    table.innerHTML = "";


    users.forEach(user => {

      table.innerHTML += `

        <tr>

          <td>${user.id}</td>

          <td>${user.name}</td>

          <td>${user.email}</td>

          <td>

            <button onclick="deleteUser(${user.id})">

              Sil

            </button>

          </td>

        </tr>

      `;

    });

  }

  catch(error) {

    console.error(error);

  }

}



// =====================================
// حذف مستخدم
// =====================================

async function deleteUser(id) {

  const confirmDelete = confirm(
    "Bu kullanıcıyı silmek istiyor musunuz?"
  );


  if (!confirmDelete) {

    return;

  }


  try {

    await AdminAPI.deleteUser(id);

    loadUsers();

    loadDashboardStats();

  }

  catch(error) {

    console.error(error);

  }

}



// =====================================
// تحميل الأطباء
// =====================================

async function loadDoctors() {

  try {

    const doctors = await AdminAPI.getDoctors();

    const table = document.getElementById("doctorsTable");


    table.innerHTML = "";


    doctors.forEach(doc => {

      table.innerHTML += `

        <tr>

          <td>${doc.id}</td>

          <td>${doc.name}</td>

          <td>${doc.departmentName}</td>

        </tr>

      `;

    });

  }

  catch(error) {

    console.error(error);

  }

}



// =====================================
// تحميل المواعيد
// =====================================

async function loadAppointments() {

  try {

    const appointments =
      await AdminAPI.getAppointments();


    const table =
      document.getElementById("appointmentsTable");


    table.innerHTML = "";


    appointments.forEach(app => {

      table.innerHTML += `

        <tr>

          <td>${app.patientName}</td>

          <td>${app.doctorName}</td>

          <td>${app.date}</td>

          <td>${app.status}</td>

        </tr>

      `;

    });

  }

  catch(error) {

    console.error(error);

  }

}



// =====================================
// تحميل كامل البيانات عند فتح الصفحة
// =====================================

loadDashboardStats();

loadUsers();

loadDoctors();

loadAppointments();
loadDepartments();
// =====================================
// إضافة طبيب
// =====================================

async function addDoctor() {

  const name =
    document.getElementById("doctorName").value;


  const department =
    document.getElementById("doctorDepartment").value;


  // تحقق من الحقول
  if (!name || !department) {

    alert("Lütfen tüm alanları doldurun");

    return;

  }


  try {

    await AdminAPI.addDoctor({

      name: name,

      departmentName: department

    });


    // تنظيف الحقول
    document.getElementById("doctorName").value = "";

    document.getElementById("doctorDepartment").value = "";


    // إعادة تحميل القائمة
    loadDoctors();

    loadDashboardStats();


    alert("Doktor başarıyla eklendi");

  }

  catch(error) {

    console.error(error);

  }

}
// =====================================
// حذف طبيب
// =====================================

async function deleteDoctor(id) {

  const confirmDelete =
    confirm("Doktor silinsin mi?");


  if (!confirmDelete) {

    return;

  }


  try {

    await AdminAPI.deleteDoctor(id);

    loadDoctors();

    loadDashboardStats();

  }

  catch(error) {

    console.error(error);

  }

}

// =====================================
// تحميل الأقسام
// =====================================

async function loadDepartments() {

  try {

    const departments =
      await AdminAPI.getDepartments();


    const table =
      document.getElementById("departmentsTable");


    table.innerHTML = "";


    departments.forEach(dep => {

      table.innerHTML += `

        <tr>

          <td>${dep.id}</td>

          <td>${dep.name}</td>

          <td>

            <button onclick="deleteDepartment(${dep.id})">

              Sil

            </button>

          </td>

        </tr>

      `;

    });

  }

  catch(error) {

    console.error(error);

  }

}

// =====================================
// إضافة قسم
// =====================================

async function addDepartment() {

  const name =
    document.getElementById("departmentName").value;


  if (!name) {

    alert("Bölüm adı gerekli");

    return;

  }


  try {

    await AdminAPI.addDepartment({

      name: name

    });


    document.getElementById("departmentName").value = "";


    loadDepartments();

    alert("Bölüm eklendi");

  }

  catch(error) {

    console.error(error);

  }

}

// =====================================
// حذف قسم
// =====================================

async function deleteDepartment(id) {

  const confirmDelete =
    confirm("Bölüm silinsin mi?");


  if (!confirmDelete) {

    return;

  }


  try {

    await AdminAPI.deleteDepartment(id);

    loadDepartments();

  }

  catch(error) {

    console.error(error);

  }

}