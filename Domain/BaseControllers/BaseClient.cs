using Microsoft.AspNetCore.Mvc;
using PersonalHealthManager.Infrastructure.Data;


namespace PersonalHealthManager.Domain.BaseControllers
{
    public class BaseClient : ControllerBase
    {
        protected readonly AppDbContext _context;

        public BaseClient(AppDbContext context)
        {
            this._context = context;
        }
    }
}