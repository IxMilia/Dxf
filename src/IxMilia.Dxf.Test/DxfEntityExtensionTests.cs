﻿using System;
using System.Collections.Generic;
using IxMilia.Dxf.Entities;
using Xunit;

namespace IxMilia.Dxf.Test
{
    public class DxfEntityExtensionTests
    {
        [Fact]
        public void ShouldContainStartAndEndAngle()
        {
            DxfPoint center = new DxfPoint(0, 0, 0);
            const double radius = 1.0; // negative radius only for DxfVertex.Bulge, but not for arcs (?)
            for (double start = 0; start <= 360; start += 10)
            {
                for (double end = 0; end <= 360; end += 10)
                {
                    DxfArc sut = new DxfArc(center, radius, start, end);
                    Assert.True(sut.ContainsAngle(start), $"{start}°=>{end}° arc contains {start}°");
                    Assert.True(sut.ContainsAngle(end), $"{start}°=>{end}° arc contains {end}°");
                }
            }
        }

        [Theory]
        // simple 90° arcs
        [InlineData(0, 90, 20)]
        [InlineData(90, 180, 110)]
        [InlineData(180, 270, 200)]
        [InlineData(270, 360, 290)]
        // large outer 270° arcs
        [InlineData(90, 0, 0)]
        [InlineData(90, 0, 95)]
        [InlineData(90, 0, 355)]
        [InlineData(90, 0, 360)]
        [InlineData(180, 90, 190)]
        [InlineData(180, 90, 355)] // be4 boundary
        [InlineData(180, 90, 5)] // after boundary
        [InlineData(180, 90, 85)]
        public void ShouldContainAngle(double startAngle, double endAngle, double angle)
        {
            DxfPoint center = new DxfPoint(0, 0, 0);
            const double radius = 1.0; // negative radius only for DxfVertex.Bulge, but not for arcs (?)
            DxfArc sut = new DxfArc(center, radius, startAngle, endAngle);
            Assert.True(sut.ContainsAngle(angle));
        }

        [Theory]
        // simple 90° arcs
        [InlineData(0, 90, 359.9)]
        [InlineData(90, 180, 0)]
        [InlineData(180, 270, 360)]
        [InlineData(270, 360, 80)]
        // large outer 270° arcs cross boundary
        [InlineData(90, 0, 45)]
        [InlineData(180, 90, 91)]
        [InlineData(180, 90, 179)]
        public void ShouldNotContainAngle(double startAngle, double endAngle, double angle)
        {
            DxfPoint center = new DxfPoint(0, 0, 0);
            const double radius = 1.0;
            DxfArc sut = new DxfArc(center, radius, startAngle, endAngle);
            Assert.False(sut.ContainsAngle(angle));
        }
    }
}
