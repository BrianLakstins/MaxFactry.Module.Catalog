// <copyright file="MaxCartItemDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="7/8/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/29/2015" author="Brian A. Lakstins" description="Add Note.">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use PrimaryKey Suffix.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for cart related information.
    /// </summary>
    public abstract class MaxProductSelectionDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Id of the Cart or Order this ProductSelection belongs to.
        /// </summary>
        public readonly string ContainerId = "ContainerId";

        /// <summary>
        /// Name of the item
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Sku of the item
        /// </summary>
        public readonly string Sku = "Sku";
        
        /// <summary>
        /// Id of the object this cart item is based on
        /// </summary>
        public readonly string PrimaryReferenceId = "PrimaryReferenceId";

        /// <summary>
        /// Type of item this cart item is based on
        /// </summary>
        public readonly string PrimaryReferenceType = "PrimaryReferenceType";

        /// <summary>
        /// Quantity of this cart item
        /// </summary>
        public readonly string Quantity = "Quantity";

        /// <summary>
        /// Price of just one of this cart item
        /// </summary>
        public readonly string ItemPrice = "ItemPrice";

        /// <summary>
        /// Text associated with the single price
        /// </summary>
        public readonly string ItemPricePer = "ItemPricePer";

        /// <summary>
        /// Total associated with this cart item
        /// </summary>
        public readonly string ItemTotal = "ItemTotal";

        /// <summary>
        /// Extra shipping charges associated with this cart item
        /// </summary>
        public readonly string ItemShipping = "ItemShipping";

        /// <summary>
        /// Options selected for this item.
        /// </summary>
        public readonly string OptionSelectionList = "OptionSelectionList";

        /// <summary>
        /// Url to use to show image in the cart.
        /// </summary>
        public readonly string ImageUrl = "ImageUrl";

        /// <summary>
        /// Url to use to show the original item that went into the cart.
        /// </summary>
        public readonly string ItemUrl = "ItemUrl";

        /// <summary>
        /// Url to use to show the original item that went into the cart.
        /// </summary>
        public readonly string Note = "Note";

        /// <summary>
        /// Amount of discount for the item.
        /// </summary>
        public readonly string DiscountAmount = "DiscountAmount";

        /// <summary>
        /// Notice about the discount for the item.
        /// </summary>
        public readonly string DiscountReason = "DiscountReason";

        /// <summary>
        /// List of discount codes that give a discount on this item.
        /// </summary>
        public readonly string DiscountCodeListText = "DiscountCodeListText";

        /// <summary>
        /// Amount of discount for the item.
        /// </summary>
        public readonly string ManualDiscountAmount = "ManualDiscountAmount";

        /// <summary>
        /// Notice about the discount for the item.
        /// </summary>
        public readonly string ManualDiscountReason = "ManualDiscountReason";
        
        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxProductSelectionDataModel()
        {
            this.SetDataStorageName("MaxCatalogProductSelection");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.ContainerId, typeof(Guid));
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.Sku, typeof(MaxShortString));
            this.AddType(this.PrimaryReferenceId, typeof(Guid));
            this.AddType(this.PrimaryReferenceType, typeof(MaxShortString));
            this.AddType(this.Quantity, typeof(int));
            this.AddType(this.ItemPrice, typeof(double));
            this.AddType(this.ItemPricePer, typeof(MaxShortString));
            this.AddType(this.ItemTotal, typeof(double));
            this.AddType(this.ItemShipping, typeof(double));
            this.AddType(this.OptionSelectionList, typeof(string));
            this.AddType(this.ImageUrl, typeof(string));
            this.AddType(this.ItemUrl, typeof(string));
            this.AddType(this.Note, typeof(string));
            this.AddNullable(this.DiscountAmount, typeof(double));
            this.AddNullable(this.DiscountReason, typeof(string));
            this.AddNullable(this.DiscountCodeListText, typeof(string));
            this.AddNullable(this.DiscountAmount, typeof(double));
            this.AddNullable(this.DiscountReason, typeof(string));
            this.AddNullable(this.ManualDiscountAmount, typeof(double));
            this.AddNullable(this.ManualDiscountReason, typeof(string));
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
                lsR = loData.Get(this.ContainerId).ToString();
            }

            return lsR;
        }
    }
}
