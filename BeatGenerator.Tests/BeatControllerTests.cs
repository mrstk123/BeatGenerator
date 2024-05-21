using BeatGenerator.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BeatGenerator.Tests
{
    public class BeatControllerTests
    {
        // Part II: Testing
        private BeatController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new BeatController();
        }

        // Part II.i: Unit Tests for Beat Generator Endpoint
        [Test]
        public void BeatGenerator_ValidInput_Test()
        {
            var result = _controller.GenerateBeat(12, 1) as OkObjectResult;
            Assert.IsNotNull(result);
            var values = result.Value as List<string>;
            Assert.That(values, Is.EqualTo(new List<string>
                            {
                              "Hi-Hat",        //12
                              "Low Floor Tom", //11
                              "Low Floor Tom", //10
                              "kick",          // 9
                              "snare",         // 8
                              "Low Floor Tom", // 7
                              "kick",          // 6
                              "Low Floor Tom", // 5
                              "snare",         // 4
                              "kick",          // 3
                              "Low Floor Tom", // 2
                              "Low Floor Tom"  // 1
                            }));
        }

        [Test]
        public void BeatGenerator_InvalidInput_Test()
        {
            var result = _controller.GenerateBeat(1, 12);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        // Part II.ii: Unit Tests for Configure Endpoint
        [Test]
        public void Configure_ValidInput_Test()
        {
            var result = _controller.Configure(3, "Bass Drum");
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Configure_InvalidInput_Test()
        {
            var result = _controller.Configure(5, "Bass Drum");
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }


        [Test]
        public void Reset_ReturnsOk()
        {
            var result = _controller.Reset();
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}