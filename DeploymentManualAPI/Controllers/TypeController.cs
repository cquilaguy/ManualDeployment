using DeploymentManualAPI.Bussines;
using DeploymentManualAPI.Response.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeploymentManualAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeBll _Repository;
        private readonly MessageResponse _MsgProvider;

        public TypeController(ITypeBll repository, MessageResponse msgProvider)
        {
            _Repository = repository;
            _MsgProvider = msgProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                var response = await _Repository.GetTypes();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(_MsgProvider.MessagesProvider(true, null, (int)HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}
