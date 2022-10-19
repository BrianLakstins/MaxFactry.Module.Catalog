rem Package the library for Nuget
copy ..\MaxFactry.Module.Catalog-NF-4.5.2\bin\Release\MaxFactry.Module.Catalog*.dll lib\Catalog\net452\

c:\install\nuget\nuget.exe pack MaxFactry.Module.Catalog.nuspec -OutputDirectory "packages" -IncludeReferencedProjects -properties Configuration=Release 

copy ..\MaxFactry.Module.Catalog.Mvc4-NF-4.5.2\bin\MaxFactry.Module.Catalog.Mvc4*.dll lib\Catalog.Mvc4\net452\

c:\install\nuget\nuget.exe pack MaxFactry.Module.Catalog.Mvc4.nuspec -OutputDirectory "packages" -IncludeReferencedProjects -properties Configuration=Release 
