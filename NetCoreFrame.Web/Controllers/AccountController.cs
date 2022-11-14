using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private IAuthService _authService;
		public AccountController(UserManager<ApplicationUser> userManager,
									  SignInManager<ApplicationUser> signInManager,
									  IAuthService authService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		#region Register
		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			DateTime? todayDate = DateTime.Now;
			var userId = Guid.NewGuid().ToString();

			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					Id = userId,
					FirstName = model.FirstName,
					LastName = model.LastName == null ? "" : model.LastName,
					UserName = model.Email,
					Email = model.Email,
					Address = "",
					Role = 1,
					IsActive = true,
					IsDeleted = false,
					CreatedDate = DateTime.Now,
					CreatedUserId = userId
				};

				var result = _authService.Register(user, model.Password);

				if (result.Result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);

					return RedirectToAction("index", "Home");
				}


				foreach (var error in result.Result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

			}
			return View(model);
		}
		#endregion

		#region Login
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}

			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LogInViewModel user)
		{
			if (ModelState.IsValid)
			{
				var result = await _authService.Login(user.Email, user.Password);

				if (result.Succeeded)
				{
					ApplicationUser currentUser = _authService.GetByEmail(user.Email);

					if (currentUser.IsDeleted)
					{
						ModelState.AddModelError(string.Empty, "Your account is deleted.");
						_authService.Logout();
						return View(user);
					}
					else if (!currentUser.IsActive)
					{
						ModelState.AddModelError(string.Empty, "Your account is not active.");
						_authService.Logout();
						return View(user);
					}

					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

			}
			return View(user);
		}
		#endregion


		#region Logout
		public IActionResult Logout()
		{
			_authService.Logout();

			return RedirectToAction("Login");
		}
		#endregion
	}
}
