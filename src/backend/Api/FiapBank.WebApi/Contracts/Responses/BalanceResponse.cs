namespace FiapBank.WebApi.Contracts.Responses
{
    /// <summary>
    /// Dados do saldo da conta
    /// </summary>
    public class BalanceResponse
    {
        /// <summary>
        /// Saldo da conta
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Limite do cheque especial
        /// </summary>
        public decimal OverdraftLimit { get; set; }
    }
}
