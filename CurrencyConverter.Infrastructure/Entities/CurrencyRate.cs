using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Infrastructure.Entities
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = default!;
        public string ToCurrency { get; set; } = default!;
        public decimal Rate { get; set; }
        public DateTime FetchedAt { get; set; }
    }
}
