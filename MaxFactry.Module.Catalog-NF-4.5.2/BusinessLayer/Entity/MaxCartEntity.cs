// <copyright file="MaxCartEntity.cs" company="Lakstins Family, LLC">
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
// <change date="7/9/2014" author="Brian A. Lakstins" description="Change CartType to ReferenceType.  Remove properties to store them as CartItem instead.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="5/27/2015" author="Brian A. Lakstins" description="Added Manual Discount.  TTD Specific recalculation including automatic shipping type selection.">
// <change date="6/8/2015" author="Brian A. Lakstins" description="Updated to improve performance.">
// <change date="4/20/2016" author="Brian A. Lakstins" description="Updated to use centralized caching.">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use centralized StorageKey handling.">
// <change date="6/7/2017" author="Brian A. Lakstins" description="Added sales tax override.">
// <change date="9/25/2018" author="Brian A. Lakstins" description="Check products and catalog to make sure they are active before allowing ordering.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Updated for new special shipping type that is free.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxCartEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private List<MaxProductSelectionEntity> _oItemList = null;

        private string[] _aCouponCodeList = null;

        private const string _sIdKey = "MaxCatalogCartIdCurrent";

        private const string _sEntityKey = "MaxCatalogCartEntityCurrent";

        private const string _sReferenceUserKey = "MaxCurrentCartUser";

        private const string _sReferenceProfileKey = "MaxCurrentCartProfile";

        private List<MaxDiscountCodeEntity> _oDiscountCodeList = null;

        public const double SalesTaxRateIN = 0.07;

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxCartEntity(MaxData loData) : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCartEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxCartEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public override bool Load(MaxData loData)
        {
            this._oItemList = null;
            this._aCouponCodeList = null;
            return base.Load(loData);
        }

        public string ReferenceType
        {
            get
            {
                return this.GetString(this.DataModel.ReferenceType);
            }

            set
            {
                this.Set(this.DataModel.ReferenceType, value);
            }
        }

        public Guid ReferenceId
        {
            get
            {
                return this.GetGuid(this.DataModel.ReferenceId);
            }

            set
            {
                this.Set(this.DataModel.ReferenceId, value);
            }
        }

        public double ItemTotal
        {
            get
            {
                return this.GetDouble(this.DataModel.ItemTotal);
            }

            set
            {
                this.Set(this.DataModel.ItemTotal, value);
            }
        }

        public double DiscountTotal
        {
            get
            {
                return this.GetDouble(this.DataModel.DiscountTotal);
            }

            set
            {
                this.Set(this.DataModel.DiscountTotal, value);
            }
        }

        public string DiscountTotalExplanation
        {
            get
            {
                return this.GetString(this.DataModel.DiscountTotalExplanation);
            }

            set
            {
                this.Set(this.DataModel.DiscountTotalExplanation, value);
            }
        }

        public double ShippingTotal
        {
            get
            {
                return this.GetDouble(this.DataModel.ShippingTotal);
            }

            set
            {
                this.Set(this.DataModel.ShippingTotal, value);
            }
        }

        public bool IsShippingTotalOverride
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsShippingTotalOverride);
            }

            set
            {
                this.Set(this.DataModel.IsShippingTotalOverride, value);
            }
        }

        public double Total
        {
            get
            {
                return this.GetDouble(this.DataModel.Total);
            }

            set
            {
                this.Set(this.DataModel.Total, value);
            }
        }

        public int ItemCount
        {
            get
            {
                return this.GetInt(this.DataModel.ItemCount);
            }

            set
            {
                this.Set(this.DataModel.ItemCount, value);
            }
        }

        public int ShippingType
        {
            get
            {
                return this.GetInt(this.DataModel.ShippingType);
            }

            set
            {
                this.Set(this.DataModel.ShippingType, value);
            }
        }

        public double ManualDiscount
        {
            get
            {
                double lnR = this.GetDouble(this.DataModel.ManualDiscount);
                if (lnR < 0)
                {
                    lnR = 0;
                }

                return lnR;
            }

            set
            {
                this.Set(this.DataModel.ManualDiscount, value);
            }
        }

        public string ManualDiscountExplanation
        {
            get
            {
                return this.GetString(this.DataModel.ManualDiscountExplanation);
            }

            set
            {
                this.Set(this.DataModel.ManualDiscountExplanation, value);
            }
        }

        public double SalesTaxRate
        {
            get
            {
                return this.GetDouble(this.DataModel.SalesTaxRate);
            }

            set
            {
                this.Set(this.DataModel.SalesTaxRate, value);
            }
        }

        public double TaxTotal
        {
            get
            {
                return this.GetDouble(this.DataModel.TaxTotal);
            }

            set
            {
                this.Set(this.DataModel.TaxTotal, value);
            }
        }

        public string TaxLocation
        {
            get
            {
                return this.GetString(this.DataModel.TaxLocation);
            }

            set
            {
                this.Set(this.DataModel.TaxLocation, value);
            }
        }

        public string Username
        {
            get
            {
                return this.GetString(this.DataModel.Username);
            }

            set
            {
                this.Set(this.DataModel.Username, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetString(this.DataModel.UserId);
            }

            set
            {
                this.Set(this.DataModel.UserId, value);
            }
        }

        public double TaxableAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.TaxableAmount);
            }

            set
            {
                this.Set(this.DataModel.TaxableAmount, value);
            }
        }

        public int TaxOverride
        {
            get
            {
                return this.GetInt(this.DataModel.TaxOverride);
            }

            set
            {
                this.Set(this.DataModel.TaxOverride, value);
            }
        }

        public string[] CouponCodeList
        {
            get
            {
                if (null == this._aCouponCodeList)
                {
                    this._aCouponCodeList = this.GetObject(this.DataModel.CouponCodeList, typeof(string[])) as string[];
                    if (null == this._aCouponCodeList)
                    {
                        this._aCouponCodeList = new string[0];
                    }
                }

                return this._aCouponCodeList;
            }

            set
            {
                this._aCouponCodeList = value;
                this._oDiscountCodeList = null;
            }
        }

        public bool IsAvailable
        {
            get
            {
                bool lbR = !this.IsActive;
                if (lbR)
                {
                    lbR = !this.IsDeleted;
                    if (lbR)
                    {
                        //// Cart can no longer be current if it has not been updated in over a month
                        if (this.LastUpdateDate.AddMonths(1) < DateTime.UtcNow)
                        {
                            lbR = false;
                        }
                    }
                }

                return lbR;
            }

            set
            {
                this.Set(this.DataModel.IsActive, !value);
            }
        }

        public List<MaxProductSelectionEntity> ItemList
        {
            get
            {
                if (null == this._oItemList)
                {
                    if (Guid.Empty.Equals(this.Id))
                    {
                        this._oItemList = new List<MaxProductSelectionEntity>();
                    }
                    else
                    {
                        SortedList<string, MaxProductSelectionEntity> loSortedList = new SortedList<string, MaxProductSelectionEntity>();
                        MaxEntityList loList = this.LoadAllItem();
                        for (int lnE = 0; lnE < loList.Count; lnE++)
                        {
                            MaxProductSelectionEntity loEntity = loList[lnE] as MaxProductSelectionEntity;
                            if (loEntity.Quantity > 0)
                            {
                                loSortedList.Add(loEntity.GetDefaultSortString(), loEntity);
                            }
                        }

                        this._oItemList = new List<MaxProductSelectionEntity>(loSortedList.Values);
                    }
                }

                return this._oItemList;
            }

            set
            {
                this._oItemList = value;
            }
        }

        public List<MaxDiscountCodeEntity> DiscountCodeList
        {
            get
            {
                if (null == this._oDiscountCodeList)
                {
                    this._oDiscountCodeList = new List<MaxDiscountCodeEntity>();
                    //// Get all codes that are always used.
                    MaxEntityList loDiscountCodeEntityList = MaxDiscountCodeEntity.Create().LoadAllUseAlways();
                    List<string> loDiscountCodeAutoList = new List<string>();
                    for (int lnE = 0; lnE < loDiscountCodeEntityList.Count; lnE++)
                    {
                        if (((MaxDiscountCodeEntity)loDiscountCodeEntityList[lnE]).CheckValid())
                        {
                            this._oDiscountCodeList.Add(loDiscountCodeEntityList[lnE] as MaxDiscountCodeEntity);
                            loDiscountCodeAutoList.Add(((MaxDiscountCodeEntity)loDiscountCodeEntityList[lnE]).Code);
                        }
                    }

                    List<string> loDiscountCodeCartList = new List<string>();
                    //// Remove any duplicate manual codes
                    foreach (string lsCode in this.CouponCodeList)
                    {
                        if (!loDiscountCodeCartList.Contains(lsCode) && !loDiscountCodeAutoList.Contains(lsCode))
                        {
                            loDiscountCodeCartList.Add(lsCode);
                        }
                    }

                    this._aCouponCodeList = loDiscountCodeCartList.ToArray();

                    //// Get all entities for codes that have been added manually
                    foreach (string lsCode in loDiscountCodeCartList)
                    {
                        if (!lsCode.StartsWith("!"))
                        {
                            loDiscountCodeEntityList = MaxDiscountCodeEntity.Create().LoadAllByCode(lsCode);
                            for (int lnE = 0; lnE < loDiscountCodeEntityList.Count; lnE++)
                            {
                                ((MaxDiscountCodeEntity)loDiscountCodeEntityList[lnE]).CheckValid();
                                this._oDiscountCodeList.Add(loDiscountCodeEntityList[lnE] as MaxDiscountCodeEntity);
                            }

                            if (loDiscountCodeEntityList.Count == 0)
                            {
                                MaxDiscountCodeEntity loEntity = MaxDiscountCodeEntity.Create();
                                loEntity.Code = lsCode;
                                loEntity.InvalidReasonCurrent = "Code not found.";
                            }
                        }
                    }

                    //// Mark any entities that have been manually removed.
                    foreach (string lsCode in loDiscountCodeCartList)
                    {
                        if (lsCode.StartsWith("!"))
                        {
                            foreach (MaxDiscountCodeEntity loEntity in this._oDiscountCodeList)
                            {
                                if ("!" + loEntity.Code == lsCode)
                                {
                                    loEntity.InvalidReasonCurrent = "Manually removed.";
                                }
                            }
                        }
                    }
                }

                return this._oDiscountCodeList;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxCartDataModel DataModel
        {
            get
            {
                return (MaxCartDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxCartEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxCartEntity),
                typeof(MaxCartDataModel)) as MaxCartEntity;
        }

        public override bool Insert()
        {
            this.ArchiveAbandoned();
            if (null != this._aCouponCodeList)
            {
                this.SetObject(this.DataModel.CouponCodeList, this._aCouponCodeList);
            }

            bool lbR = base.Insert();
            if (null != this._oItemList)
            {
                foreach (MaxProductSelectionEntity loItemEntity in this._oItemList)
                {
                    loItemEntity.ContainerId = this.Id;
                    loItemEntity.Insert();
                }
            }

            string lsCacheDataKey = this.GetType() + this.StorageKey + ".LoadById" + this.Id.ToString();
            MaxCacheRepository.Set(this.GetType(), lsCacheDataKey, this.Data);
            return lbR;
        }

        public override bool Update()
        {
            if (null != this._aCouponCodeList)
            {
                this.SetObject(this.DataModel.CouponCodeList, this._aCouponCodeList);
            }

            bool lbR = base.Update();
            if (null != this._oItemList)
            {
                //// Only add new products that are in the cart to the database.
                foreach (MaxProductSelectionEntity loItemEntity in this._oItemList)
                {
                    if (loItemEntity.Id.Equals(Guid.Empty))
                    {
                        loItemEntity.ContainerId = this.Id;
                        loItemEntity.Insert();
                    }
                }
            }

            return lbR;
        }

        public virtual double GetSalesTaxRate()
        {
            if (this.TaxOverride == 2)
            {
                return 0;
            }
            else if (this.TaxOverride == 1)
            {
                return SalesTaxRateIN;
            }

            if (this.SalesTaxRate > 0)
            {
                return this.SalesTaxRate;
            }

            if (this.TaxLocation.Equals("IN", StringComparison.InvariantCultureIgnoreCase))
            {
                return SalesTaxRateIN;
            }

            return 0;
        }

        public virtual List<MaxShippingTypeEntity> GetShippingTypeAvailableList()
        {
            MaxEntityList loList = MaxShippingTypeEntity.Create().LoadAll();
            List<MaxShippingTypeEntity> loR = new List<MaxShippingTypeEntity>();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                loR.Add(loList[lnE] as MaxShippingTypeEntity);
            }

            if (MaxClientRepository.IsTractorToolsDirect(this.Data))
            {
                List<string> loSkuList = new List<string>(new string[] { "te8wrtt", "te9wrtt", "ml300", "BV1250PF".ToLower(), "BV2000PF".ToLower() });
                bool lbIsRestricted = false;
                double lnShippingTotal = 0;
                int lnShippingType = this.ShippingType;

                foreach (MaxCartItemEntity loItem in this.ItemList)
                {
                    if (loSkuList.Contains(loItem.Sku.ToLower()))
                    {
                        lbIsRestricted = true;
                    }
                }

                MaxShippingChargeEntity loShippingChargeEntity = MaxShippingChargeEntity.Create();
                lnShippingTotal = loShippingChargeEntity.GetCartShipping(this, this.ShippingType);
                double lnProductShipping = loShippingChargeEntity.GetProductShipping(this);
                if (lnProductShipping >= 10000)
                {
                    //// Item in cart is over 10000, so shipping needs to be arranged.
                    //// Limit to only arrange type
                    foreach (MaxShippingTypeEntity loShippingType in loR)
                    {
                        if (loShippingType.ShippingType != MaxShippingTypeEntity.ShippingTypeArrange)
                        {
                            loShippingType.IsSelectable = false;
                        }
                    }

                    this.ShippingType = MaxShippingTypeEntity.ShippingTypeArrange;
                }
                else if (lbIsRestricted)
                {
                    //// Limit to commercial and pickup if there are restricted items
                    foreach (MaxShippingTypeEntity loShippingType in loR)
                    {
                        if (loShippingType.ShippingType != MaxShippingTypeEntity.ShippingTypePickup &&
                            loShippingType.ShippingType != MaxShippingTypeEntity.ShippingTypeCommercial)
                        {
                            loShippingType.IsSelectable = false;
                        }
                    }

                    if (this.ShippingType != MaxShippingTypeEntity.ShippingTypePickup &&
                        this.ShippingType != MaxShippingTypeEntity.ShippingTypeCommercial)
                    {
                        this.ShippingType = 0;
                    }
                }
                else if (lnProductShipping <= 0)
                {
                    //// Limit results to the only 2 that are free to be selectable
                    foreach (MaxShippingTypeEntity loShippingType in loR)
                    {
                        if (loShippingType.ShippingType != MaxShippingTypeEntity.ShippingTypePickup &&
                            loShippingType.ShippingType != MaxShippingTypeEntity.ShippingTypeFree)
                        {
                            loShippingType.IsSelectable = false;
                        }
                    }

                    if (this.ShippingType != MaxShippingTypeEntity.ShippingTypeFree &&
                        this.ShippingType != MaxShippingTypeEntity.ShippingTypePickup)
                    {
                        this.ShippingType = MaxShippingTypeEntity.ShippingTypeFree;
                    }
                }
                else
                {
                    //// Remove free standard shipping
                    foreach (MaxShippingTypeEntity loShippingType in loR)
                    {
                        if (loShippingType.ShippingType == MaxShippingTypeEntity.ShippingTypeFree)
                        {
                            loShippingType.IsSelectable = false;
                        }
                    }

                    if (this.ShippingType == MaxShippingTypeEntity.ShippingTypeArrange ||
                        this.ShippingType == MaxShippingTypeEntity.ShippingTypeFree)
                    {
                        //// Shipping is greater than zero and was either Arrange or Free, so reset the shipping so it has to be selected.
                        this.ShippingType = 0;
                    }
                }
            }

            return loR;
        }


        public virtual bool Recalculate()
        {
            bool lbR = false;
            double lnTotal = 0;

            if (this.CalculateProductDiscount())
            {
                lbR = true;
            }

            if (this.CalculateItemList())
            {
                lbR = true;
            }

            if (this.CalculateShipping())
            {
                lbR = true;
            }

            if (this.CalculateCartDiscount())
            {
                lbR = true;
            }

            //// Manual discount is like money off the total.  It is set and does not need calculated, but it should not be negative.
            if (this.ManualDiscount < 0)
            {
                this.ManualDiscount = 0;
                lbR = true;
            }

            if (this.CalculateTax())
            {
                lbR = true;
            }

            lnTotal = this.ItemTotal + this.ShippingTotal - this.DiscountTotal - this.ManualDiscount + this.TaxTotal;

            if (lnTotal != this.Total)
            {
                this.Total = lnTotal;
                lbR = true;
            }

            return lbR;
        }

        public virtual bool CalculateProductDiscount()
        {
            bool lbR = false;
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                //// Does basic check about discount code being valid
                if (loEntity.IsCurrentlyValid && loEntity.Calculation == MaxDiscountCodeEntity.CalculationProduct)
                {
                    //// Match products in the cart up with discount code
                    if (!string.IsNullOrEmpty(loEntity.ProductSkuListText))
                    {
                        List<string> loSkuList = new List<string>();
                        foreach (MaxProductSelectionEntity loItemEntity in this.ItemList)
                        {
                            if (loEntity.IsProductSkuMatch(loItemEntity.Sku))
                            {
                                if (null == loEntity.MatchProductCurrentList)
                                {
                                    loEntity.MatchProductCurrentList = new List<MaxProductSelectionEntity>();
                                }

                                loEntity.MatchProductCurrentList.Add(loItemEntity);
                                loEntity.MatchQuantityCurrent += loItemEntity.Quantity;
                                loEntity.MatchAmountCurrent += loItemEntity.Quantity * loItemEntity.ItemPrice;
                            }
                        }
                    }

                    if (null == loEntity.MatchProductCurrentList)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("No item in cart matches.");
                    }
                    else if (loEntity.MaximumQuanitity > 0 && loEntity.MaximumQuanitity < loEntity.MatchQuantityCurrent)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Too many items in cart match sku restrictions.");
                    }
                    else if (loEntity.MinimumQuantity > 0 && loEntity.MinimumQuantity > loEntity.MatchQuantityCurrent)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Too few items in cart match sku restrictions.");
                    }
                    else if (loEntity.MaximumAmount > 0 && loEntity.MaximumAmount < loEntity.MatchAmountCurrent)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Value of matching items in cart is too high.");
                    }
                    else if (loEntity.MinimumAmount > 0 && loEntity.MinimumAmount > loEntity.MatchAmountCurrent)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Value of matching items in cart is too low.");
                    }
                }
            }

            //// Calculate amounts of product discounts
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                if (loEntity.IsCurrentlyValid && loEntity.Calculation == MaxDiscountCodeEntity.CalculationProduct)
                {
                    if (loEntity.AmountOff > 0)
                    {
                        loEntity.DiscountAmountCurrent = loEntity.AmountOff;
                    }
                    else if (loEntity.PercentOff > 0)
                    {
                        loEntity.DiscountAmountCurrent = Convert.ToDouble(Math.Round(Convert.ToDecimal(loEntity.MatchAmountCurrent) * Convert.ToDecimal(loEntity.PercentOff / 100), 2, MidpointRounding.AwayFromZero));
                    }

                    loEntity.ValidReasonCurrent = "Product discount for code \"" + loEntity.Code + "\" was applied to the matching products.\r\n";
                    string lsReason = "Discount for code \"" + loEntity.Code + "\" (" + string.Format("{0:C}", loEntity.DiscountAmountCurrent) + "). \r\n";
                    foreach (MaxProductSelectionEntity loCartItemEntity in loEntity.MatchProductCurrentList)
                    {
                        if (loCartItemEntity.DiscountAmount != loEntity.DiscountAmountCurrent ||
                            loCartItemEntity.DiscountReason != lsReason)
                        {
                            loCartItemEntity.DiscountAmount = loEntity.DiscountAmountCurrent;
                            loCartItemEntity.DiscountReason = lsReason;
                            lbR = true;
                        }
                    }
                }
            }

            //// Remove discount amounts from any products that don't match any discounts
            foreach (MaxProductSelectionEntity loItemEntity in this.ItemList)
            {
                if (loItemEntity.DiscountAmount > 0)
                {
                    bool lbFound = false;
                    foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
                    {
                        if (null != loEntity.MatchProductCurrentList)
                        {
                            foreach (MaxProductSelectionEntity loCartItemEntity in loEntity.MatchProductCurrentList)
                            {
                                if (loCartItemEntity.Id == loItemEntity.Id)
                                {
                                    lbFound = true;
                                }
                            }
                        }
                    }

                    if (!lbFound)
                    {
                        loItemEntity.DiscountAmount = 0;
                        loItemEntity.DiscountReason = string.Empty;
                        lbR = true;
                    }
                }
            }

            return lbR;
        }

        public virtual bool CalculateItemList()
        {
            bool lbR = false;
            int lnItemCount = 0;
            double lnItemTotal = 0;
            //// Recalculate items in the cart
            foreach (MaxProductSelectionEntity loItemEntity in this.ItemList)
            {
                if (loItemEntity.Recalculate())
                {
                    //// Something changed to give a new total.  Could be quantity or could be discount.
                    lbR = true;
                }

                if (loItemEntity.Quantity > 0)
                {
                    lnItemCount++;
                    lnItemTotal += loItemEntity.ItemTotal;
                }
            }

            if (lnItemCount != this.ItemCount || lnItemTotal != this.ItemTotal)
            {
                this.ItemCount = lnItemCount;
                this.ItemTotal = lnItemTotal;
                lbR = true;
            }

            return lbR;
        }

        public virtual bool CalculateShipping()
        {
            bool lbR = false;
            if (!this.IsShippingTotalOverride)
            {
                double lnShippingTotal = 0;
                int lnShippingType = this.ShippingType;

                MaxShippingChargeEntity loShippingChargeEntity = MaxShippingChargeEntity.Create();
                lnShippingTotal = loShippingChargeEntity.GetCartShipping(this, this.ShippingType);
                double lnProductShipping = loShippingChargeEntity.GetProductShipping(this);
                if (lnProductShipping <= 0)
                {
                    if (this.ShippingType != MaxShippingTypeEntity.ShippingTypeFree &&
                        this.ShippingType != MaxShippingTypeEntity.ShippingTypePickup &&
                        this.ShippingType != MaxShippingTypeEntity.ShippingTypeSpecial)
                    {
                        lnShippingType = MaxShippingTypeEntity.ShippingTypeFree;
                    }
                }

                //// Tractor Tools Direct (TTD)
                if (MaxClientRepository.IsTractorToolsDirect(this.Data))
                {
                    if (lnProductShipping >= 10000)
                    {
                        //// Item in cart is over 10000, so shipping needs to be arranged.
                        lnShippingTotal = 0;
                        lnShippingType = MaxShippingTypeEntity.ShippingTypeArrange;
                    }
                    else if (lnProductShipping > 0 &&
                        (this.ShippingType == MaxShippingTypeEntity.ShippingTypeArrange ||
                        this.ShippingType == MaxShippingTypeEntity.ShippingTypeFree))
                    {
                        //// Shipping is greater than zero and was either Arrange or Free, so reset the shipping so it has to be selected.
                        lnShippingType = 0;
                    }
                }

                //// Display Connection
                if (MaxClientRepository.IsDisplayConnection(this.Data))
                {
                    lnShippingTotal = 0;
                    lnShippingType = MaxShippingTypeEntity.ShippingTypeArrange;
                }

                if (lnShippingTotal != this.ShippingTotal || lnShippingType != this.ShippingType)
                {
                    this.ShippingTotal = lnShippingTotal;
                    this.ShippingType = lnShippingType;
                    lbR = true;
                }
            }

            return lbR;
        }

        public virtual bool CalculateCartDiscount()
        {
            string lsDiscountExplanation = string.Empty;
            //// Update codes that are invalid with the reason
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                //// Does basic check about discount code being valid
                if (loEntity.IsCurrentlyValid && loEntity.Calculation == MaxDiscountCodeEntity.CalculationCart)
                {
                    if (loEntity.MaximumAmount > 0 && loEntity.MaximumAmount < this.ItemTotal)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Total of {0:C} is above maximum amount of {1:C}", this.ItemTotal, loEntity.MaximumAmount);
                    }
                    else if (loEntity.MinimumAmount > 0 && loEntity.MinimumAmount > this.ItemTotal)
                    {
                        loEntity.InvalidReasonCurrent = string.Format("Total of {0:C} is below minimum amount of {1:C}", this.ItemTotal, loEntity.MinimumAmount);
                    }

                    if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && !string.IsNullOrEmpty(loEntity.ProductSkuListText))
                    {
                        List<string> loSkuList = new List<string>();
                        foreach (MaxProductSelectionEntity loItemEntity in this.ItemList)
                        {
                            if (loEntity.IsProductSkuMatch(loItemEntity.Sku))
                            {
                                loEntity.MatchQuantityCurrent += loItemEntity.Quantity;
                                loEntity.MatchAmountCurrent += loItemEntity.Quantity * loItemEntity.ItemPrice;
                            }
                        }

                        if (loEntity.MatchQuantityCurrent == 0)
                        {
                            loEntity.InvalidReasonCurrent = string.Format("No item in cart matches sku restrictions.");
                        }
                    }

                    if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && loEntity.MaximumQuanitity > 0)
                    {
                        if (loEntity.MaximumQuanitity < loEntity.MatchQuantityCurrent)
                        {
                            loEntity.InvalidReasonCurrent = string.Format("Too many items in cart match sku restrictions.");
                        }
                    }

                    if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && loEntity.MinimumQuantity > 0)
                    {
                        if (loEntity.MinimumQuantity > loEntity.MatchQuantityCurrent)
                        {
                            loEntity.InvalidReasonCurrent = string.Format("Too few items in cart match sku restrictions.");
                        }
                    }

                    if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && loEntity.MaximumAmount > 0)
                    {
                        if (loEntity.MaximumAmount < loEntity.MatchAmountCurrent)
                        {
                            loEntity.InvalidReasonCurrent = string.Format("Value of matching items in cart is too high.");
                        }
                    }

                    if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && loEntity.MinimumAmount > 0)
                    {
                        if (loEntity.MinimumAmount > loEntity.MatchAmountCurrent)
                        {
                            loEntity.InvalidReasonCurrent = string.Format("Value of matching items in cart is too low.");
                        }
                    }
                }
            }

            //// Calculate amounts of cart discounts
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                if (loEntity.IsCurrentlyValid && loEntity.Calculation == MaxDiscountCodeEntity.CalculationCart)
                {
                    if (loEntity.AmountOff > 0)
                    {
                        loEntity.DiscountAmountCurrent = loEntity.AmountOff;
                    }
                    else if (loEntity.PercentOff > 0)
                    {
                        loEntity.DiscountAmountCurrent = Convert.ToDouble(Math.Round(Convert.ToDecimal(this.ItemTotal + this.ShippingTotal) * Convert.ToDecimal(loEntity.PercentOff / 100), 2, MidpointRounding.AwayFromZero));
                    }
                }
            }

            //// Invalidate all but the largest in each group
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                if (loEntity.IsCurrentlyValid && loEntity.Calculation == MaxDiscountCodeEntity.CalculationCart)
                {
                    foreach (MaxDiscountCodeEntity loEntityCheck in this.DiscountCodeList)
                    {
                        if (string.IsNullOrEmpty(loEntityCheck.InvalidReasonCurrent) &&
                            loEntityCheck.Calculation == MaxDiscountCodeEntity.CalculationCart &&
                            loEntityCheck.Group == loEntity.Group)
                        {
                            //// Update any that have a smaller amount than the current one.
                            if (loEntityCheck.DiscountAmountCurrent <= loEntity.DiscountAmountCurrent && loEntity != loEntityCheck)
                            {
                                loEntityCheck.InvalidReasonCurrent = "Another coupon in this group gives a bigger discount.";
                            }
                        }
                    }
                }
            }

            //// Add all discounts that have not been invalidated
            double lnDiscountTotal = 0;
            foreach (MaxDiscountCodeEntity loEntity in this.DiscountCodeList)
            {
                if (string.IsNullOrEmpty(loEntity.InvalidReasonCurrent) && loEntity.Calculation == MaxDiscountCodeEntity.CalculationCart)
                {
                    if (loEntity.DiscountAmountCurrent > 0)
                    {
                        loEntity.ValidReasonCurrent = "Discount for code \"" + loEntity.Code + "\" (" + string.Format("{0:C}", loEntity.DiscountAmountCurrent) + "). \r\n";
                        lsDiscountExplanation += "Discount for code \"" + loEntity.Code + "\" (" + string.Format("{0:C}", loEntity.DiscountAmountCurrent) + "). \r\n";
                        lnDiscountTotal += loEntity.DiscountAmountCurrent;
                    }
                }
            }

            if (this.DiscountTotal != lnDiscountTotal ||
                lsDiscountExplanation != this.DiscountTotalExplanation)
            {
                this.DiscountTotal = lnDiscountTotal;
                this.DiscountTotalExplanation = lsDiscountExplanation;
                return true;
            }

            return false;
        }

        public virtual bool CalculateTax()
        {
            bool lbR = false;
            if (!this.TaxLocation.Equals("Override"))
            {
                double lnTaxRate = this.GetSalesTaxRate();
                //// Taxable amount = Product Subtotal  – Product Quantity Discount – Coupon Code Discount - Manual credit
                //// Tax is only charged on product, not shipping
                double lnTaxableAmount = this.ItemTotal;
                if (this.DiscountTotal > 0 && this.DiscountTotal < this.ItemTotal)
                {
                    lnTaxableAmount -= this.DiscountTotal;
                }

                if (this.ManualDiscount > 0 && this.ManualDiscount < this.ItemTotal)
                {
                    lnTaxableAmount -= this.ManualDiscount;
                }

                //// Display Connection
                if (MaxClientRepository.IsDisplayConnection(this.Data))
                {
                    //// Shipping is also included in tax
                    lnTaxableAmount += this.ShippingTotal;
                }

                this.TaxableAmount = lnTaxableAmount;
                double lnTax = Convert.ToDouble(Math.Round(Convert.ToDecimal(lnTaxableAmount) * Convert.ToDecimal(lnTaxRate), 2, MidpointRounding.AwayFromZero));

                if (lnTax != this.TaxTotal)
                {
                    this.TaxTotal = lnTax;
                    lbR = true;
                }
            }

            return lbR;
        }

        /// <summary>
        /// Adds an item to the cart
        /// </summary>
        /// <param name="loItem">Item to add to the current cart.</param>
        /// <returns>true if item did not already exist in cart.</returns>
        public bool AddItem(MaxCartItemEntity loItemNew)
        {
            if (loItemNew.Quantity > 0)
            {
                foreach (MaxCartItemEntity loItem in this.ItemList)
                {
                    if (loItem.PrimaryReferenceId.Equals(loItemNew.PrimaryReferenceId) &&
                        loItem.PrimaryReferenceType.Equals(loItemNew.PrimaryReferenceType) &&
                        loItem.Name.Equals(loItemNew.Name) &&
                        loItem.Sku.Equals(loItemNew.Sku) &&
                        loItem.ItemPrice.Equals(loItemNew.ItemPrice))
                    {
                        List<MaxOptionByNameStructure> loCurrentList = loItem.OptionList;
                        List<MaxOptionByNameStructure> loNewList = loItemNew.OptionList;
                        bool lbMatch = loCurrentList.Count.Equals(loNewList.Count);
                        if (lbMatch)
                        {
                            for (int lnL = 0; lnL < loCurrentList.Count; lnL++)
                            {
                                if (!loCurrentList[lnL].Name.Equals(loNewList[lnL].Name) ||
                                    !loCurrentList[lnL].Option.Equals(loNewList[lnL].Option))
                                {
                                    lbMatch = false;
                                }
                            }
                        }

                        if (lbMatch)
                        {
                            loItem.Quantity += loItemNew.Quantity;
                            return true;
                        }
                    }

                }

                loItemNew.ContainerId = this.Id;
                this.ItemList.Add(loItemNew);
                return true;
            }

            return false;
        }

        public bool RemoveItem(Guid loItemId)
        {
            for (int lnE = 0; lnE < this.ItemList.Count; lnE++)
            {
                if (this.ItemList[lnE].Id.Equals(loItemId))
                {
                    this.ItemList[lnE].Delete();
                    this.ItemList.RemoveAt(lnE);
                    return true;
                }
            }

            return false;
        }

        public virtual bool CanPlaceOrder()
        {
            bool lbR = true;
            if (this.GetCanPlaceOrderMessage().Length > 0)
            {
                lbR = false;
            }

            return lbR;
        }

        public virtual string GetCanPlaceOrderMessage()
        {
            string lsR = string.Empty;
            if (this.ItemList.Count <= 0)
            {
                lsR = "There are no items in the cart.";
            }
            else if (MaxClientRepository.IsDisplayConnection(this.GetData()) && this.ItemTotal < 50)
            {
                lsR = "Total of all items must exceed $50 to place an order.";
            }

            if (lsR.Length == 0)
            {
                foreach (MaxProductSelectionEntity loItemEntity in this.ItemList)
                {
                    if (loItemEntity.PrimaryReferenceType.Equals(typeof(MaxProductEntity).ToString()))
                    {
                        //// Add any shipping that is specific to the product, but not the quantity or shipping method.
                        MaxProductEntity loProduct = MaxProductEntity.Create();
                        if (!loProduct.LoadByIdCache(loItemEntity.PrimaryReferenceId))
                        {
                            lsR = "One of the products no longer exists.";
                        }
                        else if (!loProduct.IsActive)
                        {
                            lsR = loProduct.Name + " is no longer available.  Please remove from the cart.";
                        }
                        else if (Guid.Empty != loProduct.PrimaryCatalogId)
                        {
                            MaxCatalogEntity loCatalog = MaxCatalogEntity.Create();
                            if (!loCatalog.LoadByIdCache(loProduct.PrimaryCatalogId))
                            {
                                lsR = "Catalog for " + loProduct.Name + " is no longer available. Please remove from the cart.";
                            }
                            else if (!loCatalog.IsActive)
                            {
                                lsR = loCatalog.Name + " catalog is no longer available. Please remove any products from that catalog from the cart.";
                            }
                        }
                    }
                }
            }

            if (this.ShippingType <= 0 && lsR.Length == 0)
            {
                lsR = "Shipping Method must be selected to place an order.";
            }

            return lsR;
        }


        public MaxEntityList LoadAllByReferenceId(Guid loReferenceId)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.ReferenceId, loReferenceId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        public MaxEntityList LoadAllByReferenceIdType(Guid loReferenceId, string lsReferenceType)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllCartByReferenceIdType(this.Data, loReferenceId, lsReferenceType);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        public MaxEntityList LoadAllByDate(DateTime ldDate)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByLastUpdateDateRange(this.Data, ldDate, DateTime.UtcNow);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        public virtual int ArchiveAbandoned()
        {
            int lnR = 0;
            if (this.CanProcessArchive(new TimeSpan(24, 0, 0)))
            {
                lnR = this.Archive(DateTime.MinValue, DateTime.UtcNow.AddDays(-30), false);
            }

            return lnR;
        }

        /// <summary>
        /// Archive this cart and all items related to it.
        /// </summary>
        /// <returns></returns>
        public override bool Archive()
        {
            bool lbR = true;
            foreach (MaxProductSelectionEntity loProductSelection in this.ItemList)
            {
                if (!loProductSelection.Archive())
                {
                    lbR = false;
                }
            }

            if (lbR)
            {
                lbR = base.Archive();
            }

            return lbR;
        }

        protected virtual MaxEntityList LoadAllItem()
        {
            MaxCartItemEntity loEntity = MaxCartItemEntity.Create();
            MaxEntityList loList = loEntity.LoadAllByContainerId(this.Id);
            if (loList.Count == 0)
            {
                loEntity.StorageKey = this.StorageKey;
                loList = loEntity.LoadAllByContainerId(this.Id);
            }

            return loList;
        }

    }
}
