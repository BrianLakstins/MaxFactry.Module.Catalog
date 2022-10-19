// <copyright file="MaxOrderPaymentTransactionEntity.cs" company="Lakstins Family, LLC">
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
// <change date="5/27/2015" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderPaymentTransactionEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private MaxOrderPaymentEntity _oPayment = null;

        private MaxOrderEntity _oOrder = null;

        public const int TransactionTypeVerify = 1;

        public const int TransactionTypeAuthorize = 2;

        public const int TransactionTypeSale = 4;

		/// <summary>
        /// Initializes a new instance of the MaxOrderPaymentTransactionEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxOrderPaymentTransactionEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxOrderPaymentTransactionEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderPaymentTransactionEntity(Type loDataModelType)
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

        public MaxOrderEntity Order
        {
            get
            {
                if (null == this._oOrder)
                {
                    MaxOrderEntity loEntity = MaxOrderEntity.Create();
                    if (loEntity.LoadByIdCache(this.OrderId))
                    {
                        this._oOrder = loEntity;
                    }
                }

                return this._oOrder;
            }

            set
            {
                this._oOrder = value;
            }
        }

        public Guid PaymentId
        {
            get
            {
                return this.GetGuid(this.DataModel.PaymentId);
            }

            set
            {
                this.Set(this.DataModel.PaymentId, value);
            }
        }

        public MaxOrderPaymentEntity Payment
        {
            get
            {
                if (null == this._oPayment)
                {
                    MaxOrderPaymentEntity loEntity = MaxOrderPaymentEntity.Create();
                    if (loEntity.LoadByIdCache(this.PaymentId))
                    {
                        this._oPayment = loEntity;
                    }
                }

                return this._oPayment;
            }

            set
            {
                this._oPayment = value;
            }
        }

        public double Amount
        {
            get
            {
                return this.GetDouble(this.DataModel.Amount);
            }

            set
            {
                this.Set(this.DataModel.Amount, value);
            }
        }

        public bool IsCollected
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsCollected);
            }

            set
            {
                this.Set(this.DataModel.IsCollected, value);
            }
        }

        public DateTime DateCollected
        {
            get
            {
                if (this.IsCollected)
                {
                    return this.GetDateTime(this.DataModel.DateCollected);
                }

                return this.LastUpdateDate;
            }

            set
            {
                this.Set(this.DataModel.DateCollected, value);
            }
        }

        public string Log
        {
            get
            {
                return this.GetString(this.DataModel.Log);
            }

            set
            {
                this.Set(this.DataModel.Log, value);
            }
        }

        public string ProcessingError
        {
            get
            {
                return this.GetString(this.DataModel.ProcessingError);
            }

            set
            {
                this.Set(this.DataModel.ProcessingError, value);
            }
        }

        public int TransactionType
        {
            get
            {
                return this.GetInt(this.DataModel.TransactionType);
            }

            set
            {
                this.Set(this.DataModel.TransactionType, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderPaymentTransactionDataModel DataModel
        {
            get
            {
                return (MaxOrderPaymentTransactionDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderPaymentTransactionEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderPaymentTransactionEntity),
                typeof(MaxOrderPaymentTransactionDataModel)) as MaxOrderPaymentTransactionEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return String.Format("{0:s}", this.LastUpdateDate).PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public MaxEntityList LoadAllByPaymentId(Guid loOrderId, Guid loPaymentId)
        {
            this.OrderId = loOrderId;
            MaxDataList loDataList = MaxCatalogRepository.SelectAllByProperty(this.Data, this.DataModel.PaymentId, loPaymentId);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }
    }
}
