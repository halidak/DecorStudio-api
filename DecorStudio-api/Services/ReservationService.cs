using DecorStudio_api.DTOs;
using DecorStudio_api.Migrations;
using DecorStudio_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace DecorStudio_api.Services
{
    public class ReservationService
    {
        private readonly AppDbContext context;

        public ReservationService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Reservation> MakeReservation(ReservationDto dto)
        {
            var app = await context.Appointments.FirstOrDefaultAsync(a => a.DateTime == dto.ReservationDate);
            var count = await context.Appointments.CountAsync(a => a.DateTime == dto.ReservationDate);
            var appointments = await context.Appointments
             .Where(a => a.DateTime == dto.ReservationDate && a.ReservationId == null)
             .ToListAsync();

            var numOfReservations = dto.DecorIds.Count();

            if (numOfReservations < 3 && app != null && app.ReservationId == null && count > 0)
            {
                var reservation = new Reservation
                {
                    UserId = dto.UserId,
                };

                await context.Reservations.AddAsync(reservation);
                await context.SaveChangesAsync();

                foreach (var decor in dto.DecorIds)
                {
                    var decorReservation = new Decor_Reservation
                    {
                        DecorId = decor,
                        ReservationId = reservation.Id
                    };
                    await context.Decor_Reservations.AddAsync(decorReservation);
                }

                app.ReservationId = reservation.Id;
                
                await context.SaveChangesAsync();
                return reservation;
            }

            else if (numOfReservations >= 3 && appointments != null && appointments.Count() > 1)
            {
                var reservation = new Reservation
                {
                    UserId = dto.UserId,
                };

                await context.Reservations.AddAsync(reservation);
                await context.SaveChangesAsync();

                foreach (var decor in dto.DecorIds)
                {
                    var decorReservation = new Decor_Reservation
                    {
                        DecorId = decor,
                        ReservationId = reservation.Id
                    };
                    await context.Decor_Reservations.AddAsync(decorReservation);
                }

                var prvaDvaTermina = appointments.Take(2).ToList();
                foreach (var a in prvaDvaTermina)
                {
                    a.ReservationId = reservation.Id;
                }

                await context.SaveChangesAsync();
                return reservation;
            }
            else
            {
                throw new Exception("You can't make reservation");
            }

        }
    }
}
