﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.RESTful.Models
{
    [Table("Cupones_Clientes")]
    public class Cupon_ClienteModel
    {
        [ForeignKey("Id_Cupon")]
        public int Id_Cupon { get; set; }
        [Key]
        public string? NroCupon { get; set; }
        public DateTime? FechaAsignado { get; set; }
        public string CodCliente {  get; set; }
    }
}
