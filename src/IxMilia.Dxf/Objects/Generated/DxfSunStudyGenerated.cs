// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

// The contents of this file are automatically generated by a tool, and should not be directly modified.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IxMilia.Dxf.Collections;
using IxMilia.Dxf.Entities;

namespace IxMilia.Dxf.Objects
{
    /// <summary>
    /// DxfSunStudy class
    /// </summary>
    public partial class DxfSunStudy : DxfObject, IDxfItemInternal
    {
        public override DxfObjectType ObjectType { get { return DxfObjectType.SunStudy; } }
        protected override DxfAcadVersion MaxVersion { get { return DxfAcadVersion.R2013; } }

        IEnumerable<DxfPointer> IDxfItemInternal.GetPointers()
        {
            yield return PageSetupWizardPointer;
            yield return ViewPointer;
            yield return VisualStylePointer;
            yield return TextStylePointer;
        }

        IEnumerable<IDxfItemInternal> IDxfItemInternal.GetChildItems()
        {
            return ((IDxfItemInternal)this).GetPointers().Select(p => (IDxfItemInternal)p.Item);
        }

        internal DxfPointer PageSetupWizardPointer { get; } = new DxfPointer();
        internal DxfPointer ViewPointer { get; } = new DxfPointer();
        internal DxfPointer VisualStylePointer { get; } = new DxfPointer();
        internal DxfPointer TextStylePointer { get; } = new DxfPointer();
        public int Version { get; set; }
        public string SunSetupName { get; set; }
        public string Description { get; set; }
        public short OutputType { get; set; }
        public string SheetSetName { get; set; }
        public bool UseSubset { get; set; }
        public string SheetSubsetName { get; set; }
        public bool SelectDatesFromCalendar { get; set; }
        public IList<DateTime> Dates { get; private set; }
        public bool SelectRangeOfDates { get; set; }
        public int StartTime_SecondsPastMidnight { get; set; }
        public int EndTime_SecondsPastMidnight { get; set; }
        public int IntervalInSeconds { get; set; }
        public IList<int> Hours { get; private set; }
        public IDxfItem PageSetupWizard { get { return PageSetupWizardPointer.Item as IDxfItem; } set { PageSetupWizardPointer.Item = value; } }
        public IDxfItem View { get { return ViewPointer.Item as IDxfItem; } set { ViewPointer.Item = value; } }
        public IDxfItem VisualStyle { get { return VisualStylePointer.Item as IDxfItem; } set { VisualStylePointer.Item = value; } }
        public short ShadePlotType { get; set; }
        public int ViewportsPerPage { get; set; }
        public int ViewportDistributionRowCount { get; set; }
        public int ViewportDistributionColumnCount { get; set; }
        public double Spacing { get; set; }
        public bool LockViewports { get; set; }
        public bool LabelViewports { get; set; }
        public IDxfItem TextStyle { get { return TextStylePointer.Item as IDxfItem; } set { TextStylePointer.Item = value; } }

        public DxfSunStudy()
            : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.Version = 0;
            this.SunSetupName = null;
            this.Description = null;
            this.OutputType = 0;
            this.SheetSetName = null;
            this.UseSubset = false;
            this.SheetSubsetName = null;
            this.SelectDatesFromCalendar = false;
            this.Dates = new ListNonNull<DateTime>();
            this.SelectRangeOfDates = false;
            this.StartTime_SecondsPastMidnight = 0;
            this.EndTime_SecondsPastMidnight = 0;
            this.IntervalInSeconds = 0;
            this.Hours = new ListNonNull<int>();
            this.ShadePlotType = 0;
            this.ViewportsPerPage = 0;
            this.ViewportDistributionRowCount = 0;
            this.ViewportDistributionColumnCount = 0;
            this.Spacing = 0.0;
            this.LockViewports = false;
            this.LabelViewports = false;
        }

        protected override void AddValuePairs(List<DxfCodePair> pairs, DxfAcadVersion version, bool outputHandles)
        {
            base.AddValuePairs(pairs, version, outputHandles);
            pairs.Add(new DxfCodePair(100, "AcDbSunStudy"));
            pairs.Add(new DxfCodePair(90, (this.Version)));
            pairs.Add(new DxfCodePair(1, (this.SunSetupName)));
            pairs.Add(new DxfCodePair(2, (this.Description)));
            pairs.Add(new DxfCodePair(70, (this.OutputType)));
            pairs.Add(new DxfCodePair(3, (this.SheetSetName)));
            pairs.Add(new DxfCodePair(290, (this.UseSubset)));
            pairs.Add(new DxfCodePair(4, (this.SheetSubsetName)));
            pairs.Add(new DxfCodePair(291, (this.SelectDatesFromCalendar)));
            pairs.Add(new DxfCodePair(91, Dates.Count));
            foreach (var item in Dates)
            {
                pairs.Add(new DxfCodePair(90, (int)DxfCommonConverters.DateDouble(item)));
                pairs.Add(new DxfCodePair(90, DxfCommonConverters.DateDouble(item)));
            }

            pairs.Add(new DxfCodePair(292, (this.SelectRangeOfDates)));
            if (SelectRangeOfDates)
            {
                pairs.Add(new DxfCodePair(93, (this.StartTime_SecondsPastMidnight)));
            }

            if (SelectRangeOfDates)
            {
                pairs.Add(new DxfCodePair(94, (this.EndTime_SecondsPastMidnight)));
            }

            if (SelectRangeOfDates)
            {
                pairs.Add(new DxfCodePair(95, (this.IntervalInSeconds)));
            }

            pairs.Add(new DxfCodePair(73, (short)Hours.Count));
            pairs.AddRange(this.Hours.Select(p => new DxfCodePair(290, (short)(p))));
            pairs.Add(new DxfCodePair(340, DxfCommonConverters.UIntHandle(this.PageSetupWizardPointer.Handle)));
            pairs.Add(new DxfCodePair(341, DxfCommonConverters.UIntHandle(this.ViewPointer.Handle)));
            pairs.Add(new DxfCodePair(342, DxfCommonConverters.UIntHandle(this.VisualStylePointer.Handle)));
            pairs.Add(new DxfCodePair(74, (this.ShadePlotType)));
            pairs.Add(new DxfCodePair(75, (short)(this.ViewportsPerPage)));
            pairs.Add(new DxfCodePair(76, (short)(this.ViewportDistributionRowCount)));
            pairs.Add(new DxfCodePair(77, (short)(this.ViewportDistributionColumnCount)));
            pairs.Add(new DxfCodePair(40, (this.Spacing)));
            pairs.Add(new DxfCodePair(293, (this.LockViewports)));
            pairs.Add(new DxfCodePair(294, (this.LabelViewports)));
            pairs.Add(new DxfCodePair(343, DxfCommonConverters.UIntHandle(this.TextStylePointer.Handle)));
        }
    }
}
