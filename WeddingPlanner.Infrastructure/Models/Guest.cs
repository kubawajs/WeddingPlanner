﻿using System;

namespace WeddingPlanner.Infrastructure.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsChild { get; set; }
        public bool HasPair { get; set; }
        public DateTime BirthDate { get; set; }
    }
}