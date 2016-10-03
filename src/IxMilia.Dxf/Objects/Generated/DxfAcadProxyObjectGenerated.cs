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
    /// DxfAcadProxyObject class
    /// </summary>
    public partial class DxfAcadProxyObject : DxfObject
    {
        public override DxfObjectType ObjectType { get { return DxfObjectType.AcadProxyObject; } }
        protected override DxfAcadVersion MinVersion { get { return DxfAcadVersion.R2000; } }
        public int ProxyObjectClassId { get; set; }
        public int ApplicationObjectClassId { get; set; }
        public int SizeInBits { get; set; }
        public IList<string> BinaryObjectData { get; private set; }
        private IList<string> _objectIdsA { get; set; }
        private IList<string> _objectIdsB { get; set; }
        private IList<string> _objectIdsC { get; set; }
        private IList<string> _objectIdsD { get; set; }
        private uint _objectDrawingFormat { get; set; }
        public bool IsOriginalObjectDxfFormat { get; set; }

        public DxfAcadProxyObject()
            : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.ProxyObjectClassId = 499;
            this.ApplicationObjectClassId = 500;
            this.SizeInBits = 0;
            this.BinaryObjectData = new ListNonNull<string>();
            this._objectIdsA = new ListNonNull<string>();
            this._objectIdsB = new ListNonNull<string>();
            this._objectIdsC = new ListNonNull<string>();
            this._objectIdsD = new ListNonNull<string>();
            this._objectDrawingFormat = 0u;
            this.IsOriginalObjectDxfFormat = false;
        }

        protected override void AddValuePairs(List<DxfCodePair> pairs, DxfAcadVersion version, bool outputHandles)
        {
            base.AddValuePairs(pairs, version, outputHandles);
            if (version >= DxfAcadVersion.R13)
            {
                pairs.Add(new DxfCodePair(100, "AcDbProxyObject"));
            }
            pairs.Add(new DxfCodePair(90, (this.ProxyObjectClassId)));
            pairs.Add(new DxfCodePair(91, (this.ApplicationObjectClassId)));
            pairs.Add(new DxfCodePair(93, (this.SizeInBits)));
            pairs.AddRange(this.BinaryObjectData.Select(p => new DxfCodePair(310, p)));
            foreach (var item in ObjectIds)
            {
                pairs.Add(new DxfCodePair(330, item));
            }

            pairs.Add(new DxfCodePair(94, 0));
            pairs.Add(new DxfCodePair(95, (int)(this._objectDrawingFormat)));
            pairs.Add(new DxfCodePair(70, BoolShort(this.IsOriginalObjectDxfFormat)));
        }

        internal override bool TrySetPair(DxfCodePair pair)
        {
            switch (pair.Code)
            {
                case 70:
                    this.IsOriginalObjectDxfFormat = BoolShort(pair.ShortValue);
                    break;
                case 90:
                    this.ProxyObjectClassId = (pair.IntegerValue);
                    break;
                case 91:
                    this.ApplicationObjectClassId = (pair.IntegerValue);
                    break;
                case 93:
                    this.SizeInBits = (pair.IntegerValue);
                    break;
                case 95:
                    this._objectDrawingFormat = (uint)(pair.IntegerValue);
                    break;
                case 310:
                    this.BinaryObjectData.Add((pair.StringValue));
                    break;
                case 330:
                    this._objectIdsA.Add((pair.StringValue));
                    break;
                case 340:
                    this._objectIdsB.Add((pair.StringValue));
                    break;
                case 350:
                    this._objectIdsC.Add((pair.StringValue));
                    break;
                case 360:
                    this._objectIdsD.Add((pair.StringValue));
                    break;
                default:
                    return base.TrySetPair(pair);
            }

            return true;
        }
    }
}
