// <copyright file="MaxProductImageViewModel.cs" company="Lakstins Family, LLC">
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
    using MaxFactry.Base.PresentationLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxProductImageViewModel : MaxBaseIdFileViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxProductImageViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxProductImageViewModel class
        /// </summary>
        public MaxProductImageViewModel()
            : base()
        {
            this.Entity = MaxProductImageEntity.Create();
        }

        /// <summary>
        /// Initializes a new instance of the MaxProductImageViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxProductImageViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxProductImageViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxProductImageViewModel(string lsId)
        {
            this.Entity = MaxProductImageEntity.Create();
            this.Id = lsId;
            if (this.EntityLoad())
            {
                this.Load();
            }
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Product")]
        public string ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property to be stored.
        /// </summary>
        [Display(Name = "Alternate Text")]
        public string AlternateText
        {
            get;
            set;
        }

        public string ImageType
        {
            get;
            set;
        }

        public string RelationOrder
        {
            get;
            set;
        }

        public string Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxProductImageViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxProductImageViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxProductImageViewModel loViewModel = new MaxProductImageViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxProductImageEntity loEntity = this.Entity as MaxProductImageEntity;
                if (null != loEntity)
                {
                    Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ProductId);
                    if (!Guid.Empty.Equals(loId))
                    {
                        loEntity.ProductId = loId;
                    }

                    loEntity.AlternateText = this.AlternateText;
                    loEntity.ImageType = this.ImageType;
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
                MaxProductImageEntity loEntity = this.Entity as MaxProductImageEntity;
                if (null != loEntity)
                {
                    this.ProductId = loEntity.ProductId.ToString();
                    this.AlternateText = loEntity.AlternateText;
                    this.ImageType = loEntity.ImageType;
                    this.RelationOrder = loEntity.RelationOrder.ToString();
                    return true;
                }
            }

            return false;
        }
    }
}
