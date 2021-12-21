using MaintenanceLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaintenanceWebsite.Models
{
    public class VendorViewModel
    {

        /// <summary>
        /// Used to convert <see cref="VendorModel"/> to <see cref="VendorViewModel"/>
        /// So Model coming from the database can be converted into the ViewModel which can be displayed in the view.
        /// </summary>
        /// <param name="vendor">A <see cref="VendorModel"/> that represents the Equipment info coming from the database</param>
        public static implicit operator VendorViewModel(VendorModel vendor)
        {
            return new VendorViewModel
            {
                Id = vendor.Id,
                Name = vendor.Name
            };
        }

        /// <summary>
        /// Used to convert <see cref="VendorViewModel"/> to <see cref="VendorModel"/>
        /// So ViewModel coming from the view can be converted into the Model which can be saved to the database
        /// </summary>
        /// <param name="vendorVM">A <see cref="VendorViewModel"/> represents what is coming from the View</param>
        public static implicit operator VendorModel(VendorViewModel vendorVM)
        {
            return new VendorModel
            {
                Id = vendorVM.Id,
                Name = vendorVM.Name
            };
        }


        /// <summary>
        /// Id of Area
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Vendor
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; }

    }
}
