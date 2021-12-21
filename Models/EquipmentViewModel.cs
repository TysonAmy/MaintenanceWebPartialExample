using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class EquipmentViewModel
    {
        /// <summary>
        /// Used to convert <see cref="EquipmentModel"/> to <see cref="EquipmentViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="equipment">A <see cref="EquipmentModel"/> that represents the Equipment info coming from the database</param>
        public static implicit operator EquipmentViewModel(EquipmentModel equipment)
        {
            return new EquipmentViewModel
            {
                Id = equipment.Id,
                AreaId = equipment.AreaId,
                Name = equipment.Name,
                Deactivate = equipment.Deactivate,
                Area = equipment.Area
            };
        }

        /// <summary>
        /// Used to convert <see cref="EquipmentViewModel"/> to <see cref="EquipmentModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="equipmentVM">A <see cref="EquipmentViewModel"/> represents what is coming from the View</param>
        public static implicit operator EquipmentModel(EquipmentViewModel equipmentVM)
        {
            return new EquipmentModel
            {
                Id = equipmentVM.Id,
                Name = equipmentVM.Name,
                AreaId = equipmentVM.AreaId,
                Deactivate = equipmentVM.Deactivate
            };
        }

        /// <summary>
        /// Id associated with Equipment
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Equipment
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id associated Area Equipment is located
        /// </summary>
        [DisplayName("Area")]
        public int AreaId { get; set; }

        /// <summary>
        /// A <see cref="AreaViewModel"/> representing Area Equipment is located in.
        /// </summary>
        public AreaViewModel Area { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> Equipment was deactivated.
        /// if null equipment is still in serves
        /// </summary>
        public DateTime? Deactivate { get; set; }
    }
}
