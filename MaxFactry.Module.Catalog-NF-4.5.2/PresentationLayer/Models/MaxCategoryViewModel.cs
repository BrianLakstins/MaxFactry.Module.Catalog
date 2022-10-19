// <copyright file="MaxCategoryViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxCategoryViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCategoryViewModel> _oSortedList = null;

        private MaxCatalogViewModel _oCatalog = null;

        private List<MaxCategoryViewModel> _oParentList = null;

        private List<MaxCategoryImageViewModel> _oImageList = null;

        private List<MaxProductViewModel> _oProductList = null;

        /// <summary>
        /// Initializes a new instance of the MaxCategoryViewModel class
        /// </summary>
        public MaxCategoryViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCategoryViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCategoryViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCategoryViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCategoryViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCategoryEntity.Create();
        }

        /// <summary>
        /// Gets or sets the Catalog Id property to be stored.
        /// </summary>
        [Display(Name = "Catalog")]
        [Required]
        public string CatalogId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ClientId property to be stored.
        /// </summary>
        [Display(Name = "Parent Category")]
        public string ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Primary Image")]
        public string PrimaryImageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name property to be stored.
        /// </summary>
        [Display(Name = "Sort Order")]
        public string RelationOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Catalog property based on the CatalogId
        /// </summary>
        public MaxCatalogViewModel Catalog
        {
            get
            {
                if (null == this._oCatalog)
                {
                    this._oCatalog = new MaxCatalogViewModel(this.CatalogId);
                }

                return this._oCatalog;
            }

            set
            {
                this._oCatalog = value;
            }
        }

        public string Path { get; set; }

        public string PathSeparator { get; set; }

        public string PathIdList { get; set; }

        public string PathText
        {
            get
            {
                if (null != this.Path && null != this.PathSeparator)
                {
                    return this.Path.Replace(this.PathSeparator, "/");
                }
                else if (null != this.Path)
                {
                    return this.Path;
                }

                return this.Name;
            }
        }

        public MaxCategoryViewModel GetParent(MaxIndex loIndex)
        {
            if (String.IsNullOrEmpty(this.ParentId))
            {
                return null;
            }

            if (null != loIndex)
            {
                object loObject = loIndex[this.ParentId];
                if (null == loObject)
                {
                    string[] laKey = loIndex.GetSortedKeyList();
                    for (int lnK = 0; lnK < laKey.Length && null == loObject; lnK++)
                    {
                        object loTest = loIndex[laKey[lnK]];
                        if (loTest is MaxCategoryEntity)
                        {
                            if (((MaxCategoryEntity)loTest).Id.ToString().Equals(this.ParentId))
                            {
                                loObject = loTest;
                            }
                        }
                        else if (loTest is MaxCategoryViewModel)
                        {
                            if (((MaxCategoryViewModel)loTest).Id.Equals(this.ParentId))
                            {
                                loObject = loTest;
                            }
                        }
                    }
                }

                if (loObject is MaxCategoryEntity)
                {
                    MaxCategoryViewModel loView = new MaxCategoryViewModel(loObject as MaxCategoryEntity);
                    loView.Load();
                    return loView;
                }
                else if (loObject is MaxCategoryViewModel)
                {
                    return loObject as MaxCategoryViewModel;
                }
            }

            return new MaxCategoryViewModel(this.ParentId);
        }

        public List<MaxCatalogViewModel> CatalogList
        {
            get
            {
                return this.Catalog.GetSortedList();
            }
        }

        public List<MaxCategoryViewModel> ParentList
        {
            get
            {
                if (null == this._oParentList)
                {
                    SortedList<string, MaxCategoryViewModel> loSortedList = new SortedList<string, MaxCategoryViewModel>();
                    List<MaxCategoryViewModel> loList = this.GetSortedList();
                    foreach (MaxCategoryViewModel loView in loList)
                    {
                        if (loView.CatalogId.Equals(this.CatalogId))
                        {
                            if (!loView.PathIdList.Contains(this.Id))
                            {
                                loSortedList.Add(loView.Path + "          " + Guid.NewGuid(), loView);
                            }
                        }
                    }

                    this._oParentList = new List<MaxCategoryViewModel>(loSortedList.Values);
                    MaxCategoryViewModel loCategory = new MaxCategoryViewModel();
                    loCategory.Path = "None";
                    this._oParentList.Insert(0, loCategory);
                }

                return this._oParentList;
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCategoryViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                MaxIndex loIndex = new MaxIndex();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCategoryEntity loEntity = this.EntityIndex[laKey[lnK]] as MaxCategoryEntity;
                    loIndex.Add(loEntity.Id.ToString(), loEntity);
                }

                this.PathSeparator = Guid.NewGuid().ToString();
                SortedList<string, MaxCategoryViewModel> loSortedList = new SortedList<string, MaxCategoryViewModel>();
                foreach (MaxCatalogViewModel loCatalog in this.CatalogList)
                {
                    for (int lnK = 0; lnK < laKey.Length; lnK++)
                    {
                        MaxCategoryEntity loEntity = this.EntityIndex[laKey[lnK]] as MaxCategoryEntity;
                        if (loEntity.PrimaryCatalogId.ToString().Equals(loCatalog.Id))
                        {
                            MaxCategoryViewModel loViewModel = new MaxCategoryViewModel(loEntity);
                            loViewModel.Load();
                            loViewModel.PathSeparator = this.PathSeparator;
                            loViewModel.Catalog = loCatalog;
                            string lsPath = loViewModel.Name + this.PathSeparator;
                            string lsPathIdList = loViewModel.Id;
                            MaxCategoryViewModel loParent = loViewModel.GetParent(loIndex);
                            while (null != loParent)
                            {
                                lsPath = loParent.Name + this.PathSeparator + lsPath;
                                lsPathIdList = loParent.Id + this.PathSeparator + lsPathIdList;
                                if (string.IsNullOrEmpty(loParent.Name))
                                {
                                    loParent = null;
                                }
                                else if (loParent.Name.Contains(this.PathSeparator))
                                {
                                    loParent = null;
                                }
                                else
                                {
                                    loParent = loParent.GetParent(loIndex);
                                }
                            }

                            loViewModel.Path = this.PathSeparator + loCatalog.Name + this.PathSeparator + lsPath;
                            loViewModel.PathIdList = lsPathIdList;
                            loSortedList.Add(loViewModel.Path + "          " + Guid.NewGuid(), loViewModel);
                        }
                    }
                }

                this._oSortedList = new List<MaxCategoryViewModel>(loSortedList.Values);
            }

            return this._oSortedList;
        }

        public List<MaxCategoryViewModel> GetChildList(MaxIndex loIndex)
        {
            List<MaxCategoryViewModel> loR = new List<MaxCategoryViewModel>();
            MaxCategoryEntity loEntity = this.Entity as MaxCategoryEntity;
            if (null == loIndex)
            {
                List<MaxCategoryEntity> loList = loEntity.GetChildList();
                foreach (MaxCategoryEntity loChild in loList)
                {
                    MaxCategoryViewModel loModel = new MaxCategoryViewModel(loChild);
                    loModel.Load();
                    loR.Add(loModel);
                }
            }
            else
            {
                for (int lnM = 0; lnM < loIndex.Count; lnM++)
                {
                    object loObject = loIndex[lnM];
                    if (loObject is MaxCategoryViewModel)
                    {
                        if (((MaxCategoryViewModel)loObject).ParentId == this.Id)
                        {
                            loR.Add(loObject as MaxCategoryViewModel);
                        }
                    }
                    else if (loObject is MaxCategoryEntity)
                    {
                        if (((MaxCategoryEntity)loObject).ParentId.ToString() == this.Id)
                        {
                            MaxCategoryViewModel loModel = new MaxCategoryViewModel(loObject as MaxCategoryEntity);
                            loModel.Load();
                            loR.Add(loModel);
                        }
                    }
                }
            }

            return loR;
        }

        public List<MaxCategoryImageViewModel> ImageList
        {
            get
            {
                if (null == this._oImageList)
                {
                    this._oImageList = new List<MaxCategoryImageViewModel>();
                    MaxCategoryEntity loEntity = this.Entity as MaxCategoryEntity;
                    if (!Guid.Empty.Equals(loEntity.Id))
                    {
                        MaxEntityList loList = MaxCategoryImageEntity.Create().LoadAllByCategoryId(loEntity.Id);
                        for (int lnE = 0; lnE < loList.Count; lnE++)
                        {
                            MaxCategoryImageViewModel loView = new MaxCategoryImageViewModel(loList[lnE]);
                            loView.Load();
                            this._oImageList.Add(loView);
                        }
                    }
                }

                return this._oImageList;
            }
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
                MaxCategoryEntity loEntity = this.Entity as MaxCategoryEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    Guid loCatalogId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.CatalogId);
                    if (!Guid.Empty.Equals(loCatalogId))
                    {
                        loEntity.PrimaryCatalogId = loCatalogId;
                    }

                    loEntity.ParentId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ParentId);
                    loEntity.PrimaryImageId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.PrimaryImageId);
                    loEntity.RelationOrder = MaxConvertLibrary.ConvertToDouble(typeof(object), this.RelationOrder);
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
                MaxCategoryEntity loEntity = this.Entity as MaxCategoryEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    if (!Guid.Empty.Equals(loEntity.PrimaryCatalogId))
                    {
                        this.CatalogId = loEntity.PrimaryCatalogId.ToString();
                    }

                    if (!Guid.Empty.Equals(loEntity.ParentId))
                    {
                        this.ParentId = loEntity.ParentId.ToString();
                    }

                    if (!Guid.Empty.Equals(loEntity.PrimaryImageId))
                    {
                        this.PrimaryImageId = loEntity.PrimaryImageId.ToString();
                    }

                    if (loEntity.RelationOrder > double.MinValue)
                    {
                        this.RelationOrder = loEntity.RelationOrder.ToString();
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public List<MaxProductViewModel> ProductList
        {
            get
            {
                if (null == this._oProductList)
                {
                    this._oProductList = new List<MaxProductViewModel>();
                    Guid loCategoryId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.Id);
                    if (!Guid.Empty.Equals(loCategoryId))
                    {
                        MaxEntityList loEntityList = MaxProductEntity.Create().LoadAllBySearchCategory(loCategoryId);
                        for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                        {
                            MaxProductViewModel loModel = new MaxProductViewModel(loEntityList[lnE]);
                            loModel.Load();
                            this._oProductList.Add(loModel);
                        }
                    }
                }

                return this._oProductList;
            }
        }
    }
}
