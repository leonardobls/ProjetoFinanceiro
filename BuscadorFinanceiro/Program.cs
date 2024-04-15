using BuscadorFinanceiro.Data;
using BuscadorFinanceiro.Controllers;
using BuscadorFinanceiro.Models;

List<dynamic> clientes = FileReaderController.GetContent("Arquivos/clientes.txt");
List<dynamic> pagamentos = FileReaderController.GetContent("Arquivos/pagamentos.txt");
Tratador t = new Tratador();
t.trataClientes(clientes);
t.trataPagamentos(pagamentos);


AcoesController acoes = new AcoesController();

AppDbContext context = new AppDbContext();

List<ClienteModel> newClients = context.Cliente.ToList();
List<PagamentoModel> newPagamentos = context.Pagamento.ToList();


Console.WriteLine("Bem vindo ao sitema de consulta de dívidas da sua empresa!");
while (true)
{
    Console.WriteLine(@" Escolha a operação a ser realizada:
                1-Busca pagamentos de um cliente específico
                2-Busca dívidas maiores a partir de um valor especificado
                3-Relatório completo ordenado por data
                0-Sair");
    string? caso = Console.ReadLine();
    switch (caso)
    {
        case "1":
            Console.WriteLine("Você deseja:\n0-Consultar apenas as dívidas do cliente\n1-Consultar as dívidas e pagamentos");
            string result = Console.ReadLine();
            Console.WriteLine(@"Digite o nome do cliente a ser consultado: ");
            string nome = Console.ReadLine();
            acoes.ProcuraPeloNome(newClients, newPagamentos, result, nome);
            break;
        case "2":
            Console.WriteLine("Digite o valor mínimo da dívida (se quiser visualizar todas as dívidas no sistema, digite 0): ");
            string valor = Console.ReadLine();
            acoes.ProcuraPeloValorSuperior(newClients, newPagamentos, valor);
            break;
        case "3":
            Console.WriteLine("Você deseja:\n0-Consultar apenas as dívidas\n1-Consultar apenas os pagamentos");
            string resultado = Console.ReadLine();
            acoes.RelatorioCompleto(newClients, newPagamentos, resultado);
            break;
        case "0":
            Console.WriteLine("Saindo!");
            return;
        default:
            Console.WriteLine("Valor inválido");
            break;
    }
}
