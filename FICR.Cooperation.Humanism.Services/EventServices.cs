using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Contracts.Base;
using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Entities.DTO.EventoDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;
using System.Globalization;

namespace FICR.Cooperation.Humanism.Services
{
    public class EventServices : IEventService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventRepository _eventRepository;

        public EventServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._eventRepository = unitOfWork.EventRepository;
        }

        public async Task<EventOutputDTO> CreateEvent(EventInputDTO eventInputDTO)
        {
            if (eventInputDTO == null)
            {
                throw new ArgumentNullException(nameof(eventInputDTO));
            }

            // Converter a string de data e hora para DateTime
            if (!DateTime.TryParseExact(eventInputDTO.Scheduling, "yyyy-MM-dd:HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime parsedDateTime))
            {
                throw new ArgumentException("Data e hora no formato inválido. Use 'yyyy-MM-dd:HH:mm'.");
            }
            // Ajustar para UTC
            var utcDateTime = DateTime.SpecifyKind(parsedDateTime, DateTimeKind.Utc);

            var newEvent = new Event(eventInputDTO.title, eventInputDTO.description, eventInputDTO.imagePath, utcDateTime); ;

            await _eventRepository.SaveAsync(newEvent);
            await _unitOfWork.SaveChangesAsync();

            return new EventOutputDTO
            {
                Id = newEvent.Id,
                title = newEvent.title,
                description = newEvent.description,
                imagePath = newEvent.imagePath,
                scheduling = utcDateTime
            };
        }

        public async  Task DeleteEvento(Guid Id)
        {
            var evento = await _eventRepository.FindByIdAsync(Id) ?? throw new ArgumentException("Evento não existe.", nameof(Id));

            if (evento.IsDeleted == true)
            {
                throw new ArgumentException("Evento não existe.");
            };

            _eventRepository.Delete(evento);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<PagedResult<EventOutputDTO>> GetAllEvents(PaginationFilter filter)
        {
            var pagedResult = await _eventRepository.FindAllByPagedAsync(filter);

            var eventsDTO = pagedResult.List.Select(p => new EventOutputDTO
            {
                Id = p.Id,
                title = p.title,
                description = p.description,
                imagePath = p.imagePath,
                scheduling = p.scheduling,

            }).ToList();

            var result = new PagedResult<EventOutputDTO>
            {
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount,
                PageCount = pagedResult.PageCount,
                CurrentPage = pagedResult.CurrentPage,
                List = eventsDTO
            };
            return result;
        }

        public async Task<EventOutputDTO> GetEventsById(Guid id)
        {
          var evento = await _eventRepository.FindByIdAsync(id);

            return new EventOutputDTO
            {
                Id = evento.Id,
                title = evento.title,
                description = evento.description,
                imagePath = evento.imagePath,
                scheduling = evento.scheduling,
            };

        }
    }
}