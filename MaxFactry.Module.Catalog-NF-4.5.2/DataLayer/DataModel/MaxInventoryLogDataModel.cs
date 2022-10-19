// <copyright file="MaxInventoryLogDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="7/29/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for catalog related information.
    /// </summary>
    public class MaxInventoryLogDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Id of inventory record that is changed
        /// </summary>
        public readonly string InventoryId = "InventoryId";

        /// <summary>
        /// Date and time the inventory was changed
        /// </summary>
        public readonly string ChangedDate = "ChangedDate";

        /// <summary>
        /// Username of user that is currently logged in that made the change
        /// </summary>
        public readonly string Username = "Username";

        /// <summary>
        /// User id of user that is currently logged in that made the change
        /// </summary>
        public readonly string UserId = "UserId";

        /// <summary>
        /// Id of item related to this inventory change
        /// </summary>
        public readonly string RelatedId = "RelatedId";

        /// <summary>
        /// Type of item related to this inventory change
        /// </summary>
        public readonly string RelatedItemType = "RelatedItemType";

        /// <summary>
        /// Type of amount of inventory being changed
        /// </summary>
        public readonly string AmountType = "AmountType";

        /// <summary>
        /// Inventory before change
        /// </summary>
        public readonly string AmountStart = "AmountStart";

        /// <summary>
        /// Inventory change amount
        /// </summary>
        public readonly string AmountChanged = "AmountChanged";

        /// <summary>
        /// Inventory after change
        /// </summary>
        public readonly string AmountEnd = "AmountEnd";

        /// <summary>
        /// Reason for inventory change
        /// </summary>
        public readonly string Reason = "Reason";

        /// <summary>
        /// Initializes a new instance of the MaxInventoryLogDataModel class
        /// </summary>
        public MaxInventoryLogDataModel()
        {
            this.SetDataStorageName("MaxInventoryLog");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.InventoryId, typeof(Guid));
            this.AddType(this.ChangedDate, typeof(DateTime));
            this.AddType(this.Username, typeof(string));
            this.AddType(this.UserId, typeof(Guid));
            this.AddType(this.RelatedId, typeof(Guid));
            this.AddType(this.RelatedItemType, typeof(int));
            this.AddType(this.AmountType, typeof(int));
            this.AddType(this.AmountStart, typeof(long));
            this.AddType(this.AmountChanged, typeof(long));
            this.AddType(this.AmountEnd, typeof(long));
            this.AddType(this.Reason, typeof(string));
        }
    }
}
