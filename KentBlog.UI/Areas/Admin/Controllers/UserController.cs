using KentBlog.Business.Abstract;
using KentBlog.Entity.Concrete;
using KentBlog.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KentBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var model = new List<UserListViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Add(new UserListViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Rol Yok"
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new CreateUserViewModel
            {
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    FullName = model.FullName!,
                    Email = model.Email!,
                    UserName = model.UserName!,
                    PhoneNumber = model.PhoneNumber!
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.SelectedRole!);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            model.Roles = _roleManager.Roles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToList();

            return View(model);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(Index)); // Hatalıysa yine Index’e dön
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRole = userRoles.FirstOrDefault();

            var model = new EditUserViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                SelectedRole = selectedRole,
                Roles = roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // Bilgileri güncelle
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            // Şifre güncelle
            if (!string.IsNullOrEmpty(model.Password) && model.Password == model.ConfirmPassword)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    model.Roles = _roleManager.Roles.Select(x => new SelectListItem
                    {
                        Value = x.Name,
                        Text = x.Name
                    }).ToList();
                    return View(model);
                }
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                model.Roles = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name
                }).ToList();
                return View(model);
            }

            // Rolleri güncelle
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                {
                    foreach (var error in removeResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    model.Roles = _roleManager.Roles.Select(x => new SelectListItem
                    {
                        Value = x.Name,
                        Text = x.Name
                    }).ToList();
                    return View(model);
                }
            }

            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                var addResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (!addResult.Succeeded)
                {
                    foreach (var error in addResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    model.Roles = _roleManager.Roles.Select(x => new SelectListItem
                    {
                        Value = x.Name,
                        Text = x.Name
                    }).ToList();
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }


    }
}
