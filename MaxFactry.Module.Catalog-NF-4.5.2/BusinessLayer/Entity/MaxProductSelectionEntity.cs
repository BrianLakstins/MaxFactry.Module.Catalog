// <copyright file="MaxProductSelectionEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="6/29/2015" author="Brian A. Lakstins" description="Updates to handling option selection list.  Add Note.">
// <change date="4/20/2016" author="Brian A. Lakstins" description="Updated to use centralized caching.">
// <change date="4/19/2018" author="Brian A. Lakstins" description="Fix Shipping Calculation Multiplier so it will be stored">
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

    public abstract class MaxProductSelectionEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        public List<MaxOptionByNameStructure> _oOptionList = null;


		/// <summary>
        /// Initializes a new instance of the MaxProductSelectionEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxProductSelectionEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxProductSelectionEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxProductSelectionEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid ContainerId
        {
            get
            {
                return this.GetGuid(this.ProductSelectionDataModel.ContainerId);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ContainerId, value);
            }
        }

        public string PrimaryReferenceType
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.PrimaryReferenceType);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.PrimaryReferenceType, value);
            }
        }

        public Guid PrimaryReferenceId
        {
            get
            {
                return this.GetGuid(this.ProductSelectionDataModel.PrimaryReferenceId);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.PrimaryReferenceId, value);
            }
        }

        public double ItemShipping
        {
            get
            {
                return this.GetDouble(this.ProductSelectionDataModel.ItemShipping);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ItemShipping, value);
            }
        }

        public double PriceBase
        {
            get
            {
                double loR = this.GetDouble("xPriceBase");
                if (loR == double.MinValue)
                {
                    loR = this.ItemPrice;
                    this.Set("xPriceBase", loR);
                }

                return loR;
            }

            set
            {
                this.Set("xPriceBase", value);
            }
        }

        public double PriceAdditional
        {
            get
            {
                double loR = this.GetDouble("xPriceAdditional");
                if (loR == double.MinValue)
                {
                    loR = 0;
                    this.Set("xPriceAdditional", loR);
                }

                return loR;
            }

            set
            {
                this.Set("xPriceAdditional", value);
            }
        }

        public double ItemPrice
        {
            get
            {
                return this.GetDouble(this.ProductSelectionDataModel.ItemPrice);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ItemPrice, value);
            }
        }

        public double ItemTotal
        {
            get
            {
                return this.GetDouble(this.ProductSelectionDataModel.ItemTotal);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ItemTotal, value);
            }
        }

        public int Quantity
        {
            get
            {
                return this.GetInt(this.ProductSelectionDataModel.Quantity);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.Quantity, value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.Name);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.Name, value);
            }
        }

        public string Sku
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.Sku);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.Sku, value);
            }
        }

        public string ItemPricePer
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.ItemPricePer);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ItemPricePer, value);
            }
        }

        public string ImageUrl
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.ImageUrl);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ImageUrl, value);
            }
        }

        public string ItemUrl
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.ItemUrl);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ItemUrl, value);
            }
        }

        public string Note
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.Note);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.Note, value);
            }
        }

        public double DiscountAmount
        {
            get
            {
                return this.GetDouble(this.ProductSelectionDataModel.DiscountAmount);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.DiscountAmount, value);
            }
        }

        public string DiscountReason
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.DiscountReason);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.DiscountReason, value);
            }
        }

        public string DiscountCodeListText
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.DiscountCodeListText);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.DiscountCodeListText, value);
            }
        }

        public double ManualDiscountAmount
        {
            get
            {
                return this.GetDouble(this.ProductSelectionDataModel.ManualDiscountAmount);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ManualDiscountAmount, value);
            }
        }

        public string ManualDiscountReason
        {
            get
            {
                return this.GetString(this.ProductSelectionDataModel.ManualDiscountReason);
            }

            set
            {
                this.Set(this.ProductSelectionDataModel.ManualDiscountReason, value);
            }
        }

        public int ShippingCalculationMultiplier
        {
            get
            {
                int lnR = this.GetInt("xShippingCalculationMultiplier");
                if (lnR < 1)
                {
                    lnR = 1;
                }

                return lnR;
            }

            set
            {
                this.Set("xShippingCalculationMultiplier", value);
            }
        }

        public List<MaxOptionByNameStructure> OptionList
        {
            get
            {
                if (null == this._oOptionList)
                {
                    this._oOptionList = new List<MaxOptionByNameStructure>();
                    List<MaxOptionByNameStructure> loList = this.GetObject(this.ProductSelectionDataModel.OptionSelectionList, typeof(List<MaxOptionByNameStructure>)) as List<MaxOptionByNameStructure>;
                    if (null != loList)
                    {
                        foreach (MaxOptionByNameStructure loItem in loList)
                        {
                            if (!string.IsNullOrEmpty(loItem.Name))
                            {
                                this._oOptionList.Add(loItem);
                            }
                        }
                    }
                }

                return this._oOptionList;
            }

            set
            {
                this._oOptionList = value;
            }
        }

        public List<MaxPriceByQuantityStructure> PriceByQuantityList
        {
            get
            {
                List<MaxPriceByQuantityStructure> loR = this.Get("xPriceByQuantityList") as List<MaxPriceByQuantityStructure>;
                if (null == loR)
                {
                    loR = new List<MaxPriceByQuantityStructure>();
                    this.Set("xPriceByQuantityList", loR);
                }

                return loR;
            }
            
            set
            {
                this.Set("xPriceByQuantityList", value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxProductSelectionDataModel ProductSelectionDataModel
        {
            get
            {
                return (MaxProductSelectionDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByContainerId(Guid loContainerId)
        {
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType());
            if (Guid.Empty != loContainerId)
            {
                MaxDataList loDataList = MaxCacheRepository.Get(this.GetType(), this.GetByContainerIdCacheKey(loContainerId), typeof(MaxDataList)) as MaxDataList;
                if (null == loDataList)
                {
                    loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.ProductSelectionDataModel.ContainerId, loContainerId);
                    MaxCacheRepository.Set(this.GetType(), this.GetByContainerIdCacheKey(loContainerId), loDataList);
                }

                loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            }

            return loEntityList;
        }

        public bool Recalculate()
        {
            double lnDiscountAmount = 0;
            if (this.DiscountAmount > 0)
            {
                lnDiscountAmount = this.DiscountAmount;
            }

            if (this.ManualDiscountAmount > 0)
            {
                lnDiscountAmount = this.ManualDiscountAmount;
            }

            //// Update ItemPrice based on Quantity
            double lnPrice = this.PriceBase;
            if (this.PriceByQuantityList.Count > 0)
            {
                foreach (MaxPriceByQuantityStructure loPriceByQuantity in this.PriceByQuantityList)
                {
                    if (loPriceByQuantity.Start <= this.Quantity && this.Quantity <= loPriceByQuantity.End)
                    {
                        lnPrice = loPriceByQuantity.Price;
                    }
                }
            }

            lnPrice += this.PriceAdditional;
            if (lnPrice > 0)
            {
                this.ItemPrice = lnPrice;
            }

            double lnCurrentItemTotal = (this.Quantity * this.ItemPrice) - lnDiscountAmount;
            if (lnCurrentItemTotal < 0)
            {
                lnCurrentItemTotal = 0;
            }

            if (lnCurrentItemTotal != this.ItemTotal)
            {
                this.ItemTotal = lnCurrentItemTotal;
                return true;
            }

            return false;
        }

        public override bool Update()
        {
            if (null != this._oOptionList)
            {
                this.SetObject(this.ProductSelectionDataModel.OptionSelectionList, this._oOptionList);
            }

            return base.Update();
        }

        public override bool Insert()
        {
            if (null != this._oOptionList)
            {
                this.SetObject(this.ProductSelectionDataModel.OptionSelectionList, this._oOptionList);
            }

            return base.Insert();
        }

        protected string GetByContainerIdCacheKey(Guid loContainerId)
        {
            this.ContainerId = loContainerId;
            string lsR = this.GetCacheKey() + ".LoadAllByContainerId" + loContainerId.ToString();
            return lsR;
        }
    }
}
