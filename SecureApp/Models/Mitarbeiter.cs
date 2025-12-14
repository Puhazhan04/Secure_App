using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureApp.Models
{
    public class Mitarbeiter
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vorname ist erforderlich.")]
        public string? Vorname { get; set; }

        [Required(ErrorMessage = "Nachname ist erforderlich.")]
        public string? Nachname { get; set; }

        public string? Strasse { get; set; }
        public string? Nr { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "PLZ darf nur Zahlen enthalten.")]
        public string? PLZ { get; set; }

        public string? Ort { get; set; }
        public string? Land { get; set; }

        [Required(ErrorMessage = "E-Mail ist erforderlich.")]
        [EmailAddress(ErrorMessage = "Bitte eine gültige E-Mail-Adresse eingeben.")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Telefon darf nur Zahlen enthalten.")]
        public string? Telefon { get; set; }

        public string? Funktion { get; set; }

        public string? Passwort { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Passwortbestätigung ist erforderlich.")]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein.")]
        public string? PasswortBestätigung { get; set; }

        public ICollection<Abwesenheit>? Abwesenheiten { get; set; }

    }
}
