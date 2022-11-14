using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace NetCoreFrame.Entities.DTO
{

   
    public class RoleViewModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
    }
}
