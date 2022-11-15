using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace NetCoreFrame.Entities.DTO
{

	public class UserListViewModel
	{
		public int TotalRecords { get; set; }
		public List<UserViewModel> Data { get; set; }
	}

	public class UserViewModel
	{
		public string Id { get; set; } = "";
		[Required]
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string FullName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; } = "";

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = "";

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
		public string ConfirmPassword { get; set; } = "";
		public string? PhoneNumber { get; set; } = "";
		public string? Address { get; set; } = "";
		public string DobString { get; set; }
		public DateTime? DOB { get; set; }
		public int Role { get; set; } = 0;
		public SelectList RoleList { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }

		public string CreatedUserId { get; set; } = "";

		public string CreatedUserName { get; set; } = "";

		public string? UpdatedUserId { get; set; } = "";
	}

	public class UserEditViewModel
	{
		public string Id { get; set; } = "";
		[Required]
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string FullName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; } = "";

		public string? PhoneNumber { get; set; } = "";
		public string? Address { get; set; } = "";
		public DateTime? DOB { get; set; }
		public int Role { get; set; } = 0;
		public SelectList RoleList { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }

		public string CreatedUserId { get; set; } = "";

		public string CreatedUserName { get; set; } = "";

		public string? UpdatedUserId { get; set; } = "";
	}
}
