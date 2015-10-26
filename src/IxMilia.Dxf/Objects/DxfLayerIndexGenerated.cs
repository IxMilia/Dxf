// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

// The contents of this file are automatically generated by a tool, and should not be directly modified.

using System;
using System.Collections.Generic;
using System.Linq;

namespace IxMilia.Dxf.Objects
{

    /// <summary>
    /// DxfLayerIndex class
    /// </summary>
    public partial class DxfLayerIndex : DxfObject
    {
        public override DxfObjectType ObjectType { get { return DxfObjectType.LayerIndex; } }

        public DateTime TimeStamp { get; set; }
        public List<string> LayerNames { get; private set; }
        public List<uint> IdBufferHandles { get; private set; }
        public List<int> IdBufferCounts { get; private set; }

        public DxfLayerIndex()
            : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.TimeStamp = DateTime.Now;
            this.LayerNames = new List<string>();
            this.IdBufferHandles = new List<uint>();
            this.IdBufferCounts = new List<int>();
        }

        protected override void AddValuePairs(List<DxfCodePair> pairs, DxfAcadVersion version, bool outputHandles)
        {
            base.AddValuePairs(pairs, version, outputHandles);
            pairs.Add(new DxfCodePair(100, "AcDbIndex"));
            pairs.Add(new DxfCodePair(40, DateDouble(this.TimeStamp)));
            pairs.Add(new DxfCodePair(100, "AcDbLayerIndex"));
            pairs.AddRange(this.LayerNames.Select(p => new DxfCodePair(8, p)));
            pairs.AddRange(this.IdBufferHandles.Select(p => new DxfCodePair(360, p)));
            pairs.AddRange(this.IdBufferCounts.Select(p => new DxfCodePair(90, p)));
        }

        internal override bool TrySetPair(DxfCodePair pair)
        {
            switch (pair.Code)
            {
                case 8:
                    this.LayerNames.Add((pair.StringValue));
                    break;
                case 40:
                    this.TimeStamp = DateDouble(pair.DoubleValue);
                    break;
                case 90:
                    this.IdBufferCounts.Add((pair.IntegerValue));
                    break;
                case 360:
                    this.IdBufferHandles.Add(UIntHandle(pair.StringValue));
                    break;
                default:
                    return base.TrySetPair(pair);
            }

            return true;
        }
    }

}