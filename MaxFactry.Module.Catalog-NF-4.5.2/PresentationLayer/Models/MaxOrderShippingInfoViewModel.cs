// <copyright file="MaxOrderShippingInfoViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxOrderShippingInfoViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderShippingInfoViewModel> _oSortedList = null;

        private MaxOrderShippingAddressViewModel _oShippingAddress = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingInfoViewModel class
        /// </summary>
        public MaxOrderShippingInfoViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingInfoViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderShippingInfoViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderShippingInfoViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderShippingInfoViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderShippingInfoEntity.Create();
        }

        [Display(Name = "Name")]
        public string ShippingInfoName { get; set; }

        [Display(Name = "Contact Person")]
        public string ShippingContactPerson { get; set; }

        [Display(Name = "Expected Pickup Date")]
        [UIHint("PickupDate")]
        public string PickupDate { get; set; }

        public string PickupTime { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public int ShippingType { get; set; }

        public bool IsPickup
        {
            get
            {
                MaxEntityList loList = MaxShippingTypeEntity.Create().LoadAll();
                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    MaxShippingTypeEntity loEntity = loList[lnE] as MaxShippingTypeEntity;
                    if (loEntity.Name.Contains("Pick Up"))
                    {
                        if (loEntity.ShippingType == this.ShippingType)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public string ShippingAddressId { get; set; }

        public MaxOrderShippingAddressViewModel ShippingAddress
        {
            get
            {
                if (null == this._oShippingAddress)
                {
                    MaxOrderShippingInfoEntity loEntity = this.Entity as MaxOrderShippingInfoEntity;
                    this._oShippingAddress = new MaxOrderShippingAddressViewModel(loEntity.ShippingAddress);
                    this._oShippingAddress.Load();
                }

                return this._oShippingAddress;
            }

            set
            {
                this._oShippingAddress = value;
            }
        }

        public string OrderId
        {
            get;
            set;
        }


        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderShippingInfoViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderShippingInfoViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderShippingInfoViewModel loViewModel = new MaxOrderShippingInfoViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
                MaxOrderShippingInfoEntity loEntity = this.Entity as MaxOrderShippingInfoEntity;
                if (null != loEntity)
                {
                    if (!string.IsNullOrEmpty(this.OrderId))
                    {
                        loEntity.OrderId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.OrderId);
                    }
                    
                    loEntity.ShippingType = this.ShippingType;
                    loEntity.Notes = this.Notes;
                    if (null != this.ShippingAddressId)
                    {
                        loEntity.ShippingAddressId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.ShippingAddressId);
                    }

                    if (null != this.PickupDate)
                    {
                        loEntity.PickupDate = MaxConvertLibrary.ConvertToDateTimeUtc(typeof(object), this.PickupDate);
                    }

                    if (null != this.PickupTime)
                    {
                        loEntity.PickupTime = this.PickupTime;
                    }

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
                MaxOrderShippingInfoEntity loEntity = this.Entity as MaxOrderShippingInfoEntity;
                if (null != loEntity)
                {
                    this.ShippingType = loEntity.ShippingType;
                    this.Notes = loEntity.Notes;
                    if (!Guid.Empty.Equals(loEntity.ShippingAddressId))
                    {
                        this.ShippingAddressId = loEntity.ShippingAddressId.ToString();
                    }

                    if (loEntity.PickupDate > new DateTime(2015, 1, 1))
                    {
                        this.PickupDate = String.Format("{0:yyyy}-{0:MM}-{0:dd}", MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.PickupDate));
                    }

                    this.PickupTime = loEntity.PickupTime;

                    return true;
                }
            }

            return false;
        }

        public bool Save(string lsOrderId)
        {
            this.OrderId = lsOrderId;
            return this.Save();
        }

        public override bool Save()
        {
            MaxOrderShippingInfoEntity loEntity = this.Entity as MaxOrderShippingInfoEntity;
            if (!this.IsPickup)
            {
                if (this.ShippingAddress.Save())
                {
                    this.ShippingAddressId = this.ShippingAddress.Id;
                }
            }

            return base.Save();
        }

    }
}
