using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IronWill.Models.Models;
using IronWill.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IronWill.WebApi.Services.Classes
{
	public class UserService : IUserService
	{
		private UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
		{
			_userManager = userManager;
			_roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel)
		{
			if(registerModel == null)
				throw new NullReferenceException("Register model is null");

			if(registerModel.Password != registerModel.ConfirmPassword)
			{
				return new UserManagerResponse
				{
					Message = "Confrim password doesn't match with password",
					isSuccess = false
				};
			}

            if (!await _roleManager.RoleExistsAsync(registerModel.Role.ToString()))
            {
                return new UserManagerResponse
                {
                    Message = "Invalid role specified",
                    isSuccess = false
                };
            }

            var identityUser = new IdentityUser
			{
				Email = registerModel.Email,
				UserName = registerModel.Email,
				PhoneNumber = registerModel.MobileNumber
			};

			var result = await _userManager.CreateAsync(identityUser, registerModel.Password);

			if (result.Succeeded)
			{
                // Assign the role to the user
                await _userManager.AddToRoleAsync(identityUser, registerModel.Role.ToString());

                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    isSuccess = true
                };
            }

			return new UserManagerResponse
			{
				Message = "User did not created",
				isSuccess = false,
				Errors = result.Errors.Select(e => e.Description)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel)
        {
            if (loginModel == null)
                throw new NullReferenceException("Login model is null");

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null || user.Email == null)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid email or password.",
                    isSuccess = false
                };
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!passwordCheck)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid email or password.",
                    isSuccess = false
                };
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:TokenLifetimeMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserManagerResponse
            {
                Message = tokenHandler.WriteToken(token),
                isSuccess = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<UserManagerResponse> ForgotPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            if (forgotPasswordModel == null)
                throw new NullReferenceException("ForgotPassword model is null");

            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with this email.",
                    isSuccess = false
                };
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://localhost:7113/reset-password?email={forgotPasswordModel.Email}&token={Uri.EscapeDataString(resetToken)}";
            var subject = "Reset Your Password";
            var body = $"<p>Hi,</p><p>You requested to reset your password. Use the link below to reset it:</p>" +
                       $"<a href='{resetLink}'>Reset Password</a><p>If you didn't request this, please ignore this email.</p>";
            await _emailService.SendEmailAsync(forgotPasswordModel.Email, subject, body);

            return new UserManagerResponse
            {
                Message = "Password reset token has been sent to the email.",
                isSuccess = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            if (resetPasswordModel == null)
                throw new NullReferenceException("ResetPassword model is null");

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with this email.",
                    isSuccess = false
                };
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
            if (resetPassResult.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully.",
                    isSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "Error occurred while resetting the password.",
                isSuccess = false,
                Errors = resetPassResult.Errors.Select(e => e.Description)
            };
        }
    }
}

