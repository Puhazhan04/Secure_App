using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models
{
    public class Abwesenheit
    {
        public int Id { get; set; }
        [Required]
        public int MitarbeiterId { get; set; }
        public Mitarbeiter? Mitarbeiter { get; set; }
        [Required(ErrorMessage = "Von-Datum ist erforderlich.")]
        public DateTime Von { get; set; }
        [Required(ErrorMessage = "Bis-Datum ist erforderlich.")]
        public DateTime Bis { get; set; }
        [Required(ErrorMessage = "Grund ist erforderlich.")]
        public string? Grund { get; set; }
        public string? Bemerkung { get; set; }




    }
}
