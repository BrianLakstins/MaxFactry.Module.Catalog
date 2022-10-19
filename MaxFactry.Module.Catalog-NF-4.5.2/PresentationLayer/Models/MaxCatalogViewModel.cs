// <copyright file="MaxCatalogViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="11/30/2018" author="Brian A. Lakstins" description="Moved code for setting and getting current catalog to entity.">
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
    public class MaxCatalogViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxCatalogViewModel> _oSortedList = null;
        
        private MaxClientViewModel _oClient = new MaxClientViewModel();

        /// <summary>
        /// Initializes a new instance of the MaxCatalogViewModel class
        /// </summary>
        public MaxCatalogViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxCatalogViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxCatalogViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxCatalogViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxCatalogViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxCatalogEntity.Create();
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

        /// <summary>
        /// Gets or sets the ClientId property to be stored.
        /// </summary>
        [Display(Name = "Client")]
        [Required]
        public string ClientId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ClientId property to be stored.
        /// </summary>
        [Display(Name = "Client Name")]
        public string ClientName
        {
            get
            {
                if (string.IsNullOrEmpty(this._oClient.Id) && !string.IsNullOrEmpty(this.ClientId))
                {
                    this._oClient.Id = this.ClientId;
                    this._oClient.EntityLoad();
                    this._oClient.Load();
                }

                return this._oClient.Name;
            }
        }

        /// <summary>
        /// Gets a list of all entities for this AppId
        /// </summary>
        public List<MaxClientViewModel> ClientList
        {
            get
            {
                return this._oClient.GetSortedList();
            }
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxCatalogViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxCatalogViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxCatalogViewModel loViewModel = new MaxCatalogViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    //// Map the client so that only one load of Clients is needed for the entire sorted list of Catalogs
                    foreach (MaxClientViewModel loClient in this.ClientList)
                    {
                        if (loClient.Id == loViewModel.ClientId)
                        {
                            loViewModel._oClient = loClient;
                        }
                    }

                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public static MaxCatalogViewModel Current
        {
            get
            {
                MaxCatalogViewModel loR = new MaxCatalogViewModel();
                if (!loR.LoadCurrent())
                {
                    List<MaxCatalogViewModel> loList = loR.GetSortedList();
                    foreach (MaxCatalogViewModel loModel in loList)
                    {
                        if (loModel.IsActive)
                        {
                            return loModel;
                        }
                    }
                }

                return loR;
            }
        }

        public void SetCurrent()
        {
            MaxCatalogEntity loEntity = this.Entity as MaxCatalogEntity;
            if (null != loEntity)
            {
                loEntity.SetCurrent();
            }
        }

        public bool LoadCurrent()
        {
            MaxCatalogEntity loEntity = MaxCatalogEntity.GetCurrent();
            if (null != loEntity)
            {
                this.Entity = loEntity;
                this.Load();
                return true;
            }

            return false;
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
                MaxCatalogEntity loEntity = this.Entity as MaxCatalogEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    loEntity.ClientId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ClientId);
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
                MaxCatalogEntity loEntity = this.Entity as MaxCatalogEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.ClientId = loEntity.ClientId.ToString();
                    return true;
                }
            }

            return false;
        }
    }
}
