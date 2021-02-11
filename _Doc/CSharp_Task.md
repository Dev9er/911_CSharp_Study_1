# C# 문법
## Task
- 비동기 프로그램 : IO Binding Code, CPU Binding Code
- `System.Threading.Tasks`
- 메서드 호출 방법
    - Synchronous 호출 : 메서드 호출 후, 실행이 종료(반환) 되어야 다음 메서드 호출 (Blocking Call)
    - Asynchronous 호출: Non-blocking call
        - 입출력 장치와의 속도 차이에서 오는 비효율적인 스레드 사용 문제를 극복하는데 사용
        - 작업 A를 시작한 후 A의 결과가 나올 때까지 마냥 대기하는 대신 곧이어 다른 작업 B, C, D를 수행하다가 작업 A가 끝나면 그 때 결과를 받아내는 처리 방식.
        - 메서드 호출 후, 종료를 기다리지 않고 다음 코드 실행
        - 긴 작업을 메인 스레드에서 분리하여 실행 후 결과를 반환하는 방식
- Task : 비동기 코드를 손쉽게 작성할 수 있도록 도움
    - Synchronous 코드 : 검사의 찌르기 공격
        - 메서드 호출 후, 실행이 종료(반환) 되어야 다음 메서드 호출 (Blocking Code)
    - Asynchronous 코드 : 궁수의 활쏘기
        - Shoot(Fire) & Forget
        - 작업 A를 시작한 후 A의 결과가 나올 때까지 마냥 대기하는 대신 곧이어 다른 작업 B, C, D를 수행하다가 작업 A가 끝나면 그 때 결과를 받아내는 처리 방식.
        - 메서드 호출 후, 종료를 기다리지 않고 다음 코드 실행 (Non-Blocking Code)
        - async & await
### 기존의 비동기 호출 방식 : UI Thread
- delegate의 비동기 호출을 위한 메서드로 ThreadPool의 스레드에서 실행된다.
    - BeginInvoke()
    - EndInvoke()
```C#
    public delegate long CalcMethod(int start, int end);
    CalcMethod calc = new CalcMethod((s, e) => s + e);
    // Delegate 타입의 BeginInvoke 메서드를 호출한다.
    // 이 때문에 calc 메서드는 ThreadPool의 스레드에서 실행된다.
    IAsyncResult ar = calc.BeginInvoke(1, 100, null, null);
    // BeginInvoke로 반환받은 IAsyncResult 타입의 AsyncWaitHandle 속성은 EventWaitHandle 타입이다.
    // AsyncWaitHandle 객체는 스레드 풀에서 실행된 calc의 동작이 완료됐을 때 Signal 상태로 바뀐다.
    // 따라서 아래의 호출은 calc 메서드 수행이 완료될 때까지 현재 스레드를 대기시킨다.
    ar.AsyncWaitHandle.WaitOne();
    // calc의 반환값을 얻기 위해 EndInvoke 메서드를 호출한다.
    // 반환값이 없어도 EndInvoke는 반드시 호출하는 것을 권장한다.
    long result = calc.EndInvoke(ar);
    // Callback 방식
    CalcMethod calc = new CalcMethod((s, e) => s + e);
    calc.BeginInvoke(1, 100, calcCompleted, calc);
    void calcCompleted(IAsyncResult ar)
    {
        CalcMethod calc = ar.AsyncState as CalcMethod;
        long result = calc.EndInvoke(ar);
    }
```
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
    new Task(() => {}).Start();
    new Task(obj => {}, null).Start();
    Task.Factory.StartNew(() => { Console.WriteLine("process taskitem"); });
    Task.Factory.StartNew((obj) => { Console.WriteLine("process taskitem(obj)"); }, null);
    Task<int> task4 = new Task<int>(() => {
        Random rand = new Random((int)DateTime.Now.Ticks);
        return rand.Next(); });
    // delegate를 전달하자마자 곧바로 작업 시작
    Task<int> taskReturn = Task.Factory.StartNew<int>(() => 1);
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
    // 2개의 작업을 병렬로 처리하지만, 모든 작업이 완료될 때까지 대기
    Task.WaitAll(task1, task2);
    // 2개의 작업을 병렬로 비동기 호출
    await Task.WhenAll(task3, task5);
    // WhenAny
    var allTasks = new List<Task> { rTask, sTask, eTask };
    while (allTasks.Any()) // 작업이 하나라도 있으면 실행
    {
        Task finished = await Task.WhenAny(allTasks);
        if (finished == rTask) ;
    }
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
### Task 메서드
- Task.Run() : CPU Binding된 코드를 묶어 비동기 처리를 하는 경우
```C#
    await Task.Run(() =>
    {
        for (int i = 0; i < 300; i++)
        {
            Console.WriteLine(i + 1);
        }
    });
```
- Thread.Sleep() : 현재 스레드를 차단하고, 지정된 시간만큼 기다린 후, Thread.Sleep() 이하의 문장 실행 (blocking)
- Task.Delay() : 지정된 시간 후에 종료(완료)되는 Task를 생성하여 실행하며, 동시에 Task.Delay() 이하의 문장도 계속 실행 (non-blocking)
- await Task.Delay() : 현재 Thread를 바로 호출 지점으로 반환시키고, await 문장 이후의 코드를 사용하여 새로운 Task를 생성한 후, 지정된 시간 경과 후에, 앞에 생성해 놓은 Task를 실행 (non-blocking)
- `Task<TResult> Task.FromResult<TResulet>(TResult result);`
```C#
    var forecasts = await service.GetForecastAsync(DateTime.Now);

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        var rng = new Random();

        return Task.FromResult(Enumerable.Range(1, 5).Select(idx => new WeatherForecast
        {
            Date = startDate.AddDays(idx),
            TemperatureC = rng.Next(-20, 55),
        }).ToArray());
    }

    //return await Task.FromResult<Egg>(new Egg());
```