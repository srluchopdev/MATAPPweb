using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using YourNamespace.Services;

namespace YourNamespace.Controllers
{
    public class ColliderController : Controller
    {
        private readonly NetworkScanner _networkScanner;

        public ColliderController()
        {
            _networkScanner = new NetworkScanner();
        }

        public IActionResult Index()
        {
            var model = new ColliderMotorViewModel
            {
                Colliders = new List<Collider>
                {
                    new Collider { Name = "Collider 1" },
                    new Collider { Name = "Collider 2" },
                    // Añadir más colliders según sea necesario
                },
                AvailableIPs = _networkScanner.ScanNetworkForSlimeVRDevices()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignColliders(ColliderMotorViewModel model)
        {
            // Guarda las asignaciones en un servicio o base de datos
            SaveAssignments(model);

            return RedirectToAction("Index");
        }

        private void SaveAssignments(ColliderMotorViewModel model)
        {
            // Implementa la lógica para guardar las asignaciones
        }
    }
}

