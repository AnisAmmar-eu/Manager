using MeetingManager.Data;
using MeetingManager.Models;
using MeetingManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetingManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingService _service;
        private readonly AppDbContext _db;
        private readonly EmailService _emailService;

        public MeetingsController(IMeetingService service, AppDbContext context , EmailService emailService)
        {
            _service = service;
            _db = context;
            _emailService = emailService;

        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var meetings = await _service.GetByProject(projectId);
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var meeting = await _service.GetById(id);
            if (meeting == null) return NotFound();
            return Ok(meeting);
        }
        [HttpPost("{meetingId}/assign-participant/{participantId}")]
        public async Task<IActionResult> AssignParticipant(int meetingId, int participantId)
        {
            // Assigner le participant à la réunion
            await _service.AssignMeetingToParticipant(meetingId, participantId);

      
            var meeting = await _db.Meetings.FindAsync(meetingId);
            var participant = await _db.Participants.FindAsync(participantId);

            if (meeting == null || participant == null)
            {
                return NotFound("Meeting or Participant not found.");
            }

            // Envoyer une invitation par email
            if (meeting.Link != null)
            {
                string subject = $"Invitation à la réunion : {meeting.Title}";
                string body = $@"
            Bonjour {participant.Name},

            Vous êtes invité(e) à participer à la réunion suivante suivante :

            Titre : {meeting.Title}
            Date : {meeting.StartTime:dddd dd MMMM yyyy à HH:mm}
            Lieu : {meeting.Location}
            Lien : {meeting.Link}
            Cordialement,
            MeetingManager Directeur ";

                await _emailService.SendInvitation(participant.Email, subject, body);
            }

            return Ok();
        }
        [HttpPost("{meetingId}/update-participants")]
        public async Task<IActionResult> UpdateParticipants(int meetingId, [FromBody] List<int> participantIds)
        {
            var meeting = await _db.Meetings.Include(m => m.Participants).FirstOrDefaultAsync(m => m.Id == meetingId);

            if (meeting == null)
            {
                return NotFound("Meeting not found.");
            }

            // Clear existing participants
            meeting.Participants.Clear();

            // Add the new participants
            foreach (var participantId in participantIds)
            {
                var participant = await _db.Participants.FindAsync(participantId);
                if (participant != null)
                {
                    meeting.Participants.Add(participant);
                }
                else
                {
                    return NotFound($"Participant with ID {participantId} not found.");
                }
            }

            await _db.SaveChangesAsync();

            // Optionally, resend invitations to the updated list of participants
            if (meeting.Link != null)
            {
                foreach (var participantId in participantIds)
                {
                    var participant = await _db.Participants.FindAsync(participantId);
                    if (participant != null)
                    {
                        string subject = $"Mise à jour de l'invitation à la réunion : {meeting.Title}";
                        string body = $@"
                    Bonjour {participant.Name},

                    Votre invitation à la réunion suivante a été mise à jour :

                    Titre : {meeting.Title}
                    Date : {meeting.StartTime:dddd dd MMMM yyyy à HH:mm}
                    Lieu : {meeting.Location}
                    Lien : {meeting.Link}

                    Cordialement,
                    MeetingManager Directeur ";

                        await _emailService.SendInvitation(participant.Email, subject, body);
                    }
                }
            }

            return Ok("Participants updated successfully.");
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Meeting meeting)
        {
            var created = await _service.Create(meeting);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Meeting updated)
        {
            var result = await _service.Update(id, updated);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
         
    }

}
