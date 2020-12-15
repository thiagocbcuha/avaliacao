using System.ComponentModel;
using System.Net;
using System.Net.Http;

namespace CandidateTesting.ThiagoCardosoBarbosaCunha.DBCCompany.Model
{
    public class FileLine
    {
        [Description("Nº Clientes")]
        public int CustomerCount { get; set; }

        [Description("Nº Vendedores")]
        public int SalesmanCount { get; set; }

        [Description("Venda Mais Cara")]
        public int MostExpensive { get; set; }

        [Description("Pior Vendedor")]
        public string WorseSalesman { get; set; }
    }
}