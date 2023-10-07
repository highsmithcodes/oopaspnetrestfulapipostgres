public class ChatRoom
{
    public int ChatRoomId { get; set; }
    public string Name { get; set; }

    public ICollection<UserChatRoom> UserChatRooms { get; set; }
    public ICollection<Message> Messages { get; set; }
}