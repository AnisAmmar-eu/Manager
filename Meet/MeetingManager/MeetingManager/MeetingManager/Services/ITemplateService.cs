using MeetingManager.Models;

namespace MeetingManager.Services
{
    public interface ITemplateService
    {
        Task<List<Templates>> GetByMeet(int IdMeet);
        Task<Templates?> GetById(int id);
        Task<Templates> Create(Templates templates);
        Task<Templates> Update(int id, Templates updated);
        Task Delete(int id);

    }
}
