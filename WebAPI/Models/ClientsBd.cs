using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PersonalHealthManager.WebAPI.Models
{
    public class ClientsBd
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Client_ID { get; set; } = null;

        [Required(ErrorMessage = "La edad es un campo obligatorio")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es un campo obligatorio")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "La contraseña es un campo obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Los apellidos son un campo obligatorio")]
        public string Last_Names { get; set; }

        [Required(ErrorMessage = "El correo electrónico es un campo obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string E_Mail { get; set; }

        [Required(ErrorMessage = "La altura es un campo obligatorio")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "El peso es un campo obligatorio")]
        public decimal? Weigth { get; set; }

        public decimal? BMI { get; set; }
        public decimal? GEB { get; set; }
        public decimal? ETA { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Update_Date { get; set; }
    }
}