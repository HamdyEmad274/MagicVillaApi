﻿namespace MagicVillaApi.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}