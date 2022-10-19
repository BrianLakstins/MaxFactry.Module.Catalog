// <copyright file="MaxOrderPaymentEntity.cs" company="Lakstins Family, LLC">
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
// <change date="7/13/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="4/20/2016" author="Brian A. Lakstins" description="Updated to use centralized caching.">
// <change date="12/12/2019" author="Brian A. Lakstins" description="Fix setting order payment type so it has to be a supported type.">
// <change date="12/29/2019" author="Brian A. Lakstins" description="Add loading from archive.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderPaymentEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private MaxOrderPaymentDetailEntity _oPaymentDetail = null;

        private List<MaxOrderPaymentTransactionEntity> _oTransactionList = null;

		/// <summary>
        /// Initializes a new instance of the MaxOrderPaymentEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderPaymentEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderPaymentEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid OrderId
        {
            get
            {
                return this.GetGuid(this.DataModel.OrderId);
            }

            set
            {
                this.Set(this.DataModel.OrderId, value);
            }
        }

        public string Note
        {
            get
            {
                return this.GetString(this.DataModel.Note);
            }

            set
            {
                this.Set(this.DataModel.Note, value);
            }
        }

        public double CurrentAmount
        {
            get
            {
                return this.GetDouble(this.DataModel.CurrentAmount);
            }

            set
            {
                this.Set(this.DataModel.CurrentAmount, value);
            }
        }

        public string PaymentDetailType
        {
            get
            {
                string lsR = this.GetString(this.DataModel.PaymentDetailType);
                if (lsR != MaxOrderPaymentDetailEntity.PaymentDetailTypeGeneral &&
                    lsR != MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                {
                    lsR = MaxOrderPaymentDetailEntity.PaymentDetailTypeCard;
                }

                return lsR;
            }

            set
            {
                if (value == MaxOrderPaymentDetailEntity.PaymentDetailTypeGeneral ||
                    value == MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                {
                    this.Set(this.DataModel.PaymentDetailType, value);
                }
            }
        }

        public Guid PaymentDetailId
        {
            get
            {
                return this.GetGuid(this.DataModel.PaymentDetailId);
            }

            set
            {
                this.Set(this.DataModel.PaymentDetailId, value);
            }
        }

        public MaxOrderPaymentDetailEntity PaymentDetail
        {
            get
            {
                if (null == this._oPaymentDetail)
                {
                    this._oPaymentDetail = MaxOrderPaymentDetailEntity.Create(this.PaymentDetailType);
                }

                if (!Guid.Empty.Equals(this.PaymentDetailId) && this.PaymentDetailId != this._oPaymentDetail.Id)
                {
                    this._oPaymentDetail.LoadByIdCache(this.PaymentDetailId);
                }

                return this._oPaymentDetail;
            }

            set
            {
                this._oPaymentDetail = value;
            }
        }

        public List<MaxOrderPaymentTransactionEntity> TransactionList
        {
            get
            {
                if (null == this._oTransactionList)
                {
                    this._oTransactionList = new List<MaxOrderPaymentTransactionEntity>();
                    MaxOrderPaymentTransactionEntity loEntity = MaxOrderPaymentTransactionEntity.Create();
                    SortedList<string, MaxOrderPaymentTransactionEntity> loSortedList = new SortedList<string, MaxOrderPaymentTransactionEntity>();
                    MaxEntityList loList = loEntity.LoadAllByPaymentId(this.OrderId, this.Id);
                    if (loList.Count == 0)
                    {
                        MaxOrderEntity loOrder = MaxOrderEntity.Create();
                        if (loOrder.LoadByIdCache(this.OrderId))
                        {
                            loEntity.StorageKey = loOrder.StorageKey;
                            loList = loEntity.LoadAllByPaymentId(this.OrderId, this.Id);
                        }
                    }

                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        loEntity = loList[lnE] as MaxOrderPaymentTransactionEntity;
                        loSortedList.Add(loEntity.GetDefaultSortString(), loEntity);
                    }

                    this._oTransactionList.AddRange(loSortedList.Values);
                    //this._oTransactionList.Reverse();
                }

                return this._oTransactionList;
            }
        }

        public Exception ProcessingError { get; set; }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderPaymentDataModel DataModel
        {
            get
            {
                return (MaxOrderPaymentDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderPaymentEntity Create()
        {
            MaxOrderPaymentEntity loR = MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderPaymentEntity),
                typeof(MaxOrderPaymentDataModel)) as MaxOrderPaymentEntity;

            return loR;
        }

        public override bool Load(MaxData loData)
        {
            this._oPaymentDetail = null;
            return base.Load(loData);
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.PaymentDetailType.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByOrderId(Guid loOrderId)
        {
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType());
            if (Guid.Empty != loOrderId)
            {
                MaxDataList loDataList = MaxCacheRepository.Get(this.GetType(), this.GetByOrderIdCacheKey(loOrderId), typeof(MaxDataList)) as MaxDataList;
                if (null == loDataList)
                {
                    loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.OrderId, loOrderId);
                    if (loDataList.Count == 0)
                    {
                        //// Try loading from archive
                        if (!this.Data.DataModel.DataStorageName.EndsWith("MaxArchive"))
                        {
                            MaxDataModel loDataModel = MaxFactry.Core.MaxFactryLibrary.Create(this.Data.DataModel.GetType(), this.Data.DataModel.DataStorageName + "MaxArchive") as MaxDataModel;
                            if (null != loDataModel)
                            {
                                MaxData loDataArchive = this.Data.Clone(loDataModel);
                                loDataList = MaxCatalogRepository.SelectAllByProperty(loDataArchive, this.DataModel.OrderId, loOrderId);
                            }
                        }
                    }

                    MaxCacheRepository.Set(this.GetType(), this.GetByOrderIdCacheKey(loOrderId), loDataList);
                }

                loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            }

            return loEntityList;
        }

        public override bool Archive()
        {
            bool lbR = true;
            if (this.PaymentDetail.Id != Guid.Empty && !this.PaymentDetail.Archive())
            {
                lbR = false;
            }

            foreach (MaxOrderPaymentTransactionEntity loTransaction in this.TransactionList)
            {
                if (!loTransaction.Archive())
                {
                    lbR = false;
                }
            }

            if (lbR)
            {
                lbR = base.Archive();
            }

            return lbR;
        }

        protected string GetByOrderIdCacheKey(Guid loOrderId)
        {
            this.OrderId = loOrderId;
            string lsR = this.GetCacheKey() + ".LoadAllByOrderId" + loOrderId.ToString();
            return lsR;
        }
    }
}
