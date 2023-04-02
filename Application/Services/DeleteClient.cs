using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;
using PersonalHealthManager.WebAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PersonalHealthManager.Application.Services
{
    public class DeleteClient : ControllerBase
    {
        private readonly AppDbContext _context;
        public DeleteClient(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> DeleteOneClient(string id)
        {
            try
            {
                if(id == null){return BadRequest();}
                var client = await _context.Clients
                                           .FindOneAndDeleteAsync(Builders<ClientsBd>
                                           .Filter.Eq(c => 
                                           c.Client_ID, id));
               return (client == null) 
                      ? NotFound() 
                      : Ok(
                            new 
                            { 
                                Cve_Error = 0, 
                                Cve_Mensaje = "Cliente eliminado correctamente" 
                            });
            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = "Error al eliminar el cliente: " + ex.Message
                    });
            }
        }
    }
}