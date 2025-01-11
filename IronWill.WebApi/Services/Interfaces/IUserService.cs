using System;
using IronWill.Models.Models;

namespace IronWill.WebApi.Services.Interfaces
{
	public interface IUserService
	{
		Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel);

		Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel);

		Task<UserManagerResponse> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}

