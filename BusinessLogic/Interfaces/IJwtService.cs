using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        Task<IEnumerable<Claim>> GetClaimsAsync(User user);
        string CreateToken(IEnumerable<Claim> claims);
         
    }
}
