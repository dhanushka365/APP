﻿namespace APP.Models.Domain
{
    public class Grade
    {
        public int GradeId { get; set; }
        public string? GradeValue { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
