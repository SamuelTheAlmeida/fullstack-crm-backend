﻿using FullStackCRM.Domain.Enums;
using System;

namespace FullStackCRM.Domain.Entities
{
    public class Usuario
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EPerfis Perfil { get; set; }
    }
}
