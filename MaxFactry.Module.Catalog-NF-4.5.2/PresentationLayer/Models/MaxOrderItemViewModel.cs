// <copyright file="MaxOrderItemViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="4/4/2015" author="Brian A. Lakstins" description="Initial creation.">
// <change date="6/29/2015" author="Brian A. Lakstins" description="Add Note">
// <change date="4/14/2016" author="Brian A. Lakstins" description="Fix issue with manual discount reason on product.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxOrderItemViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderItemViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderItemViewModel class
        /// </summary>
        public MaxOrderItemViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderItemViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderItemViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderItemViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderItemViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderItemEntity.Create();
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public double ItemPrice
        {
            get;
            set;
        }

        public double ItemTotal
        {
            get;
            set;
        }

        public string PricePerText
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }

        public string ImageUrl
        {
            get;
            set;
        }

        public string ItemUrl
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

        public string Sku
        {
            get;
            set;
        }

        public string CartItemId
        {
            get;
            set;
        }

        public List<MaxOptionByNameStructure> OptionList
        {
            get;
            set;
        }

        public double DiscountAmount
        {
            get;
            set;
        }

        public string DiscountReason
        {
            get;
            set;
        }


        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderItemViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderItemViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderItemViewModel loViewModel = new MaxOrderItemViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public MaxOrderItemEntity GetEntity()
        {
            if (this.MapToEntity())
            {
                return this.Entity as MaxOrderItemEntity;
            }

            return MaxOrderItemEntity.Create();
        }

        /// <summary>
        /// Loads the entity based on the Id property.
        /// Maps the current values of properties in the ViewModel to the Entity.
        /// </summary>
        /// <returns>True if successful. False if it cannot be mapped.</returns>
        protected override bool MapToEntity()
        {
            if (base.MapToEntity())
            {
                MaxOrderItemEntity loEntity = this.Entity as MaxOrderItemEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    if (this.Quantity > 0)
                    {
                        loEntity.Quantity = this.Quantity;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the properties of the Entity to the properties of the ViewModel.
        /// </summary>
        /// <returns>True if the entity exists.</returns>
        protected override bool MapFromEntity()
        {
            if (base.MapFromEntity())
            {
                MaxOrderItemEntity loEntity = this.Entity as MaxOrderItemEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.ItemPrice = loEntity.ItemPrice;
                    this.ItemTotal = loEntity.ItemTotal;
                    this.ImageUrl = loEntity.ImageUrl;
                    this.ItemUrl = loEntity.ItemUrl;
                    this.OptionList = loEntity.OptionList;
                    this.PricePerText = loEntity.ItemPricePer;
                    this.Quantity = loEntity.Quantity;
                    this.Sku = loEntity.Sku;
                    this.Note = loEntity.Note;
                    this.DiscountAmount = 0;
                    if (loEntity.DiscountAmount > 0)
                    {
                        this.DiscountAmount = loEntity.DiscountAmount;
                        this.DiscountReason = loEntity.DiscountReason;
                    }
                    if (loEntity.ManualDiscountAmount > 0)
                    {
                        this.DiscountAmount = loEntity.ManualDiscountAmount;
                        this.DiscountReason = loEntity.ManualDiscountReason;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
