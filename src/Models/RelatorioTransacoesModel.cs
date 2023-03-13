using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myfinance_web_netcore.Models
{
    public class RelatorioTransacoesModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public List<TransacaoModel> Transacao { get; set; }
        public int TransacaoReceitas { get; set; }
        public int TransacaoDespesas { get; set; }

        public RelatorioTransacoesModel()
        {
            Transacao = new List<TransacaoModel>();
        }
    }
}