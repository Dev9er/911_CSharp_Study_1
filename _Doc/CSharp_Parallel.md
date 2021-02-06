# C# 문법
## Parallel
- `System.Thread.Tasks.Parallel`
- 병렬 처리를 손쉽게 구현
```C#
    void SomeMethod(int i) {}
    Parallel.For(0, 100, SomeMethod)
```
- Parallel.ForEach()