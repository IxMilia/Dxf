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
    /// DxfDataTable class
    /// </summary>
    public partial class DxfDataTable : DxfObject
    {
        public override DxfObjectType ObjectType { get { return DxfObjectType.DataTable; } }
        protected override DxfAcadVersion MinVersion { get { return DxfAcadVersion.R2007; } }
        public short Field { get; set; }
        public int ColumnCount { get; protected set; }
        public int RowCount { get; protected set; }
        public string Name { get; set; }
        public IList<string> ColumnNames { get; private set; }

        public DxfDataTable()
            : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.Field = 0;
            this.ColumnCount = 0;
            this.RowCount = 0;
            this.Name = null;
            this.ColumnNames = new ListNonNull<string>();
        }
    }
}
