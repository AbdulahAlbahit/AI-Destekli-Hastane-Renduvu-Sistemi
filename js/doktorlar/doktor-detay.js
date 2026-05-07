const doctors = [

  {
    id:1,
    name:"Dr. Ahmet Yılmaz",
    department:"Kardiyoloji"
  },

  {
    id:2,
    name:"Dr. Elif Kaya",
    department:"Nöroloji"
  }

];

const params = new URLSearchParams(window.location.search);

const doctorId = params.get("id");

const doctor = doctors.find(d => d.id == doctorId);

if(doctor){

  document.getElementById("doctorName")
  .textContent = doctor.name;

  document.getElementById("doctorDepartment")
  .textContent = doctor.department;

}

document.getElementById("appointmentBtn")
.addEventListener("click", () => {

  window.location.href =
    `randevu.html?doktorId=${doctorId}`;

});