using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IServiceManager
    {
        IAuth Auth{ get; }
        IPersonServices PersonServices { get; }
        IPersonGroupServices PersonGroupServices { get; }
        ITaskServices TaskServices { get; }
        INoteServices NoteServices { get; }
        IJwtService JwtService{ get; }
    }
}
