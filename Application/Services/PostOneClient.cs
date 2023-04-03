using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;
using PersonalHealthManager.WebAPI.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PersonalHealthManager.Application.Services
{
    public class PostOneClient : ControllerBase
    {
        private readonly AppDbContext _context;
        public PostOneClient(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult PostingClient(ClientsBd client)
        {
            try
            {
                var existingClient = _context.Clients.Find(c => c.User_Name == client.User_Name || c.E_Mail == client.E_Mail).SingleOrDefault();
                if (existingClient != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict,
                        new
                        {
                            Cve_Error = -1,
                            Cve_Mensaje = "Ya existe un cliente con el mismo nombre de usuario o correo electrónico"
                        });
                }

                if (client.Height == null || client.Weigth == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new
                        {
                            Cve_Error = -3,
                            Cve_Mensaje = "Altura y peso son campos obligatorios y no pueden ser nulos."
                        });
                }

                client.BMI = client.Weigth / ((client.Height / 100) * (client.Height / 100));
                client.GEB = (decimal)((10 * (double)client.Weigth) + (6.25 * (double)client.Height) - (5 * client.Age) + 5);
                client.ETA = client.GEB * 0.9m;
                client.Create_Date = DateTime.Now;
                client.Update_Date = DateTime.Now;
                client.Client_ID = ObjectId.GenerateNewId().ToString();
                _context.Clients.InsertOne(client);
                return Ok(new
                {
                    Cve_Error = 0,
                    Cve_Mensaje = "Cliente agregado correctamente"
                });
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = $"Error al agregar el cliente: {ex.Message}"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = $"Error al agregar el cliente: {ex.Message}"
                    });
            }
        }
    }
}