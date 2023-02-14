using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class Venta
    {
        private long id;
        private string comentarios;
        private long idUsuario;

        public long Id { get => id; set => id = value; }
        public string Comentarios { get => comentarios; set => comentarios = value; }
        public long IdUsuario { get => idUsuario; set => idUsuario = value; }

    }
}
