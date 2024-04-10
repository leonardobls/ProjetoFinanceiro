using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace BuscadorFinanceiro.Models
{
    public class PagamentoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }

        public string? ClientId { get; set; }

        public string? CodigoProduto { get; set; }

        public double? Valor { get; set; }

        public string? Pago { get; set; }

        [Name("Data")]
        public string DataString { get; set; }

        public DateTime? Data
        {
            get
            {
                if (DateTime.TryParseExact(DataString, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var resultado))
                {
                    return resultado.ToUniversalTime();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                DataString = value?.ToUniversalTime().ToString("ddMMyyyy");
            }
        }
    }
}