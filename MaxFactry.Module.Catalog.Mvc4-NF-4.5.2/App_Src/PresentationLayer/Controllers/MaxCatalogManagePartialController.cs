// <copyright file="MaxCatalogManagePartialController.cs" company="Lakstins Family, LLC">
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
// <change date="6/3/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/23/2014" author="Brian A. Lakstins" description="Added Client functionality..">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Added Catalog, Category, Vendor, Manufacturer, and Product functionality..">
// <change date="6/30/2014" author="Brian A. Lakstins" description="Add  to the ment controllers.">
// <change date="7/9/2014" author="Brian A. Lakstins" description="Add Cart Add, Cart view, and Cart update methods.">
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added Product Import.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{

    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;

    public class MaxCatalogManagePartialController : MaxPartialController
    {
    }
}
