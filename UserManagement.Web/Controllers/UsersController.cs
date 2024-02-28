using System.Linq;

using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;


namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]

    public ViewResult List(bool? isActive)
    {
        IEnumerable<User> users;

        if (isActive.HasValue)
        {
            users = _userService.FilterByActive(isActive.Value);
        }
        else
        {
            users = _userService.GetAll();
        }

        var items = users.Select(u => new UserListItemViewModel
        {
            Id = u.Id,
            Forename = u.Forename,
            Surname = u.Surname,
            Email = u.Email,
            IsActive = u.IsActive,
            DateOfBirth = u.DateOfBirth,
        }).ToList();

        var model = new UserListViewModel
        {
            Items = items
        };

        return View(model);
    }
    public IActionResult UserDetailView(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var users = _userService.GetAll();
        var user = _userService.GetUserById(id);
        if (user == null) { return NotFound(); }
        return View(user);



    }

}
