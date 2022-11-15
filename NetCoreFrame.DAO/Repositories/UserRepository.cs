using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Entities.Data;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;

namespace NetCoreFrame.DAO.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly BulletinContext _context;
		public UserRepository(BulletinContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public UserListViewModel GetAll(TableRequestViewModel request)
		{
			UserListViewModel model = new UserListViewModel();

			var query = (from data in _context.User
						 join user in _context.User on data.CreatedUserId equals user.Id
						 where data.IsDeleted == false
						 orderby data.CreatedDate descending
						 select new UserViewModel
						 {
							 Id = data.Id,
							 FullName = user.FirstName + " " + user.LastName,
							 Email = user.Email,
							 DobString = user.Dob != null ? user.Dob.Value.ToString("yyyy/MM/dd") : null,
							 Role = user.Role,
							 PhoneNumber = user.PhoneNumber,
							 Address = user.Address,
							 CreatedDate = data.CreatedDate,
							 CreatedUserName = user.FirstName + " " + user.LastName
						 });

			model.TotalRecords = query.Count();

			if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
			{
				//query = query.OrderBy(request.SortColumn + " " + request.SortColumnDirection);
			}

			// search data when search value found
			if (!string.IsNullOrEmpty(request.Keywords))
			{
				query = query.Where(m => m.FullName.Contains(request.Keywords)
									|| m.Email.Contains(request.Keywords)
									|| m.CreatedUserName.Contains(request.Keywords));
			}

			//pagination
			model.Data = query.Skip(request.Skip).Take(request.PageSize).ToList();

			return model;
		}

		public UserViewModel Get(string id)
		{
			var query = (from data in _context.User
						 where data.Id == id
						 select new UserViewModel
						 {
							 Id = data.Id,
							 Email = data.Email,
							 FirstName = data.FirstName,
							 LastName = data.LastName,
							 PhoneNumber = data.PhoneNumber,
							 Address = data.Address,
							 DOB = data.Dob,
							 Role = data.Role

						 }).FirstOrDefault();

			return query;
		}

		public async Task<IdentityResult> Create(ApplicationUser user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);

			return result;
		}

		public bool Update(User obj)
		{
			bool success = false;
			try
			{
				var user = _context.User.FirstOrDefault(x => x.Id == obj.Id);
				user.Email = obj.Email;
				user.FirstName = obj.FirstName;
				user.LastName = obj.LastName;
				user.PhoneNumber = obj.PhoneNumber;
				user.Address = obj.Address;
				user.Dob = obj.Dob;
				user.Role = obj.Role;
				user.UpdatedDate = DateTime.Now;
				user.UpdatedUserId = obj.UpdatedUserId;

				_context.User.Update(user);
				_context.SaveChanges();

				success = true;

			}
			catch (Exception ex)
			{

			}

			return success;
		}

		public bool Delete(string id)
		{
			bool success = false;
			try
			{
				User user = _context.User.FirstOrDefault(x => x.Id == id);

				user.IsDeleted = true;
				user.DeletedDate = DateTime.Now;

				_context.User.Update(user);
				_context.SaveChanges();

				success = true;

			}
			catch (Exception ex)
			{

			}

			return success;
		}


	}
}
