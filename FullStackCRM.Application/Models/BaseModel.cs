using FluentValidation.Results;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public BaseModel(bool sucesso, IList<ValidationFailure> errosValidacao)
        {
            Sucesso = sucesso;
            ErrosValidacao = errosValidacao.Select(x => x.ErrorMessage);
        }

        public BaseModel(bool sucesso, EMensagens mensagem, T dados) : this(sucesso, mensagem) => Dados = dados;

        #endregion

        #region Propriedades
        public T Dados { get; set; }

        public EnumModel Mensagem { get; set; }
        public IEnumerable<string> ErrosValidacao { get; set; }
        public bool Sucesso { get; set; }
        #endregion


    }
}
