namespace FiapBank.WebApi.Contracts.Requests;

/// <summary>
/// Dados para criar um cliente.
/// </summary>
public class CustomerRequest
{
    /// <summary>
    /// Nome do cliente.
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Saldo inicial da conta corrente.
    /// </summary>
    public decimal InitialBalance { get; set; }

    /// <summary>
    /// Limite do cheque especial.
    /// </summary>
    public decimal OverdraftLimit { get; set; }
}
