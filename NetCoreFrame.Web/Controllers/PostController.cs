using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IPostService _postService;

        public PostController(UserManager<ApplicationUser> userManager, IPostService postService)
        {
            _userManager = userManager;
            _postService = postService;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            PostViewModel model = new PostViewModel();

            //model.posts = _postService.GetAll();

            return View(model);
        }

        [HttpPost]
        public JsonResult GetPostList()
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

            var model = _postService.GetAll(request);

            var returnObj = new
            {
                draw = draw,
                recordsTotal = model.TotalRecords,
                recordsFiltered = model.TotalRecords,
                data = model.Data
            };

            return Json(returnObj);
        
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(string id)
        {
            PostViewModel model = _postService.Get(id);

            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            try
            {
                var currentUser = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

                string currentUserId = currentUser.Id;

                Task<ApplicationUser> applicationUser =  _userManager.GetUserAsync(User);
                model.Id = Guid.NewGuid().ToString();
                model.CreatedUserId = currentUserId;

                bool success = _postService.Save(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(string id)
        {
            PostViewModel model = _postService.Get(id);

            return View(model);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel model)
        {
            try
            {
                var currentUser = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);

                string currentUserId = currentUser.Id;
                
                model.UpdatedUserId = currentUserId;

                bool success = _postService.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        [HttpPost]
        public JsonResult Delete(string id)
        {
            bool success = _postService.Delete(id);

            return Json(success);

        }


    }
}
