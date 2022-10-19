// <copyright file="MaxInventorySupplyDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="9/2/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for catalog related information.
    /// </summary>
    public class MaxInventorySupplyDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Sku for the supply.
        /// </summary>
        public readonly string SupplySku = "SupplySku";

        /// <summary>
        /// Name of the product so not everyone needs to memorize skus
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Options associated with this inventory
        /// </summary>
        public readonly string OptionList = "OptionList";

        /// <summary>
        /// Id of location where this inventory is
        /// </summary>
        public readonly string SiteId = "SiteId";

        /// <summary>
        /// Text description of where the product is stored
        /// </summary>
        public readonly string Location = "Location";

        /// <summary>
        /// Amount in inventory
        /// </summary>
        public readonly string AmountCurrent = "AmountCurrent";

        /// <summary>
        /// Amount purchased from supplier in transit to add to current inventory
        /// </summary>
        public readonly string AmountReplenish = "AmountReplenish";

        /// <summary>
        /// Amount that alerts reorder process current is below
        /// </summary>
        public readonly string ReplenishAlertLevel = "ReplenishAlertLevel";

        /// <summary>
        /// Number of business days is takes to get more inventory
        /// </summary>
        public readonly string LeadTime = "LeadTime";

        /// <summary>
        /// Indicates how the units are divided
        /// </summary>
        public readonly string UnitOfMeasure = "UnitOfMeasure";

        /// <summary>
        /// Id of the Vendor
        /// </summary>
        public readonly string VendorId = "VendorId";

        /// <summary>
        /// Sku for this Vendor
        /// </summary>
        public readonly string VendorSku = "VendorSku";

        /// <summary>
        /// Id of the Manufacturer
        /// </summary>
        public readonly string ManufacturerId = "ManufacturerId";

        /// <summary>
        /// Sku for this Manufacturer
        /// </summary>
        public readonly string ManufacturerSku = "ManufacturerSku";

        /// <summary>
        /// Initializes a new instance of the MaxInventoryDataModel class
        /// </summary>
        public MaxInventorySupplyDataModel()
        {
            this.SetDataStorageName("MaxInventorySupply");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.SupplySku, typeof(MaxShortString));
            this.AddNullable(this.Name, typeof(string));
            this.AddNullable(this.OptionList, typeof(long));
            this.AddNullable(this.SiteId, typeof(Guid));
            this.AddNullable(this.Location, typeof(string));
            this.AddNullable(this.AmountCurrent, typeof(long));
            this.AddNullable(this.AmountReplenish, typeof(long));
            this.AddNullable(this.ReplenishAlertLevel, typeof(long));
            this.AddNullable(this.LeadTime, typeof(int));
            this.AddNullable(this.UnitOfMeasure, typeof(MaxShortString));
            this.AddNullable(this.VendorId, typeof(Guid));
            this.AddNullable(this.VendorSku, typeof(MaxShortString));
            this.AddNullable(this.ManufacturerId, typeof(Guid));
            this.AddNullable(this.ManufacturerSku, typeof(MaxShortString));
        }
    }
}
