// <copyright file="MaxInventoryProductViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="9/2/2016" author="Brian A. Lakstins" description="Initial creation">
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
    public class MaxInventoryProductViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxInventoryProductViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxInventoryProductViewModel class
        /// </summary>
        public MaxInventoryProductViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxInventoryProductViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxInventoryProductViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxInventoryProductViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxInventoryProductViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxInventoryProductEntity.Create();
        }

        [Display(Name = "Product Sku")]
        public string ProductSku { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        [Display(Name = "Name")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Supply Sku List (example: sku1,sku2:3,sku3:2")]
        public string SupplySkuList { get; set; }

        [Display(Name = "Amount On Order")]
        public string AmountOnOrder { get; set; }

        [Display(Name = "Replenish Alert Level")]
        public string ReplenishAlertLevel { get; set; }

        [Display(Name = "Unit of Measure")]
        public string UnitOfMeasure { get; set; }

        [Display(Name = "Amount Available")]
        public string AmountAvailable
        {
            get
            {
                MaxInventoryProductEntity loEntity = this.Entity as MaxInventoryProductEntity;
                if (null != loEntity)
                {
                    return loEntity.AmountAvailable.ToString();
                }

                return string.Empty;
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


        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxInventoryProductViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxInventoryProductViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxInventoryProductViewModel loViewModel = new MaxInventoryProductViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxInventoryProductEntity loEntity = this.Entity as MaxInventoryProductEntity;
                if (null != loEntity)
                {
                    loEntity.ProductSku = this.ProductSku;
                    loEntity.Name = this.Name;
                    loEntity.UnitOfMeasure = MaxConvertLibrary.ConvertToInt(typeof(object), this.UnitOfMeasure);
                    loEntity.ReplenishAlertLevel = MaxConvertLibrary.ConvertToLong(typeof(object), this.ReplenishAlertLevel);
                    loEntity.SupplySkuList = this.SupplySkuList;
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
                MaxInventoryProductEntity loEntity = this.Entity as MaxInventoryProductEntity;
                if (null != loEntity)
                {
                    this.ProductSku = loEntity.ProductSku;
                    this.Name = loEntity.Name;
                    this.AmountOnOrder = loEntity.AmountOnOrder.ToString();

                    this.ReplenishAlertLevel = "0";
                    if (loEntity.ReplenishAlertLevel > long.MinValue)
                    {
                        this.ReplenishAlertLevel = loEntity.ReplenishAlertLevel.ToString();
                    }

                    this.UnitOfMeasure = loEntity.UnitOfMeasure.ToString();
                    this.SupplySkuList = loEntity.SupplySkuList;
                    return true;
                }
            }

            return false;
        }
    }
}
