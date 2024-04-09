using Caderninh.io.Application.Common.Models;

namespace Caderninh.io.Application.Common.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}