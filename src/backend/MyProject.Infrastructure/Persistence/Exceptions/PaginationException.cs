namespace MyProject.Infrastructure.Persistence.Exceptions;

/// <summary>
/// Exception thrown when pagination parameters are invalid.
/// </summary>
/// <param name="paramName">The name of the parameter that caused the exception</param>
/// <param name="message">The error message that explains the reason for the exception</param>
/// <param name="errorCode">An optional stable error code for frontend localization</param>
public class PaginationException(string paramName, string message, string? errorCode = null)
    : ArgumentOutOfRangeException(paramName, message)
{
    /// <summary>
    /// A stable, dot-separated error code for frontend localization (e.g. "pagination.invalidPage").
    /// </summary>
    public string? ErrorCode { get; } = errorCode;
}
