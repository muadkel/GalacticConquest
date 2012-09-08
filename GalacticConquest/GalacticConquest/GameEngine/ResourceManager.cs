using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine
{
    public class ResourceManager
    {
        public int invRawMaterials;

        public List<GalacticComponents.ProcessedResource> ProcessedMaterials;

        public ResourceManager()
        {
            invRawMaterials = 0;
            ProcessedMaterials = new List<GalacticComponents.ProcessedResource>();
        }

    }
}
