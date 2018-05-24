﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using IxMilia.Dxf.Objects;

namespace IxMilia.Dxf.Entities
{
    public partial class DxfImage
    {
        public IList<DxfPoint> ClippingVertices { get; } = new List<DxfPoint>();

        /// <param name="imagePath">The path to the image.</param>
        /// <param name="location">The bottom left corner of the location to display the image in the drawing.</param>
        /// <param name="imageWidth">The width of the image in pixels.</param>
        /// <param name="imageHeight">The height of the image in pixels.</param>
        /// <param name="displaySize">The display size of the image in drawing units.</param>
        public DxfImage(string imagePath, DxfPoint location, int imageWidth, int imageHeight, DxfVector displaySize)
            : this()
        {
            ImageDefinition = new DxfImageDefinition()
            {
                FilePath = imagePath,
                ImageWidth = imageWidth,
                ImageHeight = imageHeight
            };
            ImageSize = new DxfVector(imageWidth, imageHeight, 0.0);
            Location = location;
            UVector = new DxfVector(displaySize.X / imageWidth, 0.0, 0.0);
            VVector = new DxfVector(0.0, displaySize.Y / imageHeight, 0.0);
        }

        internal override void AddObjectsToOutput(List<DxfObject> objects)
        {
            objects.Add(ImageDefinition);
            objects.Add(ImageDefinitionReactor);
        }

        protected override DxfEntity PostParse()
        {
            Debug.Assert((ClippingVertexCount == _clippingVerticesX.Count) && (ClippingVertexCount == _clippingVerticesY.Count));
            for (var i = 0; i < this.ClippingVertexCount; i++)
            {
                var x = this._clippingVerticesX[i];
                var y = this._clippingVerticesY[i];
                this.ClippingVertices.Add(new DxfPoint(x, y, 0.0));
            }

            _clippingVerticesX.Clear();
            _clippingVerticesY.Clear();
            return this;
        }
    }
}
