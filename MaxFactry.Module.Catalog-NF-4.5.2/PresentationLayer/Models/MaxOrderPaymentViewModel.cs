// <copyright file="MaxOrderPaymentViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="12/12/2019" author="Brian A. Lakstins" description="Remove ability to change payment type since it's not normally changed after being created.">
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
    public class MaxOrderPaymentViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        /// <summary>
        /// Internal storage of a sorted list.
        /// Can use Generic List if supported in the framework.
        /// </summary>
        private List<MaxOrderPaymentViewModel> _oSortedList = null;

        private MaxOrderPaymentDetailViewModel _oOrderPaymentDetail = null;

        private List<MaxOrderPaymentTransactionViewModel> _oOrderPaymentTransactionList = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentViewModel class
        /// </summary>
        public MaxOrderPaymentViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderPaymentViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderPaymentViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderPaymentViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderPaymentEntity.Create();
        }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public string PaymentDetailId
        {
            get;
            set;
        }

        public string PaymentDetailType
        {
            get;
            set;
        }

        public string Note
        {
            get;
            set;
        }

        public string CurrentAmount
        {
            get;
            set;
        }

        public string IsSelected
        {
            get;
            set;
        }

        public string Amount
        {
            get;
            set;
        }

        public MaxOrderPaymentDetailViewModel OrderPaymentDetail
        {
            get
            {
                MaxOrderPaymentEntity loEntity = this.Entity as MaxOrderPaymentEntity;
                if (null == this._oOrderPaymentDetail)
                {
                    this._oOrderPaymentDetail = new MaxOrderPaymentDetailViewModel(loEntity.PaymentDetail);
                    this._oOrderPaymentDetail.Load();
                }

                if (!Guid.Empty.Equals(loEntity.PaymentDetail.Id))
                {
                    this._oOrderPaymentDetail.Id = loEntity.PaymentDetail.Id.ToString();
                }
                else if (!string.IsNullOrEmpty(this.PaymentDetailId))
                {
                    this._oOrderPaymentDetail.Id = this.PaymentDetailId;
                }

                if (this.PaymentDetailType != this._oOrderPaymentDetail.DetailType)
                {

                    this._oOrderPaymentDetail.DetailType = this.PaymentDetailType;
                }

                return this._oOrderPaymentDetail;
            }

            set
            {
                this._oOrderPaymentDetail = value;
            }
        }

        /// <summary>
        /// Order payment list
        /// </summary>
        public List<MaxOrderPaymentTransactionViewModel> OrderPaymentTransactionList
        {
            get
            {
                if (null == this._oOrderPaymentTransactionList)
                {
                    this._oOrderPaymentTransactionList = new List<MaxOrderPaymentTransactionViewModel>();
                    MaxOrderPaymentEntity loEntity = this.Entity as MaxOrderPaymentEntity;
                    foreach (MaxOrderPaymentTransactionEntity loTransaction in loEntity.TransactionList)
                    {
                        MaxOrderPaymentTransactionViewModel loModel = new MaxOrderPaymentTransactionViewModel(loTransaction);
                        loModel.Load();
                        this._oOrderPaymentTransactionList.Add(loModel);
                    }
                }

                return this._oOrderPaymentTransactionList;
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
        public List<MaxOrderPaymentViewModel> GetSortedList()
        {
            if (null == this._oSortedList)
            {
                this._oSortedList = new List<MaxOrderPaymentViewModel>();
                string[] laKey = this.EntityIndex.GetSortedKeyList();
                for (int lnK = 0; lnK < laKey.Length; lnK++)
                {
                    MaxOrderPaymentViewModel loViewModel = new MaxOrderPaymentViewModel(this.EntityIndex[laKey[lnK]] as MaxEntity);
                    loViewModel.Load();
                    this._oSortedList.Add(loViewModel);
                }
            }

            return this._oSortedList;
        }

        /// <summary>
        /// Returns an error message for the verification attempt.
        /// </summary>
        /// <returns>An error message or blank if successful.</returns>
        public string TryVerify()
        {
            return this.TryTransaction("verify");
        }

        /// <summary>
        /// Returns an error message for the authorization attempt.
        /// </summary>
        /// <returns>An error message or blank if successful.</returns>
        public string TryAuthorize()
        {
            return this.TryTransaction("authorize");
        }

        /// <summary>
        /// Returns an error message for the sale attempt.
        /// </summary>
        /// <returns>An error message or blank if successful.</returns>
        public string TrySale()
        {
            return this.TryTransaction("sale");
        }

        /// <summary>
        /// Returns an error message for the sale attempt.
        /// </summary>
        /// <returns>An error message or blank if successful.</returns>
        protected string TryTransaction(string lsType)
        {
            MaxOrderPaymentEntity loEntity = this.Entity as MaxOrderPaymentEntity;
            double lnAmount = MaxConvertLibrary.ConvertToDouble(typeof(object), this.Amount);
            if (lnAmount <= 0)
            {
                lnAmount = MaxConvertLibrary.ConvertToDouble(typeof(object), this.CurrentAmount);
                if (lnAmount < 0)
                {
                    lnAmount = 0;
                }
            }

            string lsR = "No valid payment type found";
            MaxOrderPaymentTransactionEntity loTransactionEntity = MaxOrderPaymentTransactionEntity.Create();
            loTransactionEntity.Amount = lnAmount;
            loTransactionEntity.OrderId = loEntity.OrderId;
            loTransactionEntity.PaymentId = loEntity.Id;
            loTransactionEntity.Payment = loEntity;
            loTransactionEntity.IsCollected = false;
            loTransactionEntity.TransactionType = MaxOrderPaymentTransactionEntity.TransactionTypeVerify;
            if (lsType == "sale")
            {
                loTransactionEntity.TransactionType = MaxOrderPaymentTransactionEntity.TransactionTypeSale;
            }
            else if (lsType == "authorize")
            {
                loTransactionEntity.TransactionType = MaxOrderPaymentTransactionEntity.TransactionTypeAuthorize;
            }

            loTransactionEntity.Insert();

            if (!MaxPaymentLibrary.ProcessTransaction(loTransactionEntity))
            {
                loTransactionEntity.Log += DateTime.UtcNow.ToString() + " UTC: No provider processed transaction.\r\n";
                loTransactionEntity.Update();
                lsR = "No provider processed transaction";
            }
            else
            {
                lsR = loTransactionEntity.ProcessingError;
            }

            return lsR;
        }

        public bool Save(string lsOrderId)
        {
            this.OrderId = lsOrderId;
            return this.Save();
        }

        public override bool Save()
        {
            if (this.OrderPaymentDetail.Save())
            {
                this.PaymentDetailId = this.OrderPaymentDetail.Id;
                return base.Save();
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
                MaxOrderPaymentEntity loEntity = this.Entity as MaxOrderPaymentEntity;
                if (null != loEntity)
                {
                    if (!string.IsNullOrEmpty(this.OrderId))
                    {
                        loEntity.OrderId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.OrderId);
                    } 

                    loEntity.Note = this.Note;
                    //loEntity.PaymentDetailType = this.PaymentDetailType;
                    if (null != this.PaymentDetailId)
                    {
                        loEntity.PaymentDetailId = MaxConvertLibrary.ConvertToGuid(typeof(object), this.PaymentDetailId);
                    }

                    double lnCurrentAmount = MaxConvertLibrary.ConvertToDouble(typeof(object), this.CurrentAmount);
                    if (lnCurrentAmount > 0)
                    {
                        loEntity.CurrentAmount = lnCurrentAmount;
                    }
                    else
                    {
                        loEntity.CurrentAmount = 0;
                    }

                    if (null != this.IsSelected)
                    {
                        loEntity.IsActive = MaxConvertLibrary.ConvertToBoolean(typeof(object), this.IsSelected);
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
                MaxOrderPaymentEntity loEntity = this.Entity as MaxOrderPaymentEntity;
                if (null != loEntity)
                {
                    this.Note = loEntity.Note;
                    if (this.IsActive)
                    {
                        this.IsSelected = "true";
                    }
                    
                    if (!Guid.Empty.Equals(loEntity.PaymentDetailId))
                    {
                        this.PaymentDetailId = loEntity.PaymentDetailId.ToString();
                    }

                    this.PaymentDetailType = loEntity.PaymentDetailType;

                    if (loEntity.CurrentAmount > 0)
                    {
                        this.CurrentAmount = loEntity.CurrentAmount.ToString();
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
