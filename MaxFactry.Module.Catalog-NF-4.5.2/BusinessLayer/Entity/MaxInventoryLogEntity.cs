// <copyright file="MaxInventoryLogEntity.cs" company="Lakstins Family, LLC">
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
// <change date="7/29/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxInventoryLogEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        public const int LogEntryTypeCurrent = 1;

        public const int LogEntryTypeReplenish = 2;

        public const int LogEntryTypeOrder = 3;

        public const int RelatedTypeOrder = 4;

        private static object _oLock = new Object();

        /// <summary>
        /// Initializes a new instance of the MaxInventoryLogEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxInventoryLogEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxInventoryLogEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxInventoryLogEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public Guid InventoryId
        {
            get
            {
                return this.GetGuid(this.DataModel.InventoryId);
            }

            set
            {
                this.Set(this.DataModel.InventoryId, value);
            }
        }

        public DateTime ChangedDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.ChangedDate);
            }

            set
            {
                this.Set(this.DataModel.ChangedDate, value);
            }
        }

        public Guid UserId
        {
            get
            {
                return this.GetGuid(this.DataModel.UserId);
            }

            set
            {
                this.Set(this.DataModel.UserId, value);
            }
        }

        public string Username
        {
            get
            {
                return this.GetString(this.DataModel.Username);
            }

            set
            {
                this.Set(this.DataModel.Username, value);
            }
        }

        public Guid RelatedId
        {
            get
            {
                return this.GetGuid(this.DataModel.RelatedId);
            }

            set
            {
                this.Set(this.DataModel.RelatedId, value);
            }
        }

        public int RelatedItemType
        {
            get
            {
                return this.GetInt(this.DataModel.RelatedItemType);
            }

            set
            {
                this.Set(this.DataModel.RelatedItemType, value);
            }
        }

        public int AmountType
        {
            get
            {
                return this.GetInt(this.DataModel.AmountType);
            }

            set
            {
                this.Set(this.DataModel.AmountType, value);
            }
        }

        public long AmountStart
        {
            get
            {
                return this.GetLong(this.DataModel.AmountStart);
            }

            set
            {
                this.Set(this.DataModel.AmountStart, value);
            }
        }

        public long AmountChanged
        {
            get
            {
                return this.GetLong(this.DataModel.AmountChanged);
            }

            set
            {
                this.Set(this.DataModel.AmountChanged, value);
            }
        }

        public long AmountEnd
        {
            get
            {
                return this.GetLong(this.DataModel.AmountEnd);
            }

            set
            {
                this.Set(this.DataModel.AmountEnd, value);
            }
        }

        public string Reason
        {
            get
            {
                return this.GetString(this.DataModel.Reason);
            }

            set
            {
                this.Set(this.DataModel.Reason, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxInventoryLogDataModel DataModel
        {
            get
            {
                return (MaxInventoryLogDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxInventoryLogEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxInventoryLogEntity),
                typeof(MaxInventoryLogDataModel)) as MaxInventoryLogEntity;
        }

        public static long LogInventoryChange(Guid loInventoryId, string lsReason, int lnAmount, int lnAmountType, long lnStart, string lsUserName)
        {
            MaxInventoryLogEntity loLogEntity = MaxInventoryLogEntity.Create();
            lock (_oLock)
            {
                loLogEntity.InventoryId = loInventoryId;
                loLogEntity.Reason = lsReason;
                loLogEntity.AmountChanged = lnAmount;
                loLogEntity.AmountType = lnAmountType;
                loLogEntity.ChangedDate = DateTime.UtcNow;
                loLogEntity.AmountStart = lnStart;
                loLogEntity.AmountEnd = lnStart + lnAmount;
                loLogEntity.Username = lsUserName;
                loLogEntity.Insert();
            }

            return loLogEntity.AmountEnd;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.ChangedDate.ToString("s") + this.ChangedDate.Millisecond.ToString("D4") + base.GetDefaultSortString();
        }
    }
}
