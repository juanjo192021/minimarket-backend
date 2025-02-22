﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tienda_project_backend.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string Celular { get; set; } = null!;

    public string? Genero { get; set; }

    public string IdDistrito { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public bool Confirmado { get; set; }

    public bool Restablecer { get; set; }

    public string Token { get; set; } = null!;

    public bool Estado { get; set; }

    public DateTime FechaRegistro { get; set; }

    [JsonIgnore]
    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();

    public virtual Distrito Distrito { get; set; } = null!;
}
