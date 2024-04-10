using System.Globalization;
using BuscadorFinanceiro.Models;

namespace BuscadorFinanceiro.Controllers
{
    public class AcoesController
    {
        public List<string> ProcuraPeloNome(List<ClienteModel> clientes, List<PagamentoModel> pagamentos, string result, string nome)
        {
            List<string> response = new List<string> { "teste" };
            bool show = false;
            if (result == "1") show = true;

            ClienteModel? cliente = clientes.Where(cliente => cliente.Nome == nome.ToLower()).FirstOrDefault();

            if (cliente != null)
            {
                var pagamentosEncontrados = pagamentos.Where(pagamento => pagamento.ClientId == cliente.Id).ToList();

                if (pagamentosEncontrados.Count > 0)
                {
                    bool entered = false;
                    response.Clear();
                    foreach (var p in pagamentosEncontrados)
                    {
                        if ((show == true && p.Pago == "t") || p.Pago == "f")
                        {
                            result = p.Pago == "t" ? "realizado" : "emdívida";

                            response.Add($"Data:{p.Data}Valor:{p.Valor}Pagamento{result}");
                            Console.WriteLine($"Data:{p.Data}Valor:{p.Valor}Pagamento{result}");
                            entered = true;
                        }
                    }

                    if (entered == false)
                        Console.WriteLine("Cliente sem dívidas");
                }
                else
                {
                    Console.WriteLine("Nenhum valor pendente!");
                }
            }
            else
            {
                Console.WriteLine("Cliente não encontrado na base de dados!");
            }

            return response;

        }

        public void ProcuraPeloValorSuperior(List<ClienteModel> clientes, List<PagamentoModel> pagamentos)
        {
            Console.WriteLine("Digite o valor mínimo da dívida (se quiser visualizar todas as dívidas no sistema, digite 0): ");
            string valor = Console.ReadLine();

            var pagamentosEncontrados = pagamentos.Where(pagamento => pagamento.Valor >= double.Parse(valor) && pagamento.Pago == "f").ToList();

            if (pagamentosEncontrados.Count > 0)
            {
                foreach (var p in pagamentosEncontrados)
                {
                    var clienteEncontrado = clientes.Where(c => c.Id == p.Id).FirstOrDefault();

                    if (clienteEncontrado != null)
                    {
                        Console.WriteLine($"Cliente: {clienteEncontrado.Nome}\tValor: {p.Valor}\tData: {p.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"Cliente: Não identificado\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
                    }
                }
                Console.WriteLine($"Há um total de {pagamentosEncontrados.Count} dívidas acima desse valor");
            }
        }

        public void RelatorioCompleto(List<ClienteModel> clientes, List<PagamentoModel> pagamentos)
        {

            Console.WriteLine("Você deseja:\n0-Consultar apenas as dívidas\n1-Consultar apenas os pagamentos");
            string result = Console.ReadLine();
            bool pagos = false;
            if (result == "1") pagos = true;


            var grupoPorDia = pagamentos.OrderBy(pagamento => pagamento.Data).GroupBy(pagamento => pagamento.Data).ToList();

            foreach (var grupo in grupoPorDia)
            {
                double? soma = 0;

                foreach (var dia in grupo)
                {
                    if (dia.Pago == "f" && pagos == false)
                    {
                        soma += dia.Valor;
                    }
                    else if (dia.Pago == "t" && pagos == true)
                    {
                        soma += dia.Valor;
                    }
                }
                if (pagos == false && soma > 0)
                    Console.WriteLine($"\tDia: {grupo.First().Data}\tValor total das dívidas {soma.Value.ToString("N2")}");
                else if (pagos == true && soma > 0)
                    Console.WriteLine($"\tDia: {grupo.First().Data}\tValor total dos pagamentos {soma.Value.ToString("N2")}");


            }

            Console.WriteLine("\n");
        }
    }
}