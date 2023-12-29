using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{

    public class Employee
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FullName { get; set; }

        [Required]
        public string? Birthdate { get; set; }

        [Required]
        public string? VacancyType { get; set; }

        [Required]
        public string? Phone { get; set; }

        public string? Institution { get; set; }

        [Required]
        public bool? IsStudent { get; set; }

        public string? StudyForm { get; set; }

        [Required]
        public string? EnglishLevel { get; set; }

        public string? AppProficiency { get; set; }

        [Required]
        public string? Media { get; set; }

        [Required]
        public bool? NightShift { get; set; }

        [Required]
        public string? Resume { get; set; }

        [Required]
        public string? Source { get; set; }

        [Required]
        public DateTime RegisterTime { get; set; } = DateTime.UtcNow;

        [Required]
        public Status Status { get; set; } = Status.Enrolled;
    }

    public enum Status
    {
        Enrolled,
        Accepted,
        Rejected,
    }
}
