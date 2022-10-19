// <copyright file="MaxOrderItemDataModel.cs" company="Lakstins Family, LLC">
// Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// </copyright>

#region License
// <license>
// This software is provided 'as-is', without any express or implied warranty. In no 
// event will the author be held liable for any damages arising from the use of this 
// software.
//  
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to the 
// following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim that 
// you wrote the original software. If you use this software in a product, an 
// acknowledgment (see the following) in the product documentation is required.
// 
// Portions Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// 
// 2. Altered source versions must be plainly marked as such, and must not be 
// misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.
// </license>
#endregion

#region Change Log
// <changelog>
// <change date="7/13/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use PrimaryKey Suffix.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderShippingInfoDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Name of this shipment.
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Id of the Order this shipping information is associated
        /// </summary>
        public readonly string OrderId = "OrderId";
        
        /// <summary>
        /// Shipping address type information.
        /// </summary>
        public readonly string ShippingAddressType = "ShippingAddressType";

        /// <summary>
        /// Shipping address information.
        /// </summary>
        public readonly string ShippingAddressId = "ShippingAddressId";

        /// <summary>
        /// Shipping contact person type information.
        /// </summary>
        public readonly string ShippingContactPersonType = "ShippingContactPersonType";

        /// <summary>
        /// Shipping contact person.
        /// </summary>
        public readonly string ShippingContactPersonId = "ShippingContactPersonId";

        /// <summary>
        /// Method to be used to ship the order.
        /// </summary>
        public readonly string ShippingType = "ShippingType";

        /// <summary>
        /// Notes associated with shipping the order.
        /// </summary>
        public readonly string Notes = "Notes";

        /// <summary>
        /// Date order will be picked up.
        /// </summary>
        public readonly string PickupDate = "PickupDate";

        /// <summary>
        /// Approximate time order will be picked up.
        /// </summary>
        public readonly string PickupTime = "PickupTime";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxOrderShippingInfoDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderShippingInfo");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.OrderId, typeof(Guid));
            this.AddType(this.ShippingAddressType, typeof(MaxShortString));
            this.AddType(this.ShippingAddressId, typeof(Guid));
            this.AddType(this.ShippingContactPersonType, typeof(MaxShortString));
            this.AddType(this.ShippingContactPersonId, typeof(Guid));
            this.AddNullable(this.ShippingType, typeof(int));
            this.AddType(this.Notes, typeof(MaxLongString));
            this.AddType(this.PickupDate, typeof(DateTime));
            this.AddType(this.PickupTime, typeof(MaxShortString));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingInfoDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderShippingInfoDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }

        /// <summary>
        /// Gets a suffix for the primary key based on the data to speed up future queries
        /// </summary>
        /// <param name="loData">Data to use to create the suffix</param>
        /// <returns>String to use as suffix for primary key</returns>
        public override string GetPrimaryKeySuffix(MaxData loData)
        {
            string lsR = base.GetPrimaryKeySuffix(loData);
            if (string.IsNullOrEmpty(lsR))
            {
                lsR = loData.Get(this.OrderId).ToString();
            }

            return lsR;
        }
    }
}
