// <copyright file="MaxCartViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Manual Discount.  Added direct access to currently selected ShippingType entity.  Added ability to add CartItemViewModel to cart.">
// <change date="6/29/2015" author="Brian A. Lakstins" description="Centralize add to cart code.">
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
    public class MaxCartViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        private const string _sIdKey = "__MaxCatalogCartIdCurrent";

        private const string _sEntityKey = "__MaxCatalogCartEntityCurrent";

        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCartViewModel> _oSortedList = null;

        private List<MaxCartItemViewModel> _oItemList = null;

        private MaxShippingTypeEntity _oShippingTypeEntity = null;

        private List<MaxDiscountCodeViewModel> _oDiscountCodeList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCartViewModel class
        /// </summary>
        public MaxCartViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCartViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCartViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCartViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCartViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCartEntity.Create();
        }

        public override bool EntityLoad()
        {
            if (!base.EntityLoad())
            {
                if (!this.GetCurrent())
                {
                    this.Entity = MaxCartEntity.Create();
                }
            }

            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            if (loEntity.IsAvailable)
            {
                if (loEntity.Recalculate())
                {
                    if (Guid.Empty.Equals(loEntity.Id))
                    {
                        if (loEntity.ItemCount > 0)
                        {
                            loEntity.Insert();
                        }
                    }
                    else
                    {
                        loEntity.Update();
                    }
                }

                this.Id = loEntity.Id.ToString();
            }

            this._oItemList = null;
            this._oShippingTypeEntity = null;
            return true;
        }

        public string ItemTotal
        {
            get;
            set;
        }

        public string ItemCount
        {
            get;
            set;
        }

        public string Total
        {
            get;
            set;
        }

        public string ShippingTotal
        {
            get;
            set;
        }

        public int ShippingType
        {
            get;
            set;
        }

        public string CouponCode
        {
            get;
            set;
        }

        public List<string> CouponCodeList
        {
            get;
            set;
        }

        public string DiscountTotal
        {
            get;
            set;
        }

        public string DiscountTotalExplanation
        {
            get;
            set;
        }


        public string SubTotal
        {
            get;
            set;
        }

        public string ManualDiscount
        {
            get;
            set;
        }

        public string ManualDiscountExplanation
        {
            get;
            set;
        }

        public double TaxTotal
        {
            get;
            set;
        }

        public string TaxLocation
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public double TaxableAmount
        {
            get;
            set;
        }

        public int TaxOverride
        {
            get;
            set;
        }

        public MaxCartItemViewModel CartItemNew
        {
            get;
            set;
        }

        public MaxShippingTypeEntity ShippingTypeEntity
        {
            get
            {
                if (null == this._oShippingTypeEntity)
                {
                    MaxShippingTypeEntity loEntity = MaxShippingTypeEntity.Create();
                    if (loEntity.LoadByShippingType(this.ShippingType))
                    {
                        this._oShippingTypeEntity = loEntity;
                    }
                }

                return this._oShippingTypeEntity;
            }

        }

        public List<MaxShippingTypeEntity> ShippingTypeList
        {
            get
            {
                MaxCartEntity loCartEntity = this.Entity as MaxCartEntity;
                List<MaxShippingTypeEntity> loR = loCartEntity.GetShippingTypeAvailableList();
                this.ShippingType = loCartEntity.ShippingType;
                return loR;
            }
        }

        public List<MaxCartItemViewModel> ItemList
        {
            get
            {
                if (null == this._oItemList)
                {
                    this._oItemList = new List<MaxCartItemViewModel>();
                    MaxCartEntity loEntity = this.Entity as MaxCartEntity;
                    foreach (MaxCartItemEntity loItemEntity in loEntity.ItemList)
                    {
                        MaxCartItemViewModel loModel = new MaxCartItemViewModel(loItemEntity);
                        loModel.Load();
                        this._oItemList.Add(loModel);
                    }
                }

                return this._oItemList;
            }

            set
            {
                this._oItemList = value;
            }
        }

        public List<MaxDiscountCodeViewModel> DiscountCodeList
        {
            get
            {
                if (null == this._oDiscountCodeList)
                {
                    this._oDiscountCodeList = new List<MaxDiscountCodeViewModel>();
                    MaxCartEntity loEntity = this.Entity as MaxCartEntity;
                    foreach (MaxDiscountCodeEntity loDiscountCodeEntity in loEntity.DiscountCodeList)
                    {
                        MaxDiscountCodeViewModel loModel = new MaxDiscountCodeViewModel(loDiscountCodeEntity);
                        loModel.Load();
                        this._oDiscountCodeList.Add(loModel);
                    }
                }

                return this._oDiscountCodeList;
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCartViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCartViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCartViewModel loViewModel = new MaxCartViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCartViewModel> GetCurrentSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCartViewModel>();
                MaxCartEntity loEntity = this.Entity as MaxCartEntity;
                DateTime ldDate = DateTime.UtcNow.AddMonths(-1);
                MaxEntityList loList = loEntity.LoadAllByDate(ldDate);
                SortedList<string, MaxCartViewModel> loSortedList = new SortedList<string, MaxCartViewModel>();
                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    MaxCartEntity loCurrentEntity = loList[lnE] as MaxCartEntity;
                    if (loCurrentEntity.IsAvailable && loCurrentEntity.ItemCount > 0)
                    {
                        MaxCartViewModel loViewModel = new MaxCartViewModel(loList[lnE]);
                        loViewModel.Load();
                        loSortedList.Add(loCurrentEntity.GetDefaultSortString(), loViewModel);
                    }
                }

                this._oSortedList = new List<MaxCartViewModel>(loSortedList.Values);
                this._oSortedList.Reverse();
            }

            return this._oSortedList;
        }


        public string GetShipping(int lnShippingType)
        {
            double lnShipping = MaxShippingChargeEntity.Create().GetCartShipping((MaxCartEntity)this.Entity, lnShippingType);
            return string.Format("{0:C}", lnShipping);
        }

        public bool CartAdd(MaxProductViewModel loProductModel)
        {
            bool lbR = true;
            foreach (MaxOptionCombinationStructure loOptionCombination in loProductModel.OptionCombinationList)
            {
                if (!this.CartAdd(loProductModel, loOptionCombination))
                {
                    lbR = false;
                }
            }

            return lbR;
        }

        public bool CartAdd(MaxProductViewModel loProductModel, MaxOptionCombinationStructure loOptionCombination)
        {
            MaxCartItemViewModel loCartItemModel = new MaxCartItemViewModel(loProductModel, loOptionCombination);
            return this.CartAdd(loCartItemModel);
        }

        public bool CartAdd(MaxCartItemViewModel loCartItemView)
        {
            loCartItemView.ContainerId = this.Id;
            this.ItemList.Add(loCartItemView);
            return true;
        }

        public bool CartAdd(MaxCartItemEntity loCartItem)
        {
            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            if (loEntity.AddItem(loCartItem))
            {
                this.Save();
                this._oItemList = null;
                return true;
            }

            return false;
        }

        public bool DeleteItem(string lsId)
        {
            MaxCartItemViewModel loModel = new MaxCartItemViewModel(lsId);
            return loModel.Delete();
        }

        public bool CanPlaceOrder()
        {
            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            return loEntity.CanPlaceOrder();
        }

        public string GetCanPlaceOrderMessage()
        {
            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            return loEntity.GetCanPlaceOrderMessage();
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
                MaxCartEntity loEntity = this.Entity as MaxCartEntity;
                if (null != loEntity)
                {
                    loEntity.ShippingType = MaxConvertLibrary.ConvertToInt(typeof(object), this.ShippingType);
                    loEntity.TaxLocation = this.TaxLocation;
                    loEntity.TaxOverride = this.TaxOverride;
                    if (!string.IsNullOrEmpty(loEntity.TaxLocation))
                    {
                        loEntity.TaxTotal = 0;
                        loEntity.SalesTaxRate = 0;
                    }

                    if (!string.IsNullOrEmpty(this.CouponCode))
                    {
                        List<string> loCouponCodeList = new List<string>(loEntity.CouponCodeList);
                        string[] laCode = this.CouponCode.Split(new char[] { '|' });
                        foreach (string lsCode in laCode)
                        {
                            if (!loCouponCodeList.Contains(lsCode) && !lsCode.StartsWith("+"))
                            {
                                loCouponCodeList.Add(lsCode);
                            }
                        }

                        foreach (string lsCode in laCode)
                        {
                            if (lsCode.StartsWith("+"))
                            {
                                if (loCouponCodeList.Contains("!" + lsCode.Substring(1)))
                                {
                                    loCouponCodeList.Remove("!" + lsCode.Substring(1));
                                }
                            }
                        }

                        loEntity.CouponCodeList = loCouponCodeList.ToArray();
                    }

                    if (null != this.ManualDiscount)
                    {
                        loEntity.ManualDiscount = MaxConvertLibrary.ConvertToDouble(typeof(object), string.Format("{0:0.00}", this.ManualDiscount));
                        loEntity.ManualDiscountExplanation = this.ManualDiscountExplanation;
                    }

                    //// Add any new items to the cart
                    foreach (MaxCartItemViewModel loCartItemView in ItemList)
                    {
                        if (string.IsNullOrEmpty(loCartItemView.Id))
                        {
                            loEntity.AddItem(loCartItemView.GetEntity());
                        }
                    }

                    loEntity.Username = this.Username;
                    loEntity.UserId = this.UserId;

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
                MaxCartEntity loEntity = this.Entity as MaxCartEntity;
                if (null != loEntity)
                {
                    this.ItemCount = loEntity.ItemCount.ToString();

                    double lnItemTotal = loEntity.ItemTotal;
                    if (lnItemTotal < 0)
                    {
                        lnItemTotal = 0;
                    }
                    this.ItemTotal = string.Format("{0:C}", lnItemTotal);
                    this.DiscountTotal = string.Format("{0:C}", loEntity.DiscountTotal);
                    this.DiscountTotalExplanation = loEntity.DiscountTotalExplanation;
                    this.SubTotal = string.Format("{0:C}", loEntity.ItemTotal + loEntity.ShippingTotal);

                    double lnTotal = loEntity.Total;
                    if (lnTotal < 0)
                    {
                        lnTotal = 0;
                    }

                    this.Total = string.Format("{0:C}", lnTotal);
                    this.ShippingType = loEntity.ShippingType;

                    if (loEntity.ShippingTotal > 0)
                    {
                        this.ShippingTotal = string.Format("{0:C}", loEntity.ShippingTotal);
                    }
                    else if (loEntity.ShippingType <= 0)
                    {
                        this.ShippingTotal = string.Empty;
                        this.Total = "Select Shipping!";
                    }
                    else if (loEntity.ShippingTotal <= 0)
                    {
                        MaxShippingTypeEntity loShippingType = MaxShippingTypeEntity.Create();
                        loShippingType.LoadByShippingType(this.ShippingType);
                        double lnShippingAmount = 0;
                        if (!double.TryParse(loShippingType.ShippingCalculation, out lnShippingAmount))
                        {
                            this.ShippingTotal = loShippingType.ShippingCalculation;
                        }
                    }

                    this.CouponCodeList = new List<string>();
                    foreach (string lsCouponCode in loEntity.CouponCodeList)
                    {
                        this.CouponCodeList.Add(lsCouponCode);
                    }

                    if (loEntity.ManualDiscount > 0)
                    {
                        this.ManualDiscount = loEntity.ManualDiscount.ToString();
                        this.ManualDiscountExplanation = loEntity.ManualDiscountExplanation;
                    }

                    if (null == this.CartItemNew)
                    {
                        this.CartItemNew = new MaxCartItemViewModel();
                        this.CartItemNew.Quantity = 1;
                    }

                    this.TaxTotal = loEntity.TaxTotal;
                    this.TaxLocation = loEntity.TaxLocation;
                    this.TaxOverride = loEntity.TaxOverride;
                    this.TaxableAmount = loEntity.TaxableAmount;

                    this.OriginalValues.Add("ManualDiscount", this.ManualDiscount);
                    this.OriginalValues.Add("ManualDiscountExplanation", this.ManualDiscountExplanation);
                    this.OriginalValues.Add("ShippingType", this.ShippingType);

                    this.Username = loEntity.Username;
                    this.UserId = loEntity.UserId;

                    return true;
                }
            }

            return false;
        }

        public override bool Save()
        {
            bool lbR = false;
            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            if (null != loEntity)
            {
                //// Load an existing cart if one is specified in the Id
                this.EntityLoad(this.Id);
                if (this.MapToEntity())
                {
                    //loEntity.Recalculate();
                    if (loEntity.Id.Equals(Guid.Empty))
                    {
                        lbR = loEntity.Insert();
                    }
                    else
                    {
                        lbR = loEntity.Update();
                    }

                    if (lbR)
                    {
                        lbR = this.Load();
                    }
                }
            }

            return lbR;
        }

        public void SetCurrent()
        {
            MaxCartEntity loEntity = this.Entity as MaxCartEntity;
            if (loEntity.IsAvailable)
            {
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, loEntity);
                if (Guid.Empty != loEntity.Id)
                {
                    MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, loEntity.Id);
                    // Associate this profile Id with this cart as a reference.
                    if (Guid.Empty.Equals(loEntity.ReferenceId) || string.IsNullOrEmpty(loEntity.ReferenceType))
                    {
                        loEntity.ReferenceType = "Profile";
                        loEntity.ReferenceId = MaxConvertLibrary.ConvertToGuid(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, "Id"));
                        //// Make sure the only changed fields are updated.
                        MaxCartEntity loUpdate = MaxCartEntity.Create();
                        loUpdate.SetId(loEntity.Id);
                        loUpdate.ReferenceType = loEntity.ReferenceType;
                        loUpdate.ReferenceId = loEntity.ReferenceId;
                        loUpdate.Update();
                    }
                }
            }
        }

        public bool GetCurrent()
        {
            if (null != this.Id && this.Id.Equals("new", StringComparison.InvariantCultureIgnoreCase))
            {
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, null);
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, null);
                this.Id = null;
                return false;
            }

            object loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProcess, _sEntityKey);
            if (null != loObject && loObject is MaxCartEntity)
            {
                if (((MaxCartEntity)loObject).IsAvailable)
                {
                    this.Entity = (MaxCartEntity)loObject;
                    return true;
                }
            }

            // Look up the current id in the profile.
            loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, _sIdKey);
            if (null != loObject)
            {
                Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), loObject);
                if (!Guid.Empty.Equals(loId))
                {
                    MaxCartEntity loEntity = MaxCartEntity.Create();
                    if (loEntity.LoadByIdCache(loId))
                    {
                        if (loEntity.IsAvailable)
                        {
                            MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, loEntity);
                            this.Entity = loEntity;
                            return true;
                        }
                        else
                        {
                            MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, null);
                        }
                    }
                }
            }

            return false;
        }
    }
}
