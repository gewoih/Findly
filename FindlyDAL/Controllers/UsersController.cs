using FindlyApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FindlyDAL.Controllers
{
	public class UsersController : Controller
	{
		private readonly IdentityContext _identityContext;

		public UsersController(IdentityContext identityContext)
		{
			_identityContext = identityContext;
		}

		[HttpGet]
		[Route("get_user_friends")]
		public async Task<IEnumerable<User>> GetUserFriends(Guid userId)
		{
			return _identityContext.Users;
		}
	}
}
