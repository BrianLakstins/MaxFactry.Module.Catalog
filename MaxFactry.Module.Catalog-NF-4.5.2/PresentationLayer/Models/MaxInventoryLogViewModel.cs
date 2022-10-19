// <copyright file="MaxInventoryLogViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxInventoryLogViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxInventoryLogViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxInventoryLogViewModel class
        /// </summary>
        public MaxInventoryLogViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxInventoryLogViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxInventoryLogViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxInventoryLogViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxInventoryLogViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxInventoryLogEntity.Create();
        }

        public string InventoryId { get; set; }

        public string ChangedDate { get; set; }

        public string RelatedId { get; set; }

        public string RelatedItemType { get; set; }

        public string AmountType { get; set; }

        public string AmountStart { get; set; }

        public string AmountChanged { get; set; }

        public string AmountEnd { get; set; }

        public string Reason { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxInventoryLogViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxInventoryLogViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxInventoryLogViewModel loViewModel = new MaxInventoryLogViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxInventoryLogEntity loEntity = this.Entity as MaxInventoryLogEntity;
                if (null != loEntity)
                {
                    loEntity.Reason = this.Reason;
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
                MaxInventoryLogEntity loEntity = this.Entity as MaxInventoryLogEntity;
                if (null != loEntity)
                {
                    this.InventoryId = loEntity.InventoryId.ToString();
                    this.ChangedDate = loEntity.ChangedDate.ToString();
                    this.RelatedId = loEntity.RelatedId.ToString();
                    this.RelatedItemType = loEntity.RelatedItemType.ToString();
                    this.AmountType = loEntity.AmountType.ToString();
                    this.AmountStart = loEntity.AmountStart.ToString();
                    this.AmountChanged = loEntity.AmountChanged.ToString();
                    this.AmountEnd = loEntity.AmountEnd.ToString();
                    this.Reason = loEntity.Reason;
                    this.Username = loEntity.Username;
                    this.UserId = loEntity.UserId.ToString();
                    return true;
                }
            }

            return false;
        }
    }
}
