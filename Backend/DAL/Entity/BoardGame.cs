using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class BoardGame : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ThumbnailImageURL { get; set; }
        public string Description { get; set; }
        public DateTime Published { get; set; }
        [Required]
        public int MinimalPlayers { get; set; }
        [Required]
        public int MaximalPlayers { get; set; }
        //time in minutes
        [Required]
        public int MinimalPlayingTime { get; set; }
        //time in minutes
        [Required]
        public int MaximalPlayingTime { get; set; }
        [Required]
        public int MinimalPlayerAge { get; set; }
        public List<BoardGameGategory> Categories { get; set; }
        public List<BoardGameArtist> Artists { get; set; }
        public List<BoardGameDeisgner> Designers { get; set; }
        public List<BoardGamePublisher> Publishers { get; set; }
        public List<UserRating> Ratings { get; set; }

        //TODO: statistics? (xmlapi stats=1)
    }
}
