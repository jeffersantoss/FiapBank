namespace FiapBank.WebApi.Contracts.Responses
{
    /// <summary>
    /// Dados da conta corrente
    /// </summary>
    public class ChekingAccountResponse
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
