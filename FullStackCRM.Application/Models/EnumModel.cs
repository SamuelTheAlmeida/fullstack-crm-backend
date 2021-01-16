using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class EnumModel
    {
        #region Construtores
        public EnumModel()
        {

        }

        public EnumModel(Enum enumItem)
        {
            this.Codigo = enumItem.GetEnumValue();
            this.Nome = enumItem.GetEnumName();
            this.Descricao = enumItem.GetEnumDescription();
        }
        #endregion

        #region Propriedades
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }
        #endregion
    }
}
