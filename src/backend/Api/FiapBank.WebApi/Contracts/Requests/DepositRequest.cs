namespace FiapBank.WebApi.Contracts.Requests
{
    /// <summary>
    /// Dados para realizar um depósito.
    /// </summary>
    public class DepositRequest
    {
        /// <summary>
        /// Valor do depósito.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
