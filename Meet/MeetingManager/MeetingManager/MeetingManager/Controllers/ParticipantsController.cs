using MeetingManager.Data;
using MeetingManager.Models;
using MeetingManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace MeetingManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly EmailService _emailService;
        private readonly AppDbContext _context;

        public ParticipantsController(AppDbContext db, EmailService emailService, AppDbContext context)
        {
            _db = db;
            _emailService = emailService;
            _context = context;
        }
        [HttpGet("by-meeting/{meetingId}")]
        public async Task<IActionResult> GetByMeeting(int meetingId)
        {
            var participants = await _db.Participants.Where(p => p.MeetingId == meetingId).ToListAsync();
            return Ok(participants);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Participant p)
        {
            _db.Participants.Add(p);
            await _db.SaveChangesAsync();
            return Ok(p);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Participant p)
        {
            var existing = await _db.Participants.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = p.Name;
            existing.Email = p.Email;
            existing.Role = p.Role;
            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var p = await _db.Participants.FindAsync(id);
            if (p == null) return NotFound();

            _db.Participants.Remove(p);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("participants")]
        public IActionResult GetParticipants()
        {
            return Ok(_context.Participants.ToList());
        }


    }

}

