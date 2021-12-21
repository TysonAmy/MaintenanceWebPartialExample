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
    /// <summary>
    /// Queries for <see cref="ShiftModel"/>.
    /// Can view list of Shifts. See Detail information about shift. Create a new Shift. Edit a Shift. Delete a Shift.
    /// </summary>
    [Authorize]
    public class ShiftController : Controller
    {
        // GET: ShiftController
        /// <summary>
        /// Displays a list of shifts.
        /// </summary>
        /// <returns>A <see cref="ActionResult"/> that displays a list of shifts.</returns>
        public ActionResult Index()
        {
            List<ShiftViewModel> shiftViewModels = new List<ShiftViewModel>();
            foreach (ShiftViewModel shiftViewModel in MaintenanceLibrary.BusinessLogic.ShiftsProcessor.GetShifts())
            {
                shiftViewModels.Add(shiftViewModel);
            }
            return View(shiftViewModels);
        }


        // GET: ShiftController/Details/5
        /// <summary>
        /// Queries for <see cref="ShiftModel"/> and displays information as <see cref="ShiftViewModel"/>
        /// as <see cref="ShiftViewModel"/>
        /// </summary>
        /// <param name="id">Id that representing the Shift</param>
        /// <returns>A <see cref="ActionResult"/> that displays detail information about Shift.</returns>
        public ActionResult Details(int id)
        {
            ShiftViewModel shiftViewModel = MaintenanceLibrary.BusinessLogic.ShiftsProcessor.GetShiftByShiftId(id);

            return View(shiftViewModel);
        }

        // GET: ShiftController/Create
        /// <summary>
        /// Displays a blank create form for a shift.
        /// </summary>
        /// <returns>a <see cref="ActionResult"/> that displays a blank create form for a shift.</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShiftController/Create
        /// <summary>
        /// If <see cref="ShiftViewModel"/> is valid then Shift is created and user is redirected to Shift Index.
        /// Else user is created to Shift Create form and shown errors.
        /// </summary>
        /// <param name="shift">A <see cref="ShiftViewModel"/> that represents the Shift being added.</param>
        /// <returns>A <see cref="ActionResult"/>That either redirects user to Shift Index or Create new with errors. 
        /// See Function Summary.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShiftViewModel shift)
        {
            if(ModelState.IsValid)
            {
                MaintenanceLibrary.BusinessLogic.ShiftsProcessor.Insert(shift);
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: ShiftController/Edit/5
        /// <summary>
        /// Queries for <see cref="ShiftModel"/> by shift id and displays shift edit view with prepopulated fields.
        /// </summary>
        /// <param name="id">The Id that represents the Shift</param>
        /// <returns>A <see cref="ActionResult"/> that shows Shift Edit form.</returns>
        public ActionResult Edit(int id)
        {
            ShiftViewModel shiftViewModel = MaintenanceLibrary.BusinessLogic.ShiftsProcessor.GetShiftByShiftId(id);

            return View(shiftViewModel);
        }

        // POST: ShiftController/Edit/5
        /// <summary>
        /// If <see cref="ShiftViewModel"/> is valid Shift is updated and user is redirected to Shift Index page.
        /// Else user is returned to edit because showing model errors.
        /// </summary>
        /// <param name="id"> The Id that represents the Shift</param>
        /// <param name="shiftViewModel">A <see cref="ShiftViewModel"/> that represents the shift being updated.</param>
        /// <returns>A <see cref="ActionResult"/> that either redirects user to Shift Index or returns user to 
        /// shift edit view with errors. See Function Summary.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShiftViewModel shiftViewModel)
        {
            if(ModelState.IsValid)
            {
                MaintenanceLibrary.BusinessLogic.ShiftsProcessor.Update(shiftViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(shiftViewModel);

        }

        // GET: ShiftController/Delete/5
        /// <summary>
        /// Queries for <see cref="ShiftModel"/> and display a confirmation to delete page.
        /// </summary>
        /// <param name="id">A Id that represents the Shift</param>
        /// <returns>A <see cref="ActionResult"/> that shows user Delete Shift confirmation.</returns>
        public ActionResult Delete(int id)
        {
            ShiftViewModel shiftViewModel = MaintenanceLibrary.BusinessLogic.ShiftsProcessor.GetShiftByShiftId(id);
            return View(shiftViewModel);
        }

        // POST: ShiftController/Delete/5
        /// <summary>
        /// Deletes Shift, if successful redirect to index.
        /// If unsuccessful redirect to Shift Index with errors.
        /// </summary>
        /// <param name="id">A id that represents a shift.</param>
        /// <param name="shiftViewModel">A <see cref="ShiftViewModel"/> that represents a shift.</param>
        /// <returns>A <see cref="ActionResult"/> redirect users to Shift Index, if delete not successful then errors are shown</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ShiftViewModel shiftViewModel)
        {
            var result = MaintenanceLibrary.BusinessLogic.ShiftsProcessor.Delete(shiftViewModel);
            if(result == "Success")
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction("Index", "Shift", new { errorMessage = result });

        }
    }
}
