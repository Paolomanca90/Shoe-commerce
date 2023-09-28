using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace U4_W4_D3.Models
{
    public class Scarpa
    {
        public int IdProdotto { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public decimal Prezzo { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Descrizione { get; set; }

        [Display(Name = "Immagine")]
        public string Image { get; set; }

        [Display(Name = "Immagine 2")]
        public string Image1 { get; set; }

        [Display(Name = "Immagine 3")]
        public string Image2 { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public bool Disponibile { get; set; }
    }

    public class Utente
    {
        public int IdUtente { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}