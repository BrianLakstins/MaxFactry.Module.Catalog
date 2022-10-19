// <copyright file="MaxProductManageViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="9/16/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="9/30/2014" author="Brian A. Lakstins" description="Added product update to create searchable products.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Web;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;

	/// <summary>
    /// View model for managing clients.
	/// </summary>
	public class MaxProductImportViewModel
	{
        public int ImportGroup = 0;

        public string Command { get; set; }

        public const int RuleTypeReplace = 101;

        public const int RuleTypeCategory = 201;

        private List<MaxProductImportRuleViewModel> _oProductCopyRuleList = null;

        private List<MaxProductImportRuleViewModel> _oCategoryMapRuleList = null;

        private List<MaxProductImportRuleViewModel> _oProductCreateRuleList = null;

        private List<MaxProductImportRuleTypeStructure> _oProductCreateRuleTypeList = null;

        private List<MaxProductImportRuleTypeStructure> _oProductCopyRuleTypeList = null;

        private string _sCatalogIdText = string.Empty;

        private string _sShopCatalogIdText = string.Empty;

        private string _sVendorIdText = string.Empty;

        private Guid _oCatalogId = Guid.Empty;

        private Guid _oShopCatalogId = Guid.Empty;

        private Guid _oVendorId = Guid.Empty;

        public MaxProductImportViewModel()
        {
        }

        private List<MaxCatalogViewModel> _oCatalogList = null;

        /// <summary>
        /// Internal storage of list of entities
        /// </summary>
        private List<MaxVendorViewModel> _oVendorList = null;

        private List<MaxProductImportRuleEntity> _oProductImportRuleEntityList = null;

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Import Catalog")]
        public string CatalogIdText
        {
            get
            {
                if (this._sCatalogIdText == string.Empty)
                {
                    this._sCatalogIdText = this.CatalogId.ToString();
                }

                return this._sCatalogIdText;
            }

            set
            {
                this._sCatalogIdText = value;
            }
        }

        public Guid CatalogId
        {
            get
            {
                if (this._oCatalogId.Equals(Guid.Empty))
                {
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType.Equals(1) && !string.IsNullOrEmpty(loEntity.RuleData1))
                        {
                            this._oCatalogId = new Guid(loEntity.RuleData1);
                        }
                    }
                }

                return this._oCatalogId;
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Shop Catalog")]
        public string ShopCatalogIdText
        {
            get
            {
                if (this._sShopCatalogIdText == string.Empty)
                {
                    this._sShopCatalogIdText = this.ShopCatalogId.ToString();
                }

                return this._sShopCatalogIdText;
            }

            set
            {
                this._sShopCatalogIdText = value;
            }
        }

        public Guid ShopCatalogId
        {
            get
            {
                if (this._oShopCatalogId.Equals(Guid.Empty))
                {
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType.Equals(3) && !string.IsNullOrEmpty(loEntity.RuleData1))
                        {
                            this._oShopCatalogId = new Guid(loEntity.RuleData1);
                        }
                    }
                }

                return this._oShopCatalogId;
            }
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public List<MaxCatalogViewModel> CatalogList
        {
            get
            {
                if (null == this._oCatalogList)
                {
                    this._oCatalogList = new List<MaxCatalogViewModel>();
                    MaxCatalogViewModel loCatalogSelect = new MaxCatalogViewModel();
                    loCatalogSelect.Name = "Select one ...";
                    this._oCatalogList.Add(loCatalogSelect);
                    MaxCatalogViewModel loModel = new MaxCatalogViewModel();
                    this._oCatalogList.AddRange(loModel.GetSortedList());
                }

                return this._oCatalogList;
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Vendor")]
        public string VendorIdText
        {
            get
            {
                if (this._sVendorIdText == string.Empty)
                {
                    this._sVendorIdText = this.VendorId.ToString();
                }

                return this._sVendorIdText;
            }

            set
            {
                this._sVendorIdText = value;
            }
        }

        public Guid VendorId
        {
            get
            {
                if (this._oVendorId.Equals(Guid.Empty))
                {
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType.Equals(2) && !string.IsNullOrEmpty(loEntity.RuleData1))
                        {
                            this._oVendorId = new Guid(loEntity.RuleData1);
                        }
                    }
                }

                return this._oVendorId;
            }
        }

        /// <summary>
        /// Gets a list of all entities
        /// </summary>
        public List<MaxVendorViewModel> VendorList
        {
            get
            {
                if (null == this._oVendorList)
                {
                    this._oVendorList =  new List<MaxVendorViewModel>();
                    MaxVendorViewModel loModel = new MaxVendorViewModel();
                    loModel.Name = "None";
                    this._oVendorList.Add(loModel);
                    this._oVendorList.AddRange(loModel.GetSortedList());
                }

                return this._oVendorList;
            }
        }

        public List<MaxProductImportRuleTypeStructure> ProductCreateRuleTypeList
        {
            get
            {
                if (null == this._oProductCreateRuleTypeList)
                {
                    this._oProductCreateRuleTypeList = new List<MaxProductImportRuleTypeStructure>();
                    MaxProductImportRuleTypeStructure loRuleType0 = new MaxProductImportRuleTypeStructure();
                    loRuleType0.Num = 0;
                    loRuleType0.Name = "Select One";
                    this._oProductCreateRuleTypeList.Add(loRuleType0);


                    MaxProductImportRuleTypeStructure loRuleType1 = new MaxProductImportRuleTypeStructure();
                    loRuleType1.Num = RuleTypeReplace;
                    loRuleType1.Name = "Replace Text (From->To)";
                    this._oProductCreateRuleTypeList.Add(loRuleType1);
                }

                return this._oProductCreateRuleTypeList;
            }
        }

        public List<MaxProductImportRuleTypeStructure> ProductCopyRuleTypeList
        {
            get
            {
                if (null == this._oProductCopyRuleTypeList)
                {
                    this._oProductCopyRuleTypeList = new List<MaxProductImportRuleTypeStructure>();
                    MaxProductImportRuleTypeStructure loRuleType0 = new MaxProductImportRuleTypeStructure();
                    loRuleType0.Num = 0;
                    loRuleType0.Name = "Select One";
                    this._oProductCopyRuleTypeList.Add(loRuleType0);


                    //MaxProductImportRuleTypeStructure loRuleType1 = new MaxProductImportRuleTypeStructure();
                    //loRuleType1.Num = RuleTypeCategory;
                    //loRuleType1.Name = "Category (Imported Name->Target Path)";
                    //this._oProductCopyRuleTypeList.Add(loRuleType1);
                }

                return this._oProductCopyRuleTypeList;
            }
        }

        protected List<MaxProductImportRuleEntity> ProductImportRuleEntityList
        {
            get
            {
                if (null == this._oProductImportRuleEntityList)
                {
                    MaxEntityList loList = MaxProductImportRuleEntity.Create().LoadAllByImportGroup(ImportGroup);
                    SortedList<string, MaxProductImportRuleEntity> loSortedList = new SortedList<string, MaxProductImportRuleEntity>();
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        loSortedList.Add(((MaxProductImportRuleEntity)loList[lnE]).GetDefaultSortString(), (MaxProductImportRuleEntity)loList[lnE]);
                    }

                    this._oProductImportRuleEntityList = new List<MaxProductImportRuleEntity>(loSortedList.Values);
                }

                return this._oProductImportRuleEntityList;
            }
        }

        [Display(Name = "Product Creation Rules")]
        public List<MaxProductImportRuleViewModel> ProductCreateRuleList
        {
            get
            {
                if (null == this._oProductCreateRuleList)
                {
                    this._oProductCreateRuleList = new List<MaxProductImportRuleViewModel>();
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType > 100 && loEntity.RuleType < 200)
                        {
                            MaxProductImportRuleViewModel loModel = new MaxProductImportRuleViewModel(loEntity);
                            loModel.Load();
                            this._oProductCreateRuleList.Add(loModel);
                        }
                    }

                    this._oProductCreateRuleList.Add(new MaxProductImportRuleViewModel());
                    this._oProductCreateRuleList.Add(new MaxProductImportRuleViewModel());
                    this._oProductCreateRuleList.Add(new MaxProductImportRuleViewModel());
                    this._oProductCreateRuleList.Add(new MaxProductImportRuleViewModel());
                }

                return this._oProductCreateRuleList;
            }
            set
            {
                this._oProductCreateRuleList = value;
            }
        }


        [Display(Name = "Category Mapping")]
        public List<MaxProductImportRuleViewModel> CategoryMapRuleList
        {
            get
            {
                if (null == this._oCategoryMapRuleList)
                {
                    this._oCategoryMapRuleList = new List<MaxProductImportRuleViewModel>();
                    MaxIndex loFromIndex = new MaxIndex();
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType.Equals(RuleTypeCategory))
                        {
                            if (!loFromIndex.Contains(loEntity.Name))
                            {
                                loFromIndex.Add(loEntity.Name, loEntity);
                            }
                            else 
                            {
                                MaxProductImportRuleEntity loCurrent = loFromIndex[loEntity.Name] as MaxProductImportRuleEntity;
                                //// Make sure to use the first one created if more than one was created.
                                if (loEntity.CreatedDate < loCurrent.CreatedDate)
                                {
                                    loCurrent.Delete();
                                    loFromIndex[loEntity.Name] = loEntity;
                                }
                                else
                                {
                                    loEntity.Delete();
                                }
                            }
                        }
                    }

                    MaxCategoryEntity loCategory = MaxCategoryEntity.Create();
                    MaxEntityList loCategoryList = loCategory.LoadAllByCatalogId(this.CatalogId);
                    string lsSeparator = Guid.NewGuid().ToString();
                    loCategoryList = loCategory.SortByPath(loCategoryList, lsSeparator);
                    for (int lnC = 0; lnC < loCategoryList.Count; lnC++)
                    {
                        loCategory = (MaxCategoryEntity)loCategoryList[lnC];
                        if (!loFromIndex.Contains(loCategory.Path.Replace(lsSeparator, "/")))
                        {
                            MaxProductImportRuleEntity loEntity = MaxProductImportRuleEntity.Create();
                            loEntity.RuleType = RuleTypeCategory;
                            loEntity.Name = loCategory.Path.Replace(lsSeparator, "/");
                            loEntity.Key = loCategory.Id.ToString();
                            loFromIndex.Add(loEntity.Name, loEntity);
                        }
                    }

                    string[] laKey = loFromIndex.GetSortedKeyList();
                    for (int lnK = 0; lnK < laKey.Length; lnK++)
                    {
                        MaxProductImportRuleViewModel loModel = new MaxProductImportRuleViewModel(loFromIndex[laKey[lnK]] as MaxProductImportRuleEntity);
                        loModel.Load();
                        this._oCategoryMapRuleList.Add(loModel);
                    }
                }

                return this._oCategoryMapRuleList;
            }
            set
            {
                this._oCategoryMapRuleList = value;
            }
        }


        [Display(Name = "Product Copy Rules")]
        public List<MaxProductImportRuleViewModel> ProductCopyRuleList
        {
            get
            {
                if (null == this._oProductCopyRuleList)
                {
                    this._oProductCopyRuleList = new List<MaxProductImportRuleViewModel>();
                    MaxIndex loFromIndex = new MaxIndex();
                    foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                    {
                        if (loEntity.RuleType > 201 && loEntity.RuleType < 300)
                        {
                            MaxProductImportRuleViewModel loModel = new MaxProductImportRuleViewModel(loEntity);
                            loModel.Load();
                            this._oProductCopyRuleList.Add(loModel);
                            loFromIndex.Add(loEntity.Key, true);
                        }
                    }

                    for (int lnP = 0; lnP < 4; lnP++)
                    {
                        MaxProductImportRuleViewModel loRule = new MaxProductImportRuleViewModel();
                        this._oProductCopyRuleList.Add(loRule);
                    }
                }

                return this._oProductCopyRuleList;
            }
            set
            {
                this._oProductCopyRuleList = value;
            }
        }

        public virtual string ProcessCommand()
        {
            string lsR = string.Empty;
            if (this.Command.Equals("SaveProductCreationRules", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int lnP = 0; lnP < this.ProductCreateRuleList.Count; lnP++)
                {
                    this.ProductCreateRuleList[lnP].ImportGroup = ImportGroup;
                    if (!string.IsNullOrEmpty(this.ProductCreateRuleList[lnP].Name))
                    {
                        this.ProductCreateRuleList[lnP].Save();
                    }
                    else
                    {
                        this.ProductCreateRuleList[lnP].Delete();
                    }
                }

                bool lbCatalogIdFound = false;
                bool lbVendorIdFound = false;
                foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                {
                    if (loEntity.RuleType.Equals(1))
                    {
                        loEntity.RuleData1 = this.CatalogIdText;
                        loEntity.Update();
                        lbCatalogIdFound = true;
                    }
                    else if (loEntity.RuleType.Equals(2))
                    {
                        loEntity.RuleData1 = this.VendorIdText;
                        loEntity.Update();
                        lbVendorIdFound = true;
                    }
                }

                if (!lbCatalogIdFound)
                {
                    MaxProductImportRuleEntity loEntityCatalog = MaxProductImportRuleEntity.Create();
                    loEntityCatalog.RuleType = 1;
                    loEntityCatalog.Name = "CatalogId";
                    loEntityCatalog.RuleData1 = this.CatalogIdText;
                    loEntityCatalog.ImportGroup = this.ImportGroup;
                    loEntityCatalog.Insert();
                }

                if (!lbVendorIdFound)
                {
                    MaxProductImportRuleEntity loEntityVendor = MaxProductImportRuleEntity.Create();
                    loEntityVendor.RuleType = 2;
                    loEntityVendor.Name = "VendorId";
                    loEntityVendor.RuleData1 = this.VendorIdText;
                    loEntityVendor.ImportGroup = this.ImportGroup;
                    loEntityVendor.Insert();
                }

                this.Redirect = true;
            }
            else if (this.Command.Equals("SaveBranchRules", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int lnP = 0; lnP < this.ProductCopyRuleList.Count; lnP++)
                {
                    this.ProductCopyRuleList[lnP].ImportGroup = ImportGroup;
                    if (!string.IsNullOrEmpty(this.ProductCopyRuleList[lnP].Name))
                    {
                        this.ProductCopyRuleList[lnP].Save();
                    }
                    else
                    {
                        this.ProductCopyRuleList[lnP].Delete();
                    }
                }

                for (int lnC = 0; lnC < this.CategoryMapRuleList.Count; lnC++)
                {
                    this.CategoryMapRuleList[lnC].ImportGroup = ImportGroup;
                    if (!string.IsNullOrEmpty(this.CategoryMapRuleList[lnC].RuleData1))
                    {
                        string lsRuleData1 = string.Empty;
                        object loRuleData1 = this.CategoryMapRuleList[lnC].OriginalValues["RuleData1"];
                        if (null != loRuleData1)
                        {
                            lsRuleData1 = MaxConvertLibrary.ConvertToString(typeof(object), loRuleData1);
                        }

                        if (lsRuleData1 != this.CategoryMapRuleList[lnC].RuleData1)
                        {
                            this.CategoryMapRuleList[lnC].Save();
                        }
                    }
                    else
                    {
                        this.CategoryMapRuleList[lnC].Delete();
                    }
                }

                bool lbShopCatalogIdFound = false;
                foreach (MaxProductImportRuleEntity loEntity in this.ProductImportRuleEntityList)
                {
                    if (loEntity.RuleType.Equals(3))
                    {
                        loEntity.RuleData1 = this.ShopCatalogIdText;
                        loEntity.Update();
                        lbShopCatalogIdFound = true;
                    }
                }

                if (!lbShopCatalogIdFound)
                {
                    MaxProductImportRuleEntity loEntityCatalog = MaxProductImportRuleEntity.Create();
                    loEntityCatalog.RuleType = 3;
                    loEntityCatalog.Name = "ShopCatalogId";
                    loEntityCatalog.RuleData1 = this.ShopCatalogIdText;
                    loEntityCatalog.ImportGroup = this.ImportGroup;
                    loEntityCatalog.Insert();
                }

                this.Redirect = true;
            }
            else if (Command.Equals("CreateBranch", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Diagnostics.Stopwatch loWatch = System.Diagnostics.Stopwatch.StartNew();
                string lsSeparator = Guid.NewGuid().ToString();
                MaxCategoryEntity loCategory = MaxCategoryEntity.Create();
                MaxEntityList loCategoryEntityList = loCategory.LoadAllByCatalogId(this.CatalogId);
                loCategoryEntityList = loCategory.SortByPath(loCategoryEntityList, lsSeparator);
                MaxIndex loCategoryIndexByPath = new MaxIndex();
                for (int lnC = 0; lnC < loCategoryEntityList.Count; lnC++)
                {
                    loCategoryIndexByPath.Add(((MaxCategoryEntity)loCategoryEntityList[lnC]).Path.Replace(lsSeparator, "/"), loCategoryEntityList[lnC]);
                }

                MaxEntityList loShopCategoryEntityList = loCategory.LoadAllByCatalogId(this.ShopCatalogId);
                loShopCategoryEntityList = loCategory.MapParent(loShopCategoryEntityList);
                loShopCategoryEntityList = loCategory.SortByPath(loShopCategoryEntityList, lsSeparator);
                MaxIndex loShopCategoryIndexByPath = new MaxIndex();
                for (int lnC = 0; lnC < loShopCategoryEntityList.Count; lnC++)
                {
                    loShopCategoryIndexByPath.Add(((MaxCategoryEntity)loShopCategoryEntityList[lnC]).Path.Replace(lsSeparator, "/"), loShopCategoryEntityList[lnC]);
                }

                Dictionary<Guid, List<Guid>> loCategoryMapIndex = new Dictionary<Guid, List<Guid>>();
                foreach (MaxProductImportRuleEntity loRule in this.ProductImportRuleEntityList)
                {
                    //// Process rules
                    if (loRule.RuleType.Equals(RuleTypeCategory))
                    {
                        if (loCategoryIndexByPath.Contains(loRule.Name) && !string.IsNullOrEmpty(loRule.RuleData1))
                        {
                            Guid loIdFrom = ((MaxCategoryEntity)loCategoryIndexByPath[loRule.Name]).Id;
                            string lsShopCategoryPath = loRule.RuleData1.Trim();
                            if (lsShopCategoryPath.StartsWith("/"))
                            {
                                lsShopCategoryPath = lsShopCategoryPath.Substring(1);
                            }

                            if (!lsShopCategoryPath.EndsWith("/"))
                            {
                                lsShopCategoryPath += "/";
                            }

                            if (!loShopCategoryIndexByPath.Contains(lsShopCategoryPath))
                            {
                                //// Create ShopCategory based on path
                                string[] laCategory = lsShopCategoryPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                Guid loParentId = Guid.Empty;
                                foreach (string lsCategory in laCategory)
                                {
                                    MaxCategoryEntity loCategoryNew = null;
                                    for (int lnE = 0; lnE < loShopCategoryEntityList.Count; lnE++)
                                    {
                                        MaxCategoryEntity loCategoryCheck = loShopCategoryEntityList[lnE] as MaxCategoryEntity;
                                        if (loParentId == loCategoryCheck.ParentId && loCategoryCheck.Name.Equals(lsCategory, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            loCategoryNew = loCategoryCheck;
                                        }
                                    }

                                    if (null == loCategoryNew)
                                    {
                                        loCategoryNew = MaxCategoryEntity.Create();
                                        loCategoryNew.Name = lsCategory;
                                        loCategoryNew.PrimaryCatalogId = this.ShopCatalogId;
                                        loCategoryNew.IsActive = true;
                                        loCategoryNew.ParentId = loParentId;
                                        loCategoryNew.Insert();
                                        loShopCategoryEntityList.Add(loCategoryNew);
                                    }

                                    loParentId = loCategoryNew.Id;
                                }

                                loShopCategoryEntityList = loCategory.MapParent(loShopCategoryEntityList);
                                loShopCategoryEntityList = loCategory.SortByPath(loShopCategoryEntityList, lsSeparator);
                                for (int lnC = 0; lnC < loShopCategoryEntityList.Count; lnC++)
                                {
                                    string lsPath = ((MaxCategoryEntity)loShopCategoryEntityList[lnC]).Path.Replace(lsSeparator, "/");
                                    loShopCategoryIndexByPath.Add(lsPath, loShopCategoryEntityList[lnC]);
                                }
                            }

                            Guid loIdTo = ((MaxCategoryEntity)loShopCategoryIndexByPath[lsShopCategoryPath]).Id;
                            if (!loCategoryMapIndex.ContainsKey(loIdFrom))
                            {
                                loCategoryMapIndex.Add(loIdFrom, new List<Guid>());
                            }

                            loCategoryMapIndex[loIdFrom].Add(loIdTo);
                        }
                    }
                }

                MaxEntityList loListAll = MaxProductEntity.Create().LoadAllCache();
                //// Create an index of all products that have already been copied
                Dictionary<Guid, MaxProductEntity> loProductByRootIndex = new Dictionary<Guid, MaxProductEntity>();
                for (int lnE = 0; lnE < loListAll.Count; lnE++)
                {
                    if (!((MaxProductEntity)loListAll[lnE]).RootId.Equals(Guid.Empty))
                    {
                        if (((MaxProductEntity)loListAll[lnE]).BranchType.StartsWith("branched from") &&
                            ((MaxProductEntity)loListAll[lnE]).BranchType.Contains("import"))
                        {
                            if (!loProductByRootIndex.ContainsKey(((MaxProductEntity)loListAll[lnE]).RootId))
                            {
                                loProductByRootIndex.Add(((MaxProductEntity)loListAll[lnE]).RootId, (MaxProductEntity)loListAll[lnE]);
                            }
                        }
                    }
                }

                int lnNew = 0;
                int lnUpdated = 0;
                for (int lnE = 0; lnE < loListAll.Count; lnE++)
                {
                    if (lnE % 100 == 0)
                    {
                        //HttpContext.Current.Response.Write("<!--" + lnE + " " + MaxHttpApplicationLibrary.GetTimeSinceBeginRequest().ToString() + " of " + loListAll.Count.ToString() + "-->");
                        //HttpContext.Current.Response.Flush();
                    }

                    MaxProductEntity loProduct = (MaxProductEntity)loListAll[lnE];
                    if (loProduct.BranchType.StartsWith("import") && loProduct.PrimaryCatalogId.Equals(this.CatalogId))
                    {
                        MaxProductEntity loProductBranch = loProduct.CreateBranch();
                        if (loProductByRootIndex.ContainsKey(loProductBranch.RootId))
                        {
                            loProductBranch = loProductByRootIndex[loProductBranch.RootId];
                        }
                        else
                        {
                            loProductBranch.BranchType = "branched from " + loProduct.BranchType;
                            loProductBranch.IsActive = true;
                        }

                        loProductBranch.PrimaryCatalogId = this.ShopCatalogId;

                        string lsCategoryIdList = loProduct.PrimaryCategoryId.ToString();
                        if (!string.IsNullOrEmpty(loProduct.CategoryIdList))
                        {
                            lsCategoryIdList += " " + loProduct.CategoryIdList;
                        }

                        string[] laCategoryIdList = lsCategoryIdList.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        loProductBranch.CategoryIdList = string.Empty;
                        loProductBranch.PrimaryCategoryId = Guid.Empty;
                        foreach (string lsCategoryId in laCategoryIdList)
                        {
                            Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsCategoryId);
                            if (loCategoryMapIndex.ContainsKey(loId))
                            {
                                if (loProductBranch.PrimaryCategoryId == Guid.Empty)
                                {
                                    loProductBranch.PrimaryCategoryId = loCategoryMapIndex[loId][0];
                                }

                                for (int lnC = 0; lnC < loCategoryMapIndex[loId].Count; lnC++)
                                {
                                    loProductBranch.CategoryIdList += " " + loCategoryMapIndex[loId][lnC];
                                }
                            }
                        }


                        if (loProductBranch.Id.Equals(Guid.Empty))
                        {
                            loProductBranch.Insert();
                            loProductByRootIndex.Add(loProduct.Id, loProductBranch);
                            lnNew++;
                        }
                        else
                        {
                            loProductBranch.Update();
                            lnUpdated++;
                        }
                    }
                }

                lsR = lnNew + " branched products created from imported products. " + lnUpdated + " branch products updated from imported products.  Time Taken: " + String.Format("{0:2}", loWatch.ElapsedMilliseconds / 1000) + " seconds.";
            }

            return lsR;

        }

        public bool Redirect = false;

        public virtual string ParseValue(MaxIndex loIndex, string lsKey)
        {
            object loValue = loIndex[lsKey];
            string lsR = string.Empty;
            if (null != loValue)
            {
                lsR = loValue.ToString();

                foreach (MaxProductImportRuleEntity loRule in this.ProductImportRuleEntityList)
                {
                    if (loRule.Key.Equals(lsKey) || loRule.Key.Equals(string.Empty) || loRule.Key.Equals("*"))
                    {
                        if (loRule.RuleType.Equals(RuleTypeReplace) && loRule.RuleData1.Contains("->"))
                        {
                            string[] laRule = loRule.RuleData1.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                            string lsReplace = string.Empty;
                            if (laRule.Length > 1)
                            {
                                lsReplace = laRule[1];
                            }

                            lsR = lsR.Replace(laRule[0], lsReplace);
                        }
                    }
                }

                lsR = lsR.Replace("&nbsp;", " ");
                while (lsR.Contains("  "))
                {
                    lsR = lsR.Replace("  ", " ");
                }


            }

            return lsR;
        }
    }
}
