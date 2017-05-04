using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWagner.Models
{
    public class Result
    {
        public List<Models.Transporte> Transportes { get;set;}
        public String Mensagem { get; set; }
        public Boolean Status { get; set; }
    }
}