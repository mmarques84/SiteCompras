﻿using SC.Core.Data;
using SC.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.Catalogo.API.Models
{
    public class Produto: Entity, IAggregateRoot
    {
        //IAggregateRoot estudar um pouco sobre essa agregação
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
