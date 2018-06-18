using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentitySolution.Data;
using IdentitySolution.TokenHelper;
using IdentitySolution.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySolution.Web.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/GetToken")]
        public async Task<JsonResponse> Login(LoginInputModel inputModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(inputModel.Username, inputModel.Password, false, false);

                if (!result.Succeeded)
                {
                    return new JsonResponse
                    {
                        Data = null,
                        Message = "Login failed",
                        Status = false
                    };
                }

                var user = await _userManager.FindByEmailAsync(inputModel.Username);

                var roles = await _userManager.GetRolesAsync(user);

                var token = new JwtTokenBuilder()
                                    .AddSecurityKey(JwtSecurityKey.Create())
                                    .AddSubject("token authentication")
                                    .AddIssuer("Fiver.Security.Bearer")
                                    .AddAudience("Fiver.Security.Bearer")
                                    .AddClaim("MembershipId", "111", roles)
                                    .AddExpiry(3600)
                                    .Build();
                return new JsonResponse
                {
                    Data = new
                    {
                        Token = token.Value,
                        Validity = token.ValidTo
                    },
                    Message = "Login Successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new JsonResponse
                {
                    Data = new
                    {
                        ErrorMessage = ex.Message,
                        InnerMessage = ex.InnerException?.Message,
                        ex.StackTrace
                    },
                    Message = "Login failed",
                    Status = false
                };
            }
        }
    }
}