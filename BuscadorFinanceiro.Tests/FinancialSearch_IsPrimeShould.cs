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
        public void ProcuraPeloNomeTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloNome(newClients, newPagamentos, "0", "cliente 3");

            List<string> resultadoEsperado = new List<string> { "Data: 18/01/2014\tValor: 10\tPagamento em dívida",
            "Data: 10/01/2014\tValor: 8\tPagamento em dívida" };
            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void ProcuraPeloNomeSemDividasTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloNome(newClients, newPagamentos, "0", "cliente sauna");

            List<string> resultadoEsperado = new List<string> { "Cliente sem dívidas" };
            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void ProcuraPeloNomeNaoEncontradoTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloNome(newClients, newPagamentos, "0", "cliente 89");

            List<string> resultadoEsperado = new List<string> { "Cliente não encontrado na base de dados!" };
            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void ProcuraPeloNomeTodosPagamentosTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloNome(newClients, newPagamentos, "1", "cliente 29");

            List<string> resultadoEsperado = new List<string> { "Data: 14/04/2014\tValor: 200\tPagamento realizado",
            "Data: 05/03/2014\tValor: 400\tPagamento realizado", "Data: 08/08/2014\tValor: 10\tPagamento realizado",
            "Data: 16/01/2014\tValor: 50\tPagamento em dívida", "Data: 05/02/2014\tValor: 450\tPagamento em dívida",
            "Data: 19/02/2014\tValor: 150\tPagamento em dívida", "Data: 05/03/2014\tValor: 50\tPagamento realizado",
            "Data: 05/02/2014\tValor: 130\tPagamento em dívida" };
            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void ProcuraPeloValorSuperiorTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloValorSuperior(newClients, newPagamentos, "350");

            List<string> resultadoEsperado = new List<string> { "Cliente: cliente 13\tValor: 500\tData: 12/07/2014",
            "Cliente: cliente 29\tValor: 450\tData: 05/02/2014",
            "Cliente: cliente 41\tValor: 390\tData: 02/02/2015" };

            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void ProcuraPeloValorSuperiorErrorTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            List<string> result = _financialSearch.ProcuraPeloValorSuperior(newClients, newPagamentos, "cinquenta");

            List<string> resultadoEsperado = new List<string> { "erro" };

            Assert.That(result, Is.EqualTo(resultadoEsperado), "Não foi");
        }

        [Test]
        public void RelatorioCompletoTest()
        {
            List<ClienteModel> newClients = _context.Cliente.ToList();
            List<PagamentoModel> newPagamentos = _context.Pagamento.ToList();

            int result = _financialSearch.RelatorioCompleto(newClients, newPagamentos, "0");

            List<string> resultadoEsperado = new List<string> {
                "Dia: 10/01/2014 02:00:00        Valor total das dívidas 8,00",
                "Dia: 15/01/2014 02:00:00        Valor total das dívidas 806,01",
                "Dia: 16/01/2014 02:00:00        Valor total das dívidas 403,99",
                "Dia: 18/01/2014 02:00:00        Valor total das dívidas 110,00",
                "Dia: 20/01/2014 02:00:00        Valor total das dívidas 16,00",
                "Dia: 21/01/2014 02:00:00        Valor total das dívidas 105,00",
                "Dia: 23/01/2014 02:00:00        Valor total das dívidas 9,00",
                "Dia: 25/01/2014 02:00:00        Valor total das dívidas 63,00",
                "Dia: 27/01/2014 02:00:00        Valor total das dívidas 7,00",
                "Dia: 28/01/2014 02:00:00        Valor total das dívidas 4,01",
                "Dia: 31/01/2014 02:00:00        Valor total das dívidas 2,00",
                "Dia: 03/02/2014 02:00:00        Valor total das dívidas 7,00",
                "Dia: 04/02/2014 02:00:00        Valor total das dívidas 15,66",
                "Dia: 05/02/2014 02:00:00        Valor total das dívidas 580,00",
                "Dia: 06/02/2014 02:00:00        Valor total das dívidas 490,67",
                "Dia: 07/02/2014 02:00:00        Valor total das dívidas 53,00",
                "Dia: 08/02/2014 02:00:00        Valor total das dívidas 102,00",
                "Dia: 10/02/2014 02:00:00        Valor total das dívidas 14,00",
                "Dia: 11/02/2014 02:00:00        Valor total das dívidas 392,00",
                "Dia: 13/02/2014 02:00:00        Valor total das dívidas 133,67",
                "Dia: 14/02/2014 02:00:00        Valor total das dívidas 53,00",
                "Dia: 15/02/2014 02:00:00        Valor total das dívidas 133,00",
                "Dia: 17/02/2014 03:00:00        Valor total das dívidas 2,00",
                "Dia: 18/02/2014 03:00:00        Valor total das dívidas 104,00",
                "Dia: 19/02/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 20/02/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 22/02/2014 03:00:00        Valor total das dívidas 134,02",
                "Dia: 24/02/2014 03:00:00        Valor total das dívidas 2,00",
                "Dia: 26/02/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 27/02/2014 03:00:00        Valor total das dívidas 2,00",
                "Dia: 28/02/2014 03:00:00        Valor total das dívidas 120,00",
                "Dia: 01/03/2014 03:00:00        Valor total das dívidas 16,00",
                "Dia: 04/03/2014 03:00:00        Valor total das dívidas 156,00",
                "Dia: 05/03/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 06/03/2014 03:00:00        Valor total das dívidas 284,34",
                "Dia: 07/03/2014 03:00:00        Valor total das dívidas 29,00",
                "Dia: 08/03/2014 03:00:00        Valor total das dívidas 287,99",
                "Dia: 13/03/2014 03:00:00        Valor total das dívidas 90,67",
                "Dia: 14/03/2014 03:00:00        Valor total das dívidas 58,00",
                "Dia: 15/03/2014 03:00:00        Valor total das dívidas 120,00",
                "Dia: 17/03/2014 03:00:00        Valor total das dívidas 17,00",
                "Dia: 20/03/2014 03:00:00        Valor total das dívidas 6,00",
                "Dia: 22/03/2014 03:00:00        Valor total das dívidas 50,00",
                "Dia: 03/04/2014 03:00:00        Valor total das dívidas 302,67",
                "Dia: 04/04/2014 03:00:00        Valor total das dívidas 314,00",
                "Dia: 05/04/2014 03:00:00        Valor total das dívidas 188,02",
                "Dia: 07/04/2014 03:00:00        Valor total das dívidas 92,67",
                "Dia: 08/04/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 10/04/2014 03:00:00        Valor total das dívidas 92,67",
                "Dia: 12/04/2014 03:00:00        Valor total das dívidas 58,00",
                "Dia: 14/04/2014 03:00:00        Valor total das dívidas 6,00",
                "Dia: 15/04/2014 03:00:00        Valor total das dívidas 204,00",
                "Dia: 19/04/2014 03:00:00        Valor total das dívidas 66,00",
                "Dia: 24/04/2014 03:00:00        Valor total das dívidas 9,00",
                "Dia: 29/04/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 03/05/2014 03:00:00        Valor total das dívidas 176,00",
                "Dia: 05/05/2014 03:00:00        Valor total das dívidas 209,66",
                "Dia: 08/05/2014 03:00:00        Valor total das dívidas 171,32",
                "Dia: 09/05/2014 03:00:00        Valor total das dívidas 62,00",
                "Dia: 12/05/2014 03:00:00        Valor total das dívidas 170,00",
                "Dia: 16/05/2014 03:00:00        Valor total das dívidas 33,00",
                "Dia: 19/05/2014 03:00:00        Valor total das dívidas 10,00",
                "Dia: 24/05/2014 03:00:00        Valor total das dívidas 24,00",
                "Dia: 31/05/2014 03:00:00        Valor total das dívidas 20,00",
                "Dia: 02/06/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 06/06/2014 03:00:00        Valor total das dívidas 142,67",
                "Dia: 07/06/2014 03:00:00        Valor total das dívidas 453,98",
                "Dia: 10/06/2014 03:00:00        Valor total das dívidas 22,00",
                "Dia: 11/06/2014 03:00:00        Valor total das dívidas 50,00",
                "Dia: 21/06/2014 03:00:00        Valor total das dívidas 270,00",
                "Dia: 26/06/2014 03:00:00        Valor total das dívidas 88,67",
                "Dia: 30/06/2014 03:00:00        Valor total das dívidas 196,67",
                "Dia: 02/07/2014 03:00:00        Valor total das dívidas 137,00",
                "Dia: 03/07/2014 03:00:00        Valor total das dívidas 296,67",
                "Dia: 05/07/2014 03:00:00        Valor total das dívidas 126,00",
                "Dia: 07/07/2014 03:00:00        Valor total das dívidas 130,00",
                "Dia: 09/07/2014 03:00:00        Valor total das dívidas 65,00",
                "Dia: 10/07/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 12/07/2014 03:00:00        Valor total das dívidas 500,00",
                "Dia: 14/07/2014 03:00:00        Valor total das dívidas 142,00",
                "Dia: 31/07/2014 03:00:00        Valor total das dívidas 86,67",
                "Dia: 02/08/2014 03:00:00        Valor total das dívidas 400,00",
                "Dia: 04/08/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 06/08/2014 03:00:00        Valor total das dívidas 310,00",
                "Dia: 07/08/2014 03:00:00        Valor total das dívidas 2,00",
                "Dia: 11/08/2014 03:00:00        Valor total das dívidas 32,00",
                "Dia: 13/08/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 20/08/2014 03:00:00        Valor total das dívidas 50,00",
                "Dia: 26/08/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 27/08/2014 03:00:00        Valor total das dívidas 50,00",
                "Dia: 01/09/2014 03:00:00        Valor total das dívidas 120,00",
                "Dia: 04/09/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 08/09/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 09/09/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 11/09/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 15/09/2014 03:00:00        Valor total das dívidas 125,00",
                "Dia: 20/09/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 29/09/2014 03:00:00        Valor total das dívidas 50,00",
                "Dia: 03/10/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 06/10/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 09/10/2014 03:00:00        Valor total das dívidas 370,00",
                "Dia: 11/10/2014 03:00:00        Valor total das dívidas 100,00",
                "Dia: 13/10/2014 03:00:00        Valor total das dívidas 154,00",
                "Dia: 15/10/2014 03:00:00        Valor total das dívidas 150,00",
                "Dia: 30/10/2014 02:00:00        Valor total das dívidas 100,00",
                "Dia: 01/11/2014 02:00:00        Valor total das dívidas 200,00",
                "Dia: 03/11/2014 02:00:00        Valor total das dívidas 100,00",
                "Dia: 08/11/2014 02:00:00        Valor total das dívidas 50,00",
                "Dia: 10/11/2014 02:00:00        Valor total das dívidas 250,00",
                "Dia: 13/11/2014 02:00:00        Valor total das dívidas 100,00",
                "Dia: 20/11/2014 02:00:00        Valor total das dívidas 100,00",
                "Dia: 25/11/2014 02:00:00        Valor total das dívidas 7,00",
                "Dia: 29/11/2014 02:00:00        Valor total das dívidas 50,00",
                "Dia: 02/12/2014 02:00:00        Valor total das dívidas 150,00",
                "Dia: 04/12/2014 02:00:00        Valor total das dívidas 75,00",
                "Dia: 05/12/2014 02:00:00        Valor total das dívidas 20,00",
                "Dia: 06/12/2014 02:00:00        Valor total das dívidas 33,00",
                "Dia: 08/12/2014 02:00:00        Valor total das dívidas 200,00",
                "Dia: 12/12/2014 02:00:00        Valor total das dívidas 100,00",
                "Dia: 05/01/2015 02:00:00        Valor total das dívidas 200,00",
                "Dia: 06/01/2015 02:00:00        Valor total das dívidas 200,00",
                "Dia: 10/01/2015 02:00:00        Valor total das dívidas 100,00",
                "Dia: 14/01/2015 02:00:00        Valor total das dívidas 155,00",
                "Dia: 15/01/2015 02:00:00        Valor total das dívidas 107,00",
                "Dia: 21/01/2015 02:00:00        Valor total das dívidas 70,00",
                "Dia: 23/01/2015 02:00:00        Valor total das dívidas 61,00",
                "Dia: 28/01/2015 02:00:00        Valor total das dívidas 100,00",
                "Dia: 31/01/2015 02:00:00        Valor total das dívidas 150,00",
                "Dia: 02/02/2015 02:00:00        Valor total das dívidas 600,02",
                "Dia: 05/02/2015 02:00:00        Valor total das dívidas 50,00",
                "Dia: 09/02/2015 02:00:00        Valor total das dívidas 106,00",
                "Dia: 14/02/2015 02:00:00        Valor total das dívidas 50,00",
                "Dia: 19/02/2015 02:00:00        Valor total das dívidas 50,00",
                "Dia: 23/02/2015 03:00:00        Valor total das dívidas 160,00",
                "Dia: 26/02/2015 03:00:00        Valor total das dívidas 202,00",
                "Dia: 28/02/2015 03:00:00        Valor total das dívidas 27,00",
                "Dia: 02/03/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 05/03/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 07/03/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 12/03/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 14/03/2015 03:00:00        Valor total das dívidas 238,00",
                "Dia: 19/03/2015 03:00:00        Valor total das dívidas 106,00",
                "Dia: 21/03/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 25/03/2015 03:00:00        Valor total das dívidas 150,00",
                "Dia: 04/04/2015 03:00:00        Valor total das dívidas 93,00",
                "Dia: 06/04/2015 03:00:00        Valor total das dívidas 106,00",
                "Dia: 08/04/2015 03:00:00        Valor total das dívidas 100,00",
                "Dia: 09/04/2015 03:00:00        Valor total das dívidas 100,00",
            };

            Assert.That(result, Is.EqualTo(resultadoEsperado.Count()), "Não foi");
        }
    }
}

