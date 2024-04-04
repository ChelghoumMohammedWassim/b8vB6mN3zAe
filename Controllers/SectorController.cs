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
    [Route("Sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

        public SectorController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            try
            {
                var sectors = _context.Sectors.ToList();
                if (sectors.Count == 0)
                {
                    _context.Sectors.Add(
                        new Sector
                        {   
                            ID= "global",
                            Name = "Global"
                        }
                    );
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}