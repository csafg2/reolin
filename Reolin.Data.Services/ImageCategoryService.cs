using Reolin.Data.Services.Core;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reolin.Data.DTO;

namespace Reolin.Data.Services
{
    public class ImageCategoryService : IImageCategoryService
    {
        private DataContext _context;

        public ImageCategoryService(DataContext context)
        {
            this._context = context;
        }

        private DataContext Context
        {
            get
            {
                return this._context;
            }
        }


        public Task<List<ImageCategoryInfoDTO>> GetByProfile(int profileId)
        {
            return this.Context.ImageCategories.Where(imc => imc.ProfileId == profileId).Select(imc => new ImageCategoryInfoDTO()
            {
                Id = imc.Id,
                Name = imc.Name
            }).ToListAsync(); ;
        }
    }
}
