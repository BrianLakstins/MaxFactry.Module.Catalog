// <copyright file="MaxOrderContactPersonEntity.cs" company="Lakstins Family, LLC">
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
// <change date="6/21/2017" author="Brian A. Lakstins" description="Add Note extended property.">
// <change date="12/29/2019" author="Brian A. Lakstins" description="Add loading from archive.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderContactPersonEntity : MaxFactry.Base.BusinessLayer.MaxBasePersonEntity
    {
        /// <summary>
        /// Initializes a new instance of the MaxOrderContactEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxOrderContactPersonEntity(MaxData loData)
            : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderContactPersonEntity(Type loDataModelType)
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

        public string Email
        {
            get
            {
                return this.GetString(this.DataModel.Email);
            }

            set
            {
                this.Set(this.DataModel.Email, value);
            }
        }

        public string Phone
        {
            get
            {
                return this.GetString(this.DataModel.Phone);
            }

            set
            {
                this.Set(this.DataModel.Phone, value);
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

        public bool EmailSignup
        {
            get
            {
                return this.GetBoolean(this.DataModel.EmailSignup);
            }

            set
            {
                this.Set(this.DataModel.EmailSignup, value);
            }
        }

        public string Note
        {
            get
            {
                return this.GetString("xNote");
            }

            set
            {
                this.Set("xNote", value);
            }
        }


        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxOrderContactPersonDataModel DataModel
        {
            get
            {
                return (MaxOrderContactPersonDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxOrderContactPersonEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderContactPersonEntity),
                typeof(MaxOrderContactPersonDataModel)) as MaxOrderContactPersonEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public virtual bool Subscribe(string lsList)
        {
            MaxIndex loMetaIndex = new MaxIndex();
            MaxOrderEntity loOrder = MaxOrderEntity.Create();
            loMetaIndex.Add("VAR-MERGE6", "Yes");
            if (loOrder.LoadByIdCache(this.OrderId))
            {
                loMetaIndex.Add("CurrentFirstName", this.CurrentFirstName);
                loMetaIndex.Add("CurrentLastName", this.CurrentLastName);
                loMetaIndex.Add("Order.AlternateId", loOrder.AlternateId);
                loMetaIndex.Add("Order.OrderPlacedDate", loOrder.OrderPlacedDate);
                loMetaIndex.Add("Order.ShippingTotal", loOrder.ShippingTotal);
                loMetaIndex.Add("Order.Total", loOrder.Total);

                for (int lnI = 0; lnI < loOrder.ItemList.Count; lnI++)
                {
                    loMetaIndex.Add("OrderItem.PrimaryReferenceId." + lnI.ToString(), loOrder.ItemList[lnI].PrimaryReferenceId);
                    loMetaIndex.Add("OrderItem.Name." + lnI.ToString(), loOrder.ItemList[lnI].Name);
                    loMetaIndex.Add("OrderItem.Quantity." + lnI.ToString(), loOrder.ItemList[lnI].Quantity);
                    loMetaIndex.Add("OrderItem.Sku." + lnI.ToString(), loOrder.ItemList[lnI].Sku);
                    loMetaIndex.Add("OrderItem.ItemPrice." + lnI.ToString(), loOrder.ItemList[lnI].ItemPrice);
                }
            }

            return MaxMailingListLibrary.Subscribe(lsList, this.Email, loMetaIndex);
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

        protected string GetByOrderIdCacheKey(Guid loOrderId)
        {
            this.OrderId = loOrderId;
            string lsR = this.GetCacheKey() + ".LoadAllByOrderId" + loOrderId.ToString();
            return lsR;
        }
    }
}
