// <copyright file="MaxDiscountCodeViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxDiscountCodeViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxDiscountCodeViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxDiscountCodeViewModel class
        /// </summary>
        public MaxDiscountCodeViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxDiscountCodeViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxDiscountCodeViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxDiscountCodeViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxDiscountCodeViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxDiscountCodeEntity.Create();
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Amount Off")]
        public double AmountOff { get; set; }
        [Display(Name = "Calculation Method")]
        public string Calculation { get; set; }
        [Display(Name = "Category")]
        public string CategoryIdListText { get; set; }
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Group")]
        public string Group { get; set; }
        [Display(Name = "Internal Note")]
        public string InternalNote { get; set; }
        [Display(Name = "Free Shipping")]
        public bool IsFreeShipping { get; set; }
        [Display(Name = "Use Always")]
        public bool IsUseAlways { get; set; }
        [Display(Name = "Maximum Amount")]
        public string MaximumAmount { get; set; }
        [Display(Name = "Maximum Quantity")]
        public string MaximumQuanitity { get; set; }
        [Display(Name = "Minimum Amount")]
        public string MinimumAmount { get; set; }
        [Display(Name = "Minimum Quantity")]
        public string MinimumQuantity { get; set; }
        [Display(Name = "Percent Off")]
        public double PercentOff { get; set; }
        [Display(Name = "Product Id")]
        public string ProductIdListText { get; set; }
        [Display(Name = "Product Sku")]
        public string ProductSkuListText { get; set; }
        [Display(Name = "Type")]
        public string ReferType { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Number of Uses")]
        public string UseCount { get; set; }
        [Display(Name = "Number Used")]
        public string UsedCount { get; set; }
        [Display(Name = "User Name")]
        public string UsernameListText { get; set; }
        [Display(Name = "User ID")]
        public string UserIdListText { get; set; }


        public string InvalidReasonCurrent { get; set; }

        public double DiscountAmountCurrent { get; set; }

        public double MatchAmountCurrent { get; set; }

        public int MatchQuantityCurrent { get; set; }

        public string ValidReasonCurrent { get; set; }

        public Dictionary<string, string> CalculationList
        {
            get
            {
                Dictionary<string, string> loList = new Dictionary<string, string>();
                loList.Add(MaxDiscountCodeEntity.CalculationCart.ToString(), "Cart");
                loList.Add(MaxDiscountCodeEntity.CalculationProduct.ToString(), "Product");
                return loList;
            }
        }

        public Dictionary<string, string> ReferTypeList
        {
            get
            {
                Dictionary<string, string> loList = new Dictionary<string, string>();
                loList.Add("Coupon", "Coupon");
                return loList;
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxDiscountCodeViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxDiscountCodeViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxDiscountCodeViewModel loViewModel = new MaxDiscountCodeViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
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
                MaxDiscountCodeEntity loEntity = this.Entity as MaxDiscountCodeEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    loEntity.AmountOff = this.AmountOff;
                    loEntity.Calculation = MaxConvertLibrary.ConvertToInt(typeof(object), this.Calculation);
                    loEntity.CategoryIdListText = this.CategoryIdListText;
                    loEntity.Code = this.Code;
                    loEntity.Description = this.Description;
                    loEntity.EndDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), this.EndDate);
                    loEntity.Group = this.Group;
                    loEntity.InternalNote = this.InternalNote;
                    loEntity.IsFreeShipping = MaxConvertLibrary.ConvertToBoolean(typeof(object), this.IsFreeShipping);
                    loEntity.IsUseAlways = MaxConvertLibrary.ConvertToBoolean(typeof(object), this.IsUseAlways);
                    loEntity.MaximumAmount = MaxConvertLibrary.ConvertToInt(typeof(object), this.MaximumAmount);
                    loEntity.MaximumQuanitity = MaxConvertLibrary.ConvertToInt(typeof(object), this.MaximumQuanitity);
                    loEntity.MinimumAmount = MaxConvertLibrary.ConvertToDouble(typeof(object), this.MinimumAmount);
                    loEntity.MinimumQuantity = MaxConvertLibrary.ConvertToInt(typeof(object), this.MinimumQuantity);
                    loEntity.PercentOff = this.PercentOff;
                    loEntity.ProductIdListText = this.ProductIdListText;
                    loEntity.ProductSkuListText = this.ProductSkuListText;
                    loEntity.ReferType = this.ReferType;
                    loEntity.StartDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), this.StartDate);
                    loEntity.UseCount = MaxConvertLibrary.ConvertToInt(typeof(object), this.UseCount);
                    loEntity.UsedCount = MaxConvertLibrary.ConvertToInt(typeof(object), this.UsedCount);
                    loEntity.UsernameListText = this.UsernameListText;
                    loEntity.UserIdListText = this.UserIdListText;
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
                MaxDiscountCodeEntity loEntity = this.Entity as MaxDiscountCodeEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.AmountOff = 0;
                    if (loEntity.AmountOff > 0)
                    {
                        this.AmountOff = loEntity.AmountOff;
                    }

                    if (loEntity.Calculation >= 0)
                    {
                        this.Calculation = loEntity.Calculation.ToString();
                    }

                    this.CategoryIdListText = loEntity.CategoryIdListText;
                    this.Code = loEntity.Code;
                    this.Description = loEntity.Description;
                    this.EndDate = loEntity.EndDate;
                    this.Group = loEntity.Group;
                    this.InternalNote = loEntity.InternalNote;
                    this.IsFreeShipping = loEntity.IsFreeShipping;
                    this.IsUseAlways = loEntity.IsUseAlways;
                    if (loEntity.MaximumAmount >= 0)
                    {
                        this.MaximumAmount = loEntity.MaximumAmount.ToString();
                    }

                    if (loEntity.MaximumQuanitity >= 0)
                    {
                        this.MaximumQuanitity = loEntity.MaximumQuanitity.ToString();
                    }

                    if (loEntity.MinimumAmount >= 0)
                    {
                        this.MinimumAmount = loEntity.MinimumAmount.ToString();
                    }

                    if (loEntity.MinimumQuantity >= 0)
                    {
                        this.MinimumQuantity = loEntity.MinimumQuantity.ToString();
                    }

                    this.PercentOff = 0;
                    if (loEntity.PercentOff >= 0)
                    {
                        this.PercentOff = loEntity.PercentOff;
                    }

                    this.ProductIdListText = loEntity.ProductIdListText;
                    this.ProductSkuListText = loEntity.ProductSkuListText;
                    this.ReferType = loEntity.ReferType;
                    this.StartDate = loEntity.StartDate;

                    if (loEntity.UseCount >= 0)
                    {
                        this.UseCount = loEntity.UseCount.ToString();
                    }

                    if (loEntity.UsedCount >= 0)
                    {
                        this.UsedCount = loEntity.UsedCount.ToString();
                    }

                    this.UsernameListText = loEntity.UsernameListText;
                    this.UserIdListText = loEntity.UserIdListText;

                    this.InvalidReasonCurrent = loEntity.InvalidReasonCurrent;
                    this.DiscountAmountCurrent = loEntity.DiscountAmountCurrent;
                    this.MatchAmountCurrent = loEntity.MatchAmountCurrent;
                    this.MatchQuantityCurrent = loEntity.MatchQuantityCurrent;
                    this.ValidReasonCurrent = loEntity.ValidReasonCurrent;
                    return true;
                }
            }

            return false;
        }
    }
}
