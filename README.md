# MsgPack.Light
MsgPack.Light is a lightweight http://msgpack.org/ format implementation.

## Key features
* Performance
* Extensibility
* Simple usage

## Install
Simpliest way to start using MsgPack.Light is to install [https://www.nuget.org/packages/MsgPack.Light/](Nuget package). To do that, run the following command in the  Package Manager Console:

```
 PM> Install-Package MsgPack.Light 
```

## Usage
### Serialization to bytes array:
```C#
var bytes = MsgPackSerializer.Serialize(value);
```

### Deserialization:
```C#
var value = MsgPackSerializer.Deserialize<MyType>(bytes);
```

## Performance
* Serialization performance is comparable with msgpack.cli
* Deserialization performance 2-3 times faster
* MsgPack.Light works best if a data reside a memory (*_Array benchmarks).
* Perfoming some IO operations, performance is suboptimal, but comparable with MsgPack.Cli (*_Stream benchmarks).
* More details can be found [https://github.com/roman-kozachenko/MsgPack.Light/blob/master/benchmarks.md](here).

## Credits
* Benchmark data authors: https://github.com/aensidhe/SerializationPerformanceTest_CSharp and https://github.com/maximn/SerializationPerformanceTest_CSharp 
* Thanks to [https://github.com/msgpack/msgpack-cli](msgpack.cli) authors for inspiration.

## Build statuses for master branch

Windows build status:

[![Windows build status](https://ci.appveyor.com/api/projects/status/42f0d1sdyn5kkcpc?svg=true)](https://ci.appveyor.com/project/roman-kozachenko/msgpack-light/branch/master)

Linux and OSX build status (it's not possible to separate build status per OS, so if any OS is failing build status will be failing):

[![Linux and OSX build status](https://travis-ci.org/roman-kozachenko/MsgPack.Light.svg?branch=master)](https://travis-ci.org/roman-kozachenko/MsgPack.Light)