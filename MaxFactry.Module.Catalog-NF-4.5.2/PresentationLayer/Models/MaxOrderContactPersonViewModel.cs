// <copyright file="MaxOrderContactPersonViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="6/21/2017" author="Brian A. Lakstins" description="Add Note property.">
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
    using MaxFactry.General.PresentationLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxOrderContactPersonViewModel : MaxFactry.Base.PresentationLayer.MaxBasePersonViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderContactPersonViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactPersonViewModel class
        /// </summary>
        public MaxOrderContactPersonViewModel()
            : base()
        {
            this.Entity = MaxOrderContactPersonEntity.Create();
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactPersonViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderContactPersonViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
            this.EmailSignup = true;
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderContactPersonViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderContactPersonViewModel(string lsId)
        {
            this.Entity = MaxOrderContactPersonEntity.Create();
            this.Id = lsId;
            if (this.EntityLoad())
            {
                this.Load();
            }
        }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide a last name.")]
        public override string CurrentLastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please provide a first name.")]
        public override string CurrentFirstName { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please provide a phone number.")]
        [MaxPhoneValidation(ErrorMessage = "Phone number must have 10 numbers.")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please provide an email address.")]
        [MaxEmailValidation(ErrorMessage = "Address is not valid.  It should be something like \"Person@Domain.ValidTopLevelDomain\".")]
        public string Email { get; set; }

        public bool EmailSignup { get; set; }

        public string OrderId { get; set; }

        public string Note { get; set; }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderContactPersonViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderContactPersonViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderContactPersonViewModel loViewModel = new MaxOrderContactPersonViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public bool Save(string lsOrderId)
        {
            this.OrderId = lsOrderId;
            return this.Save();
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
                MaxOrderContactPersonEntity loEntity = this.Entity as MaxOrderContactPersonEntity;
                if (null != loEntity)
                {
                    if (!string.IsNullOrEmpty(this.OrderId))
                    {
                        loEntity.OrderId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.OrderId);
                    }

                    loEntity.Email = this.Email;
                    loEntity.Phone = this.Phone;
                    loEntity.EmailSignup = this.EmailSignup;
                    loEntity.Note = this.Note;
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
                MaxOrderContactPersonEntity loEntity = this.Entity as MaxOrderContactPersonEntity;
                if (null != loEntity)
                {
                    this.Email = loEntity.Email;
                    this.Phone = loEntity.Phone;
                    this.EmailSignup = loEntity.EmailSignup;
                    this.Note = loEntity.Note;
                    return true;
                }
            }

            return false;
        }
    }
}
