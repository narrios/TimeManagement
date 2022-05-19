using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TimeManagement.Classes
{
    public class localDB : DataContext
    {
        public Table<LocalUser> user;
        public Table<LocalEvents> sarcini;
        public Table<LocalEndedEvents> sarciniF;
        public Table<Settings> setari;
        public localDB(string connection) : base(connection) { }
    }

    [Table(Name = "LocalUser")]
    public class LocalUser
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int LocalUserId { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public int UserId { get; set; }

    }

    [Table(Name = "LocalEvents")]
    public class LocalEvents
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int LocalEventId { get; set; }
        [Column]
        public string EventName { get; set; }
        [Column]
        public string EventDescription { get; set; }
        [Column]
        public DateTime? EventStart { get; set; }
        [Column]
        public DateTime? EventEnd { get; set; }
        [Column]
        public int EventId { get; set; }

    }

    [Table(Name = "LocalEndedEvents")]
    public class LocalEndedEvents
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int LocalEndedEventId { get; set; }
        [Column]
        public string EventName { get; set; }
        [Column]
        public string EventDescription { get; set; }
        [Column]
        public DateTime? EventStart { get; set; }
        [Column]
        public DateTime? EventEnd { get; set; }
        [Column]
        public int EndedEventId { get; set; }
    }

    [Table(Name = "Settings")]
    public class Settings
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int EndOn { get; set; }
        [Column]
        public int Timer { get; set; }
        [Column]
        public string Theme { get; set; }
    }
}
