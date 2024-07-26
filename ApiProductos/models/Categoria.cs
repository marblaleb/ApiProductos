﻿using System.Text.Json.Serialization;

namespace ApiProductos.models
{
    
    public partial class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Producto> Productos { get; set; }
    }
}

