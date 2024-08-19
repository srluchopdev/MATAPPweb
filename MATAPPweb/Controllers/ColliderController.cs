using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using YourNamespace.Services;
using MATAPPweb.data;
using MATAPPweb.Models;

namespace YourNamespace.Controllers
{
    public class ColliderController : Controller
    {
        private readonly NetworkScanner _networkScanner;
        private readonly AppDbContext _context;

        public ColliderController(AppDbContext context)
        {
            _networkScanner = new NetworkScanner();
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ColliderMotorViewModel
            {
                Colliders = new List<Collider>
                {
                    new Collider { Name = "pecho" },
                    new Collider { Name = "abdomen" },
                    new Collider { Name = "pelvis" },
                    new Collider { Name = "brazo D" },
                    new Collider { Name = "brazo I" },
                    new Collider { Name = "muslo D" },
                    new Collider { Name = "muslo I" },
                    new Collider { Name = "pierna D" },
                    new Collider { Name = "pierna I" },
                    new Collider { Name = "pie D" },
                    new Collider { Name = "pie I" },
                },
                AvailableIPs = _networkScanner.ScanNetworkForSlimeVRDevices()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignColliders(ColliderMotorViewModel model)
        {
            // Guarda las asignaciones en la base de datos
            SaveAssignments(model);

            return RedirectToAction("Index");
        }

        private void SaveAssignments(ColliderMotorViewModel model)
        {
            foreach (var collider in model.Colliders)
            {
                // Verifica si ya existe una asignación para este collider
                var existingAssignment = _context.ColliderAssignments
                    .FirstOrDefault(ca => ca.ColliderName == collider.Name);

                if (existingAssignment != null)
                {
                    // Si existe, actualiza la IP del motor
                    existingAssignment.MotorIP = collider.MotorIP;
                }
                else
                {
                    // Si no existe, crea una nueva asignación
                    var assignment = new ColliderAssignment
                    {
                        ColliderName = collider.Name,
                        MotorIP = collider.MotorIP
                    };
                    _context.ColliderAssignments.Add(assignment);
                }
            }

            // Guarda los cambios en la base de datos
            _context.SaveChanges();
        }
    }
}
