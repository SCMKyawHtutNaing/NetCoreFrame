using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NetCoreFrame.Entities.DTO
{

    public class PostListViewModel
    {
        public int TotalRecords { get; set; }
        public List<PostViewModel> Data { get; set; }
    }

    public class PostViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = "";
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = "";
       
        [Required]
        [Display(Name = "Published")]
        public bool IsPublished { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; } = "";

        public string CreatedUserName { get; set; } = "";

        public string UpdatedUserId { get; set; } = "";
    }
}
