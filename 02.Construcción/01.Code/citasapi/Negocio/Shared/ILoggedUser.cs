using System.Collections.Generic;

namespace Negocio.Shared
{
    public interface ILoggedUser
    {
        string UserName { get; set; }
        List<int> MenuIds { get; set; }

    }
}
