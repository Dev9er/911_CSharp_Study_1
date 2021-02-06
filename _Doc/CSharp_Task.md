# C# 문법
## Task
- `System.Threading.Tasks`
- Task : 비동기 코드를 손쉽게 작성할 수 있도록 도움
    - Synchronous 코드 : 검사의 찌르기 공격
        - 메서드 호출 후, 실행이 종료(반환) 되어야 다음 메서드 호출 (Blocking Code)
    - Asynchronous 코드 : 궁수의 활쏘기
        - Shoot(Fire) & Forget
        - 작업 A를 시작한 후 A의 결과가 나올 때까지 마냥 대기하는 대신 곧이어 다른 작업 B, C, D를 수행하다가 작업 A가 끝나면 그 때 결과를 받아내는 처리 방식.
        - 메서드 호출 후, 종료를 기다리지 않고 다음 코드 실행 (Non-Blocking Code)
        - async & await
### Thread Pool : Background Thread
- 작업 완료 시점을 알 수 없음
- 작업 수행 결과를 얻어 올 수 없음
- 취소/예외 처리 불가능
```C#
    ThreadPool.QueueUserWorkItem(() => {});
    // 이하는 상동
    Task t = new Task(() => {});
    t.Start();
    //혹은
    Task.Run(() => {});
```
### 장점
- Thread Pool 사용
- 작업 완료 시점을 알 수 있다 : Thread Chain
- 코드의 비동기 실행 결과(반환값)를 손쉽게 얻기 가능
- 취소, 예외 처리 가능
### Task 생성, 사용
#### Action, Func 대리자 사용 함수 생성
```C#
    Action SomeAction = () => {}
    Func<int> SomeFunc = () => {return result;}
```
#### Task 생성
```C#
    Task task = new Task(SomeAction);
    Task<int> task2 = new Task(SomeFunc);
    Task<int> task5 = new Task<int>((n) => SumFunc((int)n), 100);
    Task task6 = task5.ContinueWith(task => Console.WriteLine("The Sum is" + task.Result));
    Task task7 = task5.ContinueWith(task => Console.WriteLine("The Sum is" + task.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
    // TaskContinuationOptions.OnlyOnFaulted, TaskContinuationOptions.OnlyOnCanceled
    // Parent/Child Task로의 연결, TaskCreationOptions.AttachedToParent
    Task<Int32[]> parent = new Task<Int32[]>(() => {
        var results = new Int32[3];
        new Task(() =>  // 차일드로 연결
            results[0] = Sum(10), TaskCreationOptions.AttachedToParent).Start();
        new Task(() =>  // 차일드로 연결
            results[0] = Sum(20), TaskCreationOptions.AttachedToParent).Start();
        new Task(() =>  // 차일드로 연결
            results[0] = Sum(30), TaskCreationOptions.AttachedToParent).Start();
        return results;
    });
    var cwt = parent.ContineWith(   // parent Task가 끝나면 수행할 Task 연결
        parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));
    parent.Start();
    );
```
- Task.Factory.StartNew()
- Task.ContinueWith()
#### Task 실행
```C#
    task.Start();
    Task task1 = Task.Run(SomeAction);
    task2.Start();
    await Task.Delay(1000); // Thread.Sleep() 와 응답성(Blocking) 차이
```
#### Task 종료
```C#
    task.Wait();
    task.RunSynchronously();
    task1.Wait();
    task2.Wait();
```
### Task 결과값
```C#
    int i2 = task2.Result;
    int i5 = task5.Result;
```
#### 블로킹 방식
#### Callback 방식
- 애플리케이션이 스레드에게 작업 처리를 요청한 후, 다른 기능을 수행할 동안, 스레드가 작업을 완료하면 애플리케이션의 메서드를 자동 실행하는 기법으로 이 때 자동 실행되는 메서드를 콜백 메서드라고 한다.
### Task Cancel
```C#
    private static int Sum(CancellationToken ct, int n)
    {
        int sum = 0;
        for (; n > 0; n--)
        {
            // 작업 취소가 요청되면 OperationCanceledException을
            // innerException으로 하는 AggregateException 발생
            ct.ThrowIfCancellationRequested();
            checked { sum += n; }
        }
        return sum;
    }
    static void Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        Task<Int32> t = Task.Run(() => Sum(cts.Token, 10000000), cts.Token);
        cts.Cancel(); // 작업 취소
        try {
            Console.WriteLine("The Sum is: " + t.Result);
        } catch (AggregateException x) { // AggregateException handler
            x.Handle(e => e is OperatonCanceledException); // Operation 이면 처리된 것으로...
            Console.WriteLine("Sum was canceled");
        }
    }
```
### UI Thread
- BeginInvoke()
### 동기화