// <copyright file="MaxOrderItemEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="3/7/2018" author="Brian A. Lakstins" description="Updates cart item mapping.">
// <change date="3/13/2018" author="Brian A. Lakstins" description="Add one more property to cart item mapping.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderItemEntity : MaxProductSelectionEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxOrderItemEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxOrderItemEntity(MaxData loData) : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderItemEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderItemEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string SecondaryReferenceType
        {
            get
            {
                return this.GetString(this.DataModel.SecondaryReferenceType);
            }

            set
            {
                this.Set(this.DataModel.SecondaryReferenceType, value);
            }
        }

        public Guid SecondaryReferenceId
        {
            get
            {
                return this.GetGuid(this.DataModel.SecondaryReferenceId);
            }

            set
            {
                this.Set(this.DataModel.SecondaryReferenceId, value);
            }
        }

        public string ThirdReferenceType
        {
            get
            {
                return this.GetString(this.DataModel.ThirdReferenceType);
            }

            set
            {
                this.Set(this.DataModel.ThirdReferenceType, value);
            }
        }

        public Guid ThirdReferenceId
        {
            get
            {
                return this.GetGuid(this.DataModel.ThirdReferenceId);
            }

            set
            {
                this.Set(this.DataModel.ThirdReferenceId, value);
            }
        }

        public bool IsFound
        {
            get { return this.GetBoolean("IsFound"); }
            set { this.Set("IsFound", value); }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderItemDataModel DataModel
        {
            get
            {
                return (MaxOrderItemDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderItemEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderItemEntity),
                typeof(MaxOrderItemDataModel)) as MaxOrderItemEntity;
        }

        public void MapCartItem(MaxCartItemEntity loCartItem)
        {
            this.PrimaryReferenceId = loCartItem.PrimaryReferenceId;
            this.PrimaryReferenceType = loCartItem.PrimaryReferenceType;
            this.SecondaryReferenceId = loCartItem.Id;
            this.SecondaryReferenceType = "CartItem";
            this.ThirdReferenceId = loCartItem.ContainerId;
            this.ThirdReferenceType = "Cart";
            this.ItemShipping = loCartItem.ItemShipping;
            this.ItemPrice = loCartItem.ItemPrice;
            this.ItemTotal = loCartItem.ItemTotal;
            this.Name = loCartItem.Name;
            this.Quantity = loCartItem.Quantity;
            this.ImageUrl = loCartItem.ImageUrl;
            this.Sku = loCartItem.Sku;
            this.OptionList = loCartItem.OptionList;
            this.ImageUrl = loCartItem.ImageUrl;
            this.ItemUrl = loCartItem.ItemUrl;
            this.Note = loCartItem.Note;

            this.DiscountAmount = loCartItem.DiscountAmount;
            this.DiscountReason = loCartItem.DiscountReason;
            this.ManualDiscountAmount = loCartItem.ManualDiscountAmount;
            this.ManualDiscountReason = loCartItem.ManualDiscountReason;

            this.PriceAdditional = loCartItem.PriceAdditional;
            this.PriceBase = loCartItem.PriceBase;
            this.PriceByQuantityList = loCartItem.PriceByQuantityList;

            this.ShippingCalculationMultiplier = loCartItem.ShippingCalculationMultiplier;
        }
    }
}
