using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ChatRoomController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ChatRoomController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/ChatRoom
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom()
    {
        return await _context.ChatRoom.ToListAsync();
    }

    // GET: api/ChatRoom/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ChatRoom>> GetChatRoom(int id)
    {
        var chatRoom = await _context.ChatRoom.FindAsync(id);

        if (chatRoom == null)
        {
            return NotFound();
        }

        return chatRoom;
    }

    // POST: api/ChatRoom
    [HttpPost]
    public async Task<ActionResult<ChatRoom>> CreateChatRoom(ChatRoom chatRoom)
    {
        _context.ChatRoom.Add(chatRoom);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetChatRoom", new { id = chatRoom.ChatRoomId }, chatRoom);
    }

    // PUT: api/ChatRoom/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChatRoom(int id, ChatRoom chatRoom)
    {
        if (id != chatRoom.ChatRoomId)
        {
            return BadRequest();
        }

        _context.Entry(chatRoom).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChatRoomExists(id))
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

    // DELETE: api/ChatRoom/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChatRoom(int id)
    {
        var chatRoom = await _context.ChatRoom.FindAsync(id);
        if (chatRoom == null)
        {
            return NotFound();
        }

        _context.ChatRoom.Remove(chatRoom);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ChatRoomExists(int id)
    {
        return _context.ChatRoom.Any(e => e.ChatRoomId == id);
    }
}
