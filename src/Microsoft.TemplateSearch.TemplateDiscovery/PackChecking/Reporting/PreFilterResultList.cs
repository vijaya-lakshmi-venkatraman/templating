﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.TemplateSearch.TemplateDiscovery.PackChecking.Reporting
{
    public class PreFilterResultList
    {
        public PreFilterResultList()
        {
            Results = new List<PreFilterResult>();
        }

        public PreFilterResultList(List<PreFilterResult> results)
        {
            Results = results;
        }

        public IReadOnlyList<PreFilterResult> Results { get; }

        // return true if any of the filter results have IsFiltered == true
        public bool ShouldBeFiltered
        {
            get
            {
                return Results.Any(r => r.IsFiltered);
            }
        }

        public string Reason
        {
            get
            {
                return string.Join("; ", Results.Where(r => r.IsFiltered && !string.IsNullOrWhiteSpace(r.Reason)).Select(r => r.Reason));
            }
        }
    }
}
