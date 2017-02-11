using Reolin.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IImageCategoryService
    {
        Task<List<ImageCategoryInfoDTO>> GetByProfile(int profileId);
    }
}
