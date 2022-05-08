using DevTrackR.API.Entities;

namespace DevTrackR.API.Persistence
{
    public class DevTrackRContext
    {
        public DevTrackRContext()
        {
            Packages = new List<Package>();
        }

        public List<Package> Packages { get; set; }
    }
}