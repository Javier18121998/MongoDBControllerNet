using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PersonalHealthManager.Application.Services
{
    public class GetClient : ControllerBase
    {
        private readonly AppDbContext _context;
        public GetClient(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Getclient(string id)
        {
            var collection = _context.Clients;
            if (!string.IsNullOrEmpty(id))
            {
                if (!ObjectId.TryParse(id, out ObjectId objectId))
                {
                    return BadRequest(new
                    {
                        Cve_Error = -1,
                        Cve_Mensaje = "El parámetro 'id' debe ser una cadena de 24 caracteres hexadecimal."
                    });
                }

                var client = collection.Find(c => c.Client_ID == objectId.ToString())
                                    .FirstOrDefault();
                if (client == null)
                {
                    return NotFound(new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = $"No se encontró el cliente con el ID '{id}'."
                    });
                }

                return Ok(client);
            }
            else
            {
                var clients = collection.Find(_ => true).ToList();
                return Ok(clients);
            }
        }
        public IActionResult Getclients()
        {
            var collection = _context.Clients;
            var clients = collection.Find(_ => true).ToList();
            return Ok(clients);
        }
    }
}