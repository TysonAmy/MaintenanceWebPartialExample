using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    /// <summary>
    /// Used to model Area data for the view.
    /// </summary>
    public class AreaViewModel
    {

        /// <summary>
        /// Used to convert a <see cref="AreaModel"/> to <see cref="AreaViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="area">A <see cref="AreaModel"/> that represents the Area info coming from the database</param>
        public static implicit operator AreaViewModel(AreaModel area)
        {
            if(area is not null)
            {
                    List<EquipmentViewModel> equipment = null;
                    if (area.Equipment is not null)
                    {
                        equipment = new List<EquipmentViewModel>();
                        foreach (EquipmentViewModel equipmentVM in area.Equipment)
                        {
                            equipment.Add(equipmentVM);
                        }
                    }
                    return new AreaViewModel
                    {
                        Id = area.Id,
                        Name = area.Name,
                        Equipments = equipment
                    };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Used to convert a <see cref="AreaViewModel"/> to <see cref="AreaModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="areaVM">A <see cref="AreaViewModel"/> that represents what is coming from the View</param>
        public static implicit operator AreaModel(AreaViewModel areaVM)
        {
            return new AreaModel
            {
                Id = areaVM.Id,
                Name = areaVM.Name
            };
        }

        /// <summary>
        /// Id associated with an Area
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Area
        /// </summary>
        [Required]
        [DisplayName("Area Name")]
        public string Name { get; set; }

        /// <summary>
        /// Area Deactivated
        /// </summary>
        public DateTime? Deactivate { get; set; }

        /// <summary>
        /// List of <see cref="EquipmentViewModel"/> that the Area contains.
        /// </summary>
        public List<EquipmentViewModel>? Equipments { get; set; }
    }
}
