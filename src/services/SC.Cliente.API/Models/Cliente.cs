﻿using SC.Core.DomainObjects;
using System;

namespace SC.Clientes.API.Models
{
    public class Cliente : Entity,IAggregateRoot
    {
        public string Nome {  get; private set; }
        public Email Email {  get; private set; }
        public Cpf Cpf {  get; private set; }
        public bool Excluido { get; private set; }

        public Endereco Endereco {  get; private set; }

        //EF RELACIONAMENTO PORQUE ELE HERDA DA CLAS 
        protected Cliente()
        {

        }
        //ctor sigla para criar um construtor
        public Cliente(Guid id,string nome,string email,string cpf, bool excluido)
        {
            Id = id;
            Nome = nome;
            Email = new Email(email);//validar o exception da DomainObjects
            Cpf = new Cpf(cpf);
            Excluido = false;
            //Endereco = endereco;

        }
        public void TrocarEmail(string email)
        {
            Email = new Email(email);
        }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }


    }
}
