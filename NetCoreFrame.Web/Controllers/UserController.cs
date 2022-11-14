using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreFrame.Services.Services;

namespace NetCoreFrame.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IRoleService _roleService;
        private IUserService _userService;

        public UserController(UserManager<ApplicationUser> userManager, 
            IUserService userService,
            IRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
            _userService = userService;
        }

        #region User List
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetUserList()
        {

            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            TableRequestViewModel request = new TableRequestViewModel();
            request.Keywords = searchValue;
            request.Skip = skip;
            request.PageSize = pageSize;
            request.SortColumn = sortColumn;
            request.SortColumnDirection = sortColumnDirection;

            var model = _userService.GetAll(request);

            var returnObj = new
            {
                draw = draw,
                recordsTotal = model.TotalRecords,
                recordsFiltered = model.TotalRecords,
                data = model.Data
            };

            return Json(returnObj);
        
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel();

            model.RoleList = new SelectList(_roleService.GetAll(),"Id","Name");

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
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
                    Address = model.Address == null ? "" : model.Address,
                    Role = model.Role,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = userId
                };

                var result = _userService.Create(user, model.Password);

                if(result.Result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    model.RoleList = new SelectList(_roleService.GetAll(), "Id", "Name");
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

        #region Edit
        public IActionResult Edit(string id)
        {
            UserViewModel model = _userService.Get(id);
            UserEditViewModel userModel = new UserEditViewModel();
            userModel.Id = model.Id;
            userModel.FirstName = model.FirstName;
            userModel.LastName = model.LastName;
            userModel.Email = model.Email;
            userModel.Id = model.Id;
            userModel.PhoneNumber = model.PhoneNumber;
            userModel.Address = model.Address;
            userModel.DOB = model.DOB;
            userModel.Role = model.Role;

            userModel.RoleList = new SelectList(_roleService.GetAll(), "Id", "Name");

            return View(userModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            DateTime? todayDate = DateTime.Now;
            var userId = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                
                bool success = _userService.Update(model);
;
                if(success)
                {
                    return RedirectToAction("Index", "User");
                }

            }

            model.RoleList = new SelectList(_roleService.GetAll(), "Id", "Name");

            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public JsonResult Delete(string id)
        {
            bool success = _userService.Delete(id);

            return Json(success);

        }
        #endregion
    }
}
