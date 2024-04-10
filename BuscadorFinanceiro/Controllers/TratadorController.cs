using System.Globalization;
using BuscadorFinanceiro.Data;
using BuscadorFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscadorFinanceiro.Controllers
{
    internal class Tratador
    {

        AppDbContext context = new AppDbContext();

        public async void trataClientes(List<dynamic> clientes)
        {

            List<ClienteModel> newClients = new List<ClienteModel>();

            foreach (var client in clientes)
            {

                var clientToAdd = new ClienteModel();

                DateTime? data;

                if (DateTime.TryParseExact(client.Data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    data = result;
                }
                else
                {
                    data = null;
                }


                clientToAdd.Id = client.Id;
                clientToAdd.Data = data;
                clientToAdd.Valor = client.Valor;
                clientToAdd.Cpf = client.Cpf;
                clientToAdd.Nome = client.Nome.ToString().ToLower();

                // Task<bool> response = AddCliente(clientToAdd);
            }


        }

        public void trataPagamentos(List<dynamic> pagamentos)
        {
            List<PagamentoModel> newPagamentos = new List<PagamentoModel>();

            foreach (var pagamento in pagamentos)
            {

                if (pagamento.Data.Length < 8)
                {
                    string ano = pagamento.Data.Substring(pagamento.Data.Length - 4);
                    string mes = pagamento.Data.Substring(pagamento.Data.Length - 6, 2);
                    string dia = pagamento.Data.Length == 7 ? pagamento.Data.Substring(0, 1) : pagamento.Data.Substring(0, 2);
                    if (dia.Length == 1)
                    {
                        dia = "0" + dia;
                        var teste = 0;
                    }
                    pagamento.Data = dia + mes + ano;
                }

                DateTime? data;

                if (DateTime.TryParseExact(pagamento.Data, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    data = result;
                }
                else
                {
                    data = null;
                }

                var pagamentoToAdd = new PagamentoModel()
                {
                    ClientId = pagamento.Id,
                    Data = data,
                    CodigoProduto = pagamento.CodigoProduto,
                    Valor = !string.IsNullOrEmpty(pagamento.Valor) ? Math.Round(double.Parse(pagamento.Valor, CultureInfo.InvariantCulture), 2) : (double?)null,
                    Pago = pagamento.Pago,
                };


                // Task<bool> response = AddPagamento(pagamentoToAdd);
            }

        }

        public async Task<bool> AddCliente(ClienteModel model)
        {

            context.Cliente.Add(model);
            context.SaveChanges();

            return true;
        }

        public async Task<bool> AddPagamento(PagamentoModel model)
        {
            context.Pagamento.Add(model);
            context.SaveChanges();

            return true;
        }
    }
}