using BuscadorFinanceiro.Controllers;
using BuscadorFinanceiro.Data;
using BuscadorFinanceiro.Models;

namespace BuscadorFinanceiro.Tests
{
    [TestFixture]
    public class Tests
    {
        private AcoesController _financialSearch;
        private AppDbContext _context;
        [SetUp]
        public void Setup()
        {
            _financialSearch = new AcoesController();
            _context = new AppDbContext();
        }

        [Test]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloNome(newClients, newPagamentos, "0", "cliente 3");

            List<string> resultadoEsperado = new List<string> { "Data:18/01/2014 02:00:00Valor:10Pagamentoemdívida", "Data:10/01/2014 02:00:00Valor:8Pagamentoemdívida" };

            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }
    }
}

