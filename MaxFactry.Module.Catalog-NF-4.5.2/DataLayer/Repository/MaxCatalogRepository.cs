// <copyright file="MaxCatalogRepository.cs" company="Lakstins Family, LLC">
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
// <change date="6/16/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/28/2014" author="Brian A. Lakstins" description="Change to BaseId.">
// <change date="7/9/2014" author="Brian A. Lakstins" description="Added Cart selection filter methods.">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated for changes to core and to use base functionality.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Repository for managing catalog data storage.
    /// </summary>
    public class MaxCatalogRepository : MaxFactry.Base.DataLayer.MaxBaseIdRepository
    {
        /// <summary>
        /// Selects data from the database
        /// </summary>
        /// <param name="loData">Element with data used to determine the provider.</param>
        /// <param name="loReferenceId">ReferenceId for the cart</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllCartByReferenceIdType(MaxData loData, Guid loReferenceId, string lsType)
        {
            MaxCartDataModel loDataModel = loData.DataModel as MaxCartDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            loData.Set(loDataModel.ReferenceId, loReferenceId);
            loData.Set(loDataModel.ReferenceType, lsType);
            MaxData loDataFilter = new MaxData(loData);
            loDataFilter.Set(loDataModel.ReferenceId, loReferenceId);
            loDataFilter.Set(loDataModel.ReferenceType, lsType);

            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.ReferenceId, "=", loReferenceId);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.ReferenceType, "=", lsType);
            loDataQuery.EndGroup();
            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal);
            return loDataList;
        }

        /// <summary>
        /// Selects data from the database
        /// </summary>
        /// <param name="loData">Element with data used to determine the provider.</param>
        /// <param name="loReferenceId">ReferenceId for the cart</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllOrderByProcessingStatus(MaxData loData, int[] laProcessingStatus)
        {
            MaxOrderDataModel loDataModel = loData.DataModel as MaxOrderDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            MaxData loDataFilter = new MaxData(loData);

            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            for (int lnP = 0; lnP < laProcessingStatus.Length; lnP++)
            {
                if (lnP > 0)
                {
                    loDataQuery.AddCondition("OR");
                }

                loDataQuery.AddFilter(loDataModel.ProcessingStatus, "=", laProcessingStatus[lnP]);
            }

            loDataQuery.EndGroup();
            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal);
            return loDataList;
        }

        /// <summary>
        /// Selects data from the database
        /// </summary>
        /// <param name="loData">Element with data used to determine the provider.</param>
        /// <param name="loReferenceId">ReferenceId for the cart</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllOrderByReferenceIdType(MaxData loData, Guid loReferenceId, string lsReferenceType)
        {
            MaxOrderDataModel loDataModel = loData.DataModel as MaxOrderDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            loData.Set(loDataModel.ReferenceId, loReferenceId);
            loData.Set(loDataModel.ReferenceType, lsReferenceType);
            MaxData loDataFilter = new MaxData(loData);
            loDataFilter.Set(loDataModel.ReferenceId, loReferenceId);
            loDataFilter.Set(loDataModel.ReferenceType, lsReferenceType);

            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.AddFilter(loDataModel.ReferenceId, "=", loReferenceId);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.ReferenceType, "=", lsReferenceType);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.IsDeleted, "=", false);

            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal);
            return loDataList;
        }

        public static MaxDataList SearchAllProductBySearchText(MaxData loData, string lsSearchText, int lnMaxCount)
        {
            MaxData loDataFilter = new MaxData(loData);
            IMaxStorageReadRepositoryProvider loRepositoryProvider = Instance.GetStorageReadRepositoryProvider(loDataFilter);
            IMaxCatalogSearchRepositoryProvider loProvider = loRepositoryProvider as IMaxCatalogSearchRepositoryProvider;
            if (null == loProvider)
            {
                throw new MaxException("Error casting [" + loRepositoryProvider.GetType() + "] for Provider");
            }

            MaxDataList loDataList = loProvider.SearchAllProductBySearchText(loDataFilter, lsSearchText, lnMaxCount);
            return loDataList;
        }

        public static MaxDataList SearchAllProductActiveBySearchText(MaxData loData, string lsSearchText, int lnMaxCount)
        {
            MaxData loDataFilter = new MaxData(loData);
            IMaxStorageReadRepositoryProvider loRepositoryProvider = Instance.GetStorageReadRepositoryProvider(loDataFilter);
            IMaxCatalogSearchRepositoryProvider loProvider = loRepositoryProvider as IMaxCatalogSearchRepositoryProvider;
            if (null == loProvider)
            {
                throw new MaxException("Error casting [" + loRepositoryProvider.GetType() + "] for Provider");
            }

            MaxDataList loDataList = loProvider.SearchAllProductActiveBySearchText(loDataFilter, lsSearchText, lnMaxCount);
            return loDataList;
        }

        /// <summary>
        /// Selects data from the database
        /// </summary>
        /// <param name="loData">Element with data used to determine the provider.</param>
        /// <param name="loCategoryId">Category Id</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SearchAllProductByCategoryId(MaxData loData, Guid loCategoryId)
        {
            MaxData loDataFilter = new MaxData(loData);
            IMaxStorageReadRepositoryProvider loRepositoryProvider = Instance.GetStorageReadRepositoryProvider(loDataFilter);
            IMaxCatalogSearchRepositoryProvider loProvider = loRepositoryProvider as IMaxCatalogSearchRepositoryProvider;
            if (null == loProvider)
            {
                throw new MaxException("Error casting [" + loRepositoryProvider.GetType() + "] for Provider");
            }

            MaxDataList loDataList = loProvider.SearchAllProductByCategoryId(loDataFilter, loCategoryId);
            return loDataList;
        }

        /// <summary>
        /// Selects entities created within the date range
        /// </summary>
        /// <param name="loData">Data used to determine repository provider.</param>
        /// <param name="ldStart">Start date of range</param>
        /// <param name="ldEnd">End date of range</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllOrderByOrderPlacedDateRange(MaxData loData, DateTime ldStart, DateTime ldEnd, params string[] laFields)
        {
            MaxData loDataFilter = new MaxData(loData);
            MaxOrderDataModel loDataModel = loData.DataModel as MaxOrderDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.OrderPlacedDate, ">=", ldStart);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.OrderPlacedDate, "<", ldEnd);
            loDataQuery.EndGroup();
            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal, laFields);
            return loDataList;
        }

        /// <summary>
        /// Selects entities created within the date range
        /// </summary>
        /// <param name="loData">Data used to determine repository provider.</param>
        /// <param name="ldStart">Start date of range</param>
        /// <param name="ldEnd">End date of range</param>
        /// <param name="laFields">list of fields to return from select</param>
        /// <returns>List of data from select</returns>
        public static MaxDataList SelectAllOrderByExternal(MaxData loData, string lsExternalOrderType, string lsExternalOrderId, params string[] laFields)
        {
            MaxOrderDataModel loDataModel = loData.DataModel as MaxOrderDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            MaxData loDataFilter = new MaxData(loData);
            loDataFilter.Set(loDataModel.ExternalOrderType, lsExternalOrderType);
            loDataFilter.Set(loDataModel.ExternalOrderId, lsExternalOrderId);
            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.ExternalOrderType, "=", lsExternalOrderType);
            loDataQuery.AddCondition("AND");
            loDataQuery.AddFilter(loDataModel.ExternalOrderId, "=", lsExternalOrderId);
            loDataQuery.EndGroup();
            int lnTotal = 0;
            MaxDataList loDataList = Select(loDataFilter, loDataQuery, 0, 0, string.Empty, out lnTotal, laFields);
            return loDataList;
        }
    }
}
