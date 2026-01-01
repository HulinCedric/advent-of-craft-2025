using ControlSystem.Core;
using FluentAssertions;

namespace ControlSystem.Tests
{
    public class TestControlSystem : IDisposable
    {
        private readonly StringWriter _output;
        private readonly TextWriter _originalOutput;

        public TestControlSystem()
        {
            _output = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_output);
        }

        [Fact]
        public void TestStart()
        {
            // The system has been started
            var controlSystem = new Core.ControlSystem(
                status: SleighEngineStatus.Off,
                action: SleighAction.Flying);
            controlSystem.StartSystem();
            controlSystem.Status.Should().Be(SleighEngineStatus.On);
            _output.ToString().Trim().Should().Be("""
                                                  Starting the sleigh...
                                                  System ready.
                                                  """);
        }
        
        [Fact]
        public void TestAlreadyStart()
        {
            var controlSystem = new Core.ControlSystem(
                status: SleighEngineStatus.On,
                action: SleighAction.Flying);
            controlSystem.StartSystem();
            controlSystem.Status.Should().Be(SleighEngineStatus.On);
            _output.ToString().Trim().Should().BeEmpty();
        }

        [Fact]
        public void TestAscend()
        {
            var controlSystem = new Core.ControlSystem();
            controlSystem.StartSystem();
            controlSystem.Ascend();
            controlSystem.Action.Should().Be(SleighAction.Flying);
            _output.ToString().Trim().Should().Be($"""
                                                   Starting the sleigh...
                                                   System ready.
                                                   Ascending...
                                                   """);
        }

        [Fact]
        public void TestDescend()
        {
            var controlSystem = new Core.ControlSystem();
            controlSystem.StartSystem();
            controlSystem.Ascend();
            controlSystem.Invoking(cs => cs.Descend()).Should().NotThrow<SleighNotStartedException>();
            controlSystem.Action.Should().Be(SleighAction.Hovering);
            _output.ToString().Trim().Should()
                .Be("""
                    Starting the sleigh...
                    System ready.
                    Ascending...
                    Descending...
                    """);
        }
        
        [Fact]
        public void TestDescendWhenParked()
        {
            var controlSystem = new Core.ControlSystem(SleighEngineStatus.On, SleighAction.Parked);
            controlSystem.Descend();
            controlSystem.Action.Should().Be(SleighAction.Parked);
            _output.ToString().Trim().Should().BeEmpty();
        }
        
        [Fact]
        public void TestDescendWhenHovering()
        {
            var controlSystem = new Core.ControlSystem(SleighEngineStatus.On, SleighAction.Hovering);
            controlSystem.Descend();
            controlSystem.Action.Should().Be(SleighAction.Hovering);
            _output.ToString().Trim().Should().BeEmpty();
        }

        [Fact]
        public void TestPark()
        {
            var controlSystem = new Core.ControlSystem();
            controlSystem.StartSystem();

            //we want to drain all the magic power to test the parking
            SafeAscendManyTimes(controlSystem, 10);

            controlSystem.Park();
            controlSystem.Ascend();

            Assert.True(controlSystem.Action == SleighAction.Flying);
            _output.ToString().Trim().Should()
                .Be("""
                    Starting the sleigh...
                    System ready.
                    Ascending...
                    Ascending...
                    Ascending...
                    Ascending...
                    Ascending...
                    Parking...
                    Ascending...
                    """);
        }
        
        [Fact]
        public void TestStop()
        {
            // The system has been started
            var controlSystem = new Core.ControlSystem(SleighEngineStatus.On, SleighAction.Parked);
            controlSystem.StopSystem();
            Assert.True(controlSystem.Status == SleighEngineStatus.Off);
            _output.ToString().Trim().Should()
                .Be("""
                    Stopping the sleigh...
                    System shutdown.
                    """);
        }
        
        [Fact]
        public void TestAlreadyStop()
        {
            var controlSystem = new Core.ControlSystem(SleighEngineStatus.Off, SleighAction.Parked);
            controlSystem.StopSystem();
            controlSystem.Status .Should().Be(SleighEngineStatus.Off);
            _output.ToString().Trim().Should().BeEmpty();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _output.Dispose();
        }
        
        private static void SafeAscendManyTimes(Core.ControlSystem controlSystem, int numberOfTimes) {
            try {
                for (int i=0;i<numberOfTimes;i++){
                    controlSystem.Ascend();
                }
            }
                catch(ReindeersNeedRestException e) {
                //we want to continue
            }
        }
    }
}