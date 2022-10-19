// <copyright file="MaxOrderProcessLogViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxOrderProcessLogViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderProcessLogViewModel> _oSortedList = null;
        
        /// <summary>
        /// Initializes a new instance of the MaxOrderProcessLogViewModel class
        /// </summary>
        public MaxOrderProcessLogViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderProcessLogViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderProcessLogViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderProcessLogViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderProcessLogViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderProcessLogEntity.Create();
        }

        /// <summary>
        /// Gets or sets the ORder Id
        /// </summary>
        public string OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Log Entry
        /// </summary>
        public string LogEntry
        {
            get;
            set;
        }

        public bool IsCustomerVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderProcessLogViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderProcessLogViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderProcessLogViewModel loViewModel = new MaxOrderProcessLogViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxOrderProcessLogEntity loEntity = this.Entity as MaxOrderProcessLogEntity;
                if (null != loEntity)
                {
                    if (null != this.OrderId)
                    {
                        loEntity.OrderId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.OrderId);
                    }

                    if (null != this.UserId)
                    {
                        loEntity.UserId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.UserId);
                    }

                    loEntity.UserName = this.UserName;
                    loEntity.LogEntry = this.LogEntry;
                    loEntity.IsCustomerVisible = this.IsCustomerVisible;
                    
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
                MaxOrderProcessLogEntity loEntity = this.Entity as MaxOrderProcessLogEntity;
                if (null != loEntity)
                {
                    if (!Guid.Empty.Equals(this.UserId))
                    {
                        this.UserId = loEntity.UserId.ToString();
                    }

                    this.OrderId = loEntity.OrderId.ToString();
                    this.UserName = loEntity.UserName;
                    this.LogEntry = loEntity.LogEntry;
                    this.IsCustomerVisible = loEntity.IsCustomerVisible;

                    return true;
                }
            }

            return false;
        }
    }
}
