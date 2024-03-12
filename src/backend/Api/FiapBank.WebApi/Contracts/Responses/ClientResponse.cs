namespace FiapBank.WebApi.Contracts.Responses
{
    /// <summary>
    /// Dados do cliente
    /// </summary>
    public class ClientResponse
    {
        /// <summary>
        /// Identificador do cliente
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Dados da conta corrente do cliente
        /// </summary>
        public ChekingAccountResponse? ChekingAccount { get; set; }
    }
}
