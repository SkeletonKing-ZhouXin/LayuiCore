using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Data
{
    public interface ISelfEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }
}
