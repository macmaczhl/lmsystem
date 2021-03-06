﻿using Application.Core.Data;

namespace LMPlatform.Models
{
    public class StudentLabMark : ModelBase
    {
        public int LabId { get; set; }

        public int StudentId { get; set; }

        public string Mark { get; set; }

        public Labs Lab { get; set; }

        public Student Student { get; set; }
    }
}