// <copyright file="MaxOrderPaymentTransactionViewModel.cs" company="Lakstins Family, LLC">
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
    public class MaxOrderPaymentTransactionViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderPaymentTransactionViewModel> _oSortedList = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentTransactionViewModel class
        /// </summary>
        public MaxOrderPaymentTransactionViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentTransactionViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderPaymentTransactionViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderPaymentTransactionViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderPaymentTransactionViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderPaymentTransactionEntity.Create();
        }

        public bool IsCollected
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string DateCollected
        {
            get;
            set;
        }

        public string Log
        {
            get;
            set;
        }

        public string Amount
        {
            get;
            set;
        }

        public string PaymentId
        {
            get;
            set;
        }

        public string TransactionType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public List<MaxOrderPaymentTransactionViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderPaymentTransactionViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderPaymentTransactionViewModel loViewModel = new MaxOrderPaymentTransactionViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        public bool Save(string lsPaymentId)
        {
            this.PaymentId = lsPaymentId;
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
                MaxOrderPaymentTransactionEntity loEntity = this.Entity as MaxOrderPaymentTransactionEntity;
                if (null != loEntity)
                {
                    loEntity.Amount = MaxConvertLibrary.ConvertToDouble(typeof(object), this.Amount);
                    loEntity.DateCollected = MaxConvertLibrary.ConvertToDateTime(typeof(object), this.DateCollected);
                    loEntity.IsCollected = this.IsCollected;
                    loEntity.PaymentId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.PaymentId);
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
                MaxOrderPaymentTransactionEntity loEntity = this.Entity as MaxOrderPaymentTransactionEntity;
                if (null != loEntity)
                {
                    this.Log = loEntity.Log;
                    this.Amount = string.Format("{0:C}", loEntity.Amount);
                    this.PaymentId = loEntity.PaymentId.ToString();
                    this.IsCollected = loEntity.IsCollected;
                    this.DateCollected = MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.LastUpdateDate).ToString();
                    if (this.IsCollected && loEntity.DateCollected > DateTime.MinValue)
                    {
                        this.DateCollected = MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.DateCollected).ToString();
                    }

                    if (loEntity.IsCollected)
                    {
                        this.Status = "Collected";
                    }
                    else if (loEntity.IsActive && loEntity.TransactionType == MaxOrderPaymentTransactionEntity.TransactionTypeAuthorize)
                    {
                        this.Status = "Authorized";
                    }
                    else if (loEntity.IsActive && loEntity.TransactionType == MaxOrderPaymentTransactionEntity.TransactionTypeVerify)
                    {
                        this.Status = "Verified";
                    }

                    if (loEntity.TransactionType == MaxOrderPaymentTransactionEntity.TransactionTypeSale)
                    {
                        this.TransactionType = "Sale";
                    }
                    else if (loEntity.TransactionType == MaxOrderPaymentTransactionEntity.TransactionTypeAuthorize)
                    {
                        this.TransactionType = "Authorize";
                    }
                    else if (loEntity.TransactionType == MaxOrderPaymentTransactionEntity.TransactionTypeVerify)
                    {
                        this.TransactionType = "Verify";
                    }

                    return true;
                }
            }

            return false;
        }


    }
}
