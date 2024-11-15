using ShoeStore.Backend.Data;
using ShoeStore.Backend.Services.Interfaces;
using ShoeStore.Dto.Reservation;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services
{
    public class ReservarionService: IReservationService
    {
        private readonly ApplicationContext _context;
        public ReservarionService(ApplicationContext context)
        {
            _context = context;
        }

        public Reservation Create(Item item, Client client, ReservationCreateDto dto)
        {
            ArgumentNullException.ThrowIfNull(item);
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(dto.Count);
            ArgumentNullException.ThrowIfNull(dto.EndAt);
            ArgumentNullException.ThrowIfNull(dto.Deposit);

            var reservation = new Reservation()
            {
                Item = item,
                Client = client,
                Count = dto.Count,
                EndAt = dto.EndAt.Value,
                Deposit = dto.Deposit
            };
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }

        public bool Cancel(Item item, Client client)
        {
            ArgumentNullException.ThrowIfNull(item);
            ArgumentNullException.ThrowIfNull(client);

            var reservation = _context.Reservations
                .Where(x => x.Item.Id == item.Id)
                .Where(x => x.Client.Id == client.Id)
                .Where(x => DateTime.Now < x.EndAt).FirstOrDefault();
            
            if (reservation == null)
            {
                return false;
            }
            
            reservation.EndAt = DateTime.Now;
            _context.SaveChanges();

            return true;
        }
    }
}
