using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Entities.Data;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Drawing.Printing;

namespace NetCoreFrame.DAO.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BulletinContext _context;
        public PostRepository(BulletinContext context)
        {
            _context = context;
        }

        public PostListViewModel GetAll(TableRequestViewModel request)
        {
            PostListViewModel model = new PostListViewModel();

            var query = (from data in _context.Posts
                         join user in _context.User on data.CreatedUserId equals user.Id
                         where data.IsDeleted==false
                         orderby data.CreatedDate descending
                         select new PostViewModel
                         {
                           Id = data.Id,
                           Title = data.Title,
                           Description = data.Description,
                           IsPublished = data.IsPublished,
                           CreatedDate =data.CreatedDate,
                           CreatedUserName = user.FirstName+" "+user.LastName
                         });

            model.TotalRecords = query.Count();

            if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDirection)))
            {
                //query = query.OrderBy(request.SortColumn + " " + request.SortColumnDirection);
            }

            // search data when search value found
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(m => m.Title.Contains(request.Keywords)
                                    || m.Description.Contains(request.Keywords)
                                    || m.CreatedUserName.Contains(request.Keywords));
            }

            //pagination
            model.Data = query.Skip(request.Skip).Take(request.PageSize).ToList();

            return model;
        }

        public PostViewModel Get(string id)
        {
            var query = (from data in _context.Posts
                         where data.Id == id
                         select new PostViewModel
                         {
                             Id = data.Id,
                             Title = data.Title,
                             Description = data.Description,
                             IsPublished = data.IsPublished,
                         }).FirstOrDefault();

            return query;
        }

        public bool Save(Posts obj)
        {
            bool success = false;
            try
            {
                obj.CreatedDate = DateTime.Now;
                _context.Add(obj);
                _context.SaveChanges();

                success = true;

            }
            catch (Exception ex)
            {

            }

            return success;
        }

        public bool Update(Posts obj)
        {
            bool success = false;
            try
            {
                var post = _context.Posts.FirstOrDefault(x => x.Id == obj.Id);
                post.Title = obj.Title;
                post.Description = obj.Description;
                post.IsPublished = obj.IsPublished;
                post.UpdatedDate = DateTime.Now;
                post.UpdatedUserId = obj.UpdatedUserId;

                _context.Posts.Update(post);
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
                Posts post = _context.Posts.FirstOrDefault(x => x.Id == id);

                post.IsDeleted = true;
                post.DeletedDate = DateTime.Now;

                _context.Posts.Update(post);
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
