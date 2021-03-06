﻿// try running with postman on http://localhost:49501/api/commands/5



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
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;   // autmapper

        public CommandsController(ICommanderRepo repository, IMapper mapper)  // Dependency Injection
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        // GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));   // Map commandItems to IEnumerable<CommandReadDto>
        }

        // GET api/commands/{id}
        [Route("api/commands/{id:int}")]
        [HttpGet("{Id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));  // Map commandItem to <CommandReadDto>

            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);  // Map CommandCreateDto to Command
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);   // Creates object and returns location of the newly created object
            //return Ok(commandReadDto);
        }

        // PUT api/commands/{id}
        // Body (in Postman) to test
        /*
         * {
                "howTo": "Run a .Net Core application",
                "line": "dotnet run",
                "Platform": ".Net"
            }
         * */
        [HttpPut("{Id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            // Check we have an object in our repository to update
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            _mapper.Map(commandUpdateDto, commandModelFromRepo);   // Map commandUpdateDto to commandModelFromRepo

            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // PATCH
        // Attributes -- Add, Remove, Replace, Copy, Move, Test
        //Example:  (All operations need to complete successfully)
        //   [
        //      {
        //          "op": "replace",
        //          "path": "/howto",
        //          "value": "Some new value"
        //      },
        //      {
        //          "op": "test",
        //          "path": "/line",
        //          "value": "dotnet new"
        //      }
        //   ]

        // PATCH api/commands/{{id}
        [HttpPatch("{Id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            // Check we have an object in our repository to update
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            // To apply patch, need to create CommandUpdateDto object
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);   // Uses CommandProfile: 4th mapping

            // Apply patch
            patchDoc.ApplyTo(commandToPatch, ModelState);

            // Do validation check
            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandModelFromRepo);   // Map commandToPatch to commandModelFromRepo

            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{Id}")]
        public ActionResult DeleteCommand(int id)
        {
            // Check we have an object in our repository to update
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();   // Return noContent message.   mmm
        }









        //// GET api/commands
        //[Route("api/commands/{id2:bool}")]
        //[HttpGet("{Id2}", Name = "GetProductById")]
        //public  async Task<ActionResult<SpGetProductByPriceGreaterThan1000>> GetProductById(bool id2)
        //{
        //    int id = 0;

        //    ProductViewModel m = new ProductViewModel();
        //    m.ProductID = id;
        //    m.ProductDetail = new SpGetProductByID();
        //    m.ProductsGreaterThan1000 = new List<SpGetProductByPriceGreaterThan1000>();

        //    try
        //    {
        //        // Verification.
        //        if (m.ProductID > 0)
        //        {
        //            // Settings.
        //            var details = await _repository.GetProductByIDAsync(m.ProductID);
        //            m.ProductDetail = details.First();
        //        }

        //        // Settings.
        //        m.ProductsGreaterThan1000 = await _repository.GetProductByPriceGreaterThan1000Async();

        //        return Ok(_mapper.Map<ProductViewModel>(m.ProductsGreaterThan1000));  // Map commandItem to <CommandReadDto>
        //    }
        //    catch (Exception ex)
        //    {
        //        // Info.
        //        Console.Write(ex);
        //        return NotFound();
        //    }
        //}

        ///// <summary>  
        ///// GET: /Index/productId  
        ///// </summary>  
        ///// <param name="productId">Product ID parameter</param>  
        ///// <returns> Returns - index page</returns>  
        //public async Task OnGet(int productId = 0)
        //{
        //    // Initialization.  
        //    this.ProductVM = new ProductViewModel();
        //    this.ProductVM.ProductID = productId;
        //    this.ProductVM.ProductDetail = new SpGetProductByID();
        //    this.ProductVM.ProductsGreaterThan1000 = new List<SpGetProductByPriceGreaterThan1000>();

        //    try
        //    {
        //        // Verification.  
        //        if (this.ProductVM.ProductID > 0)
        //        {
        //            // Settings.  
        //            var details = await this.databaseManager.GetProductByIDAsync(this.ProductVM.ProductID);
        //            this.ProductVM.ProductDetail = details.First();
        //        }

        //        // Settings.  
        //        this.ProductVM.ProductsGreaterThan1000 = await this.databaseManager.GetProductByPriceGreaterThan1000Async();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Info  
        //        Console.Write(ex);
        //    }
        //}
    }
}
