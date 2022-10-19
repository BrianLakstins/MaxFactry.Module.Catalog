// <copyright file="MaxCatalogRepositoryProvider.cs" company="Lakstins Family, LLC">
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
// <change date="9/30/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer.Provider
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Default Provider for CatalogRepository
    /// </summary>
    public class MaxCatalogSearchRepositoryProvider : MaxFactry.Base.DataLayer.Provider.MaxStorageWriteRepositoryDefaultProvider, IMaxCatalogSearchRepositoryProvider
    {
        public MaxDataList SearchAllProductBySearchText(MaxData loData, string lsSearchText, int lnMaxCount)
        {
            MaxProductDataModel loDataModel = loData.DataModel as MaxProductDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            loData.Set(loDataModel.IsDeleted, false);
            loData.Set(loDataModel.IsDeleted + "-IsQueryKey", true);
            MaxDataQuery loDataQuery = new MaxDataQuery();

            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.Name, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Keywords, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Sku, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Description, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.DescriptionShort, ":", lsSearchText);
            loDataQuery.EndGroup();
            
            int lnTotal = 0;

            MaxDataList loDataList = this.Select(loData, loDataQuery, 1, lnMaxCount, out lnTotal);
            return loDataList;
        }

        public MaxDataList SearchAllProductActiveBySearchText(MaxData loData, string lsSearchText, int lnMaxCount)
        {
            MaxProductDataModel loDataModel = loData.DataModel as MaxProductDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            loData.Set(loDataModel.IsDeleted, false);
            loData.Set(loDataModel.IsDeleted + "-IsQueryKey", true);
            loData.Set(loDataModel.IsActive, true);
            loData.Set(loDataModel.IsActive + "-IsQueryKey", true);
            MaxDataQuery loDataQuery = new MaxDataQuery();

            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.Name, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Keywords, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Sku, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.Description, ":", lsSearchText);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.DescriptionShort, ":", lsSearchText);
            loDataQuery.EndGroup();
            int lnTotal = 0;

            MaxDataList loDataList = this.Select(loData, loDataQuery, 1, lnMaxCount, out lnTotal);
            return loDataList;
        }

        /// <summary>
        /// Selects data from the database
        /// </summary>
        /// <param name="loDataModel">Data model used to determine the structure.</param>
        /// <param name="loCategoryId">Category Id</param>
        /// <returns>List of data from select</returns>
        public MaxDataList SearchAllProductByCategoryId(MaxData loData, Guid loCategoryId)
        {
            MaxProductDataModel loDataModel = loData.DataModel as MaxProductDataModel;
            if (null == loDataModel)
            {
                throw new MaxException("Error casting [" + loData.DataModel.GetType() + "] for DataModel");
            }

            MaxDataQuery loDataQuery = new MaxDataQuery();
            loDataQuery.StartGroup();
            loDataQuery.AddFilter(loDataModel.CategoryIdList, ":", loCategoryId);
            loDataQuery.AddCondition("OR");
            loDataQuery.AddFilter(loDataModel.PrimaryCategoryId, "=", loCategoryId);
            loDataQuery.EndGroup();
            int lnTotal = 0;
            MaxDataList loDataList = this.Select(loData, loDataQuery, 0, 0, out lnTotal);
            return loDataList;
        }
    }
}
