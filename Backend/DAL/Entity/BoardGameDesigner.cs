﻿using Riganti.Utils.Infrastructure.Core;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class BoardGameDeisgner : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
