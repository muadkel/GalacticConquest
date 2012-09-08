using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticConquest.GameEngine.GalacticComponents
{
    public class ProcessedResource
    {
        public string ID;

        public string Name;

        public string Title;

        public string Description;

        public int invProcessedMaterials;

        public ProcessedResource()
        {
            ID = "";
            Name = "";
            Title = "";
            Description = "";
            invProcessedMaterials = 0;
        }

        public ProcessedResource(string id, string name, string title, string description)
        {
            ID = id;
            Name = name;
            Title = title;
            Description = description;
            invProcessedMaterials = 0;
        }

        public ProcessedResource(string id, string name, string title, string description, int startingProcessedMaterials)
        {//Send the starting inventory amount for this resource
            ID = id;
            Name = name;
            Title = title;
            Description = description;
            invProcessedMaterials = startingProcessedMaterials;
        }

    }
}
