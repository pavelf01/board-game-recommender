using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class BoardGame : BaseEntity<int>
    {
        [Key]
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
        [InverseProperty("Games")]
        public List<BoardGameCategory> Categories { get; set; }
        [InverseProperty("Games")]
        public List<BoardGameArtist> Artists { get; set; }
        [InverseProperty("Games")]
        public List<BoardGameDesigner> Designers { get; set; }
        [InverseProperty("Games")]
        public List<BoardGamePublisher> Publishers { get; set; }
        public List<UserRating> Ratings { get; set; }
        public int BGGId { get; set; }

        //TODO: statistics? (xmlapi stats=1)
    }
}
