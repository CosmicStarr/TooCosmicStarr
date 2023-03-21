using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Data.Classes
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
        public ApplicationUser(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, ITokenService Token)
        {
            _token = Token;
            _signInManager = signInManager;
            _userManager = userManager;
            
        }
        public async Task<LoginDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null) return null;
            var result = await _signInManager.PasswordSignInAsync(user,loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return null;
            }
            // var role = await _userManager.GetRolesAsync(user);
            // IList<Claim> Claim = await _userManager.GetClaimsAsync(user); 
            return new LoginDTO
            {
                Email = loginDTO.Email,
                Password = loginDTO.Password,
                Token = await _token.CreateToken(user)
            };
        }

        public async Task<RegisterModelDTO> SiginUp(RegisterModelDTO registerModelDTO)
        {
            var newUser = new AppUser()
            {
                Email = registerModelDTO.Email,
                UserName = registerModelDTO.Email,
                PasswordHash = registerModelDTO.Password
            };
            var results = await _userManager.CreateAsync(newUser,registerModelDTO.Password);
            if(!results.Succeeded) return null;
            //Add some logic to send email confirmation and roles in the future 
            return new RegisterModelDTO
            {
                Email = registerModelDTO.Email,
                Password = registerModelDTO.Password,
                Token =  await _token.CreateToken(newUser)
            };
        }
    }
}