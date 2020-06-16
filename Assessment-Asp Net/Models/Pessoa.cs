﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_Asp_Net.Models
{
    public class Pessoa
    {
        //private Pessoa a;

        //public Pessoa(Pessoa a)
        //{
        //    this.a = a;
        //}

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        [DataType(DataType.Date)]
        public DateTime Aniversario { get; set; }
        //public double Dias { get; set; }

    }
}
