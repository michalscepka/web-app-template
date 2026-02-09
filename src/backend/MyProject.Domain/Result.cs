namespace MyProject.Domain;

/// <summary>
/// Represents the result of an operation, indicating success or failure, and optionally containing a value.
/// </summary>
/// <typeparam name="T">The type of the value returned in case of success.</typeparam>
/// <remarks>Pattern documented in src/backend/AGENTS.md — update both when changing.</remarks>
public class Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the stable error code for frontend localization, if the operation failed.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Gets the value returned by the operation if successful.
    /// </summary>
    public T? Value { get; }

    private Result(bool isSuccess, string? error, string? errorCode, T? value)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
        Value = value;
    }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns>A successful result containing the value.</returns>
    public static Result<T> Success(T value)
    {
        return new Result<T>(true, null, null, value);
    }

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <param name="errorCode">An optional stable error code for frontend localization.</param>
    /// <returns>A failed result containing the error message.</returns>
    public static Result<T> Failure(string error, string? errorCode = null)
    {
        return new Result<T>(false, error, errorCode, default);
    }
}

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message if the operation failed.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Gets the stable error code for frontend localization, if the operation failed.
    /// </summary>
    public string? ErrorCode { get; }

    private Result(bool isSuccess, string? error = null, string? errorCode = null)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful result.</returns>
    public static Result Success()
    {
        return new Result(true);
    }

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <param name="errorCode">An optional stable error code for frontend localization.</param>
    /// <returns>A failed result containing the error message.</returns>
    public static Result Failure(string error, string? errorCode = null)
    {
        return new Result(false, error, errorCode);
    }
}
