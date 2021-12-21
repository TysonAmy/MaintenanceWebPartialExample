using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        // GET: EquipmentController
        /// <summary>
        /// Queries and Returns a display of all equipment, no matter the area.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that displays a list all equipment.</returns>
        public ActionResult Index()
        {
            List<EquipmentViewModel> equipmentViewModels = new List<EquipmentViewModel>();
            foreach (EquipmentViewModel equipmentViewModel in MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipments())
            {
                equipmentViewModels.Add(equipmentViewModel);
            }
            return View(equipmentViewModels);
        }

        // GET: EquipmentController/Details/5
        /// <summary>
        /// Displays detail about piece of equipment.
        /// </summary>
        /// <param name="id">Id associated with the Equipment</param>
        /// <returns>A <see cref="ActionResult"/> that displays information about an Equipment.</returns>
        public ActionResult Details(int id)
        {
            EquipmentViewModel equipmentViewModel = MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipmentByEquipmentId(id);
            return View(equipmentViewModel);
        }

        // GET: EquipmentController/Create
        /// <summary>
        /// Displays a create form with the dropdown box prepopulated with the Equipment's Area.
        /// </summary>
        /// <param name="id">Id associated with the Equipment being added</param>
        /// <returns>An <see cref="ActionResult"/> that shows user a create form with area it should be added to 
        /// already selected.</returns>
        public ActionResult Create(int id)
        {
            EquipmentViewModel equipmentViewModel = new EquipmentViewModel
            {
                AreaId = id
            };
            return View(equipmentViewModel);
        }

        // POST: EquipmentController/Create
        /// <summary>
        /// If <see cref="EquipmentViewModel"/> model is valid then it is inserted.
        /// If model is not valid user is returned to Create Form with model errors displayed.
        /// </summary>
        /// <param name="equipmentViewModel">A <see cref="EquipmentViewModel"/> that represents a piece of equipment.</param>
        /// <returns>A <see cref="ActionResult"/> that if <see cref="EquipmentViewModel"/> is valid redirects user
        /// to Area Details of area equipment is located in.
        /// If model is invalid user is returned to Create Form with model errors displayed</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentViewModel equipmentViewModel)
        {
            if(ModelState.IsValid)
            {
                MaintenanceLibrary.BusinessLogic.EquipmentProcessor.Insert(equipmentViewModel);
                return RedirectToAction("Details", "Area", new { id = equipmentViewModel.AreaId } ) ;

            }
            return View(equipmentViewModel);
        }

        // GET: EquipmentController/Edit/5
        /// <summary>
        /// Queries Equipment by given Id and displays edit form prepopulated.
        /// </summary>
        /// <param name="id">Id representing the piece of Equipment</param>
        /// <returns>A <see cref="ActionResult"/> that display the Equipment info  in edit view.</returns>
        public ActionResult Edit(int id)
        {
            EquipmentViewModel equipmentViewModel = MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipmentByEquipmentId(id);

            return View(equipmentViewModel);
        }

        // POST: EquipmentController/Edit/5
        /// <summary>
        /// Checks that <see cref="EquipmentViewModel"/> then updates it and redirects user to Equipment Index
        /// If model is not valid then user is sent back to edit view with errors.
        /// </summary>
        /// <param name="id">Id associated with the Equipment</param>
        /// <param name="equipmentViewModel"><see cref="EquipmentViewModel"/> model representing a Piece of Equipment</param>
        /// <returns>A <see cref="ActionResult"/> redirects user to Equipment Index, unless model is invalid then 
        /// user is sent back to edit view with errors displayed.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EquipmentViewModel equipmentViewModel)
        {
            if(ModelState.IsValid)
            {
                MaintenanceLibrary.BusinessLogic.EquipmentProcessor.Update(equipmentViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentViewModel);
        }

        // GET: EquipmentController/Deactivate/5
        /// <summary>
        /// Queries for a piece of equipment and displays a confirmation page as to if peice of equipment should 
        /// be deactivated.
        /// </summary>
        /// <param name="id">Id representing Equipment</param>
        /// <returns>A <see cref="ActionResult"/> that takes user to deactivate confirmation view.</returns>
        public ActionResult Deactivate(int id)
        {
            EquipmentViewModel equipmentViewModel = MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipmentByEquipmentId(id);
            return View(equipmentViewModel);
        }

        // POST: EquipmentController/Deactivate/5
        /// <summary>
        /// Updates Equipment to be deactivated and redirects user to Area Index.
        /// </summary>
        /// <param name="id">Id representing a Piece of Equipment</param>
        /// <param name="equipmentViewModel">A <see cref="EquipmentViewModel"/> that represents the Equipment.</param>
        /// <returns>A <see cref="ActionResult"/> redirecting users to Area Index.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id, EquipmentViewModel equipmentViewModel)
        {
            string result = MaintenanceLibrary.BusinessLogic.EquipmentProcessor.Deactivate(equipmentViewModel);
            if(result != "Success")
            {
                return RedirectToAction("Index", "Area", new { errorMessage = result });
            }
            else
            {
                return RedirectToAction("Index", "Area");
            }
        }
    }
}
