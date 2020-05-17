using SmartSolucionesCuba.SAPRESSC.Business.Billboard.Cinema.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class Event : Core.Persistence.Entities.IHabilitableEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Enabled { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Thematic { get; set; }

        [Required]
        public string Organizer { get; set; }

        [Required]
        public ICollection<EventPersonalityBridge> EventPersonalityBridges { get; set; }

        [Required]
        public string About { get; set; }

        public string Description { get; set; }

        [Required]
        public Middleware.Media.Persistence.Entities.Image PosterImage { get; set; }

        public Middleware.Media.Persistence.Entities.Video PromoVideo { get; set; }

        public Event()
        {
            EventPersonalityBridges = new List<EventPersonalityBridge>();
        }

        public ICollection<Personality> GetPersonalities()
        {
            var result = new List<Personality>();

            foreach (var bridge in EventPersonalityBridges)
            {
                result.Add(bridge.Personality);
            }

            return result;
        }

        public void SetPersonalities(ICollection<Personality> personalities)
        {
            EventPersonalityBridges.Clear();

            if (personalities == null) return;

            foreach (var item in personalities)
            {
                EventPersonalityBridges.Add(new EventPersonalityBridge
                {
                    EventId = Id,
                    Event = this,
                    PersonalityId = item.Id,
                    Personality = item
                });
            }
        }
    }
}
