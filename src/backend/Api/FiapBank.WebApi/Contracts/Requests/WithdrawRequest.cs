namespace FiapBank.WebApi.Contracts.Requests
{
    /// <summary>
    /// Dados para realizar um saque.
    /// </summary>
    public class WithdrawRequest
    {
        /// <summary>
        /// Valor do saque.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
