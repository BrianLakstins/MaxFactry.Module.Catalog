// <copyright file="MaxOrderShippingInfoEntity.cs" company="Lakstins Family, LLC">
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
// <change date="12/29/2019" author="Brian A. Lakstins" description="Add loading from archive.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderShippingInfoEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private MaxOrderShippingAddressEntity _oShippingAddress = null;

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingInfoEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxOrderShippingInfoEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderShippingInfoEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderShippingInfoEntity(Type loDataModelType)
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

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }

        public string ShippingAddressType
        {
            get
            {
                return this.GetString(this.DataModel.ShippingAddressType);
            }

            set
            {
                this.Set(this.DataModel.ShippingAddressType, value);
            }
        }

        public Guid ShippingAddressId
        {
            get
            {
                return this.GetGuid(this.DataModel.ShippingAddressId);
            }

            set
            {
                this.Set(this.DataModel.ShippingAddressId, value);
            }
        }

        public string ShippingContactPersonType
        {
            get
            {
                return this.GetString(this.DataModel.ShippingContactPersonType);
            }

            set
            {
                this.Set(this.DataModel.ShippingContactPersonType, value);
            }
        }

        public Guid ShippingContactPersonId
        {
            get
            {
                return this.GetGuid(this.DataModel.ShippingContactPersonId);
            }

            set
            {
                this.Set(this.DataModel.ShippingContactPersonId, value);
            }
        }

        public int ShippingType
        {
            get
            {
                return this.GetInt(this.DataModel.ShippingType);
            }

            set
            {
                this.Set(this.DataModel.ShippingType, value);
            }
        }

        public string Notes
        {
            get
            {
                return this.GetString(this.DataModel.Notes);
            }

            set
            {
                this.Set(this.DataModel.Notes, value);
            }
        }

        public DateTime PickupDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.PickupDate);
            }

            set
            {
                this.Set(this.DataModel.PickupDate, value);
            }
        }

        public string PickupTime
        {
            get
            {
                return this.GetString(this.DataModel.PickupTime);
            }

            set
            {
                this.Set(this.DataModel.PickupTime, value);
            }
        }

        public MaxOrderShippingAddressEntity ShippingAddress
        {
            get
            {
                if (null == this._oShippingAddress)
                {
                    this._oShippingAddress = MaxOrderShippingAddressEntity.Create();

                    if (!Guid.Empty.Equals(this.ShippingAddressId))
                    {
                        this._oShippingAddress.LoadByIdCache(this.ShippingAddressId);
                    }
                }

                return this._oShippingAddress;
            }

            set
            {
                this._oShippingAddress = value;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderShippingInfoDataModel DataModel
        {
            get
            {
                return (MaxOrderShippingInfoDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderShippingInfoEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderShippingInfoEntity),
                typeof(MaxOrderShippingInfoDataModel)) as MaxOrderShippingInfoEntity;
        }

        public MaxOrderShippingInfoEntity CreateByOrderId(Guid loOrderId)
        {
            this.OrderId = loOrderId;
            if (Guid.Empty != loOrderId)
            {
                MaxOrderEntity loOrder = MaxOrderEntity.Create();
                if (loOrder.LoadByIdCache(loOrderId))
                {
                    MaxEntityList loList = this.LoadAllByOrderId(loOrderId);
                    if (loList.Count > 0)
                    {
                        this.Load(loList[0].GetData());
                        if (this.ShippingType != loOrder.ShippingType)
                        {
                            this.ShippingType = loOrder.ShippingType;
                            this.Update();
                        }
                    }
                    else
                    {
                        this.ShippingType = loOrder.ShippingType;
                        this.Insert();
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
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
            if (this.ShippingAddress.Id != Guid.Empty && !this.ShippingAddress.Archive())
            {
                lbR = false;
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
