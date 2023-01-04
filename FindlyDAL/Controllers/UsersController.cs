using FindlyDAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Controllers
{
    [Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IdentityContext _identityContext;

		public UsersController(IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("PostgreSQL");
			_identityContext = new IdentityContext(new DbContextOptionsBuilder<IdentityContext>().UseNpgsql(connectionString).Options);
		}

		[HttpGet]
		[Route("get_user_friends")]
		public async Task<IEnumerable<IdentityUser>> GetUserFriends(Guid userId)
		{
			return _identityContext.Users;
		}
	}
}
