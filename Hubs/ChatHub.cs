using Microsoft.AspNetCore.SignalR;
using BACKEND.Models;
using BACKEND.Data;
using System.Threading.Tasks;
using System;

namespace BACKEND.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        // Ο κατασκευαστής (constructor) - θα μας δώσει πρόσβαση στη βάση
        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        // Αυτή η μέθοδος καλείται όταν κάποιος στέλνει ένα μήνυμα
        public async Task SendMessage(string user, string content)
        {
            // 1. Δημιουργούμε ένα νέο αντικείμενο Message
            var message = new Message
            {
                User = user,
                Content = content,
                Timestamp = DateTime.Now
            };

            // 2. Το αποθηκεύουμε στη βάση δεδομένων
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // 3. Το στέλνουμε σε ΟΛΟΥΣ τους συνδεδεμένους χρήστες
            await Clients.All.SendAsync("ReceiveMessage", message.User, message.Content, message.Timestamp);
        }
    }
}