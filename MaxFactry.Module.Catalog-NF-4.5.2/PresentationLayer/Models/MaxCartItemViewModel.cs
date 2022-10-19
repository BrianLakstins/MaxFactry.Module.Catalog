// <copyright file="MaxCartItemViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Note.">
// <change date="6/29/2015" author="Brian A. Lakstins" description="Update to be preferred method to add new items to the cart.">
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
    public class MaxCartItemViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCartItemViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCartItemViewModel class
        /// </summary>
        public MaxCartItemViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCartItemViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCartItemViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCartItemViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCartItemViewModel(string lsId) 
        {
            this.CreateEntity();
            this.Id = lsId;
            if (this.EntityLoad())
            {
                //// Check for shipping in option and add it to base.  
                //// This should not be needed for any cart items created after 7/4/2015.
                MaxCartItemEntity loEntity = this.Entity as MaxCartItemEntity;
                if (loEntity.CreatedDate < new DateTime(2015, 7, 4))
                {
                    if (loEntity.ItemShipping <= 0)
                    {
                        foreach (MaxOptionByNameStructure loOption in loEntity.OptionList)
                        {
                            if (loOption.AdditionalShipping > 0)
                            {
                                loEntity.ItemShipping += loOption.AdditionalShipping;
                            }
                        }

                        if (loEntity.ItemShipping > 0)
                        {
                            loEntity.Update();
                        }
                    }
                }

                this.Load();
            }
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCartItemEntity.Create();
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCartItemViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCartItemViewModel(MaxProductViewModel loProductModel, MaxOptionCombinationStructure loOptionCombination)
        {
            this.Entity = MaxCartItemEntity.Create();
            this.PrimaryReferenceType = typeof(MaxProductEntity).ToString();
            this.PrimaryReferenceId = loProductModel.Id;
            this.Quantity = loOptionCombination.Quantity;
            this.Name = loProductModel.Name;
            this.PriceBase = loProductModel.PriceBase;
            this.PriceAdditional = 0;
            this.ItemPrice = loProductModel.PriceBase;
            this.PricePerText = loProductModel.PerText;
            this.Sku = loProductModel.Sku;
            this.ItemShipping = loProductModel.PriceBaseShipping;
            this.PriceByQuantityList = new List<BusinessLayer.MaxPriceByQuantityStructure>();
            foreach (BusinessLayer.MaxPriceByQuantityStructure loPriceByQuantity in loProductModel.PriceByQuantityList)
            {
                if (loPriceByQuantity.End > 0 && loPriceByQuantity.Price > 0)
                {
                    this.PriceByQuantityList.Add(loPriceByQuantity);
                }
            }
            //MaxProductImageViewModel loProductImageModel = new MaxProductImageViewModel(loProductModel.PrimaryImageId);
            //this.ImageUrl = loProductImageModel.ContentUrl;

            this.OptionList = new List<MaxOptionByNameStructure>();

            if (null != loOptionCombination.OptionPerNameList)
            {
                foreach (MaxOptionByNameStructure loOption in loProductModel.OptionByNameList)
                {
                    if (!string.IsNullOrEmpty(loOption.Option) && !string.IsNullOrEmpty(loOption.Name))
                    {
                        foreach (MaxOptionByNameStructure loOptionSelected in loOptionCombination.OptionPerNameList)
                        {
                            if (loOption.Option.Equals(loOptionSelected.Option) && 
                                loOption.Name.Equals(loOptionSelected.Name))
                            {
                                this.OptionList.Add(loOption);
                                if (!string.IsNullOrEmpty(loOption.SkuSuffix))
                                {
                                    if (this.Sku.Length > 0)
                                    {
                                        this.Sku += "-";
                                    }

                                    this.Sku += loOption.SkuSuffix;
                                }

                                if (loOption.AdditionalPrice > -1 * this.ItemPrice)
                                {
                                    this.PriceAdditional += loOption.AdditionalPrice;
                                }

                                if (loOption.AdditionalShipping > -1 * this.ItemPrice)
                                {
                                    this.ItemShipping += loOption.AdditionalShipping;
                                }
                            }
                        }
                          
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        public double PriceBase
        {
            get;
            set;
        }

        public double PriceAdditional
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

        [Required]
        public string Sku
        {
            get;
            set;
        }

        public List<MaxOptionByNameStructure> OptionList
        {
            get;
            set;
        }

        public double ItemShipping
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

        public string ContainerId
        {
            get;
            set;
        }

        public string PrimaryReferenceType
        {
            get;
            set;
        }

        public string PrimaryReferenceId
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

        public double ManualDiscountAmount
        {
            get;
            set;
        }

        public string ManualDiscountReason
        {
            get;
            set;
        }

        public long InventoryAmount
        {
            get
            {
                long lnR = MaxInventoryProductEntity.GetInventoryAmountAvailable(this.Sku);
                return lnR;
            }
        }

        public string InventoryMessage
        {
            get
            {
                string lsR = string.Empty;
                if (this.InventoryAmount > long.MinValue)
                {
                    lsR = "There are " + this.InventoryAmount.ToString() + " items in inventory";
                }


                return lsR;
            }
        }

        public int ShippingCalculationMultiplier
        {
            get;
            set;
        }

        public List<MaxPriceByQuantityStructure> PriceByQuantityList
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCartItemViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCartItemViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCartItemViewModel loViewModel = new MaxCartItemViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        /// <summary>
        /// Gets a new entity with the values mapped, 
        /// or gets the current entity.
        /// It DOES NOT get an updated entity.
        /// </summary>
        /// <returns></returns>
        public MaxCartItemEntity GetEntity()
        {
            this.EntityLoad();
            if (!this.IsEntityLoaded)
            {
                this.MapToEntity();
            }

            return this.Entity as MaxCartItemEntity;
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
                MaxCartItemEntity loEntity = this.Entity as MaxCartItemEntity;
                if (null != loEntity)
                {
                    loEntity.ContainerId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ContainerId);
                    loEntity.Quantity = MaxConvertLibrary.ConvertToInt(typeof(object), this.Quantity);
                    if (this.ManualDiscountAmount >= 0)
                    {
                        loEntity.ManualDiscountAmount = this.ManualDiscountAmount;
                    }

                    loEntity.ManualDiscountReason = this.ManualDiscountReason;

                    //// Only update properties of the entity if the name is being set.
                    if (!string.IsNullOrEmpty(this.Name))
                    {
                        loEntity.Name = this.Name;
                        loEntity.Sku = this.Sku;
                        loEntity.ItemPricePer = this.PricePerText;
                        loEntity.PriceBase = this.PriceBase;
                        loEntity.PriceAdditional = this.PriceAdditional;
                        loEntity.ItemPrice = this.ItemPrice;
                        loEntity.ItemShipping = this.ItemShipping;
                        loEntity.PrimaryReferenceType = this.PrimaryReferenceType;
                        loEntity.PrimaryReferenceId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.PrimaryReferenceId);
                        loEntity.ImageUrl = this.ImageUrl;
                        loEntity.OptionList = this.OptionList;
                        loEntity.ItemUrl = this.ItemUrl;
                        loEntity.Note = this.Note;
                        loEntity.PriceByQuantityList = this.PriceByQuantityList;
                        loEntity.ShippingCalculationMultiplier = this.ShippingCalculationMultiplier;
                    }

                    if (loEntity.Quantity < 0)
                    {
                        loEntity.Quantity = 0;
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
                MaxCartItemEntity loEntity = this.Entity as MaxCartItemEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.PriceBase = loEntity.PriceBase;
                    this.PriceAdditional = loEntity.PriceAdditional;
                    this.ItemPrice = loEntity.ItemPrice;
                    this.ItemTotal = loEntity.ItemTotal;
                    this.ImageUrl = loEntity.ImageUrl;
                    this.ItemUrl = loEntity.ItemUrl;
                    this.OptionList = loEntity.OptionList;
                    this.PricePerText = loEntity.ItemPricePer;
                    this.Quantity = loEntity.Quantity;
                    this.Sku = loEntity.Sku;
                    this.ContainerId = loEntity.ContainerId.ToString();
                    this.Note = loEntity.Note;
                    this.PriceByQuantityList = loEntity.PriceByQuantityList;
                    this.ShippingCalculationMultiplier = loEntity.ShippingCalculationMultiplier;
                    this.OriginalValues.Add("Quantity", this.Quantity);

                    if (loEntity.DiscountAmount > 0)
                    {
                        this.DiscountAmount = loEntity.DiscountAmount;
                        this.DiscountReason = loEntity.DiscountReason;
                    }
                    
                    if (loEntity.ManualDiscountAmount > 0)
                    {
                        this.ManualDiscountAmount = loEntity.ManualDiscountAmount;
                        this.DiscountAmount = loEntity.ManualDiscountAmount;
                        this.DiscountReason = loEntity.ManualDiscountReason;
                    }

                    this.ManualDiscountReason = loEntity.ManualDiscountReason;
                    this.OriginalValues.Add("ManualDiscountAmount", this.ManualDiscountAmount);
                    this.OriginalValues.Add("ManualDiscountReason", this.ManualDiscountReason);


                    return true;
                }
            }

            return false;
        }

        protected override bool EntityLoad(string lsId)
        {
            MaxCartItemEntity loEntity = this.Entity as MaxCartItemEntity;
            loEntity.ContainerId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ContainerId);
            return base.EntityLoad(lsId);
        }
    }
}
