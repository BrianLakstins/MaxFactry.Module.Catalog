// <copyright file="MaxOrderPaymentDetailCardViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxOrderPaymentDetailViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {

        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderPaymentDetailViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCardViewModel class
        /// </summary>
        public MaxOrderPaymentDetailViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentDetailCardViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderPaymentDetailViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderPaymentDetailCardViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderPaymentDetailViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderPaymentDetailCardEntity.Create();
        }

        public string DetailType
        {
            get; set;
        }

        [Display(Name = "Name on card")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Payment Code")]
        public string Code { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumberHidden { get; set; }

        [Display(Name = "Expiration Month")]
        [UIHint("CardExpireMonth")]
        public string CardExpireMonth { get; set; }

        [Display(Name = "Expiration Year")]
        [UIHint("CardExpireYear")]
        public string CardExpireYear { get; set; }

        [Display(Name = "Verification Code")]
        public string CardVerification { get; set; }

        [Display(Name = "Verification Code")]
        public string CardVerificationHidden { get; set; }

        [Display(Name = "Credit Card Override Note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderPaymentDetailViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderPaymentDetailViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderPaymentDetailViewModel loViewModel = new MaxOrderPaymentDetailViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
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
            MaxOrderPaymentDetailEntity loEntity = this.Entity as MaxOrderPaymentDetailEntity;
            if (loEntity.DetailType != this.DetailType)
            {
                this.Entity = MaxOrderPaymentDetailEntity.Create(this.DetailType);
                loEntity = this.Entity as MaxOrderPaymentDetailEntity;
            }

            if (base.MapToEntity())
            {
                if (null != loEntity)
                {
                    if (loEntity is MaxOrderPaymentDetailCardEntity)
                    {
                        MaxOrderPaymentDetailCardEntity loCardEntity = loEntity as MaxOrderPaymentDetailCardEntity;


                        loCardEntity.Name = this.CardName;
                        loCardEntity.CardVerificationCode = this.CardVerification;
                        loCardEntity.Note = this.Note;
                        if (null == this.CardNumber || !this.CardNumber.StartsWith("XXXX"))
                        {
                            loCardEntity.CardNumber = this.CardNumber;
                        }

                        if (null != this.CardExpireYear && null != this.CardExpireMonth)
                        {
                            int lnYear = MaxConvertLibrary.ConvertToInt(typeof(object), this.CardExpireYear);
                            int lnMonth = MaxConvertLibrary.ConvertToInt(typeof(object), this.CardExpireMonth);

                            loCardEntity.ExpirationDate = new DateTime(lnYear, lnMonth, 1);
                        }
                    }

                    if (loEntity is MaxOrderPaymentDetailGeneralEntity)
                    {
                        MaxOrderPaymentDetailGeneralEntity loGeneralEntity = loEntity as MaxOrderPaymentDetailGeneralEntity;
                        loGeneralEntity.Name = this.CardName;
                        loGeneralEntity.Code = this.Code;
                        loGeneralEntity.Note = this.Note;
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
                MaxOrderPaymentDetailEntity loEntity = this.Entity as MaxOrderPaymentDetailEntity;
                if (null != loEntity)
                {
                    if (loEntity is MaxOrderPaymentDetailGeneralEntity)
                    {
                        MaxOrderPaymentDetailGeneralEntity loGeneralEntity = loEntity as MaxOrderPaymentDetailGeneralEntity;
                        this.CardName = loGeneralEntity.Name;
                        this.Code = loGeneralEntity.Code;
                        this.Note = loGeneralEntity.Note;
                    }

                    if (loEntity is MaxOrderPaymentDetailCardEntity)
                    {
                        MaxOrderPaymentDetailCardEntity loCardEntity = loEntity as MaxOrderPaymentDetailCardEntity;
                        this.CardName = loCardEntity.Name;
                        this.CardNumber = loCardEntity.CardNumber;
                        if (loCardEntity.CardNumber.Length > 4)
                        {
                            this.CardNumberHidden = new string('X', loCardEntity.CardNumber.Length - 4) + loCardEntity.CardNumber.Substring(loCardEntity.CardNumber.Length - 4, 4);
                        }

                        this.CardVerification = loCardEntity.CardVerificationCode;
                        if (loCardEntity.CardVerificationCode.Length > 0)
                        {
                            this.CardVerificationHidden = new string('X', loCardEntity.CardVerificationCode.Length);
                        }

                        if (loCardEntity.ExpirationDate > new DateTime(2015,1,1))
                        {
                            this.CardExpireMonth = loCardEntity.ExpirationDate.Month.ToString();
                            this.CardExpireYear = loCardEntity.ExpirationDate.Year.ToString();
                        }

                        this.Note = loCardEntity.Note;
                    }

                    this.DetailType = loEntity.DetailType;

                    return true;
                }
            }

            return false;
        }

    }
}
