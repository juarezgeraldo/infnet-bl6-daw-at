﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace infnet_bl6_daw_at.Domain.Entities
{
    public class Usuario
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string SenhaUsuario { get; set; }
        [Required]
        public string EmailUsuario { get; set; }
        public string TelefoneUsuario { get; set; }

    }
}
