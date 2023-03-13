using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_netcore.Models
{
    public class TransacaoModel
    {
        public int? Id { get; set; }
        public DateTime Data {get; set;}
        public decimal Valor {get; set;}
        public string? Historico { get; set; }
        public int PlanoContaId {get; set;}
        public PlanoContaModel ItemPlanoConta {get; set;}
        public IEnumerable<SelectListItem>? PlanoContas {get; set;}
        public String PlanoContaTipo {get; set;}
    }
}