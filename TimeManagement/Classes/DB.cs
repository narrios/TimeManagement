using System;
using System.Data.Linq.Mapping;

namespace TimeManagement.Classes
{
    public class DB
    {
        [Table(Name = "Users")]
        public class Users
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int UserId { get; set; }
            [Column]
            public string Name { get; set; }
            [Column]
            public string Email { get; set; }
            [Column]
            public string Password { get; set; }

        }

        [Table(Name = "Events")]
        public class Events
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int EventId { get; set; }
            [Column]
            public string EventName { get; set; }
            [Column]
            public string EventDescription { get; set; }
            [Column]
            public Nullable<DateTime> EventStart { get; set; }
            [Column]
            public Nullable<DateTime> EventEnd { get; set; }
            [Column]
            public int UserId { get; set; }
        }

        [Table(Name = "EndedEvents")]
        public class EndedEvents
        {
            [Column(IsPrimaryKey = true, IsDbGenerated = true)]
            public int EndedEventId { get; set; }
            [Column]
            public string EventName { get; set; }
            [Column]
            public string EventDescription { get; set; }
            [Column]
            public Nullable<DateTime> EventStart { get; set; }
            [Column]
            public Nullable<DateTime> EventEnd { get; set; }
            [Column]
            public int UserId { get; set; }
        }
    }
}
