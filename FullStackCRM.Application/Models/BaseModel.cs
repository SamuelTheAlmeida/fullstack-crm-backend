using FullStackCRM.Domain.Enums;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class BaseModel<T> where T : class
    {
        #region Construtores
        public BaseModel()
        {

        }

        public BaseModel(bool sucesso, EMensagens mensagem)
        {
            Sucesso = sucesso;
            Mensagem = new EnumModel
            {
                Codigo = mensagem.GetEnumValue(),
                Nome = mensagem.GetEnumName(),
                Descricao = mensagem.GetEnumDescription()
            };
        }

        public BaseModel(bool sucesso, EMensagens mensagem, T dados) : this(sucesso, mensagem) => Dados = dados;

        public BaseModel(bool sucesso, EMensagens mensagem, T dados, ValidationResult[] resultadoValidacao) : this(sucesso, mensagem, dados) => ResultadoValidacao = resultadoValidacao;
        #endregion

        #region Propriedades
        public T Dados { get; set; }

        public EnumModel Mensagem { get; set; }

        public ValidationResult[] ResultadoValidacao { get; set; }

        public bool Sucesso { get; set; }
        #endregion


    }
}
