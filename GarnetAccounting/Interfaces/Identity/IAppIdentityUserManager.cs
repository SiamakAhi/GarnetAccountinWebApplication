using GarnetAccounting.Areas.Courier.Dto;
using GarnetAccounting.Models.Identity;
using GarnetAccounting.ViewModels.IdentityViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GarnetAccounting.Interfaces.Identity
{
    public interface IAppIdentityUserManager
    {
        #region BaseClass
        IPasswordHasher<AppIdentityUser> PasswordHasher { get; set; }
        IList<IUserValidator<AppIdentityUser>> UserValidators { get; }
        IList<IPasswordValidator<AppIdentityUser>> PasswordValidators { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IdentityOptions Options { get; set; }
        bool SupportsUserAuthenticationTokens { get; }
        bool SupportsUserAuthenticatorKey { get; }
        bool SupportsUserTwoFactorRecoveryCodes { get; }
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
        IQueryable<AppIdentityUser> Users { get; }
        Task<string> GenerateConcurrencyStampAsync(AppIdentityUser user);
        Task<IdentityResult> CreateAsync(AppIdentityUser user);
        Task<IdentityResult> UpdateAsync(AppIdentityUser user);
        Task<IdentityResult> DeleteAsync(AppIdentityUser user);
        Task<AppIdentityUser> FindByIdAsync(string userId);
        Task<AppIdentityUser> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(AppIdentityUser user, string password);
        string NormalizeKey(string key);
        Task UpdateNormalizedUserNameAsync(AppIdentityUser user);
        Task<string> GetUserNameAsync(AppIdentityUser user);
        Task<IdentityResult> SetUserNameAsync(AppIdentityUser user, string userName);
        Task<string> GetUserIdAsync(AppIdentityUser user);
        Task<bool> CheckPasswordAsync(AppIdentityUser user, string password);
        Task<bool> HasPasswordAsync(AppIdentityUser user);
        Task<IdentityResult> AddPasswordAsync(AppIdentityUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(AppIdentityUser user, string currentPassword, string newPassword);
        Task<IdentityResult> RemovePasswordAsync(AppIdentityUser user);
        Task<string> GetSecurityStampAsync(AppIdentityUser user);
        Task<IdentityResult> UpdateSecurityStampAsync(AppIdentityUser user);
        Task<string> GeneratePasswordResetTokenAsync(AppIdentityUser user);
        Task<IdentityResult> ResetPasswordAsync(AppIdentityUser user, string token, string newPassword);
        Task<AppIdentityUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<IdentityResult> RemoveLoginAsync(AppIdentityUser user, string loginProvider, string providerKey);
        Task<IdentityResult> AddLoginAsync(AppIdentityUser user, UserLoginInfo login);
        Task<IList<UserLoginInfo>> GetLoginsAsync(AppIdentityUser user);
        Task<IdentityResult> AddClaimAsync(AppIdentityUser user, Claim claim);
        Task<IdentityResult> AddClaimsAsync(AppIdentityUser user, IEnumerable<Claim> claims);
        Task<IdentityResult> ReplaceClaimAsync(AppIdentityUser user, Claim claim, Claim newClaim);
        Task<IdentityResult> RemoveClaimAsync(AppIdentityUser user, Claim claim);
        Task<IdentityResult> RemoveClaimsAsync(AppIdentityUser user, IEnumerable<Claim> claims);
        Task<IList<Claim>> GetClaimsAsync(AppIdentityUser user);
        Task<IdentityResult> AddToRoleAsync(AppIdentityUser user, string role);
        Task<IdentityResult> AddToRolesAsync(AppIdentityUser user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRoleAsync(AppIdentityUser user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(AppIdentityUser user, IEnumerable<string> roles);
        Task<IList<string>> GetRolesAsync(AppIdentityUser user);
        Task<bool> IsInRoleAsync(AppIdentityUser user, string role);
        Task<string> GetEmailAsync(AppIdentityUser user);
        Task<IdentityResult> SetEmailAsync(AppIdentityUser user, string email);
        Task<AppIdentityUser> FindByEmailAsync(string email);
        Task UpdateNormalizedEmailAsync(AppIdentityUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(AppIdentityUser user);
        Task<IdentityResult> ConfirmEmailAsync(AppIdentityUser user, string token);
        Task<bool> IsEmailConfirmedAsync(AppIdentityUser user);
        Task<string> GenerateChangeEmailTokenAsync(AppIdentityUser user, string newEmail);
        Task<IdentityResult> ChangeEmailAsync(AppIdentityUser user, string newEmail, string token);
        Task<string> GetPhoneNumberAsync(AppIdentityUser user);
        Task<IdentityResult> SetPhoneNumberAsync(AppIdentityUser user, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(AppIdentityUser user, string phoneNumber, string token);
        Task<bool> IsPhoneNumberConfirmedAsync(AppIdentityUser user);
        Task<string> GenerateChangePhoneNumberTokenAsync(AppIdentityUser user, string phoneNumber);
        Task<bool> VerifyChangePhoneNumberTokenAsync(AppIdentityUser user, string token, string phoneNumber);
        Task<bool> VerifyUserTokenAsync(AppIdentityUser user, string tokenProvider, string purpose, string token);
        Task<string> GenerateUserTokenAsync(AppIdentityUser user, string tokenProvider, string purpose);
        void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<AppIdentityUser> provider);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(AppIdentityUser user);
        Task<bool> VerifyTwoFactorTokenAsync(AppIdentityUser user, string tokenProvider, string token);
        Task<string> GenerateTwoFactorTokenAsync(AppIdentityUser user, string tokenProvider);
        Task<bool> GetTwoFactorEnabledAsync(AppIdentityUser user);
        Task<IdentityResult> SetTwoFactorEnabledAsync(AppIdentityUser user, bool enabled);
        Task<bool> IsLockedOutAsync(AppIdentityUser user);
        Task<IdentityResult> SetLockoutEnabledAsync(AppIdentityUser user, bool enabled);
        Task<bool> GetLockoutEnabledAsync(AppIdentityUser user);
        Task<DateTimeOffset?> GetLockoutEndDateAsync(AppIdentityUser user);
        Task<IdentityResult> SetLockoutEndDateAsync(AppIdentityUser user, DateTimeOffset? lockoutEnd);
        Task<IdentityResult> AccessFailedAsync(AppIdentityUser user);
        Task<IdentityResult> ResetAccessFailedCountAsync(AppIdentityUser user);
        Task<int> GetAccessFailedCountAsync(AppIdentityUser user);
        Task<IList<AppIdentityUser>> GetUsersForClaimAsync(Claim claim);
        Task<IList<AppIdentityUser>> GetUsersInRoleAsync(string roleName);
        Task<string> GetAuthenticationTokenAsync(AppIdentityUser user, string loginProvider, string tokenName);
        Task<IdentityResult> SetAuthenticationTokenAsync(AppIdentityUser user, string loginProvider, string tokenName, string tokenValue);
        Task<IdentityResult> RemoveAuthenticationTokenAsync(AppIdentityUser user, string loginProvider, string tokenName);
        Task<string> GetAuthenticatorKeyAsync(AppIdentityUser user);
        Task<IdentityResult> ResetAuthenticatorKeyAsync(AppIdentityUser user);
        string GenerateNewAuthenticatorKey();
        Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(AppIdentityUser user, int number);
        Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(AppIdentityUser user, string code);
        Task<int> CountRecoveryCodesAsync(AppIdentityUser user);
        Task<byte[]> CreateSecurityTokenAsync(AppIdentityUser user);

        #endregion

        #region CustomMethod
        Task<List<AppIdentityUser>> GetAllUsersAsync();
        IQueryable<UserViewModel> GetAllUsersWithRolesAsync();
        UserViewModel UserInfo(string UserName);
        Task<string> GetFullNameAsync(AppIdentityUser user);
        string GetFullName();
        Task<string[]> GetUserRolesAsync(AppIdentityUser user);
        string[] GetUserRoles(string UserId);
        Task<int> GetActiveUsersCount();
        Task<IdentityResult> AddEmployeeAsync(VmRegisterUser emp);
        Task<IdentityResult> AddCustomerOwnerAccount(VmRegisterUser emp);
        Task<clsResult> AddBranchUserAccountAsync(AddBranchUserDto emp);
        Task<IdentityResult> UpdateProfile(VmUpdateProfile model);
        Task<VmUpdateProfile> GetUserVmAsync(string UserName);
        Task<VmRegisterUser> GetUserForEditAsync(string id);
        Task<string> GetFullNameByIdAsync(string userId);
        Task<List<VmRole>> GetUserRolesByNameAsync(string userName);

        //Task<int> GetCustomerId(string userName);

        //Task<int> GetPersonId(string userName);
        //Task<int> GetAgentSupportId(string userName);
        Task<List<UserRoleViewModel>> GetUserRolesVmAsync(string userName);
        Task<int?> GetCustomerIdByUsername(string username);
        Task<string[]> userArrayRolesAsync(string username);
        Task<string[]?> userArrayRolesDescAsync(string username);
        Task<short?> GetUserDepartmentCodeAsync(string username);
        #endregion
    }
}
