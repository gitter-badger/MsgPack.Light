using Shouldly;

using Xunit;

namespace MsgPack.Light.Tests.Writer
{
    public class Boolean
    {
        [Fact]
        public void False()
        {
            MsgPackSerializer.Serialize(false).ShouldBe(new[] {(byte) DataTypes.False});
        }

        [Fact]
        public void True()
        {
            MsgPackSerializer.Serialize(true).ShouldBe(new[] {(byte) DataTypes.True});
        }
    }
}
