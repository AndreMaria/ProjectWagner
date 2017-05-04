using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWagner.Models
{
    public class Transporte
    {
        public DateTime DataHora { get; set; }
        public String Ordem { get; set; }
        public String Linha { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public decimal Velocidade { get; set; }
    }
}