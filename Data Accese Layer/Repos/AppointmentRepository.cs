using Data_Accese_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Accese_Layer.Repos
{
    public  class AppointmentRepository:IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
                _context   = context;
        }
        public async Task<List<Appointment>> GetAllAppointment()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            var appoint = await _context.Appointments.SingleOrDefaultAsync(c => c.AppointmentId == id);
            if (appoint == null)
                return null;

            return appoint;
        }

        public async Task AddAppointment(Appointment appointment)
        {
          
            if (appointment != null) { 
            _context.Appointments.Add(appointment);
            }
         await   _context.SaveChangesAsync();
        }

        public async Task DeleteAppointment(int id)
        {      
        var  appointment = await _context.Appointments.SingleOrDefaultAsync(c=>c.AppointmentId == id);
            if (appointment != null) { 
            _context.Appointments.Remove(appointment);
            }

          await  _context.SaveChangesAsync();

        }

     
        public async Task  UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
