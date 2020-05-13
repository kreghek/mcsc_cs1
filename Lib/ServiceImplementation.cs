using System.Threading.Tasks;

namespace Lib
{
    public class ServiceImplementation : IValueSource, IValueProvider
    {
        public Task<string> GetValueAsync(string prefix)
        {
            throw new System.NotImplementedException();
        }

        public void SetValue(string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
