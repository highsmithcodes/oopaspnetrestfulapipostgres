using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MessageController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Message
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetMessage()
    {
        return await _context.Message.ToListAsync();
    }

    // GET: api/Message/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessage(int id)
    {
        var message = await _context.Message.FindAsync(id);

        if (message == null)
        {
            return NotFound();
        }

        return message;
    }

    // POST: api/Message
   [HttpPost]
    public async Task<ActionResult<Message>> CreateMessage(string Content)
    {
        // Log the received content to see if it's correctly bound.
        Console.WriteLine($"Received Content: {Content}");

        if (string.IsNullOrEmpty(Content))
        {
            return BadRequest("Invalid message content.");
        }

        try
        {
            // Create a new Message object with the provided content
            var message = new Message
            {
                Content = Content,
            };

            // Add the message to the context and save changes
            _context.Message.Add(message);
            await _context.SaveChangesAsync();

            // Return the newly created message as a response
            return CreatedAtAction("GetMessage", new { id = message.MessageId }, message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    // PUT: api/Message/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMessage(int id, Message message)
    {
        if (id != message.MessageId)
        {
            return BadRequest();
        }

        _context.Entry(message).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MessageExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Message/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
        var message = await _context.Message.FindAsync(id);
        if (message == null)
        {
            return NotFound();
        }

       _context.Message.Remove(message);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MessageExists(int id)
    {
        return _context.Message.Any(e => e.MessageId == id);
    }
}