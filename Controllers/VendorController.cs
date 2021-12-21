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
    public class VendorController : Controller
    {
        // GET: VendorController
        /// <summary>
        /// Queries Vendor and returns them to a view.
        /// </summary>
        /// <returns>An Instance of <see cref="ActionResult"/> class representing Vendors</returns>
        public ActionResult Index()
        {
            List<VendorModel> vendors = MaintenanceLibrary.BusinessLogic.VendorProcessor.GetVendors().ToList();
                List<VendorViewModel> vendorsVM = new();
                foreach (VendorViewModel item in vendors)
                {
                    vendorsVM.Add(item);
                }
                return View(vendorsVM);

        }

        // GET: VendorController/Details/5
        /// <summary>
        /// Queries a single Vendor record and returns a view of Vendor
        /// </summary>
        /// <param name="id">Id for Vendor</param>
        /// <returns>An Instance of <see cref="ActionResult"/> that displays a single vendor</returns>
        public ActionResult Details(int id)
        {
            VendorViewModel vendorVM = MaintenanceLibrary.BusinessLogic.VendorProcessor.GetVendorByVendorId(id);
            return View(vendorVM);
        }

        // GET: VendorController/Create
        /// <summary>
        /// User to create form for creating Vendor.
        /// </summary>
        /// <returns>An Instance of <see cref="ActionResult"/> displays an empty Create Vendor Form</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorController/Create
        /// <summary>
        /// Saves a new Vendor
        /// </summary>
        /// <param name="vendorVM">A <see cref="VendorViewModel"/> used to create an Area/</param>
        /// <returns>If Model is valid an instance of the <see cref="ActionResult"/> that Vendor Index.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorViewModel vendorVM)
        {
                if(ModelState.IsValid)
                {
                    MaintenanceLibrary.BusinessLogic.VendorProcessor.Insert(vendorVM);
                    return RedirectToAction("Index");
                }
                return View(vendorVM);
        }

        // GET: VendorController/Edit/5
        /// <summary>
        /// Creates a page that contains a prepopulated edit Vendor Form.
        /// </summary>
        /// <param name="id">Id of the Vendor</param>
        /// <returns>An <see cref="ActionResult"/> that shows Edit for prepopulated with Vendor Info</returns>
        public ActionResult Edit(int id)
        {
            return View((VendorViewModel)MaintenanceLibrary.BusinessLogic.VendorProcessor.GetVendorByVendorId(id));
        }

        // POST: VendorController/Edit/5
        /// <summary>
        /// Updates the Vendor
        /// </summary>
        /// <param name="id">Id of Vendor</param>
        /// <param name="vendorVM">The <see cref="VendorViewModel"/> instance that repersents the Vendor.</param>
        /// <returns>An <see cref="ActionResult"/> redirects user to Index</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VendorViewModel vendorVM)
        {
            MaintenanceLibrary.BusinessLogic.VendorProcessor.Update(vendorVM);

                return RedirectToAction("Index");
        }
    }
}
