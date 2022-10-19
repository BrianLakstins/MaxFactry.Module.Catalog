// <copyright file="MaxProductViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="6/29/2015" author="Brian A. Lakstins" description="Use OptionCombinationStructure instead of CartAddStructure.">
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
    public class MaxProductViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxProductViewModel> _oSortedList = null;

        private MaxCatalogViewModel _oCatalog = new MaxCatalogViewModel();

        private MaxVendorViewModel _oVendor = new MaxVendorViewModel();

        private MaxManufacturerViewModel _oManufacturer = new MaxManufacturerViewModel();

        private MaxCategoryViewModel _oCategory = new MaxCategoryViewModel();

        private List<MaxDescriptionListStructure> _oDescriptionList = null;

        private List<MaxPriceByQuantityStructure> _oPriceList = null;

        private List<MaxOptionByNameStructure> _oOptionList = null;

        private List<MaxOptionCombinationStructure> _oOptionCombinationList = null;

        private List<MaxProductImageViewModel> _oImageList = null;

        private MaxProductViewModel _oBaseProduct = null;

        private MaxProductViewModel _oParentProduct = null;

        /// <summary>
        /// Initializes a new instance of the MaxProductViewModel class
        /// </summary>
        public MaxProductViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxProductViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxProductViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxProductViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxProductEntity.Create();
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Catalog")]
        [Required]
        public string PrimaryCatalogId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "SKU")]
        public string Sku
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Item Price")]
        [Required]
        public double PriceBase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Price Text")]
        public string PerText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Price Quantity")]
        public string PerQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Shipping Price")]
        [Required]
        public double PriceBaseShipping
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Excerpt")]
        public string DescriptionShort
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Manufacturer")]
        public string PrimaryManufacturerId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Vendor")]
        public string PrimaryVendorId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Primar yManufacturer SKU")]
        public string PrimaryManufacturerSku
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Vendor SKU")]
        public string PrimaryVendorSku
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Description")]
        public string Description
        {
            get;
            set;
        }

        public string BranchType
        {
            get;
            set;
        }

        [Display(Name = "Descriptions")]
        [UIHint("String")]
        public List<MaxDescriptionListStructure> DescriptionList
        {
            get
            {
                if (null == this._oDescriptionList)
                {
                    this._oDescriptionList = new List<MaxDescriptionListStructure>();
                }

                if (this._oDescriptionList.Count.Equals(0) || !string.IsNullOrEmpty(this._oDescriptionList[this._oDescriptionList.Count - 1].Title))
                {
                    this._oDescriptionList.Add(new MaxDescriptionListStructure());
                    this._oDescriptionList.Add(new MaxDescriptionListStructure());
                    this._oDescriptionList.Add(new MaxDescriptionListStructure());
                    this._oDescriptionList.Add(new MaxDescriptionListStructure());
                }

                return this._oDescriptionList;
            }
            set
            {
                this._oDescriptionList = value;
            }
        }

        [Display(Name = "Prices")]
        [UIHint("String")]
        public List<MaxPriceByQuantityStructure> PriceByQuantityList
        {
            get
            {
                if (null == this._oPriceList)
                {
                    this._oPriceList = new List<MaxPriceByQuantityStructure>();
                }

                if (this._oPriceList.Count.Equals(0) || !this._oPriceList[this._oPriceList.Count - 1].Price.Equals(0))
                {
                    this._oPriceList.Add(new MaxPriceByQuantityStructure());
                    this._oPriceList.Add(new MaxPriceByQuantityStructure());
                    this._oPriceList.Add(new MaxPriceByQuantityStructure());
                    this._oPriceList.Add(new MaxPriceByQuantityStructure());
                }

                return this._oPriceList;
            }
            set
            {
                this._oPriceList = value;
            }
        }

        [Display(Name = "Options")]
        [UIHint("String")]
        public List<MaxOptionByNameStructure> OptionByNameList
        {
            get
            {
                if (null == this._oOptionList)
                {
                    this._oOptionList = new List<MaxOptionByNameStructure>();
                }

                if (this._oOptionList.Count.Equals(0) || !string.IsNullOrEmpty(this._oOptionList[this._oOptionList.Count - 1].Name))
                {
                    this._oOptionList.Add(new MaxOptionByNameStructure());
                    this._oOptionList.Add(new MaxOptionByNameStructure());
                    this._oOptionList.Add(new MaxOptionByNameStructure());
                    this._oOptionList.Add(new MaxOptionByNameStructure());
                }

                return this._oOptionList;
            }
            set
            {
                this._oOptionList = value;
            }
        }

        /// <summary>
        /// Gets or sets the BaseId (the product this one is based on)
        /// </summary>
        [Display(Name = "Root Product")]
        public string RootId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        public MaxProductViewModel RootProduct
        {
            get
            {
                if (null == this._oBaseProduct)
                {
                    this._oBaseProduct = new MaxProductViewModel(this.RootId);
                }

                return this._oBaseProduct;
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Parent Product")]
        public string ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        public MaxProductViewModel ParentProduct
        {
            get
            {
                if (null == this._oParentProduct)
                {
                    this._oParentProduct = new MaxProductViewModel(this.ParentId);
                }

                return this._oParentProduct;
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Child List")]
        public string ChildListText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Branch List")]
        public string BranchListText
        {
            get;
            set;
        }

        public List<MaxProductViewModel> GetChildList()
        {
            List<MaxProductViewModel> loR = new List<MaxProductViewModel>();
            MaxProductEntity loEntity = this.Entity as MaxProductEntity;
            List<MaxProductEntity> loList = loEntity.GetChildList();
            foreach (MaxProductEntity loChild in loList)
            {
                MaxProductViewModel loModel = new MaxProductViewModel(loChild);
                loModel.Load();
                loR.Add(loModel);
            }

            return loR;
        }

        public List<MaxProductViewModel> GetBranchList()
        {
            List<MaxProductViewModel> loR = new List<MaxProductViewModel>();
            MaxProductEntity loEntity = this.Entity as MaxProductEntity;
            List<MaxProductEntity> loList = loEntity.GetBranchList();
            foreach (MaxProductEntity loChild in loList)
            {
                MaxProductViewModel loModel = new MaxProductViewModel(loChild);
                loModel.Load();
                loR.Add(loModel);
            }

            return loR;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Depth")]
        public string Depth
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Height")]
        public string Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Insurance")]
        public string InsuranceAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Key Words")]
        public string Keywords
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Maximum Quantity")]
        public string MaximumQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Minimum Quantity")]
        public string MinimumQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Notes")]
        public string Notes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Compare")]
        public string PriceCompare
        {
            get;
            set;
        }



        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Volume")]
        public string Volume
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Weight")]
        public string Weight
        {
            get;
            set;
        }



        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Category")]
        public string PrimaryCategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Image")]
        public string PrimaryImageId
        {
            get;
            set;
        }

        public List<MaxProductImageViewModel> ImageList
        {
            get
            {
                if (null == this._oImageList)
                {
                    this._oImageList = new List<MaxProductImageViewModel>();
                    MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                    if (!string.IsNullOrEmpty(this.Id))
                    {
                        Guid loProductId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.Id);
                        MaxEntityList loList = MaxProductImageEntity.Create().LoadAllByProductId(loProductId);
                        for (int lnE = 0; lnE < loList.Count; lnE++)
                        {
                            MaxProductImageViewModel loView = new MaxProductImageViewModel(loList[lnE]);
                            loView.Load();
                            if (loView.Id == this.PrimaryImageId)
                            {
                                this._oImageList.Insert(0, loView);
                            }
                            else
                            {
                                this._oImageList.Add(loView);
                            }
                        }
                    }
                }

                return this._oImageList;
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Categories")]
        public string CategoryIdList
        {
            get;
            set;
        }

        public List<string> SkuList
        {
            get
            {
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                return loEntity.GetSkuList();
            }
        }

        /// <summary>
        /// Gets the property information
        /// </summary>
        public string PrimaryCatalogName
        {
            get
            {
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;

                if (!Guid.Empty.Equals(loEntity.PrimaryCatalogId))
                {
                    MaxCatalogEntity loCatalog = MaxCatalogEntity.Create();
                    if (loCatalog.LoadByIdCache(loEntity.PrimaryCatalogId))
                    {
                        return loCatalog.Name;
                    }
                }

                return "None";
            }
        }

        public string SearchText { get; set; }



        /// <summary>
        /// Gets the property information
        /// </summary>
        public bool IsEditable
        {
            get
            {
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                if (loEntity.BranchType.StartsWith("import", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsOverrideName
        {
            get;
            set;
        }

        public bool IsOverrideSku
        {
            get;
            set;
        }
        public bool IsOverrideDescriptionShort
        {
            get;
            set;
        }
        public bool IsOverrideDescription
        {
            get;
            set;
        }


        public bool IsOverridePriceBase
        {
            get;
            set;
        }

        public bool IsOverrideDescriptionList
        {
            get;
            set;
        }

        public bool IsOverridePriceBaseShipping
        {
            get;
            set;
        }

        public bool IsOverridePerText
        {
            get;
            set;
        }

        public bool IsOverridePerQuantity
        {
            get;
            set;
        }

        public bool IsOverridePriceByQuantityList
        {
            get;
            set;
        }

        public bool IsOverrideOptionByNameList
        {
            get;
            set;
        }

        public bool IsOverridePrimaryImageId
        {
            get;
            set;
        }

        public List<MaxManufacturerViewModel> ManufacturerList
        {
            get
            {
                List<MaxManufacturerViewModel> loList = new List<MaxManufacturerViewModel>(this._oManufacturer.GetSortedList());
                MaxManufacturerViewModel loModel = new MaxManufacturerViewModel();
                loModel.Name = "None";
                loList.Insert(0, loModel);
                return loList;
            }
        }

        public List<MaxCategoryViewModel> CategoryList
        {
            get
            {
                List<MaxCategoryViewModel> loR = new List<MaxCategoryViewModel>();
                List<MaxCategoryViewModel> loList = this._oCategory.GetSortedList();
                foreach (MaxCategoryViewModel loCategory in loList)
                {
                    if (this.PrimaryCatalogId == null)
                    {
                        loR.Add(loCategory);
                    }
                    else if (this.PrimaryCatalogId == loCategory.CatalogId)
                    {
                        loR.Add(loCategory);
                    }
                }

                return loR;
            }
        }

        public List<MaxCatalogViewModel> CatalogList
        {
            get
            {
                return this._oCatalog.GetSortedList();
            }
        }

        public List<MaxVendorViewModel> VendorList
        {
            get
            {
                List<MaxVendorViewModel> loList = new List<MaxVendorViewModel>(this._oVendor.GetSortedList());
                MaxVendorViewModel loModel = new MaxVendorViewModel();
                loModel.Name = "None";
                loList.Insert(0, loModel);
                return loList;
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxProductViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxProductViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxProductViewModel loViewModel = new MaxProductViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public List<MaxProductViewModel> GetSearchList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxProductViewModel>();
                if (!string.IsNullOrEmpty(this.SearchText))
                {
                    MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                    MaxEntityList loList = loEntity.LoadAllBySearchText(this.SearchText);
                    if (loList.Count.Equals(0) || this.SearchText == "ALL PRODUCTS")
                    {
                        return this.GetSortedList();
                    }

                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        MaxProductViewModel loViewModel = new MaxProductViewModel(loList[lnE] as MaxEntity);
                        loViewModel.Load();
                        this._oSortedList.Add(loViewModel);
                    }
                }
            }

            return this._oSortedList;
        }


        public List<MaxProductViewModel> GetSearchCategoryList(string lsCategoryId)
        {
            Guid loCategoryId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsCategoryId);
            if (null == this._oSortedList && !Guid.Empty.Equals(loCategoryId))
            {
                this._oSortedList = new List<MaxProductViewModel>();
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                MaxEntityList loList = loEntity.LoadAllBySearchCategory(loCategoryId);

                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    MaxProductViewModel loViewModel = new MaxProductViewModel(loList[lnE] as MaxEntity);
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
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    if (!string.IsNullOrEmpty(this.Depth))
                    {
                        loEntity.Depth = double.Parse(this.Depth);
                    }

                    loEntity.Description = this.Description;
                    loEntity.DescriptionShort = this.DescriptionShort;
                    if (!string.IsNullOrEmpty(this.Height))
                    {
                        loEntity.Height = double.Parse(this.Height);
                    }

                    if (!string.IsNullOrEmpty(this.InsuranceAmount))
                    {
                        loEntity.InsuranceAmount = double.Parse(this.InsuranceAmount);
                    }

                    loEntity.Keywords = this.Keywords;
                    if (!string.IsNullOrEmpty(this.MaximumQuantity))
                    {
                        loEntity.MaximumQuantity = int.Parse(this.MaximumQuantity);
                    }

                    if (!string.IsNullOrEmpty(this.MinimumQuantity))
                    {
                        loEntity.MinimumQuantity = int.Parse(this.MinimumQuantity);
                    }

                    loEntity.Notes = this.Notes;
                    if (!string.IsNullOrEmpty(this.PerQuantity))
                    {
                        loEntity.PerQuantity = int.Parse(this.PerQuantity);
                    }

                    loEntity.PerText = this.PerText;
                    if (this.PriceBase >= 0)
                    {
                        loEntity.PriceBase = this.PriceBase;
                    }

                    if (this.PriceBaseShipping >= 0)
                    {
                        loEntity.PriceBaseShipping = this.PriceBaseShipping;
                    }

                    double lnPriceCompare = MaxConvertLibrary.ConvertToDouble(typeof(object), this.PriceCompare);
                    if (lnPriceCompare >= 0)
                    {
                        loEntity.PriceCompare = lnPriceCompare;
                    }

                    loEntity.Sku = this.Sku;
                    if (!string.IsNullOrEmpty(this.Volume))
                    {
                        loEntity.Volume = double.Parse(this.Volume);
                    }

                    if (!string.IsNullOrEmpty(this.Weight))
                    {
                        loEntity.Weight = double.Parse(this.Weight);
                    }

                    if (!string.IsNullOrEmpty(this.PrimaryCatalogId))
                    {
                        loEntity.PrimaryCatalogId = Guid.Parse(this.PrimaryCatalogId);
                    }

                    if (!string.IsNullOrEmpty(this.PrimaryCategoryId))
                    {
                        loEntity.PrimaryCategoryId = Guid.Parse(this.PrimaryCategoryId);
                    }

                    if (!string.IsNullOrEmpty(this.PrimaryManufacturerId))
                    {
                        loEntity.PrimaryManufacturerId = Guid.Parse(this.PrimaryManufacturerId);
                    }

                    loEntity.PrimaryManufacturerSku = this.PrimaryManufacturerSku;
                    if (!string.IsNullOrEmpty(this.PrimaryVendorId))
                    {
                        loEntity.PrimaryVendorId = Guid.Parse(this.PrimaryVendorId);
                    }

                    loEntity.PrimaryVendorSku = this.PrimaryVendorSku;

                    loEntity.PriceByQuantityList = this.PriceByQuantityList;

                    loEntity.DescriptionList = this.DescriptionList;

                    loEntity.OptionByNameList = this.OptionByNameList;

                    loEntity.SetOverride("Name", this.IsOverrideName);
                    loEntity.SetOverride("Sku", this.IsOverrideSku);
                    loEntity.SetOverride("DescriptionShort", this.IsOverrideDescriptionShort);
                    loEntity.SetOverride("Description", this.IsOverrideDescription);
                    loEntity.SetOverride("PriceBase", this.IsOverridePriceBase);
                    loEntity.SetOverride("PriceBaseShipping", this.IsOverridePriceBaseShipping);
                    loEntity.SetOverride("DescriptionList", this.IsOverrideDescriptionList);
                    loEntity.SetOverride("PerText", this.IsOverridePerText);
                    loEntity.SetOverride("PerQuantity", this.IsOverridePerQuantity);
                    loEntity.SetOverride("PriceByQuantityList", this.IsOverridePriceByQuantityList);
                    loEntity.SetOverride("OptionByNameList", this.IsOverrideOptionByNameList);
                    loEntity.SetOverride("PrimaryImageId", this.IsOverridePrimaryImageId);

                    if (!string.IsNullOrEmpty(this.PrimaryImageId))
                    {
                        loEntity.PrimaryImageId = Guid.Parse(this.PrimaryImageId);
                    }

                    loEntity.CategoryIdList = this.CategoryIdList;
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
                MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    if (!Guid.Empty.Equals(loEntity.RootId))
                    {
                        this.RootId = loEntity.RootId.ToString();
                    }

                    if (!Guid.Empty.Equals(loEntity.ParentId))
                    {
                        this.ParentId = loEntity.ParentId.ToString();
                    }

                    if (loEntity.Depth != double.MinValue)
                    {
                        this.Depth = loEntity.Depth.ToString();
                    }

                    this.Description = loEntity.Description;
                    this.DescriptionShort = loEntity.DescriptionShort;
                    if (loEntity.Height != double.MinValue)
                    {
                        this.Height = loEntity.Height.ToString();
                    }

                    if (loEntity.InsuranceAmount != double.MinValue)
                    {
                        this.InsuranceAmount = loEntity.InsuranceAmount.ToString();
                    }

                    this.Keywords = loEntity.Keywords;
                    if (loEntity.MaximumQuantity != int.MinValue)
                    {
                        this.MaximumQuantity = loEntity.MaximumQuantity.ToString();
                    }

                    if (loEntity.MinimumQuantity != int.MinValue)
                    {
                        this.MinimumQuantity = loEntity.MinimumQuantity.ToString();
                    }

                    this.Notes = loEntity.Notes;
                    if (loEntity.PerQuantity >= 0)
                    {
                        this.PerQuantity = loEntity.PerQuantity.ToString();
                    }

                    this.PerText = loEntity.PerText;
                    if (loEntity.PriceBase >= 0)
                    {
                        this.PriceBase = loEntity.PriceBase;
                    }

                    if (loEntity.PriceBaseShipping >= 0)
                    {
                        this.PriceBaseShipping = loEntity.PriceBaseShipping;
                    }

                    if (loEntity.PriceCompare >= 0)
                    {
                        this.PriceCompare = loEntity.PriceCompare.ToString();
                    }

                    this.Sku = loEntity.Sku;
                    if (loEntity.Volume != double.MinValue)
                    {
                        this.Volume = loEntity.Volume.ToString();
                    }

                    if (loEntity.Weight != double.MinValue)
                    {
                        this.Weight = loEntity.Weight.ToString();
                    }

                    this.PrimaryCatalogId = loEntity.PrimaryCatalogId.ToString();
                    this.PrimaryCategoryId = loEntity.PrimaryCategoryId.ToString();
                    this.PrimaryManufacturerId = loEntity.PrimaryManufacturerId.ToString();
                    this.PrimaryVendorId = loEntity.PrimaryVendorId.ToString();
                    this.PrimaryManufacturerSku = loEntity.PrimaryManufacturerSku;
                    this.PrimaryVendorSku = loEntity.PrimaryVendorSku;

                    this.PriceByQuantityList = loEntity.PriceByQuantityList;
                    this.DescriptionList = loEntity.DescriptionList;
                    this.OptionByNameList = loEntity.OptionByNameList;
                    this.BranchType = loEntity.BranchType;

                    this.IsOverrideName = loEntity.IsOverride("Name");
                    this.IsOverrideSku = loEntity.IsOverride("Sku");
                    this.IsOverrideDescriptionShort = loEntity.IsOverride("DescriptionShort");
                    this.IsOverrideDescription = loEntity.IsOverride("Description");
                    this.IsOverridePriceBase = loEntity.IsOverride("PriceBase");
                    this.IsOverridePriceBaseShipping = loEntity.IsOverride("PriceBaseShipping");
                    this.IsOverrideDescriptionList = loEntity.IsOverride("DescriptionList");
                    this.IsOverridePerText = loEntity.IsOverride("PerText");
                    this.IsOverridePerQuantity = loEntity.IsOverride("PerQuantity");
                    this.IsOverridePriceByQuantityList = loEntity.IsOverride("PriceByQuantityList");
                    this.IsOverrideOptionByNameList = loEntity.IsOverride("OptionByNameList");
                    this.IsOverridePrimaryImageId = loEntity.IsOverride("PrimaryImageId");

                    if (!Guid.Empty.Equals(loEntity.PrimaryImageId))
                    {
                        this.PrimaryImageId = loEntity.PrimaryImageId.ToString();
                    }

                    this.CategoryIdList = loEntity.CategoryIdList;
                    return true;
                }
            }

            return false;
        }

        public Guid CreateBranch()
        {
            MaxProductEntity loEntity = this.Entity as MaxProductEntity;
            MaxProductEntity loEntityNew = loEntity.CreateBranch();
            loEntityNew.Insert();
            return loEntityNew.Id;
        }

        public Guid CreateChild()
        {
            MaxProductEntity loEntity = this.Entity as MaxProductEntity;
            MaxProductEntity loEntityNew = loEntity.CreateChild();
            loEntityNew.Insert();
            return loEntityNew.Id;
        }

        public List<MaxOptionByNameStructure> GetOptionSelectListByName(string lsName)
        {
            MaxProductEntity loEntity = this.Entity as MaxProductEntity;

            List<MaxOptionByNameStructure> loList = loEntity.GetProductOptionByNameIndex()[lsName];
            foreach (MaxOptionByNameStructure loOption in loList)
            {
                loOption.DisplayText = GetOptionText(loOption);
            }

            return loList;
        }

        public List<MaxOptionCombinationStructure> OptionCombinationList
        {
            get
            {
                if (null == this._oOptionCombinationList)
                {
                    this._oOptionCombinationList = new List<MaxOptionCombinationStructure>();
                    MaxProductEntity loEntity = this.Entity as MaxProductEntity;
                    Dictionary<string, List<MaxOptionByNameStructure>> loProductOptionByNameIndex = loEntity.GetProductOptionByNameIndex();
                    MaxOptionCombinationStructure loOptionCombination = new MaxOptionCombinationStructure();
                    loOptionCombination.Quantity = 1;
                    foreach (string lsName in loProductOptionByNameIndex.Keys)
                    {
                        //// Add one OptionByName per name for the combination
                        MaxOptionByNameStructure loOption = new MaxOptionByNameStructure();
                        loOption.Name = lsName;
                        //// The option will be used for the default value in a selection list
                        loOption.Option = string.Empty;
                        loOptionCombination.OptionPerNameList.Add(loOption);
                    }

                    this._oOptionCombinationList.Add(loOptionCombination);
                }

                return this._oOptionCombinationList;
            }

            set
            {
                this._oOptionCombinationList = value;
            }
        }

        private string GetOptionText(MaxOptionByNameStructure loOption)
        {
            string lsR = loOption.Option;
            if (loOption.AdditionalPrice != Double.MinValue)
            {
                if (loOption.AdditionalPrice > 0)
                {
                    lsR += " (add " + string.Format("{0:C}", loOption.AdditionalPrice) + ")";
                }
                else if (loOption.AdditionalPrice < 0)
                {
                    lsR += " (subtract " + string.Format("{0:C}", loOption.AdditionalPrice) + ")";
                }
            }

            return lsR;
        }
    }
}
