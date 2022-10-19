// <copyright file="MaxProductImportRuleDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="12/17/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for product import rules. 
    /// </summary>
    public class MaxProductImportRuleDataModel : MaxFactry.Base.DataLayer.MaxBaseIdDataModel
    {
        /// <summary>
        /// Name of the rule.
        /// </summary>
        public readonly string Name = "Name";

        /// <summary>
        /// Index key to process rule on.
        /// </summary>
        public readonly string Key = "Key";

        /// <summary>
        /// Product Import to use this group of rules on.
        /// </summary>
        public readonly string ImportGroup = "ImportGroup";

        /// <summary>
        /// Type of the rule
        /// </summary>
        public readonly string RuleType = "RuleType";

        /// <summary>
        /// Data used to process the rule
        /// </summary>
        public readonly string RuleData1 = "RuleData1";

        /// <summary>
        /// Data used to process the rule
        /// </summary>
        public readonly string RuleData2 = "RuleData2";

        /// <summary>
        /// Data used to process the rule
        /// </summary>
        public readonly string RuleData3 = "RuleData3";
        
        /// <summary>
        /// Relative order this rule should be processed.
        /// </summary>
        public readonly string ProcessOrder = "ProcessOrder";

        /// <summary>
        /// Initializes a new instance of the MaxClientDataModel class
        /// </summary>
        public MaxProductImportRuleDataModel()
        {
            this.SetDataStorageName("MaxCatalogProductImportRule");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.Name, typeof(MaxShortString));
            this.AddType(this.Key, typeof(MaxShortString));
            this.AddType(this.ImportGroup, typeof(int));
            this.AddType(this.RuleType, typeof(int));
            this.AddType(this.RuleData1, typeof(string));
            this.AddType(this.RuleData2, typeof(string));
            this.AddType(this.RuleData3, typeof(string));
            this.AddType(this.ProcessOrder, typeof(int));
        }
    }
}
