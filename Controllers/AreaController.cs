using MaintenanceLibrary.Models;
using MaintenanceWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Controllers
{
    /// <summary>
    /// Controls the querying for <see cref="AreaModel"/> and displaying of <see cref="AreaViewModel"/> model.
    /// </summary>
    [Authorize]
    public class AreaController : Controller
    {
        // GET: AreaController
        /// <summary>
        /// Queries Areas and returns them to a view.
        /// </summary>
        /// <returns>An Instance of <see cref="ActionResult"/> class representing Areas</returns>
        public ActionResult Index()
        {
                List<AreaModel> areas = MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreas();
                List<AreaViewModel> areasVM = new();
                foreach (AreaViewModel item in areas)
                {
                    areasVM.Add(item);
                }
                return View(areasVM);

        }

        // GET: AreaController/Details/5
        /// <summary>
        /// Queries a single Area record and returns a view of Areas and Equipment in those areas.
        /// </summary>
        /// <param name="id">Id for Area</param>
        /// <returns>An Instance of <see cref="ActionResult"/> that displays a single area and equipment in that area</returns>
        public ActionResult Details(int id)
        {
            AreaViewModel areaVM = MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreaByAreaId(id);
            return View(areaVM);
        }

        // GET: AreaController/Create
        /// <summary>
        /// User to create form for creating Areas.
        /// </summary>
        /// <returns>An Instance of <see cref="ActionResult"/> displays an empty Create Area Form</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaController/Create
        /// <summary>
        /// Saves a new Area
        /// </summary>
        /// <param name="areaVM">A <see cref="AreaViewModel"/> used to create an Area/</param>
        /// <returns>If Model is valid an instance of the <see cref="ActionResult"/> that shows the Main Dashboard to the user. If Model is not valid returns <see cref="ActionResult"/> that hows the edit form with errors the model has.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AreaViewModel areaVM)
        {
                if(ModelState.IsValid)
                {
                    MaintenanceLibrary.BusinessLogic.AreasProcessor.Insert(areaVM);
                    return RedirectToAction("Main","Dashboards");
                }
                return View(areaVM);
        }

        // GET: AreaController/Edit/5
        /// <summary>
        /// Creates a page that contains a prepopulated edit Area Form.
        /// </summary>
        /// <param name="id">Id of the Area</param>
        /// <returns>An <see cref="ActionResult"/> that shows Edit for prepopulated with Area Info</returns>
        public ActionResult Edit(int id)
        {
            return View((AreaViewModel)MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreaByAreaId(id));
        }

        // POST: AreaController/Edit/5
        /// <summary>
        /// Updates the Area
        /// </summary>
        /// <param name="id">Id of Area</param>
        /// <param name="areaVM">The <see cref="AreaViewModel"/> instance that repersents the Area.</param>
        /// <returns>An <see cref="ActionResult"/> redirects user to Main/Dashboards</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AreaViewModel areaVM)
        {
                MaintenanceLibrary.BusinessLogic.AreasProcessor.Update((AreaModel)areaVM);

                return RedirectToAction("Main","Dashboards");
        }

        // GET: AreaController/Deactivate/5
        /// <summary>
        /// Request Permission to deactivate.
        /// </summary>
        /// <param name="id">The Id for the Area</param>
        /// <returns>An <see cref="ActionResult"/> that show information about Area and request to confirm Deactivation.</returns>
        public ActionResult Deactivate(int id)
        {
            AreaViewModel areavm = MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreaByAreaId(id);
            if(areavm.Equipments.Count != 0)
            {
                ModelState.AddModelError("", "You can not delete this area, because there is equipment associated with it.");
            }
            return View(areavm);

        }

        // POST: AreaController/Deactivate/5
        /// <summary>
        /// Deactivates Area
        /// </summary>
        /// <param name="id">Id of the Area</param>
        /// <param name="areaVM">The <see cref="AreaViewModel"/> instance that represents the Area</param>
        /// <returns>An instance of the <see cref="ActionResult"/> class representing a redirect to Main/Dashboards, until the deactivation was unsuccessful, in which case the the deactivate Area request form is return with error.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(int id, AreaViewModel areaVM)
        {
            string Response;
            Response = MaintenanceLibrary.BusinessLogic.AreasProcessor.Deactivate(areaVM);
            if (Response != "Success")
            {
                ModelState.AddModelError("", Response);
                return View(areaVM);
            }
            return RedirectToAction("Main", "Dashboards");
        }
    }
}
