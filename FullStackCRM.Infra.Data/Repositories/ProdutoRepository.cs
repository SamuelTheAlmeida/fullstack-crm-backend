using FullStackCRM.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Infra.Data.Repositories
{
    public class ProdutoRepository : BaseDataAccess, IProdutoRepository
    {
        public void Inserir()
        {
            var result = ExecuteScalar("select * from produtos", new List<System.Data.SqlClient.SqlParameter>());  
        }
    }
}
