using MaintenanceLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{



    /// <summary>
    /// Returns SelectList that can be used in Drop downs and combo boxes.
    /// </summary>
    public class SelectListModel
    {
        /// <summary>
        /// Creates a List Vendors to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getVendorsSelectList()
        {
            List<VendorModel> Vendors;
            Vendors = MaintenanceLibrary.BusinessLogic.VendorProcessor.GetVendors().ToList();
            return new SelectList(Vendors.AsEnumerable<VendorModel>(), "Id", "Name");
        }



        /// <summary>
        /// Creates a List Shifts to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getShiftsSelectList()
        {
            List<ShiftModel> Shifts;
            Shifts = MaintenanceLibrary.BusinessLogic.ShiftsProcessor.GetShifts();
            return new SelectList(Shifts.AsEnumerable<ShiftModel>(), "Id", "Name");
        }

        /// <summary>
        /// Creates a List Employee, with Full Names, to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getEmployeesSelectList()
        {
            List<AppUserModel> Supervisors;
            Supervisors = MaintenanceLibrary.BusinessLogic.AppUserProcessor.GetAllCurrentEmployees();
            return new SelectList(Supervisors.AsEnumerable<AppUserModel>(), "Id", "FullName");
        }

        /// <summary>
        /// Creates a List Employees who have a particular role
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getEmployeesSelectListByRole(string RoleName1, string RoleName2 = "", string RoleName3 = "")
        {
            List<AppUserModel> Supervisors;
            Supervisors = MaintenanceLibrary.BusinessLogic.AppUserProcessor.GetAllCurrentEmployeesByRoleName(RoleName1, RoleName2, RoleName3);
            return new SelectList(Supervisors.AsEnumerable<AppUserModel>(), "Id", "FullName");
        }

        /// <summary>
        /// Creates a List Equipment inside and Area to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getEquipmentSelectList(int AreaId)
        {
            List<EquipmentModel> equipment;
            equipment = MaintenanceLibrary.BusinessLogic.EquipmentProcessor.GetEquipmentByAreaId(AreaId);
            return new SelectList(equipment.AsEnumerable<EquipmentModel>(), "Id", "Name");
        }

        /// <summary>
        /// Creates a List Employees, minus one employee, to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getEmployeesSelectList(string removeId)
        {
            List<AppUserModel> Supervisors;
            Supervisors = MaintenanceLibrary.BusinessLogic.AppUserProcessor.GetAllCurrentEmployees();
            var self = Supervisors.Where(item => item.Id == removeId).First<AppUserModel>();
            Supervisors.Remove(self);
            return new SelectList(Supervisors.AsEnumerable<AppUserModel>(), "Id", "FullName");

        }

        /// <summary>
        /// Creates a List Areas to put in dropdown box or combo box.
        /// </summary>
        /// <returns>A <see cref="SelectList"/> that can used in dropdown box or combo box</returns>
        static public SelectList getAreaSelectList()
        {
            List<AreaModel> areas = MaintenanceLibrary.BusinessLogic.AreasProcessor.GetAreas();
            return new SelectList(areas.AsEnumerable<AreaModel>(), "Id", "Name");
        }
    }
}
