# C# 문법
## Thread : CPU 가상화
### 용어
- 파일, 프로그램
- Process
    - 현재 실행 중인 프로그램
    - 실행 파일이 실행되어 메모리에 적재된 인스턴스
    - 운영체제로부터 할당받은 메모리에 코드와 데이터를 저장 및 CPU를 할당받아 실행 가능한 상태 (메모리 개념)
    - 하나 이상의 스레드로 구성
    - IPC : Interprocess Communication
- Thread
    - 작업자 한 명
    - 운영체제가 CPU 시간을 할당하는 기본 단위
    - 프로세스를 할당받고 코드를 실행 (실행 개념)
    - 프로세스안에서 실행하는 단위 프로그램(메서드)
    - Thread : 함수 실행용 운영체제 자원 in 멀티 태스킹 운영체제
    - 프로그램 실행(Main) : Main Thread (Single Thread)
    - 코드에 의해 실행(Thread) : Main과 별개 독립 실행 (Multi Thread)
### 멀티 스레드
- 하나의 작업을 여러 작업자가 나눠서 수행한 뒤, 다시 하나의 결과로 만드는 것.
- 다중 스레딩: 동시에 여러 작업을 수행하여 앱의 응답성을 높이고, 다중 코어에서 처리량 향상. 여러 작업자를 두고 동시에 작업을 처리하는 것.
- Core 수
    - 동시성(Concurrency): 멀티 작업을 위해 하나의 코어에서 멀티 스레드가 번갈아 가며 실행하는 성질, 한 번에 하나의 작업
        - Time Slice 방식
        - Context Switching
        - Thread Scheduling : 스레드를 어떤 순서로 실행할 것인가를 결정
            - CPU Scheduler Scheduling
            - Thread Priority(우선 순위 방식)
            - Round Robin(순환 할당 방식)
    - 병렬 처리(Parallel Processing): 멀티 작업을 위해 멀티 코어에서 개별 스레드를 동시에 실행하는 것 (TPL)
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
    //public delegate void ThreadStart();
    //public delegate void ParameterizedThreadStart(object obj);
    ThreadStart thereadStart = new ThreadStart(ThreadFunction);
    ThreadStart parameterizedThereadStart = new ParameterizedThreadStart(ParameterizedThreadFunction);
```
#### Thread 생성
```C#
    Thread thread = new Thread(delegate() {});
    Thread thread = new Thread(() => {});
    Thread thread = new Thread(ThreadFunction);
    Thread thread = new Thread(new ThreadStart(ThreadFunction));
    Thread thread = new Thread(threadStart);

    Thread thread2 = new Thread(new ParameterizedThreadStart(ParameterizedThreadFunction));
    new Thread(state => {}).Start(obj);
```
#### Thread 실행
```C#
    thread.Start(); // 새로운 스레드를 생성하고 실행할 때까지 시간 소요
    thread2.Start(parameter);
    // Process
    Process.Start("Notepad.exe");  // 메모장 실행
```
#### Thread 종료
```C#
    thread.Join();
    thread.Interrupt();
```
### Thread 속성, 메서드
#### Thread 속성
- IsBackground
    - `thread.IsBackground = true;`
    - Foreground : 주 쓰레드와 독립적으로 동작.
    - Background : Main Thread 와 생사(종료)를 같이 한다.
- public static Thread CurrentThread { get; }
    - `Thread thread = Thread.CurrentThread;`
    - Thread.CurrentThread.ManagedThreadId;
    - Thread.CurrentThread.GetHashCode();
    - Thread.CurrentThread.Abort();
- Priority : 우선 순위
    - `thread.Priority = ThreadPriority.Highest;` // 우선순위 높게 
    - ThreadPriority 열거형
        - Highest
        - Normal
        - Lowest
- Thread State
    - ThreadState 열거형
    `ThreadState threadState = thread.ThreadState;`
    - ThreadState 열거형은 [Flags] 특성을 사용하므로, 여러 상태를 동시에 나타낼 수 있다.
- Name
- IsAlive
#### Thread 메서드
- Start()
- Join() : 실, 계산 작업을 하는 스레드가 모든 계산 작업을 마쳤을때, 계산 결과값을 받아 이용하는 경우에 주로 사용, 현재 스레드는 일시 정지됨.
- Thread.Sleep() : 다른 스레드도 CPU를 사용할 수 있도록 CPU 점유를 푼다. ThreadState.WaitSleepJoin 상태로 실행 중단
    - `Thread.Sleep(1000); // 1초 대기(지연)`
- Thread.Interrupt() : 종료시, 추천
    - WaitSleepJoin 상태에서 ThreadInterruptedException 예외 던짐
- Thread.SpinWait()
    - Sleep()과 유사하게, 스레드를 대기하게 하지만, 스레드가 한 동안 Running 상태를 갖도록 지정
- Abort() : 사용 자제.
    - CLR에 의해 ThreadAbortException 예외 발생
        - 동작하던 스레드가 즉시 종료된다는 보장 안됨
        - 자원을 독점한 스레드가 해제 못한 상태로 종료되는 문제점
    - Thread.ResetAbort()
    - Suspend(), Resume() 제거됨.
##### Thread 실행 시간 측정
```C#
    DateTime start = DateTime.Now;
    int elapsed = (DateTime.Now - start).TotalSeconds;

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    stopwatch.Stop();
    stopwatch.ElapsedMilliseconds;
```
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
### Thread Pool
- 스레드 동작 방식
    - 상시 실행
    - 일회성의 임시 실행
- Pool : 재사용할 수 있는 자원의 집합
- Thread Pool : 필요할 때마다 스레드를 꺼내 쓰고 필요없어지면 다시 풀에 스레드가 반환되는 기능
- 임시적인 목적으로 언제든 원하는 때에 스레드를 사용
- ThreadPool은 프로그램 시작과 함께 0개의 스레드를 가지며 생성
- QueueUserWorkItem을 호출할 때, 필요하면 1개의 스레드를 생성해 실행
- 일정 시간 동안 재사용되지 않는다면 스레드는 풀에서 제거되어 종료
```C#
    // System.Threading.ThreadPool
    void threadFunction(object state) {}
    ThreadPool.QueueUserWorkItem(threadFunction, data);

```
### Thread 동기화 : 공유 자원 사용 문제
- 파일 핸들, 네트워크 커넥션, 메모리에 선언한 변수
- Field, Shared Resource
- 작업들 사이의 수행 시기를 맞추는 것
- 다수의 스레드가 동시에 공유 자원을 사용할 때, 순서를 정하는 것
- 자원을 한 번에 하나의 스레드가 사용하도록 보장
- Thread Safe
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
#### Monitor : public static class Monitor
##### Monitor는 lock 보다 저수준 동기화 가능
- 반드시 lock 블록 안에서 호출
- Monitor.Wait() : 스레드를 WaitSleepJoin 상태로 만들고 Waiting Queue에 입력
- Monitor.Pulse() : Waiting Queue의 첫 요소 스레드를 꺼내 Ready Queue에 입력
```C#
    private object obj = new object();
    lock (lockObject)
    {
        if (countWork > 0 || isLocked)
        {
            Monitor.Wait(lockObject);
        }
        isLocked = true;
        countWork++;
        isLocked = false;
        Monitor.Pulse(lockObject);
    }
```
#### Interlocked
- `Interlocked.Increment(ref number);`  // Atomic Operation
- `Interlocked.Exchange(ref number, 5);`
#### Mutex : public sealed calss Mutex : WaitHandle
```C#
    static Mutex mutex = new Mutex();
    mutex.WaitOne();    // public virtual bool WaitOne()   // 진입
    mutex.ReleaseMutex();   // public void ReleaseMutex()  // 해제
```
#### EventWaitHandle : 스레드 간에 신호를 전달하는 역할
- Event 객체는 두가지 상태를 가진다
    - Non-Signal -> Set() -> Signal
    - Signal -> Reset() -> Non-Signal
- WaitOne() : Event 객체의 Signal을 기다리는 메서드
    - 어떤 스레드가 WaitOne() 메서드를 호출하는 시점에 이벤트 객체가 Signal 상태이면 메서드에서 곧바로 제어가 반환되지만, Non-Signal 상태였다면 이벤트 객체가 Signal 상태로 바뀔 때까지 WaitOne() 메서드는 제어를 반환하지 않는다. 즉, 스레드는 대기 상태에 빠진다
- ThreadPool에 넣은 스레드의 Join() 역할 가능
- AutoReset, ManualReset
    - Signal 상태로 전환된 Event 객체가 Non-Signal 상태로 자동으로 전환되는가 혹은 수동으로 전환되는가의 차이
    - AutoReset : 대기하고 있던 스레드 중 단 1개의 스레드만을 깨운 후 곧바로 Non-Signal 상태로 바뀐다. 대기하고 있던 스레드가 없다면 그에 상관없이 곧바로 Non-Signal 상태로 바뀐다
    - ManualReset : 명시적인 Reset() 메서드를 호출하기 전까지 이벤트의 Signal 상태를 지속시킨다. 관건은 깨어난 스레드가 얼마나 빨리 Reset() 메서드를 호출하느냐이다.
```C#
    // 생성자의 첫 번째 인자가 false이면 Non-Signal 상태로 시작.
    // true이면 Signal 상태로 시작
    EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
    new Thread(obj => (obj as EventWaitHandle).Set()).Start(ewh);
    ewh.WaitOne(); // Non-Signal 상태에서 WaitOne을 호출했으므로 Signal 상태로 바뀔 때까지 대기
```
#### ReaderWriterLock
### NetWork 구현: Server, Client
- Server
    - TcpListener
        - AcceptTcpClient()
    - TcpClient
        - new Thread(ServerProcess)
- Client
### Thread가 사라진 이유
- Process 보다 가볍지만, 의외로 상당히 무거운 객체
- Context Switching
- 작업 완료 시점을 알 수 없다.
- 반환값을 못 받는다.
- 취소, 예외 처리가 어렵다.