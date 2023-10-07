public class Message
{
    public int MessageId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }
    
}