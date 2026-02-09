namespace MyProject.Domain;

/// <summary>
/// Stable, dot-separated error codes returned alongside human-readable error messages.
/// Frontend clients use these codes to resolve localized translations.
/// <para>
/// Convention: <c>{domain}.{feature}.{errorType}</c> — e.g. <c>auth.login.invalidCredentials</c>.
/// </para>
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// Authentication error codes.
    /// </summary>
    public static class Auth
    {
        public const string LoginInvalidCredentials = "auth.login.invalidCredentials";
        public const string LoginAccountLocked = "auth.login.accountLocked";
        public const string RegisterFailed = "auth.register.failed";
        public const string RegisterRoleAssignFailed = "auth.register.roleAssignFailed";
        public const string TokenMissing = "auth.token.missing";
        public const string TokenNotFound = "auth.token.notFound";
        public const string TokenInvalidated = "auth.token.invalidated";
        public const string TokenReused = "auth.token.reused";
        public const string TokenExpired = "auth.token.expired";
        public const string TokenUserNotFound = "auth.token.userNotFound";
        public const string NotAuthenticated = "auth.notAuthenticated";
        public const string UserNotFound = "auth.userNotFound";
        public const string PasswordIncorrect = "auth.password.incorrect";
        public const string PasswordChangeFailed = "auth.password.changeFailed";
    }

    /// <summary>
    /// User self-service error codes.
    /// </summary>
    public static class User
    {
        public const string NotAuthenticated = "user.notAuthenticated";
        public const string NotFound = "user.notFound";
        public const string UpdateFailed = "user.updateFailed";
        public const string DeleteInvalidPassword = "user.delete.invalidPassword";
        public const string DeleteLastRole = "user.delete.lastRole";
    }

    /// <summary>
    /// Administrative operation error codes.
    /// </summary>
    public static class Admin
    {
        public const string UserNotFound = "admin.user.notFound";
        public const string HierarchyInsufficient = "admin.hierarchy.insufficient";
        public const string RoleNotExists = "admin.role.notExists";
        public const string RoleAlreadyAssigned = "admin.role.alreadyAssigned";
        public const string RoleNotAssigned = "admin.role.notAssigned";
        public const string RoleRankTooHigh = "admin.role.rankTooHigh";
        public const string RoleSelfRemove = "admin.role.selfRemove";
        public const string RoleLastRole = "admin.role.lastRole";
        public const string RoleAssignFailed = "admin.role.assignFailed";
        public const string RoleRemoveFailed = "admin.role.removeFailed";
        public const string LockSelfAction = "admin.lock.selfAction";
        public const string LockFailed = "admin.lock.failed";
        public const string UnlockFailed = "admin.unlock.failed";
        public const string DeleteSelfAction = "admin.delete.selfAction";
        public const string DeleteLastRole = "admin.delete.lastRole";
        public const string DeleteFailed = "admin.delete.failed";
    }

    /// <summary>
    /// Pagination error codes.
    /// </summary>
    public static class Pagination
    {
        public const string InvalidPage = "pagination.invalidPage";
        public const string InvalidPageSize = "pagination.invalidPageSize";
    }

    /// <summary>
    /// Rate limiting error codes.
    /// </summary>
    public static class RateLimit
    {
        public const string Exceeded = "rateLimit.exceeded";
    }

    /// <summary>
    /// Server-level error codes.
    /// </summary>
    public static class Server
    {
        public const string InternalError = "server.internalError";
    }

    /// <summary>
    /// Generic entity operation error codes (repository layer).
    /// </summary>
    public static class Entity
    {
        public const string AddFailed = "entity.addFailed";
        public const string NotFound = "entity.notFound";
        public const string NotDeleted = "entity.notDeleted";
    }
}
