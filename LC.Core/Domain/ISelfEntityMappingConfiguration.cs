using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Core.Domain
{
    public interface ISelfEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }
}
