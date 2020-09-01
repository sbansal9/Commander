// try running with postman on http://localhost:49501/api/commands/5



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Commander.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;   // autmapper

        public VendorController(ICommanderRepo repository, IMapper mapper)  // Dependency Injection
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET api/commands
        [HttpGet("{Id}", Name = "GetProductById")]
        public async Task<ActionResult<SpGetProductByPriceGreaterThan1000>> GetProductById(int id)
        {
            ProductViewModel m = new ProductViewModel();
            m.ProductID = id;
            m.ProductDetail = new SpGetProductByID();
            m.ProductsGreaterThan1000 = new List<SpGetProductByPriceGreaterThan1000>();

            try
            {
                // Verification.
                if (m.ProductID > 0)
                {
                    // Settings.
                    var details = await _repository.GetProductByIDAsync(m.ProductID);
                    m.ProductDetail = details.First();
                }

                // Settings.
                m.ProductsGreaterThan1000 = await _repository.GetProductByPriceGreaterThan1000Async();

                //return Ok(_mapper.Map<ProductViewModel>(m.ProductsGreaterThan1000));  // Map commandItem to <CommandReadDto>
                return Ok(m);
            }
            catch (Exception ex)
            {
                // Info.
                Console.Write(ex);
                return NotFound();
            }
        }
    }
}
