using System;

namespace Luxary.Services
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public ulong DiscordID { get; set; }
        public double Money { get; set; }
        public string Name { get; set; }
        public Int32 Counter { get; set; }
        public string DailyGot { get; set; }
    }
}