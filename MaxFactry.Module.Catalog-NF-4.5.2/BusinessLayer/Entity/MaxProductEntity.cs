// <copyright file="MaxProductEntity.cs" company="Lakstins Family, LLC">
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
// <change date="6/16/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/17/2014" author="Brian A. Lakstins" description="Update to use instance data model instead of static.">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Add default sorting.  Add properties.">
// <change date="6/28/2014" author="Brian A. Lakstins" description="Change to BaseId.">
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added loading methods and two complex properties.">
// <change date="9/30/2014" author="Brian A. Lakstins" description="Integrated product search into insert and update processes.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
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

    public class MaxProductEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {

        private List<MaxPriceByQuantityStructure> _oPriceByQuantityList = null;

        private List<MaxDescriptionListStructure> _oDescriptionList = null;

        private List<MaxOptionByNameStructure> _oOptionByNameList = null;

        private MaxProductEntity _oBaseProduct = null;

        private List<MaxProductEntity> _oBranchList = null;

        private MaxProductEntity _oParentProduct = null;

        private List<MaxProductEntity> _oChildList = null;

        private Dictionary<string, List<MaxOptionByNameStructure>> _oProductOptionByNameIndex = null;

        /// <summary>
        /// Initializes a new instance of the MaxProductEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxProductEntity(MaxData loData) : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxProductEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string Name
        {
            get
            {
                if (this.IsOverride(this.DataModel.Name))
                {
                    return this.GetString(this.DataModel.Name);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Name;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Name;
                }

                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        public string Sku
        {
            get
            {
                if (this.IsOverride(this.DataModel.Sku))
                {
                    return this.GetString(this.DataModel.Sku);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Sku;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Sku;
                }

                return this.GetString(this.DataModel.Sku);
            }

            set
            {
                this.Set(this.DataModel.Sku, value);
            }
        }

        public string DescriptionShort
        {
            get
            {
                if (this.IsOverride(this.DataModel.DescriptionShort))
                {
                    return this.GetString(this.DataModel.DescriptionShort);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.DescriptionShort;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.DescriptionShort;
                }

                return this.GetString(this.DataModel.DescriptionShort);
            }

            set
            {
                this.Set(this.DataModel.DescriptionShort, value);
            }
        }

        public string Description
        {
            get
            {
                if (this.IsOverride(this.DataModel.Description))
                {
                    return this.GetString(this.DataModel.Description);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Description;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Description;
                }

                return this.GetString(this.DataModel.Description);
            }

            set
            {
                this.Set(this.DataModel.Description, value);
            }
        }

        public string Keywords
        {
            get
            {
                if (this.IsOverride(this.DataModel.Keywords))
                {
                    return this.GetString(this.DataModel.Keywords);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Keywords;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Keywords;
                }

                return this.GetString(this.DataModel.Keywords);
            }

            set
            {
                this.Set(this.DataModel.Keywords, value);
            }
        }

        public string Notes
        {
            get
            {
                if (this.IsOverride(this.DataModel.Notes))
                {
                    return this.GetString(this.DataModel.Notes);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Notes;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Notes;
                }

                return this.GetString(this.DataModel.Notes);
            }

            set
            {
                this.Set(this.DataModel.Notes, value);
            }
        }

        public string PerText
        {
            get
            {
                if (this.IsOverride(this.DataModel.PerText))
                {
                    return this.GetString(this.DataModel.PerText);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PerText;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PerText;
                }

                return this.GetString(this.DataModel.PerText);
            }

            set
            {
                this.Set(this.DataModel.PerText, value);
            }
        }

        public int MaximumQuantity
        {
            get
            {
                if (this.IsOverride(this.DataModel.MaximumQuantity))
                {
                    return this.GetInt(this.DataModel.MaximumQuantity);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.MaximumQuantity;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.MaximumQuantity;
                }

                return this.GetInt(this.DataModel.MaximumQuantity);
            }

            set
            {
                this.Set(this.DataModel.MaximumQuantity, value);
            }
        }

        public int MinimumQuantity
        {
            get
            {
                if (this.IsOverride(this.DataModel.MinimumQuantity))
                {
                    return this.GetInt(this.DataModel.MinimumQuantity);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.MinimumQuantity;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.MinimumQuantity;
                }

                return this.GetInt(this.DataModel.MinimumQuantity);
            }

            set
            {
                this.Set(this.DataModel.MinimumQuantity, value);
            }
        }

        public int PerQuantity
        {
            get
            {
                if (this.IsOverride(this.DataModel.PerQuantity))
                {
                    return this.GetInt(this.DataModel.PerQuantity);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PerQuantity;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PerQuantity;
                }

                return this.GetInt(this.DataModel.PerQuantity);
            }

            set
            {
                this.Set(this.DataModel.PerQuantity, value);
            }
        }

        public double Depth
        {
            get
            {
                if (this.IsOverride(this.DataModel.Depth))
                {
                    return this.GetDouble(this.DataModel.Depth);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Depth;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Depth;
                }

                return this.GetDouble(this.DataModel.Depth);
            }

            set
            {
                this.Set(this.DataModel.Depth, value);
            }
        }

        public double Height
        {
            get
            {
                if (this.IsOverride(this.DataModel.Height))
                {
                    return this.GetDouble(this.DataModel.Height);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Height;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Height;
                }

                return this.GetDouble(this.DataModel.Height);
            }

            set
            {
                this.Set(this.DataModel.Height, value);
            }
        }

        public double InsuranceAmount
        {
            get
            {
                if (this.IsOverride(this.DataModel.InsuranceAmount))
                {
                    return this.GetDouble(this.DataModel.InsuranceAmount);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.InsuranceAmount;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.InsuranceAmount;
                }

                return this.GetDouble(this.DataModel.InsuranceAmount);
            }

            set
            {
                this.Set(this.DataModel.InsuranceAmount, value);
            }
        }

        public double PriceBase
        {
            get
            {
                if (this.IsOverride(this.DataModel.PriceBase))
                {
                    return this.GetDouble(this.DataModel.PriceBase);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PriceBase;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PriceBase;
                }

                return this.GetDouble(this.DataModel.PriceBase);
            }

            set
            {
                this.Set(this.DataModel.PriceBase, value);
            }
        }

        public double PriceCompare
        {
            get
            {
                if (this.IsOverride(this.DataModel.PriceCompare))
                {
                    return this.GetDouble(this.DataModel.PriceCompare);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PriceCompare;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PriceCompare;
                }

                return this.GetDouble(this.DataModel.PriceCompare);
            }

            set
            {
                this.Set(this.DataModel.PriceCompare, value);
            }
        }

        public double Volume
        {
            get
            {
                if (this.IsOverride(this.DataModel.Volume))
                {
                    return this.GetDouble(this.DataModel.Volume);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Volume;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Volume;
                }

                return this.GetDouble(this.DataModel.Volume);
            }

            set
            {
                this.Set(this.DataModel.Volume, value);
            }
        }

        public double Weight
        {
            get
            {
                if (this.IsOverride(this.DataModel.Weight))
                {
                    return this.GetDouble(this.DataModel.Weight);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.Weight;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.Weight;
                }

                return this.GetDouble(this.DataModel.Weight);
            }

            set
            {
                this.Set(this.DataModel.Weight, value);
            }
        }

        public Guid PrimaryManufacturerId
        {
            get
            {
                if (this.IsOverride(this.DataModel.PrimaryManufacturerId))
                {
                    return this.GetGuid(this.DataModel.PrimaryManufacturerId);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PrimaryManufacturerId;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PrimaryManufacturerId;
                }

                return this.GetGuid(this.DataModel.PrimaryManufacturerId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryManufacturerId, value);
            }
        }

        public string PrimaryManufacturerSku
        {
            get
            {
                if (this.IsOverride(this.DataModel.PrimaryManufacturerSku))
                {
                    return this.GetString(this.DataModel.PrimaryManufacturerSku);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PrimaryManufacturerSku;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PrimaryManufacturerSku;
                }

                return this.GetString(this.DataModel.PrimaryManufacturerSku);
            }

            set
            {
                this.Set(this.DataModel.PrimaryManufacturerSku, value);
            }
        }

        public Guid PrimaryVendorId
        {
            get
            {
                if (this.IsOverride(this.DataModel.PrimaryVendorId))
                {
                    return this.GetGuid(this.DataModel.PrimaryVendorId);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PrimaryVendorId;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PrimaryVendorId;
                }

                return this.GetGuid(this.DataModel.PrimaryVendorId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryVendorId, value);
            }
        }

        public string PrimaryVendorSku
        {
            get
            {
                if (this.IsOverride(this.DataModel.PrimaryVendorSku))
                {
                    return this.GetString(this.DataModel.PrimaryVendorSku);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PrimaryVendorSku;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PrimaryVendorSku;
                }

                return this.GetString(this.DataModel.PrimaryVendorSku);
            }

            set
            {
                this.Set(this.DataModel.PrimaryVendorSku, value);
            }
        }

        public Guid PrimaryImageId
        {
            get
            {
                if (this.IsOverride(this.DataModel.PrimaryImageId))
                {
                    return this.GetGuid(this.DataModel.PrimaryImageId);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PrimaryImageId;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PrimaryImageId;
                }

                return this.GetGuid(this.DataModel.PrimaryImageId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryImageId, value);
            }
        }

        public double PriceBaseShipping
        {
            get
            {
                if (this.IsOverride(this.DataModel.PriceBaseShipping))
                {
                    return this.GetDouble(this.DataModel.PriceBaseShipping);
                }
                else if (null != this.ParentProduct)
                {
                    return this.ParentProduct.PriceBaseShipping;
                }
                else if (null != this.RootProduct)
                {
                    return this.RootProduct.PriceBaseShipping;
                }

                return this.GetDouble(this.DataModel.PriceBaseShipping);
            }

            set
            {
                this.Set(this.DataModel.PriceBaseShipping, value);
            }
        }

        public Guid PrimaryCatalogId
        {
            get
            {
                return this.GetGuid(this.DataModel.PrimaryCatalogId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryCatalogId, value);
            }
        }

        public Guid PrimaryCategoryId
        {
            get
            {
                return this.GetGuid(this.DataModel.PrimaryCategoryId);
            }

            set
            {
                this.Set(this.DataModel.PrimaryCategoryId, value);
            }
        }

        public string CategoryIdList
        {
            get
            {
                return this.GetString(this.DataModel.CategoryIdList);
            }

            set
            {
                this.Set(this.DataModel.CategoryIdList, value);
            }
        }

        public Guid RootId
        {
            get
            {
                return this.GetGuid(this.DataModel.RootId);
            }

            set
            {
                this.Set(this.DataModel.RootId, value);
            }
        }

        /// <summary>
        /// Product that this Linked Copy product comes from.
        /// </summary>
        public MaxProductEntity RootProduct
        {
            get
            {
                if (!Guid.Empty.Equals(this.RootId) && null == this._oBaseProduct)
                {
                    MaxProductEntity loProduct = MaxProductEntity.Create();
                    loProduct.LoadByIdCache(this.RootId);
                    this._oBaseProduct = loProduct;
                }

                return this._oBaseProduct;
            }
        }


        public string BranchType
        {
            get
            {
                return this.GetString(this.DataModel.BranchType);
            }

            set
            {
                this.Set(this.DataModel.BranchType, value);
            }
        }

        public Guid ParentId
        {
            get
            {
                return this.GetGuid(this.DataModel.ParentId);
            }

            set
            {
                this.Set(this.DataModel.ParentId, value);
            }
        }

        /// <summary>
        /// Product that this Child product comes from.
        /// Child products are variations of their parent.
        /// </summary>
        public MaxProductEntity ParentProduct
        {
            get
            {
                if (!Guid.Empty.Equals(this.ParentId) && null == this._oParentProduct)
                {
                    MaxProductEntity loProduct = MaxProductEntity.Create();
                    loProduct.LoadByIdCache(this.ParentId);
                    this._oParentProduct = loProduct;
                }

                return this._oParentProduct;
            }
        }

        public List<MaxPriceByQuantityStructure> PriceByQuantityList
        {
            get
            {
                if (null == this._oPriceByQuantityList)
                {
                    if (this.IsOverride(this.DataModel.PriceByQuantityList))
                    {
                        this._oPriceByQuantityList = new List<MaxPriceByQuantityStructure>();
                        object loObject = this.GetObject(this.DataModel.PriceByQuantityList, typeof(List<MaxPriceByQuantityStructure>));
                        if (loObject is List<MaxPriceByQuantityStructure>)
                        {
                            List<MaxPriceByQuantityStructure> loList = (List<MaxPriceByQuantityStructure>)loObject;
                            foreach (MaxPriceByQuantityStructure loPrice in loList)
                            {
                                if (!loPrice.Price.Equals(0))
                                {
                                    _oPriceByQuantityList.Add(loPrice);
                                }
                            }
                        }
                    }
                    else if (null != this.ParentProduct)
                    {
                        this._oPriceByQuantityList = this.ParentProduct.PriceByQuantityList;
                    }
                    else if (null != this.RootProduct)
                    {
                        this._oPriceByQuantityList = this.RootProduct.PriceByQuantityList;
                    }
                    else
                    {
                        this._oPriceByQuantityList = new List<MaxPriceByQuantityStructure>();
                        object loObject = this.GetObject(this.DataModel.PriceByQuantityList, typeof(List<MaxPriceByQuantityStructure>));
                        if (loObject is List<MaxPriceByQuantityStructure>)
                        {
                            List<MaxPriceByQuantityStructure> loList = (List<MaxPriceByQuantityStructure>)loObject;
                            foreach (MaxPriceByQuantityStructure loPrice in loList)
                            {
                                if (!loPrice.Price.Equals(0))
                                {
                                    _oPriceByQuantityList.Add(loPrice);
                                }
                            }
                        }
                    }
                }

                return this._oPriceByQuantityList;
            }

            set
            {
                this._oPriceByQuantityList = value;
            }
        }

        public List<MaxDescriptionListStructure> DescriptionList
        {
            get
            {
                if (null == this._oDescriptionList)
                {

                    if (this.IsOverride(this.DataModel.DescriptionList))
                    {
                        this._oDescriptionList = new List<MaxDescriptionListStructure>();
                        List<MaxDescriptionListStructure> loList = this.GetObject(this.DataModel.DescriptionList, typeof(List<MaxDescriptionListStructure>)) as List<MaxDescriptionListStructure>;
                        if (null != loList)
                        {
                            foreach (MaxDescriptionListStructure loDescription in loList)
                            {
                                if (!string.IsNullOrEmpty(loDescription.Title))
                                {
                                    this._oDescriptionList.Add(loDescription);
                                }
                            }
                        }
                    }
                    else if (null != this.ParentProduct)
                    {
                        this._oDescriptionList = this.ParentProduct.DescriptionList;
                    }
                    else if (null != this.RootProduct)
                    {
                        this._oDescriptionList = this.RootProduct.DescriptionList;
                    }
                    else
                    {
                        this._oDescriptionList = new List<MaxDescriptionListStructure>();
                        List<MaxDescriptionListStructure> loList = this.GetObject(this.DataModel.DescriptionList, typeof(List<MaxDescriptionListStructure>)) as List<MaxDescriptionListStructure>;
                        if (null != loList)
                        {
                            foreach (MaxDescriptionListStructure loDescription in loList)
                            {
                                if (!string.IsNullOrEmpty(loDescription.Title))
                                {
                                    this._oDescriptionList.Add(loDescription);
                                }
                            }
                        }
                    }
                }

                return this._oDescriptionList;
            }

            set
            {
                this._oDescriptionList = value;
            }
        }

        public List<MaxOptionByNameStructure> OptionByNameList
        {
            get
            {
                if (null == this._oOptionByNameList)
                {
                    if (this.IsOverride(this.DataModel.OptionByNameList))
                    {
                        this._oOptionByNameList = new List<MaxOptionByNameStructure>();
                        List<MaxOptionByNameStructure> loList = this.GetObject(this.DataModel.OptionByNameList, typeof(List<MaxOptionByNameStructure>)) as List<MaxOptionByNameStructure>;
                        if (null != loList)
                        {
                            foreach (MaxOptionByNameStructure loOption in loList)
                            {
                                if (!string.IsNullOrEmpty(loOption.Name))
                                {
                                    loOption.ProductId = this.Id;
                                    this._oOptionByNameList.Add(loOption);
                                }
                            }
                        }
                    }
                    else if (null != this.ParentProduct)
                    {
                        this._oOptionByNameList = this.ParentProduct.OptionByNameList;
                    }
                    else if (null != this.RootProduct)
                    {
                        this._oOptionByNameList = this.RootProduct.OptionByNameList;
                    }
                    else
                    {
                        this._oOptionByNameList = new List<MaxOptionByNameStructure>();
                        List<MaxOptionByNameStructure> loList = this.GetObject(this.DataModel.OptionByNameList, typeof(List<MaxOptionByNameStructure>)) as List<MaxOptionByNameStructure>;
                        if (null != loList)
                        {
                            foreach (MaxOptionByNameStructure loOption in loList)
                            {
                                if (!string.IsNullOrEmpty(loOption.Name))
                                {
                                    loOption.ProductId = this.Id;
                                    this._oOptionByNameList.Add(loOption);
                                }
                            }
                        }
                    }
                }

                return this._oOptionByNameList;
            }

            set
            {
                this._oOptionByNameList = value;
            }
        }

        public bool IsImport
        {
            get
            {
                if (this.BranchType.StartsWith("import"))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsBranch
        {
            get
            {
                if (this.BranchType.StartsWith("branch"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxProductDataModel DataModel
        {
            get
            {
                return (MaxProductDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxProductEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxProductEntity),
                typeof(MaxProductDataModel)) as MaxProductEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public virtual List<MaxProductEntity> GetBranchList()
        {
            if (null == this._oBranchList)
            {
                SortedList<string, MaxProductEntity> loSortList = new SortedList<string, MaxProductEntity>();
                if (!Guid.Empty.Equals(this.Id))
                {
                    MaxEntityList loEntityList = MaxProductEntity.Create().LoadAllByRootId(this.Id);
                    for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                    {
                        loSortList.Add(((MaxProductEntity)loEntityList[lnE]).GetDefaultSortString(), (MaxProductEntity)loEntityList[lnE]);
                    }
                }

                this._oBranchList = new List<MaxProductEntity>(loSortList.Values);
            }

            return this._oBranchList;
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public virtual List<MaxProductEntity> GetChildList(MaxEntityList loList)
        {
            if (null == this._oChildList)
            {
                SortedList<string, MaxProductEntity> loSortList = new SortedList<string, MaxProductEntity>();
                if (!Guid.Empty.Equals(this.Id))
                {
                    MaxEntityList loEntityList = loList;
                    if (null == loEntityList)
                    {
                        loEntityList = MaxProductEntity.Create().LoadAllByParentId(this.Id);
                    }

                    for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                    {
                        MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                        if (loEntity.ParentId.Equals(this.Id))
                        {
                            loSortList.Add(loEntity.GetDefaultSortString(), loEntity);
                        }
                    }
                }

                this._oChildList = new List<MaxProductEntity>(loSortList.Values);
            }

            return this._oChildList;
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public virtual List<MaxProductEntity> GetChildList()
        {
            return this.GetChildList(null);
        }


        public virtual List<string> GetSkuList()
        {
            List<string> loR = new List<string>();
            List<MaxOptionByNameStructure> loOptionList = new List<MaxOptionByNameStructure>(this.OptionByNameList);
            foreach (MaxProductEntity loVariation in this.GetChildList())
            {
                loOptionList.AddRange(loVariation.OptionByNameList);
            }

            List<List<MaxOptionByNameStructure>> loOptionCombinationList = GetOptionCombinationList(loOptionList);
            string lsBaseSku = this.Sku;
            foreach (List<MaxOptionByNameStructure> loList in loOptionCombinationList)
            {
                string lsSku = lsBaseSku;
                foreach (MaxOptionByNameStructure loOption in loList)
                {
                    lsSku += "-" + loOption.SkuSuffix;
                }

                loR.Add(lsSku);
            }

            return loR;
        }

        public virtual Dictionary<string, List<MaxOptionByNameStructure>> GetProductOptionByNameIndex()
        {
            if (null == this._oProductOptionByNameIndex)
            {
                this._oProductOptionByNameIndex = new Dictionary<string, List<MaxOptionByNameStructure>>();
                List<MaxOptionByNameStructure> loOptionList = new List<MaxOptionByNameStructure>();
                List<string> loKeyList = new List<string>();
                //// Add making sure there are no duplicates
                foreach (MaxOptionByNameStructure loOption in this.OptionByNameList)
                {
                    string lsKey = loOption.Name + "|" + loOption.Option;
                    if (!loKeyList.Contains(lsKey))
                    {
                        loOptionList.Add(loOption);
                        loKeyList.Add(lsKey);
                    }
                }

                foreach (MaxProductEntity loChild in this.GetChildList())
                {
                    foreach (MaxOptionByNameStructure loOption in loChild.OptionByNameList)
                    {
                        string lsKey = loOption.Name + "|" + loOption.Option;
                        if (!loKeyList.Contains(lsKey))
                        {
                            loOptionList.Add(loOption);
                            loKeyList.Add(lsKey);
                        }
                    }
                }

                //// Create a sorted list by display order
                SortedList<double, MaxOptionByNameStructure> loSortedList = new SortedList<double, MaxOptionByNameStructure>();
                Random loRandom = new Random();
                foreach (MaxOptionByNameStructure loOption in loOptionList)
                {
                    double lnSort = loOption.DisplayOrder + (loRandom.NextDouble() / 1000);
                    loSortedList.Add(lnSort, loOption);
                }

                List<MaxOptionByNameStructure> loOptionSortedList = new List<MaxOptionByNameStructure>(loSortedList.Values);
                foreach (MaxOptionByNameStructure loOption in loOptionSortedList)
                {
                    if (!string.IsNullOrEmpty(loOption.Name))
                    {
                        if (!this._oProductOptionByNameIndex.ContainsKey(loOption.Name))
                        {
                            this._oProductOptionByNameIndex.Add(loOption.Name, new List<MaxOptionByNameStructure>());
                        }

                        this._oProductOptionByNameIndex[loOption.Name].Add(loOption);
                    }
                }
            }

            return this._oProductOptionByNameIndex;

        }

        public virtual List<List<MaxOptionByNameStructure>> GetOptionCombinationList(List<MaxOptionByNameStructure> loOptionList)
        {
            List<List<MaxOptionByNameStructure>> loR = new List<List<MaxOptionByNameStructure>>();

            if (null != loOptionList)
            {
                //// Create a sorted list by display order
                SortedList<double, MaxOptionByNameStructure> loSortedList = new SortedList<double, MaxOptionByNameStructure>();
                Random loRandom = new Random();
                foreach (MaxOptionByNameStructure loOption in loOptionList)
                {
                    double lnSort = loOption.DisplayOrder + (loRandom.NextDouble() / 1000);
                    loSortedList.Add(lnSort, loOption);
                }

                List<MaxOptionByNameStructure> loOptionSortedList = new List<MaxOptionByNameStructure>(loSortedList.Values);

                //// Create a list of option names
                List<string> loOptionNameList = new List<string>(this.GetProductOptionByNameIndex().Keys);

                //// Create a counter and total for each option name and determine the total number of option combinations
                Dictionary<string, int> loCurrentIndex = new Dictionary<string, int>();
                int lnTotal = 1;
                foreach (string lsName in loOptionNameList)
                {
                    loCurrentIndex.Add(lsName, 0);
                    lnTotal = lnTotal * this.GetProductOptionByNameIndex()[lsName].Count;
                }

                for (int lnCurrent = 0; lnCurrent < lnTotal; lnCurrent++)
                {
                    List<MaxOptionByNameStructure> loListCurrent = new List<MaxOptionByNameStructure>();
                    for (int lnN = 0; lnN < loOptionNameList.Count; lnN++)
                    {
                        loListCurrent.Add(this.GetProductOptionByNameIndex()[loOptionNameList[lnN]][loCurrentIndex[loOptionNameList[lnN]]]);
                    }

                    loR.Add(loListCurrent);

                    //// Increment one of the counters
                    bool lbIncreased = false;
                    for (int lnN = loOptionNameList.Count - 1; lnN >= 0; lnN--)
                    {
                        if (!lbIncreased)
                        {
                            string lsName = loOptionNameList[lnN];
                            if (loCurrentIndex[lsName] < this.GetProductOptionByNameIndex()[lsName].Count - 1)
                            {
                                loCurrentIndex[lsName] = loCurrentIndex[lsName] + 1;
                                lbIncreased = true;
                            }
                            else if (loCurrentIndex[lsName] >= this.GetProductOptionByNameIndex()[lsName].Count - 1)
                            {
                                loCurrentIndex[lsName] = 0;
                            }
                        }
                    }
                }
            }

            return loR;
        }

        public virtual bool IsOverride(string lsPropertyName)
        {
            if (lsPropertyName.Equals(this.DataModel.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 0);
            }
            else if (lsPropertyName.Equals(this.DataModel.Sku, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 1);
            }
            else if (lsPropertyName.Equals(this.DataModel.DescriptionShort, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 2);
            }
            else if (lsPropertyName.Equals(this.DataModel.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 3);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBase, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 4);
            }
            else if (lsPropertyName.Equals(this.DataModel.DescriptionList, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 5);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBaseShipping, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 6);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerText, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 7);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 8);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceByQuantityList, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 9);
            }
            else if (lsPropertyName.Equals(this.DataModel.OptionByNameList, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 10);
            }
            else if (lsPropertyName.Equals(this.DataModel.Depth, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 11);
            }
            else if (lsPropertyName.Equals(this.DataModel.Height, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 12);
            }
            else if (lsPropertyName.Equals(this.DataModel.Notes, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 13);
            }
            else if (lsPropertyName.Equals(this.DataModel.MaximumQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 14);
            }
            else if (lsPropertyName.Equals(this.DataModel.MinimumQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 15);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 16);
            }
            else if (lsPropertyName.Equals(this.DataModel.InsuranceAmount, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 17);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBase, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 18);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceCompare, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 19);
            }
            else if (lsPropertyName.Equals(this.DataModel.Volume, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 20);
            }
            else if (lsPropertyName.Equals(this.DataModel.Weight, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 21);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryManufacturerId, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 22);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryManufacturerSku, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 23);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryVendorId, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 24);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryVendorSku, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 25);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryImageId, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 26);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBaseShipping, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.GetBit(this.DataModel.OverrideList, 27);
            }

            return true;
        }

        public virtual void SetOverride(string lsPropertyName, bool lbValue)
        {
            if (lsPropertyName.Equals(this.DataModel.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 0, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Sku, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 1, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.DescriptionShort, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 2, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Description, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 3, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBase, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 4, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.DescriptionList, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 5, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBaseShipping, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 6, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerText, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 7, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 8, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceByQuantityList, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 9, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.OptionByNameList, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 10, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Depth, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 11, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Height, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 12, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Notes, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 13, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.MaximumQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 14, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.MinimumQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 15, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PerQuantity, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 16, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.InsuranceAmount, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 17, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBase, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 18, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceCompare, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 19, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Volume, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 20, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.Weight, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 21, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryManufacturerId, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 22, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryManufacturerSku, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 23, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryVendorId, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 24, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryVendorSku, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 25, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PrimaryImageId, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 26, lbValue);
            }
            else if (lsPropertyName.Equals(this.DataModel.PriceBaseShipping, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SetBit(this.DataModel.OverrideList, 27, lbValue);
            }
        }

        public override bool Load(MaxData loData)
        {
            this._oBaseProduct = null;
            this._oBranchList = null;
            this._oChildList = null;
            this._oDescriptionList = null;
            this._oOptionByNameList = null;
            this._oParentProduct = null;
            this._oPriceByQuantityList = null;
            this._oProductOptionByNameIndex = null;
            return base.Load(loData);
        }

        public override bool LoadByIdCache(Guid loId)
        {
            /*
            MaxEntityList loEntityList = this.LoadAllCache();
            if (null != loEntityList)
            {
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                    if (null != loEntity && loEntity.Id == loId)
                    {
                        this.Load(loEntity.Data);
                        return true;
                    }
                }
            }
            */

            return base.LoadByIdCache(loId);
        }

        public virtual MaxEntityList LoadAllByPrimaryCategoryId(Guid loCategoryId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                if (null != loEntity && loEntity.PrimaryCategoryId == loCategoryId)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }

        public virtual MaxEntityList LoadAllByCatalogId(Guid loCatalogId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                if (null != loEntity && loEntity.PrimaryCatalogId == loCatalogId)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }

        public virtual MaxEntityList LoadAllBySku(string lsSku)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                if (null != loEntity && loEntity.Sku == lsSku)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }

        public virtual MaxEntityList LoadAllByParentId(Guid loParentId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                if (null != loEntity && loEntity.ParentId == loParentId)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }

        public virtual MaxEntityList LoadAllByRootId(Guid loRootId)
        {
            MaxEntityList loR = MaxEntityList.Create(this.GetType());
            MaxEntityList loEntityList = this.LoadAllCache();
            for (int lnE = 0; lnE < loEntityList.Count; lnE++)
            {
                MaxProductEntity loEntity = loEntityList[lnE] as MaxProductEntity;
                if (null != loEntity && loEntity.RootId == loRootId)
                {
                    loR.Add(loEntity);
                }
            }

            return loR;
        }

        public virtual MaxEntityList LoadAllBySearchText(string lsSearchText)
        {
            if (!string.IsNullOrEmpty(lsSearchText))
            {
                this.UseSearchProvider();
                MaxDataList loDataList = MaxCatalogRepository.SearchAllProductBySearchText(this.Data, lsSearchText, 20);
                MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
                return loEntityList;
            }

            return MaxEntityList.Create(this.GetType());
        }

        public virtual MaxEntityList LoadAllBySearchCategory(Guid loCategoryId)
        {
            this.UseSearchProvider();
            MaxDataList loDataList = MaxCatalogRepository.SearchAllProductByCategoryId(this.Data, loCategoryId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        protected void UseSearchProvider()
        {
            this.SetRepositoryProviderType(typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogSearchRepositoryProvider));
        }

        public override bool Insert()
        {
            if (null != this._oOptionByNameList)
            {
                this.SetObject(this.DataModel.OptionByNameList, this._oOptionByNameList);
            }

            if (null != this._oPriceByQuantityList)
            {
                this.SetObject(this.DataModel.PriceByQuantityList, this._oPriceByQuantityList);
            }

            if (null != this._oDescriptionList)
            {
                this.SetObject(this.DataModel.DescriptionList, this._oDescriptionList);
            }

            if (base.Insert())
            {
                return this.InsertSearch();
            }

            return false;
        }


        public override bool Update()
        {
            if (null != this._oOptionByNameList)
            {
                this.SetObject(this.DataModel.OptionByNameList, this._oOptionByNameList);
            }

            if (null != this._oPriceByQuantityList)
            {
                this.SetObject(this.DataModel.PriceByQuantityList, this._oPriceByQuantityList);
            }

            if (null != this._oDescriptionList)
            {
                this.SetObject(this.DataModel.DescriptionList, this._oDescriptionList);
            }

            if (base.Update())
            {
                return this.UpdateSearch();
            }

            return false;
        }

        public override bool Delete()
        {
            this.Set(this.DataModel.IsDeleted, true);
            if (base.Update())
            {
                this.UseSearchProvider();
                if (MaxCatalogRepository.Delete(this.Data))
                {
                    return true;
                }
            }

            return false;
        }

        public virtual MaxProductEntity GetSearchEntity()
        {
            MaxProductEntity loEntity = this.Copy();
            //// Set properties that can be overridden so that the base MaxData holds the values
            if (loEntity.ParentId != Guid.Empty || loEntity.RootId != Guid.Empty)
            {
                loEntity.Name = this.Name;
                loEntity.SetOverride(this.DataModel.Name, true);
                loEntity.Sku = this.Sku;
                loEntity.SetOverride(this.DataModel.Sku, true);
                loEntity.DescriptionShort = this.DescriptionShort;
                loEntity.SetOverride(this.DataModel.DescriptionShort, true);
                loEntity.Description = this.Description;
                loEntity.SetOverride(this.DataModel.Description, true);
                loEntity.PriceBase = this.PriceBase;
                loEntity.SetOverride(this.DataModel.PriceBase, true);
                loEntity.DescriptionList = this.DescriptionList;
                loEntity.SetOverride(this.DataModel.DescriptionList, true);
                loEntity.PriceBaseShipping = this.PriceBaseShipping;
                loEntity.SetOverride(this.DataModel.PriceBaseShipping, true);
                loEntity.PerText = this.PerText;
                loEntity.SetOverride(this.DataModel.PerText, true);
                loEntity.PerQuantity = this.PerQuantity;
                loEntity.SetOverride(this.DataModel.PerQuantity, true);
                loEntity.PriceByQuantityList = this.PriceByQuantityList;
                loEntity.SetOverride(this.DataModel.PriceByQuantityList, true);
                loEntity.OptionByNameList = this.OptionByNameList;
                loEntity.SetOverride(this.DataModel.OptionByNameList, true);
                loEntity.Depth = this.Depth;
                loEntity.SetOverride(this.DataModel.Depth, true);
                loEntity.Height = this.Height;
                loEntity.SetOverride(this.DataModel.Height, true);
                loEntity.Notes = this.Notes;
                loEntity.SetOverride(this.DataModel.Notes, true);
                loEntity.MaximumQuantity = this.MaximumQuantity;
                loEntity.SetOverride(this.DataModel.MaximumQuantity, true);
                loEntity.MinimumQuantity = this.MinimumQuantity;
                loEntity.SetOverride(this.DataModel.MinimumQuantity, true);
                loEntity.PerQuantity = this.PerQuantity;
                loEntity.SetOverride(this.DataModel.PerQuantity, true);
                loEntity.InsuranceAmount = this.InsuranceAmount;
                loEntity.SetOverride(this.DataModel.InsuranceAmount, true);
                loEntity.PriceBase = this.PriceBase;
                loEntity.SetOverride(this.DataModel.PriceBase, true);
                loEntity.PriceCompare = this.PriceCompare;
                loEntity.SetOverride(this.DataModel.PriceCompare, true);
                loEntity.Volume = this.Volume;
                loEntity.SetOverride(this.DataModel.Volume, true);
                loEntity.Weight = this.Weight;
                loEntity.SetOverride(this.DataModel.Weight, true);
                loEntity.PrimaryManufacturerId = this.PrimaryManufacturerId;
                loEntity.SetOverride(this.DataModel.PrimaryManufacturerId, true);
                loEntity.PrimaryManufacturerSku = this.PrimaryManufacturerSku;
                loEntity.SetOverride(this.DataModel.PrimaryManufacturerSku, true);
                loEntity.PrimaryVendorId = this.PrimaryVendorId;
                loEntity.SetOverride(this.DataModel.PrimaryVendorId, true);
                loEntity.PrimaryVendorSku = this.PrimaryVendorSku;
                loEntity.SetOverride(this.DataModel.PrimaryVendorSku, true);
                loEntity.PrimaryImageId = this.PrimaryImageId;
                loEntity.SetOverride(this.DataModel.PrimaryImageId, true);
                loEntity.PriceBaseShipping = this.PriceBaseShipping;
                loEntity.SetOverride(this.DataModel.PriceBaseShipping, true);
            }

            loEntity.UseSearchProvider();
            return loEntity;
        }

        public virtual bool InsertSearch()
        {
            MaxProductEntity loEntity = this.GetSearchEntity();
            return MaxCatalogRepository.Insert(loEntity.Data);
        }

        public virtual bool UpdateSearch()
        {
            MaxProductEntity loEntity = this.GetSearchEntity();
            return MaxCatalogRepository.Update(loEntity.Data);
        }

        public virtual MaxProductEntity CreateBranch()
        {
            MaxProductEntity loEntity = MaxProductEntity.Create();
            loEntity.RootId = this.Id;
            return loEntity;
        }

        public virtual MaxProductEntity CreateChild()
        {
            MaxProductEntity loEntity = MaxProductEntity.Create();
            loEntity.ParentId = this.Id;
            return loEntity;
        }

        public virtual MaxProductEntity Copy()
        {
            MaxProductEntity loEntity = MaxProductEntity.Create();
            loEntity.Load(this.GetData());
            return loEntity;
        }
    }
}
