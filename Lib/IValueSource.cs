using System.Threading.Tasks;

namespace Lib
{
    public interface IValueSource
    {
        Task<string> GetValueAsync(string prefix);
    }
}
