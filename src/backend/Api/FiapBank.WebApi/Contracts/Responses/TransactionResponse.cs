using FiapBank.Domain.Enums;

namespace FiapBank.WebApi.Contracts.Responses
{
    /// <summary>
    /// Dados da transação realizada pelo cliente.
    /// </summary>
    public class TransactionResponse
    {
        /// <summary>
        /// Id da transação.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Data da transação.
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Valor da transação.
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Tipo da transação que pode ser: Deposit e Withdraw
        /// </summary>
        public TransactionType Type { get; set; }
    }
}
