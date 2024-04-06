using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("zip-code")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

        public ZipCodeController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
        }

    }
}