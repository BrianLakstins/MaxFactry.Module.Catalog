// <copyright file="MaxInventorySellDataModel.cs" company="Lakstins Family, LLC">
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
    public class MaxInventoryProductDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Sku for the product.
        /// </summary>
        public readonly string ProductSku = "ProductSku";

        /// <summary>
        /// Name of the product so not everyone needs to memorize skus
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Options associated with this inventory
        /// </summary>
        public readonly string OptionList = "OptionList";

        /// <summary>
        /// Supply Skus associated with this sales sku
        /// </summary>
        public readonly string SupplySkuList = "SupplySkuList";

        /// <summary>
        /// Amount on order
        /// </summary>
        public readonly string AmountOnOrder = "AmountOnOrder";

        /// <summary>
        /// Amount to alert when needing more supply
        /// </summary>
        public readonly string ReplenishAlertLevel = "ReplenishAlertLevel";

        /// <summary>
        /// Indicates how the units are divided
        /// </summary>
        public readonly string UnitOfMeasure = "UnitOfMeasure";

        /// <summary>
        /// Initializes a new instance of the MaxInventoryDataModel class
        /// </summary>
        public MaxInventoryProductDataModel()
        {
            this.SetDataStorageName("MaxInventoryProduct");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.ProductSku, typeof(MaxShortString));
            this.AddNullable(this.Name, typeof(string));
            this.AddNullable(this.OptionList, typeof(long));
            this.AddNullable(this.SupplySkuList, typeof(MaxLongString));
            this.AddNullable(this.AmountOnOrder, typeof(long));
            this.AddNullable(this.ReplenishAlertLevel, typeof(long));
            this.AddNullable(this.UnitOfMeasure, typeof(int));
        }
    }
}
