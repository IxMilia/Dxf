﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using IxMilia.Dxf.Entities;

namespace IxMilia.Dxf.Objects
{
    public partial class DxfLightList
    {
        internal override DxfObject PopulateFromBuffer(DxfCodePairBufferReader buffer)
        {
            bool readVersionNumber = false;
            while (buffer.ItemsRemain)
            {
                var pair = buffer.Peek();
                if (pair.Code == 0)
                {
                    break;
                }

                while (this.TrySetExtensionData(pair, buffer))
                {
                    pair = buffer.Peek();
                }

                switch (pair.Code)
                {
                    case 5:
                        // pointer to a new light
                        LightsPointers.Pointers.Add(new DxfPointer(DxfCommonConverters.UIntHandle(pair.StringValue)));
                        break;
                    case 1:
                        // don't worry about the name; it'll be read from the light entity directly
                        break;
                    case 90:
                        if (readVersionNumber)
                        {
                            // count of lights is ignored since it's implicitly set by reading the values
                        }
                        else
                        {
                            Version = pair.IntegerValue;
                            readVersionNumber = true;
                        }
                        break;
                    default:
                        if (!base.TrySetPair(pair))
                        {
                            ExcessCodePairs.Add(pair);
                        }
                        break;
                }

                buffer.Advance();
            }

            return PostParse();
        }

        protected override void AddValuePairs(List<DxfCodePair> pairs, DxfAcadVersion version, bool outputHandles)
        {
            base.AddValuePairs(pairs, version, outputHandles);
            pairs.Add(new DxfCodePair(100, "AcDbLightList"));
            pairs.Add(new DxfCodePair(90, (this.Version)));
            pairs.Add(new DxfCodePair(90, Lights.Count));
            foreach (var item in LightsPointers.Pointers)
            {
                pairs.Add(new DxfCodePair(5, UIntHandle(item.Handle)));
                pairs.Add(new DxfCodePair(1, ((DxfLight)item.Item).Name));
            }

        }
    }
}
