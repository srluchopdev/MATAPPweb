using System.Collections.Generic;

namespace YourNamespace.Models
{
    public class ColliderMotorViewModel
    {
        public List<Collider> Colliders { get; set; }
        public List<string> AvailableIPs { get; set; }
    }

    public class Collider
    {
        public string Name { get; set; }
        public string MotorIP { get; set; }
    }
}
