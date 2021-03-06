using System.IO;
using System.Text;

using BenchmarkDotNet.Attributes;

using MsgPack.Light;

namespace msgpack.light.benchmark
{
    [Config(typeof(BenchmarkConfig))]
    public class BeerListDeserializeBenchmark
    {
        private readonly MemoryStream _json;

        private readonly MemoryStream _msgPack;

        private readonly byte[] _msgPackArray;

        public BeerListDeserializeBenchmark()
        {
            var serializer = new BeerListSerializeBenchmark();
            _json = PrepareJson(serializer);
            _msgPack = PrepareMsgPack(serializer);
            _msgPackArray = _msgPack.ToArray();
        }

        private MemoryStream PrepareMsgPack(BeerListSerializeBenchmark serializer)
        {
            var memoryStream = new MemoryStream();
            serializer.MsgPackSerialize(memoryStream);
            return memoryStream;
        }

        private MemoryStream PrepareMsgPackLight(BeerListSerializeBenchmark serializer)
        {
            var memoryStream = new MemoryStream();
            serializer.MsgPackLightSerialize(memoryStream);
            return memoryStream;
        }

        private MemoryStream PrepareJson(BeerListSerializeBenchmark serializer)
        {
            var memoryStream = new MemoryStream();
            serializer.JsonSerialize(memoryStream);
            return memoryStream;
        }

        [Benchmark]
        public void JsonNet()
        {
            _json.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(_json, Encoding.UTF8, false, 1024, true))
            {
                var beer = Serializers<Beer[]>.Newtonsoft.Deserialize(reader, typeof(Beer[]));
            }
        }

        [Benchmark]
        public void JsonStack()
        {
            _json.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(_json, Encoding.UTF8, false, 1024, true))
            {
                var beer = Serializers<Beer[]>.ServiceStack.DeserializeFromReader(reader);
            }
        }

        [Benchmark(Baseline = true)]
        public void MPCli_Stream()
        {
            _msgPack.Seek(0, SeekOrigin.Begin);
            var beer = Serializers<Beer[]>.MsgPack.GetSerializer<Beer[]>().Unpack(_msgPack);
        }

        [Benchmark]
        public void MPCli_Array()
        {
            var beer = Serializers<Beer[]>.MsgPack.GetSerializer<Beer[]>().UnpackSingleObject(_msgPackArray);
        }

        [Benchmark]
        public void MPLight_Stream()
        {
            _msgPack.Seek(0, SeekOrigin.Begin);
            var beer = MsgPackSerializer.Deserialize<Beer[]>(_msgPack, Serializers<Beer[]>.MsgPackLight);
        }

        [Benchmark]
        public void MPLight_Array()
        {
            var beer = MsgPackSerializer.Deserialize<Beer[]>(_msgPackArray, Serializers<Beer[]>.MsgPackLight);
        }

        [Benchmark]
        public void MPCliH_Stream()
        {
            _msgPack.Seek(0, SeekOrigin.Begin);
            var beer = Serializers<Beer[]>.MsgPackHardcore.GetSerializer<Beer[]>().Unpack(_msgPack);
        }

        [Benchmark]
        public void MPCliH_Array()
        {
            var beer = Serializers<Beer[]>.MsgPackHardcore.GetSerializer<Beer[]>().UnpackSingleObject(_msgPackArray);
        }

        [Benchmark]
        public void MPLightH_Stream()
        {
            _msgPack.Seek(0, SeekOrigin.Begin);
            var beer = MsgPackSerializer.Deserialize<Beer[]>(_msgPack, Serializers<Beer[]>.MsgPackLightHardcore);
        }

        [Benchmark]
        public void MPLightH_Array()
        {
            var beer = MsgPackSerializer.Deserialize<Beer[]>(_msgPackArray, Serializers<Beer[]>.MsgPackLightHardcore);
        }
    }
}