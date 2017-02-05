using Reolin.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Data.Services
{
    // TODO: decide about this class
    public class ProfileLocationService : ProfileService//, IProfileLocationService
    {
        public ProfileLocationService(DataContext context) : base(context) { }

        //public event ProfilesLocationRetrievedHandler ProfileRetrieved;


        // TODO: override when base class modfies tags collection of a suer
        // TODO: override when user is frist created
        // TODO: override when user location is updated
        public override Task<List<ProfileRedisCacheDTO>> GetInRangeAsync(string tag, double radius, double sourceLat, double sourceLong)
        {
            return base.GetInRangeAsync(tag, radius, sourceLat, sourceLong);

            //List<ProfileRedisCacheDTO> result = await base.GetInRangeAsync(tag, radius, sourceLat, sourceLong);


            // TODO: store this result into redis cache for later querying
            //return result;
        }
    }
}