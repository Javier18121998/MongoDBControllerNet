using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;
using PersonalHealthManager.Domain.BaseControllers;
using PersonalHealthManager.WebAPI.Models;
using MongoDB.Driver;

namespace PersonalHealthManager.Application.Services
{
    public class PutClient : BaseClient
    {
        private PutClient(AppDbContext context) : base(context) {}
        public IActionResult UpdateClientbyId(int? id, ClientsBd client)
        {
            try
            {
                // Validar que al menos uno de los atributos User_Name, E_Mail estén presentes en el cuerpo de la solicitud PUT
                if (string.IsNullOrEmpty(client.User_Name) && string.IsNullOrEmpty(client.E_Mail))
                {
                    return BadRequest("Debe especificar al menos uno de los siguientes atributos para actualizar el cliente: User_Name, E_Mail.");
                }
                // Buscar el cliente a actualizar en la base de datos
                var filter = id.HasValue 
                            ? Builders<ClientsBd>.Filter.Eq("Client_ID", id.Value)
                            : !string.IsNullOrEmpty(client.User_Name) 
                            ? Builders<ClientsBd>.Filter.Eq(c => c.User_Name, client.User_Name)
                            : !string.IsNullOrEmpty(client.E_Mail) 
                            ? Builders<ClientsBd>.Filter.Eq(c => c.E_Mail, client.E_Mail)
                            : throw new 
                            ArgumentException("Debe especificar el identificador del cliente o al menos uno de los siguientes atributos para actualizar el cliente: User_Name, E_Mail.");

                var existingClient = _context.Clients.Find(filter).FirstOrDefault();
                if (existingClient == null)
                {
                    return NotFound();
                }

                // Actualizar los datos del cliente
                existingClient.Name = client.Name ?? existingClient.Name;
                existingClient.Last_Names = client.Last_Names ?? existingClient.Last_Names;
                existingClient.Age = client.Age ?? existingClient.Age;
                existingClient.Height = client.Height ?? existingClient.Height;
                existingClient.Weigth = client.Weigth ?? existingClient.Weigth;
                existingClient.BMI = client.BMI ?? existingClient.BMI;
                existingClient.GEB = client.GEB ?? existingClient.GEB;
                existingClient.ETA = client.ETA ?? existingClient.ETA;
                existingClient.Update_Date = DateTime.Now;

                _context.Clients.ReplaceOne(filter, existingClient);

                return Ok(new
                {
                    Cve_Error = 0,
                    Cve_Mensaje = "Cliente actualizado correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = "Error al actualizar el cliente: " + ex.Message
                    });
            }
        }
        public IActionResult UpdateClient(ClientsBd client)
        {
            try
            {
                // Validar que al menos uno de los atributos User_Name, E_Mail estén presentes en el cuerpo de la solicitud PUT
                if (string.IsNullOrEmpty(client.User_Name) && string.IsNullOrEmpty(client.E_Mail))
                {
                    return BadRequest("Debe especificar al menos uno de los siguientes atributos para actualizar el cliente: User_Name, E_Mail.");
                }

                // Buscar el cliente a actualizar en la base de datos
                var filter = Builders<ClientsBd>.Filter.Empty;

                if (!string.IsNullOrEmpty(client.User_Name))
                {
                    filter = filter & Builders<ClientsBd>.Filter.Eq(c => c.User_Name, client.User_Name);
                }

                if (!string.IsNullOrEmpty(client.E_Mail))
                {
                    filter = filter & Builders<ClientsBd>.Filter.Eq(c => c.E_Mail, client.E_Mail);
                }

                var existingClient = _context.Clients.Find(filter).FirstOrDefault();

                if (existingClient == null)
                {
                    return NotFound();
                }

                // Actualizar los datos del cliente
                existingClient.Name = client.Name ?? existingClient.Name;
                existingClient.Last_Names = client.Last_Names ?? existingClient.Last_Names;
                existingClient.Age = client.Age ?? existingClient.Age;
                existingClient.Height = client.Height ?? existingClient.Height;
                existingClient.Weigth = client.Weigth ?? existingClient.Weigth;
                existingClient.BMI = client.BMI ?? existingClient.BMI;
                existingClient.GEB = client.GEB ?? existingClient.GEB;
                existingClient.ETA = client.ETA ?? existingClient.ETA;
                existingClient.Update_Date = DateTime.Now;

                _context.Clients.ReplaceOne(filter, existingClient);

                return Ok(new
                {
                    Cve_Error = 0,
                    Cve_Mensaje = "Cliente actualizado correctamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Cve_Error = -2,
                        Cve_Mensaje = "Error al actualizar el cliente: " + ex.Message
                    });
            }
        }
    }
}