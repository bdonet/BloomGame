﻿using BloomPrototype.GameTypes.Plants;
using Shouldly;
using System;
using System.Linq;
using Tests.Utilities;

namespace Tests.Unit.PlantTests.TreeTests;

public class ctorTest
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(0, 1)]
	[InlineData(1, 0)]
	[InlineData(1, 1)]
	public void ctor_GeneralCall_SetsTreeLocationToGivenLocationXAndLocationY(int expectedX, int expectedY)
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(2);

		/// Act
		var tree = new Tree(map, expectedX, expectedY, PlantMaturity.Seedling);

		/// Assert
		tree.Location.ShouldBe(map.GetSoil(expectedX, expectedY));
	}

	[Fact]
	public void ctor_GeneralCall_SetsCactusMaturityToGivenMaturity()
	{
		/// Arrange
		var map = MapHelper.SetupTestMap(1);

		/// Act
		var tree = new Tree(map, 0, 0, PlantMaturity.Seedling);

		/// Assert
		tree.Maturity.ShouldBe(PlantMaturity.Seedling);
	}
}
