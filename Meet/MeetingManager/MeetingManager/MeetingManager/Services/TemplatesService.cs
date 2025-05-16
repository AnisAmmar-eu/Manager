using MeetingManager.Data;
using MeetingManager.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Services
{
    public class TemplatesService : ITemplateService
    {
        private readonly AppDbContext _db;

        public TemplatesService(AppDbContext db) => _db = db;
        public async Task<Templates> Create(Templates templates)
        {
            // Vérifie si le meet est déjà assigné à une autre template
            bool meetAlreadyAssigned = await _db.Templates
                .AnyAsync(t => t.MeetingId == templates.MeetingId);

            if (meetAlreadyAssigned)
            {
                throw new InvalidOperationException("Ce meeting est déjà assigné à une autre template.");
            }

            _db.Templates.Add(templates);
            await _db.SaveChangesAsync();
            return templates;
        }


        public async Task Delete(int id)
        {
            var template = await _db.Templates.FindAsync(id);
            if (template == null) return;
            _db.Templates.Remove(template);
            await _db.SaveChangesAsync();
        }

        public async Task<Templates?> GetById(int id)
        
             => await _db.Templates
            .Include(m => m.Meeting)
            .FirstOrDefaultAsync(m => m.Id == id);
        

        public async Task<List<Templates>> GetByMeet(int IdMeet)
        
                   => await _db.Templates
            .Include(m => m.Meeting)
            .ToListAsync();
        

        public async Task<Templates> Update(int id, Templates updated)
        {
            var existing = await _db.Templates.FindAsync(id);
            if (existing == null) throw new Exception("Template not found");

            existing.Name = updated.Name;


            await _db.SaveChangesAsync();
            return existing;
        }
    }
}
