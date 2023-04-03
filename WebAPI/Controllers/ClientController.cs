using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;
using PersonalHealthManager.Application.Services;
using PersonalHealthManager.WebAPI.Models;

namespace PersonalHealthManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ClientController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpPost("/NutriNET/Cliente")]
        public IActionResult Postingclient([FromBody] ClientsBd client)
        {
            PostOneClient postingIn = new PostOneClient(_context);
            return postingIn.PostingClient(client);
        }
        [HttpGet("/NutriNET/Cliente/{id?}")]
        public IActionResult GetClients(string id)
        {
            GetClient gettingOne = new GetClient(_context);
            return gettingOne.Getclient(id);
        }  
        [HttpGet("/NutriNET/Cliente/")]
        public IActionResult GetAllClients()
        {
            GetClient gettingAll = new GetClient(_context);
            return gettingAll.Getclients();
        }  
        [HttpPut("/NutriNET/Cliente/{id?}")]
        public IActionResult PutClient(int? id, [FromBody] ClientsBd client)
        {
            PutClient putById = new PutClient(_context);
            return putById.UpdateClientbyId(id, client);
        }
        [HttpPut("/NutriNET/Cliente/")]
        public IActionResult PutClient([FromBody] ClientsBd client)
        {
            PutClient putNoId = new PutClient(_context);
            return putNoId.UpdateClient(client);
        }
        [HttpDelete("/NutriNET/Cliente/{id?}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            DeleteClient deleteOne = new DeleteClient(_context);
            var result = await deleteOne.DeleteOneClient(id);
            return result;
        }
    }
}