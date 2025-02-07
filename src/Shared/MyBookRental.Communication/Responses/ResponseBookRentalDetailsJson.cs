using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookRental.Communication.Responses
{
    public class ResponseBookRentalDetailsJson
    {
        public long RentalId { get; set; }
        public string UserName { get; set; } = string.Empty;    // Nome do usuário que fez a locação
        public string BookTitle { get; set; } = string.Empty;   // Título do livro alugado
        public DateTime RentalDate { get; set; }               // Data da locação
        public DateTime DueDate { get; set; }                  // Data de devolução prevista
        public DateTime? ReturnDate { get; set; }              // Data de devolução real (null se não devolvido)
        public string Status { get; set; } = "Pendente";       // Status da locação: Pendente, Devolvido, Renovado
        public int RenewalsCount { get; set; } // Número de renovações do empréstimo
    }
}
