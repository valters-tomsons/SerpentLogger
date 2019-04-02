using System;
using Xunit;
using SerpentLogger;
using SerpentAPI.Models;
using SerpentAPI.Enums;
using System.Linq;
using SerpentAPI.Exceptions;
using Moq;
using SerpentAPI.Interfaces;

namespace SerpentTests.Tests.Kernel
{
    public class RecordFlushingTests
    {
        [Fact]
        public void PassFlushWithoutBypass()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentKernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty);

            serpent.ForceFlushOnRecord = false;
            serpent.Record(newEntry);

            Assert.NotEmpty(serpent.GetRecords());
            serpent.FlushRecords();
            Assert.Empty(serpent.GetRecords());
        }        

        [Fact]
        public void ThrowManualFlushWithBypass()
        {
            var outputMock = new Mock<IDirectOutput>();
            var flushMock = new Mock<IRecordFlusher>();
            var serpent = new SerpentKernel(flushMock.Object, outputMock.Object);
            var newEntry = new SerpentEntry(String.Empty);

            serpent.ForceFlushOnRecord = true;

            Assert.Throws<InvalidFlushOperationException>( () => serpent.FlushRecords());
        }        
    }
}