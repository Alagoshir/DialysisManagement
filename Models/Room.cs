using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Sala dialisi
    /// </summary>
    public class Room
    {
        public int RoomId { get; set; }
        public string NomeSala { get; set; }
        public int NumeroPostazioni { get; set; }
        public int? Piano { get; set; }
        public string Note { get; set; }
        public bool Attiva { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
