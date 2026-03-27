using ShoeStore.Dto.Reservation;
using ShoeStore.Models;

namespace ShoeStore.Backend.Services.Interfaces
{
    public interface IReservationService
    {
        Reservation Create(Item item, Client client, ReservationCreateDto dto);
        bool Cancel(Item item, Client client);
    }
}
