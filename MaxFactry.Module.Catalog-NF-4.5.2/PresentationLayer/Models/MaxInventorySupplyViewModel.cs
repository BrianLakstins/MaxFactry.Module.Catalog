// <copyright file="MaxInventorySupplyViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="7/29/2016" author="Brian A. Lakstins" description="Initial creation">
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
    public class MaxInventorySupplyViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxInventorySupplyViewModel> _oSortedList = null;

        private MaxInventorySiteViewModel _oInventorySite = new MaxInventorySiteViewModel();

        private MaxVendorViewModel _oVendor = new MaxVendorViewModel();

        private MaxManufacturerViewModel _oManufacturer = new MaxManufacturerViewModel();

        /// <summary>
        /// Initializes a new instance of the MaxInventorySupplyViewModel class
        /// </summary>
        public MaxInventorySupplyViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxInventorySupplyViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxInventorySupplyViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxInventorySupplyViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxInventorySupplyViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxInventorySupplyEntity.Create();
        }

        [Display(Name = "Supply Sku")]
        public string SupplySku { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Site")]
        public string SiteId { get; set; }

        [Display(Name = "Location")]
        public string Location
        {
            get;
            set;
        }

        [Display(Name = "Current Amount")]
        public string AmountCurrent { get; set; }

        [Display(Name = "Incoming Amount")]
        public string AmountReplenish { get; set; }

        [Display(Name = "On Order Amount")]
        public string AmountOnOrder { get; set; }

        [Display(Name = "Replenish Alert Level")]
        public string ReplenishAlertLevel { get; set; }

        [Display(Name = "Lead Time (days)")]
        public string LeadTime { get; set; }

        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        [Display(Name = "Vendor")]
        public string VendorId { get; set; }

        [Display(Name = "Vendor Sku")]
        public string VendorSku { get; set; }

        [Display(Name = "Manufacturer")]
        public string ManufacturerId { get; set; }

        [Display(Name = "Manufacturer Sku")]
        public string ManufacturerSku { get; set; }

        [Display(Name = "Always Allow Backorder")]
        public bool IsBackOrderAllowed { get; set; }
       

        public List<MaxInventorySiteViewModel> SiteList
        {
            get
            {
                return this._oInventorySite.GetSortedList();
            }
        }

        public List<MaxVendorViewModel> VendorList
        {
            get
            {
                return this._oVendor.GetSortedList();
            }
        }

        public List<MaxManufacturerViewModel> ManufacturerList
        {
            get
            {
                return this._oManufacturer.GetSortedList();
            }
        }

        public Dictionary<int, string> UnitOfMeasureList
        {
            get
            {
                Dictionary<int, string> loR = new Dictionary<int, string>();
                loR.Add(0, "Each");
                return loR;
            }
        }

        public List<MaxInventoryLogViewModel> LogList
        {
            get
            {
                return new List<MaxInventoryLogViewModel>();
            }
        }


        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxInventorySupplyViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxInventorySupplyViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxInventorySupplyViewModel loViewModel = new MaxInventorySupplyViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxInventorySupplyEntity loEntity = this.Entity as MaxInventorySupplyEntity;
                if (null != loEntity)
                {
                    loEntity.SupplySku = this.SupplySku;
                    loEntity.Name = this.Name;
                    loEntity.Location = this.Location;
                    loEntity.ReplenishAlertLevel = MaxConvertLibrary.ConvertToLong(typeof(object), this.ReplenishAlertLevel);
                    loEntity.LeadTime = MaxConvertLibrary.ConvertToInt(typeof(object), this.LeadTime);
                    loEntity.UnitOfMeasure = MaxConvertLibrary.ConvertToInt(typeof(object), this.UnitOfMeasure);
                    loEntity.VendorSku = this.VendorSku;
                    loEntity.VendorId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.VendorId);
                    loEntity.SiteId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.SiteId);
                    loEntity.ManufacturerSku = this.ManufacturerSku;
                    loEntity.ManufacturerId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ManufacturerId);
                    loEntity.IsBackOrderAllowed = this.IsBackOrderAllowed;
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
                MaxInventorySupplyEntity loEntity = this.Entity as MaxInventorySupplyEntity;
                if (null != loEntity)
                {
                    this.SupplySku = loEntity.SupplySku;
                    this.Name = loEntity.Name;
                    this.Location = loEntity.Location;
                    this.SiteId = loEntity.SiteId.ToString();
                    this.AmountCurrent = loEntity.AmountCurrent.ToString();
                    this.AmountReplenish = loEntity.AmountReplenish.ToString();

                    this.ReplenishAlertLevel = "0";
                    if (loEntity.ReplenishAlertLevel > long.MinValue)
                    {
                        this.ReplenishAlertLevel = loEntity.ReplenishAlertLevel.ToString();
                    }

                    this.LeadTime = loEntity.LeadTime.ToString();
                    this.UnitOfMeasure = loEntity.UnitOfMeasure.ToString();
                    this.VendorId = loEntity.VendorId.ToString();
                    this.VendorSku = loEntity.VendorSku;
                    this.ManufacturerId = loEntity.ManufacturerId.ToString();
                    this.ManufacturerSku = loEntity.ManufacturerSku;
                    this.IsBackOrderAllowed = loEntity.IsBackOrderAllowed;
                    this.AmountOnOrder = loEntity.AmountOnOrder.ToString();

                    return true;
                }
            }

            return false;
        }
    }
}
