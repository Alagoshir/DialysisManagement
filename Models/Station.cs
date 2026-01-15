using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Postazione dialisi (rene)
    /// </summary>
    public class Station
    {
        public int StationId { get; set; }
        public int RoomId { get; set; }
        public int NumeroPostazione { get; set; }
        public bool Isolamento { get; set; }
        public string Note { get; set; }
        public bool Attiva { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
