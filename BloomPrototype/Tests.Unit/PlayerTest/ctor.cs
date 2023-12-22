using BloomPrototype.GameTypes;
using BloomPrototype.GameTypes.Soils;
using Shouldly;
using System;
using System.Linq;

namespace Tests.Unit.PlayerTest;

public class ctor
{
    [Fact]
    public void ctor_GeneralCall_SetsPlayerLocationToSoilAtX1Y1()
    {
        /// Arrange
        var soilArray = new Soil[2, 2];
        for(var i = 0; i < 2; i++)
            for(var j = 0; j < 2; j++)
                soilArray[i, j] = new Soil();

        var map = new Map(soilArray);

        /// Act
        var player = new Player(map);

        /// Assert
        player.Location.ShouldBe(soilArray[1, 1]);
    }
}
