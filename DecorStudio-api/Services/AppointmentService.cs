using DecorStudio_api.DTOs;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DecorStudio_api.Services
{
    public class AppointmentService
    {
        private readonly AppDbContext context;

        public AppointmentService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Appointment> CreateAppointment(AppointmentDto appointmentDto)
        {
            var appointment = new Appointment
            {
                DateTime = appointmentDto.DateTime,
                UserId = appointmentDto.UserId
            };
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            return await context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointment(int id)
        {
            return await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Appointment> UpdateAppointment(int id, AppointmentDto appointmentDto)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                return null;
            }
            appointment.DateTime = appointmentDto.DateTime;
            appointment.UserId = appointmentDto.UserId;
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> DeleteAppointment(int id)
        {
            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
            {
                return null;
            }
            context.Appointments.Remove(appointment);
            await context.SaveChangesAsync();
            return appointment;
        }

        public async Task<List<Appointment>> GetAppointmentsByUserId(string userId)
        {
            return await context.Appointments.Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
