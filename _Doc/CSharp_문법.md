# C# 문법
## 개발 환경 구축
- 비주얼 스튜디오 2019 커뮤니티 설치
## 첫 C# 프로그램 작성하기
-  출력문 : Top level Program
```C#
    System.Console.WriteLine("Hello, World!");
```
- Name Space, Class, Method
- 식별자, 예약어(Key Word) : 대, 소문자
- () 연산자
- 단문: 문장 종결자 ;
    - 공문
    - 식
```C#
        System.Console.WriteLine();
        System.Console.Write();
```
- 공백, 들여쓰기 (Indent)
- Literal
    - 숫자: 3, 3L, 3.14D, 3.0F, 3M, 3U, 5LU, 3.14E-3,
        - 이진 리터럴: 0B_1010L
        - 16진 리터럴: 0X_FL
    - 문자(값 형식에 속하는 유니코드 문자, UTF-16 기반): 'A'
        - '\uAC00' ~ '\uD7A3' (유니코드 이스케이프 시퀀스)
        - '\x006A' == '\x6A' (16진수 이스케이프 시퀀스)
        - 리틀 엔디언, 빅 엔디언 (Endian)
    - 문자열(참조 형식에 속하는 유니코드 문자열): "A"
        - 이스케이프 시퀀스 (Escape Sequence)
            - \\, \n, \t, \", \'
- 연산: 수나 식을 일정한 규칙에 따라 계산하는 것
        하나 이상의 피연산자를 연산자의 정의에 따라 계산하여 하나의 결과값을 도출해 내는 과정
    - 숫자 연산: +, -, *, /, %
    - 문자열 연결: +
        - "A" + 1 + 'a'
## 사용자 입력 받기
```C#
    var input = System.Console.ReadLine();
    System.Console.WriteLine($"{input}");
    System.Console.ReadKey();   // Debug용 입력 사용 안하기
```
- 식별자, 예약어(Key Word)
- Type : 데이터 형식, (3.14).GetType()
    - var
    - int, long, sbyte, short : .NET 형식, SByte, Int16, Int32, Int64
    - uint, ulong, byte, ushort : Byte, UInt16, UInt32, UInt64
    - float, double, decimal : Single, Double, Decimal
        - MinValue, MaxValue
    - Cast 연산자 : 묵시적, 명시적
    - char, string
- 변수 : Type 데이터 형식의 저장 공간의 이름
    - 선언, 초기화, 정의
    - 결정된 정수 리터럴 형식이 int이고 리터럴이 나타내는 값이 대상 형식의 범위 내에 있는 경우, 해당 값이 암시적으로 sbyte, byte, short, ushort, uint 또는 ulong으로 변환될 수 있습니다.
    - default
    _ 형식이 같은 변수 여러 개를 한 번에 선언하기
```C#
            int number1, number2, number3;
```
    - L-Value, R-Value
- 상수 : 리터럴과 차이점?
```C#
        const double PI = 3.14;
```
- 할당 연산자: =, +=
    - 문자열 연결, 숫자 연산 추가
```C#
        a += b -= c = 10;
```
- 주석문 (코드 설명문) : //, /* */, ///
```C#
        #region *전처리기 지시문 영역*
        var comment = "/// : XML 문서 주석";
        System.Console.WriteLine("comment");
        #endregion *전처리기 지시문 영역*
```
- 문자열 보간법: $""
    - Place Holder
    - string.Format("{0}님, {1}", "홍길동", "안녕하세요.")
- @"" : 공백/탭 포함 문자열
- using 문 : namespace
    - using System; // 멀티 커서
    - using static System.Console;
- Convert Type
```C#
        string input = Console.ReadLine(); // "10"을 입력한다면
        int number = Convert.ToInt32(input); // "10"을 정수 10으로 변환
        int number2 = int.Parse(input);
        Console.WriteLine($"{number:#,###.00}");

        // 10진수 10을 2진수로 변경하면 1010
        Console.WriteLine($"10 : {Convert.ToString(10, 2)}");
        // 2진수 1010을 10진수로 변경하면 10
        Console.WriteLine($"1010 : {Convert.ToInt32("1010", 2)}");
```
- DateTime 형식 : Year, Month, Day, Hour, Minute, Second
## delegate
## Anonymous Function
### Anonymous Method
### 람다식 : Lambda Expression, 무명 함수
- 분명하고 간결한 방법으로 함수를 묘사하기 위해 고안
- 함수의 정의와 변수, 그리고 함수의 적용으로 구성
- 매개 변수 목록 => 식;
- () => {}
- => : 입력 연산자, Goes to 연산자, Arrow 연산자, Gives me
#### 람다식 작성법
- 매개 타입은 런타임 시에 대입값에 따라 자동으로 인식하기 때문에 생략 가능 : (v) -> {}
- 하나의 매개변수만 있을 경우에는 괄호() 생략 가능 : v -> {}
- 하나의 실행문만 있다면 중괄호 생략 가능 : v -> Console.Write();
- 매개변수가 없다면 괄호 생략 불가 : () -> {}
- 리턴값이 있는 경우, return 문을 사용 : (x, y) -> { return x + y; }
- 중괄호에 return 문만 있을 경우, 중괄호와 return 생략 가능 : (x, y) -> x + y;
#### Lambda 식 형식
##### 식 형식
```C#
    delegate int Calculate(int a, int b);
    Calculate calc = (a, b) => a + b;
    calc(3, 4);
```
##### 문 형식
```C#
    delegate void DoSomething();
    DoSomething DoIt = () =>
    {
        Console.WriteLine("이렇게");
        Console.WriteLine("사용");
    };
    DoIt();
```
#### 무명(익명) 메서드를 람다식으로
```C#
    delegate int Calculate(int a, int b);
    Calculate calc = (int a, int b) => a + b;
    Calculate calc = (a, b) => a + b;
    calc(3, 4);
```
```C#
    button.Click += delegate (object s, EventArgs ea)
    {
        MessageBox.Show("Done");
    };
    // 상동
    button.Click += (object s, EventArgs ea) => {
        MessageBox.Show("Done");
    };
    // 상동
    button.Click += (s, ea) => MessageBox.Show("Done");
```
#### delegate 변수에 Lambda 식 할당
```C#
    delegate void Lambda();
    Lambda hi = () => Console.WriteLine("Hi~");
    Lambda hi = _ => Console.WriteLine("Hi~");
    hi();

    delegate int FN(int a);
    FN sqr = (x) => {return x * x; };
    FN sqr = (x) => x * x;
    FN sqr = x => x * x;
    int result = sqr(4);
    // 상동
    Func<int, int> sqr = (x) => x * x;
```
- in Jave : 익명 구현 객체 생성
    - Target Type : interface 변수 = 람다식;
        - 람다식이 대입되는 인터페이스
        - 익명 구현 객체를 만들 때 사용할 인터페이스
    - Functional Interface : 함수적 인터페이스
        - 하나의 추상 메서드만 선언된 인터페이스
        - 람다식은 하나의 메서드를 정의하기 때문에 하나의 추상 메서드만 선언된 인터페이스만 타겟 타입이 될 수 있음
        - @FunctionalInterface 어노테이션
            - 하나의 추상 메서드만을 가지는지 컴파일러가 체크하도록 함
            - 두 개 이상의 추상 메서드가 선언되어 있으면 컴파일 오류 발생
    - 람다식 실행 블록에는 클래스의 멤버인 필드와 메소드를 제약없이 사용할 수 있다
    - 람다식에서 사용하는 외부 로컬 변수는 final 특성을 갖는다
    - 람다식 실행 블록 내에서 this는 람다식을 실행한 객체의 참조이다
- java.util.function 패키지의 표준 API Functional Interface
    - 매개타입으로 사용되어 람다식을 매개값으로 대입할 수 있도록 해준다
    - Consumer
    - Supplier
    - Function
    - Operator
    - Predicate
- Method References
    - 정적 메서드 참조
        - Math::max
        - IntBinaryOperator operator = Math::math
    - 인스턴스 메서드 참조
        - 참조변수::메서드
            - instance::method
```Java
        @FunctionalInterface
        public interface MyFunctionalInterface {
            public void method();
        }
        MyFunctionalInterface lambda = () -> {};
        // 상동
        MyFunctionalInterface lambda = new MyFunctionalInterface() {
            public void method() {};
        };
        lambda.method();
```
#### Lambda 식을 메서드 파라미터로 전달
```C#
    private List<string> data = new List<string> {"Alexa", "Jane"};
    private void GetData(Func<string, bool> filterCondition)
    {
        foreach (var in data)
        {
            if (filterCondition(item))
            {
                Debug.WriteLine(item);
            }
        }
    }
    // 호출
    GetData(p => p.StartsWith("A"));
    GetData(p => p.Contains("an"));
    GetData(p => p.Substring(0, 1) == "P" && p.Substring(2, 1) == "n");
```
#### Action<T>, Func<T, U> 사용
```C#
    Action act = () => Console.WriteLine("Hi");
    int result = 0;
    Action<int> act2 = (x) => result = x * x;
    Action<double, double> act3 = (x, y) =>
    {
        double pi = x / y;
        Console.WriteLine($"Result: {x} / {y} = {pi}");
    }
    act3(22.0, 7.0);

    Func<int> func1 = () => 10;
    Func<int, int> func2 = (x) => x * 2;
    Func<int, int, int> func3 = (x, y) => x + y;
```
#### Linq 에서 사용
```C#
    var list = data.Where(name => name.StartWith("A"));
```
#### 식 트리
- 식을 트리로 표현한 자료 구조
- 부모 노드(연산자)가 단 두 개의 자식 노드(피연산자)만 갖는 이진 트리
- 트리의 잎 노드부터 계산해서 루트까지 올라가면 전체 식의 결과
- C#은 코드에서 직접 식 트리를 조립 및 컴파일 해서 사용할 수 있는 기능 제공
- 프로그램 실행 중에 동적으로 무명 함수를 만들어 사용
- System.Linq.Expressions.Expression 클래스
    - 식 트리를 구성하는 노드 표현
    - 파생 클래스들의 객체를 생성하는 역할(팩토리 메서드)
- Expression<TDelegate> 클래스를 이용한 람다식으로 컴파일
```C#
    using System.Linq.Expressions
    Expression const1 = Expression.Constant(1);
    Expression param1 = Expression.Parameter(typeof(int), "x");
    Expression exp = Expression.Add(const1, param1);    // 1 + x
    Expression<Func<int, int>> lambda1 =
        Expression<Func<int, int>>.Lambda<Func<int, int>>(
            exp,
            new ParameterExpression[] {(ParameterExpression)param1 } );
    Func<int, int> compiledExp = lambda1.Compile(); // 실행가능 코드로 컴파일
    Console.WriteLine(compiledExp(3));  // 4 출력
```
#### Expression-Bodied Member : 식 본문 멤버
- 멤버의 본문을 식 만으로 구현
- 멤버 => 식;
```C#
    private List<string> list = new List<string>();
    public FriendList() => Console.WriteLine("생성자");
    public int Capacity // 속성
    {
        get => list.Capacity;
        set => list.Capacity = value;
    }
    public string this[int index] => list[index];   // readonly 인덱서
    public void Add(string name) => list.Add(name);
    public void Remove(string name) => list.Remove(name);
```
## Thread : CPU 가상화
### 용어
- 파일, 프로그램
- 프로세스
    - 실행 파일이 실행되어 메모리에 적재된 인스턴스
    - 운영체제로부터 할당받은 메모리에 코드와 데이터를 저장 및 CPU를 할당받아 실행 가능한 상태 (메모리 개념)
    - 하나 이상의 스레드로 구성
- 스레드
    - 운영체제가 CPU 시간을 할당하는 기본 단위
    - 프로세스를 할당받고 코드를 실행 (실행 개념)
    - Thread : 함수 실행용 운영체제 자원 in 멀티 태스킹 운영체제
    - 프로그램 실행(Main) : Main Thread (Single Thread)
    - 코드에 의해 실행(Thread) : Main과 별개 독립 실행 (Multi Thread)
- 동기 코드 : 메서드 호출 후, 실행이 종료(반환) 되어야 다음 메서드 호출
- 비동기 코드: 메서드 호출 후, 종료를 기다리지 않고 다음 코드 실행
### 멀티 스레드
- 동시성(Concurrency): 멀티 작업을 위해 하나의 코어에서 멀티 스레드가 번갈아 가며 실행하는 성질, 한 번에 하나의 작업
    - Time Slice 방식
    - Context Switching
    - Thread Scheduling : 스레드를 어떤 순서로 실행할 것인가를 결정
        - CPU Scheduler Scheduling
        - Thread Priority(우선 순위 방식)
        - Round Robin(순환 할당 방식)
- 병렬성(Parallelism): 멀티 작업을 위해 멀티 코어에서 개별 스레드를 동시에 실행하는 성질
- 멀티 스레드 장점
    - 사용자 대화형 프로그램에서 응답성을 높임
        - Freeze, Lock, Hang, Lag
    - 성능 개선 : 멀티 CPU에 한해서 성능 개선
    - 경제성 : 메모리와 자원을 할당하는 비용 절감
        - 스레드는 프로세스 보다는 가볍지만 매우 무거운 리소스이다. (1M 이상 공간 비용)
    - 멀티 프로세스 방식에 비해 멀티 스레딩 방식이 자원 공유가 쉽다
- 멀티 스레드 단점
    - 구현하기 까다롭고 테스트가 쉽지 않다.
    - 과다한 사용은 성능 저하 야기 : Context Switching 시간 비용
        - DLL Thread attach/detach notification
    - 자식 스레드의 문제가 생기면, 전체 프로세스에 영향을 끼침
### Thread 생성, 사용
#### Thread 함수 구현
```C#
    public void ThreadFunction() {}
    public void ParameterizedThreadFunction(object obj) {}
```
#### delegate 생성과 Thread 함수 설정
```C#
    using System.Threading;
    public delegate void ThreadStart();
    public delegate void ParameterizedThreadStart(object obj);
    ThreadStart thereadStart = new ThreadStart(ThreadFunction);
    ThreadStart parameterizedThereadStart = new ParameterizedThreadStart(ParameterizedThreadFunction);
```
#### Thread 생성
```C#
    Thread thread = new Thread(new ThreadStart(ThreadFunction));
    Thread thread = new Thread(threadStart);
    Thread thread = new Thread(ThreadFunction);

    Thread thread2 = new Thread(new ParameterizedThreadStart(ParameterizedThreadFunction));
```
#### Thread 실행
```C#
    thread.Start();
    thread2.Start(parameter);
```
### Thread 속성, 메서드
#### Thread 속성
- Name
- IsAlive
- IsBackground
    - Foreground : 주 쓰레드와 독립적으로 동작.
    - Background : Main Thread 와 생사(종료)를 같이 한다.
- public static Thread CurrentThread { get; }
    - Thread.CurrentThread.GetHashCode();
    - Thread.CurrentThread.Abort();
    - Thread.CurrentThread.ManagedThreadId;
- ThreadState 열거형
#### Thread 메서드
- Start()
- Join() : 계산 작업을 하는 스레드가 모든 계산 작업을 마쳤을때, 계산 결과값을 받아 이용하는 경우에 주로 사용, 나는 일시 정지됨.
- Thread.Interrupt() : Abort() 대신 추천
    - WaitSleepJoin 상태에서 ThreadInterruptedException 예외 던짐
    - Thread.SpinWait()
- Abort()
    - CLR에 의해 ThreadAbortException 예외 발생
        - 동작하던 스레드가 즉시 종료된다는 보장 안됨
        - 자원을 독점한 스레드가 해제 못한 상태로 종료되는 문제점
    - Thread.ResetAbort()
    - Suspend(), Resume() 제거됨.
- Thread.Sleep() : 다른 스레드도 CPU를 사용할 수 있도록 CPU 점유를 푼다.
### Thread Stack
- User 모드 Thread Stack
- Kernel 모드 Thread Stack
- Call Stack
- Local 변수
#### Thread Debugging
- Show Threads in Source in Visual Studio 2019
```C#
    using System.Diagnostics;
    Debugger.Break();
```
### Thread 동기화 : 공유 자원 사용 문제
- Field, Shared Resource
- Thread Safe
- 작업들 사이의 수행 시기를 맞추는 것
- 다수의 스레드가 동시에 공유 자원을 사용할 때, 순서를 정하는 것
- 자원을 한 번에 하나의 스레드가 사용하도록 보장
#### lock
    - Critical Section
    - 코드 영역을 한 번에 한 스레드만 사용하도록 보장
    - 외부 코드에서도 접근할 수 있는 객체를 lock의 매개변수로 사용 금지
```C#
    private object obj = new object();
    lock (obj) { // 임계(경계) 영역 }

    System.Threading.Monitor.Enter(obj)
    try {
    } finally {
        System.Threading.Monitor.Exit(obj);
    }
```
```Java
    public synchronized void method() { // 임계 영역}   // 동기화 메서드
    // 혹은
    public void method() {
        //synchronized(공유 객체) {   // 동기화 블록
        synchronized(this) {   // 동기화 블록
            // 임계 영역
        }
    }
```
#### Monitor : public static class Monitor
##### Monitor는 lock 보다 저수준 동기화 가능
- 반드시 lock 블록 안에서 호출
- Monitor.Wait() : 스레드를 WaitSleepJoin 상태로 만들고 Waiting Queue에 입력
- Monitor.Pulse() : Waiting Queue의 첫 요소 스레드를 꺼내 Ready Queue에 입력
```C#
    private object obj = new object();
    public static Monitor.Enter(obj)
    // Thread 구현
    public static Monitor.Exit(obj);
```
#### Mutex : public sealed calss Mutex : WaitHandle
```C#
    static Mutex mutex = new Mutex();
    mutex.WaitOne();    // public virtual bool WaitOne()   // 진입
    mutex.ReleaseMutex();   // public void ReleaseMutex()  // 해제
```
#### Interlocked
#### AutoResetEvent, ManualResetEvent
#### ReaderWriterLock
### NetWork 구현: Server, Client
- Server
    - TcpListener
        - AcceptTcpClient()
    - TcpClient
        - new Thread(ServerProcess)
- Client
### Thread가 사라진 이유
- 상당히 무거운 객체
- Context Switching
- 작업 완료 시점을 알 수 없다.
- 반환값을 못 받는다.
- 취소, 예외 처리가 어렵다.
## Task
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
    Action SomeAction = _ => {}
    Func<int> SomeFunc = _ => {return result;}
```
#### Task 생성
```C#
    Task task = new Task(SomeAction);
    Task task1 = Task.Run(SomeAction);
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
#### Task 사용
```C#
    task.Start();
    task.Wait();

    task1.Wait();

    task2.Start();
    task2.Wait();
    int i2 = task2.Result;
    int i5 = task5.Result;
```
- task.RunSynchronously()
### Task 결과값
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
## async, await
### async
- 메서드 이벤트 처리기, 태스크, 람다식 등 수식하기만 하면 비동기 코드 생성
### 반환 형식
- Task, Task<TResult> : 작업이 완료될 때까지 기다리는 메서드
- void : 실행하고 잊어버릴 (Shoot and Forget) 작업을 담고 있는 메서드
### Await
- async void : await 연산자가 없어도 비동기로 실행
- async Task, async Task<TResult>
    - await 기술된 곳에서 호출자에게 제어 반환
    - await가 없는 경우 동기로 실행
### async, await 사용
```C#
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        string url = "https://www.naver.com";
        WebClient client = new WebClient();
        string t = await client.DownloadStringTaskAsync(new Uri(url));
        // await client.DownloadStringTaskAsync(new Uri(url)).ConfigureWait(false);
        Debug.WriteLine(t);
    }
```
### 비동기 API
- System.IO.Stream
    - FileStream.ReadAsync(), FileStream.WriteAsync()
## Parallel
- 병렬 처리를 손쉽게 구현
```C#
    void SomeMethod(int i) {}
    Parallel.For(0, 100, SomeMethod)
```

/* The End */
######
- Token, 구둣점, 구분자
- 논리 데이터 형식 : bool
    - 논리값 리터럴: true, false
    - bool flag = true;
        - bool isBig = 5 > 3;
- int? x = null; // 널 가능 형식
- null: null 키워드는 개체를 참조하지 않는 null 참조를 나타내는 리터럴입니다.
        null은 참조 형식 변수의 기본값(default) 입니다.
        nullable 값 형식을 제외한 일반 값 형식은 null일 수 없습니다.
- var numbers = new int[] { 1, 2, 3 };
- Name Space, Class, Method
- System NameSpace 사용: System.Out 
- object 형 : Object 형
- 3.2 C#의 기본 코드 구조
- Flags 특성 : 열거형을 비트 필드로 사용
######


09 연산자 개요 및 단항·산술 연산자 사용하기

__9.1 연산자

__9.2 단항 연산자

__9.3 변환 연산자: ( ) 기호로 데이터 형식 변환하기

__9.4 산술 연산자

__9.5 문자열 연결 연산자

10 할당 연산자와 증감 연산자 사용하기

__10.1 할당 연산자

__10.2 증감 연산자

11 관계형 연산자와 논리 연산자 사용하기

__11.1 관계형 연산자

__11.2 논리 연산자

12 비트 연산자와 시프트 연산자로 비트 단위 연산하기

__12.1 비트 연산자

__12.2 시프트 연산자

__12.3 기타 연산자

__12.4 연산자 우선순위

13 제어문 소개 및 if/else 문

__13.1 제어문

__13.2 순차문: 순서대로 실행하기

__13.3 조건문: if 문과 가지치기

__13.4 else 문

__13.5 else if 문(다중 if 문, 조건식 여러 개 처리)

__13.6 조건문(if 문)을 사용한 조건 처리 전체 정리

14 조건문: switch 문으로 다양한 조건 처리하기

__14.1 switch 문 소개

__14.2 switch 문 사용하기

15 반복문(for 문)을 사용하여 구간 반복하기

__15.1 for 문으로 반복하기

__15.2 무한 루프

__15.3 for 문으로 1부터 4까지 팩토리얼 값을 출력하는 프로그램

__15.4 구구단을 가로로 출력하기

16 while 문과 do 문, foreach 문으로 반복 처리하기

__16.1 while 문

__16.2 피보나치 수열을 while 문으로 표현하기

__16.3 do while 반복문으로 최소 한 번은 실행하기

__16.4 foreach 문으로 배열 반복하기

17 break, continue, goto로 반복문 제어하기

__17.1 break 문

__17.2 continue 문으로 코드 건너뛰기

__17.3 goto로 프로그램 흐름을 원하는 대로 바꾸기

18 배열 사용하기

__18.1 컬렉션

__18.2 배열

__18.3 배열 선언하기

__18.4 1차원 배열

__18.5 다차원 배열

__18.6 가변 배열

__18.7 var 키워드로 배열 선언하기

19 함수 사용하기

__19.1 함수

__19.2 함수 정의하고 사용하기

__19.3 매개변수와 반환값

__19.4 매개변수가 있는 함수

__19.5 반환값이 있는 함수

__19.6 함수를 사용하여 큰 값과 작은 값, 절댓값 구하기

__19.7 XML 문서 주석을 사용하여 함수 설명 작성하기

__19.8 기본 매개변수

__19.9 명명된 매개변수

__19.10 함수 오버로드: 다중 정의

__19.11 재귀 함수

__19.12 함수 범위: 전역 변수와 지역 변수

__19.13 화살표 함수: =>

__19.14 식 본문 메서드

__19.15 로컬 함수

__19.16 Main 메서드의 명령줄 인수

20 C# 인터렉티브로 출력문부터 함수까지 내용 복습하기

__20.1 C# 인터렉티브

 

3부 C# 활용

21 닷넷 API

__21.1 닷넷 API 탐색기와 Docs

__21.2 클래스, 구조체, 열거형, 네임스페이스

__21.3 Math 클래스 사용하기

__21.4 클래스 또는 메서드 이름을 문자열로 가져오기: nameof 연산자

22 구조체 사용하기

__22.1 구조체란?

__22.2 구조체 만들기

__22.3 구조체 선언 및 사용하기

__22.4 구조체 배열

__22.5 구조체 매개변수: 함수의 매개변수에 구조체 사용하기

__22.6 내장형 구조체

23 열거형 형식 사용하기

__23.1 열거형 형식 사용하기

__23.2 열거형 만들기

__23.3 열거형 항목에 상수 값 주기

__23.4 열거형 관련 클래스 사용하기

24 클래스 사용하기

__24.1 클래스 소개하기

__24.2 클래스 만들기

__24.3 클래스 여러 개 만들기

__24.4 클래스 시그니처

__24.5 자주 사용하는 내장 클래스

__24.6 Environment 클래스로 프로그램 강제 종료하기

__24.7 환경 변수 사용하기

__24.8 EXE 파일 실행하기

__24.9 Random 클래스

__24.10 프로그램 실행 시간 구하기

__24.11 정규식

__24.12 닷넷에 있는 엄청난 양의 API

__24.13 값 형식과 참조 형식

__24.14 박싱과 언박싱

__24.15 is 연산자로 형식 비교하기

__24.16 as 연산자로 형식 변환하기

__24.17 패턴 매칭: if 문과 is 연산자 사용하기

25 문자열 다루기

__25.1 문자열 다루기

__25.2 문자열 처리 관련 주요 API 살펴보기

__25.3 StringBuilder 클래스를 사용하여 문자열 연결하기

__25.4 String과 StringBuilder 클래스의 성능 차이 비교하기

26 예외 처리하기

__26.1 예외와 예외 처리

__26.2 try~catch~finally 구문

__26.3 Exception 클래스로 예외 처리하기

__26.4 예외 처리 연습하기

__26.5 throw 구문으로 직접 예외 발생시키기

27 컬렉션 사용하기

__27.1 배열과 컬렉션

__27.2 리스트 출력 구문

__27.3 Array 클래스

__27.4 컬렉션 클래스

__27.5 Stack 클래스

__27.6 Queue 클래스

__27.7 ArrayList 클래스

__27.8 Hashtable 클래스

28 제네릭 사용하기

__28.1 Cup of T

__28.2 Stack 제네릭 클래스 사용하기

__28.3 List<T> 제네릭 클래스 사용하기

__28.4 Enumerable 클래스로 컬렉션 만들기

__28.5 Dictionary<T, T> 제네릭 클래스 사용하기

29 널(null) 다루기

__29.1 null 값

__29.2 null 가능 형식: Nullable<T> 형식

__29.3 null 값을 다루는 연산자 소개하기

30 LINQ

__30.1 LINQ 개요

__30.2 확장 메서드 사용하기

__30.3 화살표 연산자와 람다 식으로 조건 처리

__30.4 데이터 정렬과 검색

__30.5 메서드 구문과 쿼리 구문

__30.6 Select( ) 확장 메서드를 사용하여 새로운 형태로 가공하기

__30.7 ForEach( ) 메서드로 반복 출력하기

31 알고리즘과 절차 지향 프로그래밍

__31.1 알고리즘

__31.2 합계 구하기: SUM 알고리즘

__31.3 개수 구하기: COUNT 알고리즘

__31.4 평균 구하기: AVERAGE 알고리즘

__31.5 최댓값 구하기: MAX 알고리즘

__31.6 최솟값 구하기: MIN 알고리즘

__31.7 근삿값 구하기: NEAR 알고리즘

__31.8 순위 구하기: RANK 알고리즘

__31.9 순서대로 나열하기: SORT 알고리즘

__31.10 특정 값 검색하기: SEARCH 알고리즘

__31.11 배열을 하나로 합치기: MERGE 알고리즘

__31.12 최빈값 구하기: MODE 알고리즘

__31.13 그룹화하기: GROUP 알고리즘

32 개체 만들기

__32.1 클래스와 개체

__32.2 개체와 인스턴스

__32.3 인스턴스 메서드

__32.4 익명 형식

__32.5 정적 멤버와 인스턴스 멤버

__32.6 프로젝트에 클래스를 여러 개 사용하기

__32.7 ToString( ) 메서드 오버라이드

__32.8 클래스 배열

__32.9 var 키워드를 사용하여 클래스의 인스턴스 생성하기

33 네임스페이스

__33.1 네임스페이스

__33.2 네임스페이스 만들기

__33.3 using 지시문

34 필드 만들기

__34.1 필드

__34.2 액세스 한정자

__34.3 여러 가지 형태의 필드 선언, 초기화, 참조 구현하기

35 생성자

__35.1 생성자

__35.2 매개변수가 있는 생성자 만들기

__35.3 클래스에 생성자 여러 개 만들기

__35.4 정적 생성자와 인스턴스 생성자

__35.5 this( ) 생성자로 다른 생성자 호출하기

__35.6 생성자를 사용하여 읽기 전용 필드 초기화

__35.7 식 본문 생성자

36 소멸자

__36.1 종료자

__36.2 가비지 수집기

__36.3 생성자, 메서드, 소멸자 실행 시점 살펴보기

__36.4 소멸자를 사용한 클래스 역할 마무리하기

__36.5 생성자, 메서드, 소멸자 함께 사용하기

37 메서드와 매개변수

__37.1 메서드

__37.2 메서드의 매개변수 전달 방식

__37.3 가변 길이 매개변수

__37.4 메서드 본문을 줄여 표현하기

__37.5 선택적 매개변수

38 속성 사용하기

__38.1 속성

__38.2 접근자와 전체 속성

__38.3 자동으로 구현된 속성

__38.4 자동 속성 이니셜라이저

__38.5 읽기 전용 속성과 쓰기 전용 속성

__38.6 속성의 여러 가지 유형 살펴보기

__38.7 속성을 사용한 클래스의 멤버 설정 및 참조하기

__38.8 화살표 연산자로 속성과 메서드를 줄여서 표현하기

__38.9 개체 이니셜라이저

__38.10 자동 속성을 사용하여 레코드 클래스 구현하기

__38.11 nameof 연산자

__38.12 익명 형식

__38.13 익명 형식과 덕 타이핑

__38.14 생성자로 속성에 대한 유효성 검사 구현하기

__38.15 메서드로 속성 값 초기화하기

__38.16 속성에서 ?.와 ?? 연산자를 함께 사용하기

39 인덱서와 반복기

__39.1 인덱서

__39.2 인덱서를 사용하여 배열 형식의 개체 만들기

__39.3 문자열 매개변수를 받는 인덱서 사용하기

__39.4 반복기와 yield 키워드

40 대리자

__40.1 대리자(위임/델리게이트)

__40.2 대리자를 사용하여 메서드 대신 호출하기

__40.3 대리자를 사용하여 메서드 여러 개를 다중 호출하기

__40.4 무명 메서드

__40.5 메서드의 매개변수에 대리자 형식 사용하기

__40.6 Action, Func, Predicate 대리자

__40.7 메서드의 매개변수로 메서드 전달하기

41 이벤트

__41.1 이벤트

__41.2 이벤트와 대리자를 사용하여 메서드 등록 및 호출하기

42 클래스 기타

__42.1 부분 클래스

__42.2 정적 클래스

__42.3 필드에 public을 붙여 외부 클래스에 공개하기

__42.4 함수형 프로그래밍 스타일: 메서드 체이닝

__42.5 불변 형식

43 상속으로 클래스 확장하기

__43.1 클래스 상속하기

__43.2 부모 클래스와 자식 클래스

__43.3 Base 클래스와 Sub 클래스

__43.4 Object 클래스 상속

__43.5 부모 클래스 형식 변수에 자식 클래스의 개체 할당하기

__43.6 상속은 영어로 is a(is an) 관계를 표현

__43.7 this와 this( ) 그리고 base와 base( )

__43.8 봉인 클래스

__43.9 추상 클래스

__43.10 자식 클래스에만 멤버 상속하기

__43.11 기본 클래스의 멤버 숨기기

44 메서드 오버라이드

__44.1 메서드 오버라이드: 재정의

__44.2 상속 관계에서 메서드 오버라이드

__44.3 메서드 오버로드와 오버라이드

__44.4 메서드 오버라이드 봉인

__44.5 ToString( ) 메서드 오버라이드

__44.6 메서드 오버라이드로 메서드 재사용하기

45 인터페이스

__45.1 인터페이스

__45.2 인터페이스 형식 개체에 인스턴스 담기

__45.3 생성자의 매개변수에 인터페이스 사용하기

__45.4 인터페이스를 사용한 다중 상속 구현하기

__45.5 명시적인 인터페이스 구현하기

__45.6 인터페이스와 추상 클래스 비교하기

__45.7 IEnumerator 인터페이스 사용하기

__45.8 IDisposable 인터페이스 사용하기

__45.9 인터페이스를 사용하여 멤버 이름 강제로 적용하기

46 특성과 리플렉션

__46.1 특성

__46.2 Obsolete 특성 사용하기

__46.3 특성의 매개변수

__46.4 [Conditional] 특성 사용하기

__46.5 특성을 사용하여 메서드 호출 정보 얻기

__46.6 사용자 지정 특성 만들기

__46.7 리플렉션

__46.8 Type과 Assembly 클래스

__46.9 특정 클래스의 메서드와 속성을 동적으로 호출하기

__46.10 Type 클래스로 클래스의 멤버 호출하기

__46.11 특정 속성에 적용된 특성 읽어 오기

__46.12 Type과 Activator 클래스로 개체의 인스턴스를 동적 생성하기

47 개체와 개체 지향 프로그래밍

__47.1 개체 지향 프로그래밍 소개하기

__47.2 현실 세계의 자동차 설계도 및 자동차 개체 흉내 내기

__47.3 개체 지향 프로그래밍의 네 가지 큰 개념

__47.4 캡슐화를 사용하여 좀 더 세련된 프로그램 만들기

__47.5 다형성 기법을 사용하여 프로그램 융통성 높이기

__47.6 클래스의 멤버 종합 연습: 자동차 클래스 구현하기

 

4부 C# 확장 기능

48 제네릭 클래스 만들기

__48.1 사용자 정의 클래스를 매개변수로 사용하는 제네릭 클래스

__48.2 사전 제네릭 클래스 소개

__48.3 제네릭 인터페이스

__48.4 제네릭 클래스 만들기

49 확장 메서드 만들기

__49.1 확장 메서드

__49.2 확장 메서드로 문자열 기능 확장하기

__49.3 확장 메서드로 기존 형식에 새로운 메서드 추가하기

__49.4 확장 메서드를 사용하여 형식에 메서드 추가하기

50 동적 형식

__50.1 dynamic 키워드

__50.2 dynamic 변수로 런타임할 때 데이터 형식 결정하기

__50.3 동적 바인딩

__50.4 확장 메서드와 dynamic

51 튜플

__51.1 튜플

52 클래스 라이브러리와 닷넷 스탠다드

__52.1 클래스 라이브러리 프로젝트

__52.2 어셈블리

__52.3 닷넷 스탠다드 프로젝트로 자신만의 라이브러리 만들기

53 테스트 프로젝트와 단위 테스트

__53.1 자동 테스트

__53.2 테스트 프로젝트 생성 및 Assert 클래스 사용하기

__53.3 Dul 프로젝트를 테스트하는 테스트 코드 작성 및 실행하기

54 NuGet 패키지

__54.1 패키지 관리자와 NuGet

__54.2 자신만의 NuGet 패키지 만들기

__54.3 내가 만든 NuGet 패키지 사용하기

55 스레드

__55.1 스레드

__55.2 스레드 생성 및 호출하기

__55.3 다중 스레드를 사용한 메서드 함께 호출하기

__55.4 스레드 동기화

__55.5 병렬 프로그래밍

56 비동기 프로그래밍

__56.1 동기 프로그래밍

__56.2 비동기 프로그래밍

__56.3 비동기 Main( ) 메서드

__56.4 간단한 async와 await 키워드 사용 예제

__56.5 Task.Run( ) 메서드로 비동기 메서드 호출하기

__56.6 Task.FromResult( )를 사용하여 비동기로 반환값 전달하기

__56.7 async와 await를 사용한 C# 비동기 프로그래밍하기

57 인메모리 데이터베이스 프로그래밍 맛보기

__57.1 인메모리 데이터베이스

__57.2 CRUD 작업하기

__57.3 리포지토리 패턴

__57.4 인메모리 데이터베이스를 만들고 CRUD 작업 수행하기

58 스트림과 파일 입출력 맛보기

__58.1 System.IO 네임스페이스

__58.2 문자열에서 파일 이름 추출하기

__58.3 Path 클래스로 파일 이름 및 확장자, 폴더 정보 얻기

__58.4 파일과 디렉터리 관련 클래스

__58.5 텍스트 데이터를 컬렉션 데이터로 가져오기

59 XML과 JSON 맛보기

__59.1 XElement 클래스를 사용하여 XML 요소를 생성하거나 가공하기

__59.2 JSON 데이터 직렬화 및 역직렬화하기

60 네트워크 프로그래밍 맛보기

__60.1 HttpClient 클래스로 웹 데이터 가져오기

61 함수와 함수형 프로그래밍 소개하기

__61.1 함수형 프로그래밍

__61.2 문과 식

__61.3 고차 함수

__61.4 LINQ로 함수형 프로그래밍 스타일 구현하기

62 모던 C#

__62.1 C#의 새로운 기능

__62.2 패턴 매칭하기

__62.3 C# 8.0 버전의 기능을 테스트 프로젝트에서 실행하기

__62.4 C# 8.0 버전의 새로운 기능 열 가지 소개하기

 

부록 A 디버거 사용하기

부록 B 팁과 트릭