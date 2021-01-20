using FluentValidation.Results;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Shared;
using System.Collections.Generic;
using System.Linq;

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
            Mensagens = new EnumModel[]
            {
                new EnumModel() {
                    Codigo = mensagem.GetEnumValue(),
                    Nome = mensagem.GetEnumName(),
                    Descricao = mensagem.GetEnumDescription()
                }
            };
        }

        public BaseModel(bool sucesso, IList<ValidationFailure> errosValidacao)
        {
            Sucesso = sucesso;
            Mensagens = new EnumModel[] { };
            errosValidacao
                .ToList().ForEach(e => Mensagens.ToList().Add(new EnumModel()
                {
                    Codigo = 99,
                    Nome = e.PropertyName,
                    Descricao = e.ErrorMessage
                }));
        }

        public BaseModel(bool sucesso, EMensagens mensagem, T dados) : this(sucesso, mensagem) => Dados = dados;

        #endregion

        #region Propriedades
        public T Dados { get; set; }

        public EnumModel[] Mensagens { get; set; }
        public IEnumerable<string> ErrosValidacao { get; set; }
        public bool Sucesso { get; set; }
        #endregion


    }
}
