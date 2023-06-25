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
            return await context.Appointments.Include(a => a.User).ToListAsync();
        }

        //svi slobodni termini osoblja iz odredjene prodavnice
        public async Task<List<Appointment>> GetAppointmentsByStore(int storeId)
        {
            return await context.Appointments.Where(a => a.User.StoreId == storeId && a.ReservationId == null).ToListAsync();
        }

        //svi slobodni termini osoblja koji su veci od danasnjeg datuma
        public async Task<List<Appointment>> GetAppointmentsByStoreAndDate(int number)
        {
            List<Appointment> appointments;

            if (number < 3)
            {
                appointments = await context.Appointments
                    .Where(a => a.ReservationId == null && a.DateTime.Date >= DateTime.Now.Date)
                    .GroupBy(a => new { a.DateTime.Year, a.DateTime.Month, a.DateTime.Day }) // Grupisanje po danu, godini i mesecu
                    .Select(g => g.First()) // Izbor prvog elementa iz svake grupe
                    .ToListAsync();
            }
            else
            {
                 appointments = await context.Appointments
                  .Where(a => a.ReservationId == null && a.DateTime.Date >= DateTime.Now.Date)
                  .GroupBy(a => a.DateTime)
                  .Where(g => g.Select(a => a.UserId).Distinct().Count() > 1)
                  .Select(g => g.First())
                  .ToListAsync();
            }

            return appointments;
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
            return await context.Appointments.Include(a => a.User).Where(a => a.UserId == userId && a.ReservationId == null).ToListAsync();
        }
    }
}
