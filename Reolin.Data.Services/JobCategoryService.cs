using Reolin.Data.Services.Core;
using System.Linq;
using System.Collections.Generic;
using Reolin.Data.DTO;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Reolin.Data.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private DataContext _context;

        public JobCategoryService(DataContext context)
        {
            this._context = context;
        }

        protected DataContext Context
        {
            get
            {
                return _context;
            }
        }


        public Task<List<JobCategoryInfoDTO>> GetAllAsync()
        {
            return this.Context
                .JobCategories
                    .Select(j => new JobCategoryInfoDTO()
                        {
                            Name = j.Name,
                            Id = j.Id,
                            IsSubCategory = j.IsSubCategory
                        }).ToListAsync();
        }
    }
}
