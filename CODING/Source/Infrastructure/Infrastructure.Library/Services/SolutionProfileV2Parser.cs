//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The SolutionProfileV2Parser class provides the implementation for v2 of the Profile Catalog
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PEA.BPM.Infrastructure.Library.SolutionProfileV2;
using PEA.BPM.Infrastructure.Library.Services;
using Microsoft.Practices.CompositeUI.Configuration;
using Properties = PEA.BPM.Infrastructure.Library.Properties;

namespace PEA.BPM.Infrastructure.Library.Services
{
    public class SolutionProfileV2Parser : ISolutionProfileParser
    {
        public const string Namespace = "http://schemas.microsoft.com/pag/cab-profile/2.0";

        public IModuleInfo[] Parse(string xml)
        {
            SolutionProfileElement solution = XmlValidationHelper.DeserializeXml<SolutionProfileV2.SolutionProfileElement>(xml,
                "SolutionProfileV2.xsd", Namespace);

            List<DependentModuleInfo> dmis = new List<DependentModuleInfo>();

            if (solution.Section != null)
            {
                foreach (SectionElement section in solution.Section)
                {
                    foreach (ModuleInfoElement moduleInfo in section.Modules)
                    {
                        DependentModuleInfo dmi = new DependentModuleInfo(moduleInfo.AssemblyFile);
                        SetModuleName(moduleInfo, dmi);
                        SetModuleRoles(moduleInfo, dmi);
                        SetSectionDependencies(solution.Section, section, dmi);
                        SetModuleDependencies(moduleInfo, dmi);
                        dmis.Add(dmi);
                    }
                }
            }

            return dmis.ToArray();
        }

        private static void SetModuleName(ModuleInfoElement configModuleInfo, DependentModuleInfo resultModuleInfo)
        {
            resultModuleInfo.SetName(configModuleInfo.Name);

            // If no name in config, check metadata
            if (resultModuleInfo.Name == null)
                resultModuleInfo.SetName(ModuleMetadataReflectionHelper.GetModuleName(resultModuleInfo.AssemblyFile));

            // If still no name, generate one
            if (resultModuleInfo.Name == null)
                resultModuleInfo.SetName(Guid.NewGuid().ToString());

            // Push the "true" name back into the object graph so we can find it later
            configModuleInfo.Name = resultModuleInfo.Name;
        }

        private static void SetModuleRoles(ModuleInfoElement moduleInfo, DependentModuleInfo dmi)
        {
            if (moduleInfo.Roles != null && moduleInfo.Roles.Length > 0)
                foreach (RoleElement role in moduleInfo.Roles)
                    dmi.AddRoles(role.Allow);
        }

        private static void SetSectionDependencies(SectionElement[] sections, SectionElement section, DependentModuleInfo dmi)
        {
            if (section.Dependencies == null)
                return;

            foreach (DependencyElement dep in section.Dependencies)
            {
                bool dependentSectionFound = false;

                foreach (SectionElement sec in sections)
                {
                    if (sec.Name == dep.Name)
                    {
                        dependentSectionFound = true;

                        foreach (ModuleInfoElement mod in sec.Modules)
                            dmi.Dependencies.Add(mod.Name);

                        break;
                    }
                }

                if (!dependentSectionFound)
                    throw new InvalidOperationException(string.Format(Properties.Resources.DependencyNotFound, section.Name, dep.Name));
            }
        }

        private static void SetModuleDependencies(ModuleInfoElement moduleInfo, DependentModuleInfo dmi)
        {
            if (moduleInfo.Dependencies == null)
                dmi.Dependencies.AddRange(ModuleMetadataReflectionHelper.GetModuleDependencies(dmi.AssemblyFile));
            else
                foreach (DependencyElement dep in moduleInfo.Dependencies)
                    dmi.Dependencies.Add(dep.Name);
        }
    }
}
