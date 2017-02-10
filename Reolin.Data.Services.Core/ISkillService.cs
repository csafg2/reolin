using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface ISkillService
    {
        Task<int> CreateSkill(string skillName);
    }
}
