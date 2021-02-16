/# Java 문법
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
- 다중 스레딩: 동시에 여러 작업을 수행하여 앱의 응답성을 높이고, 다중 코어에서 처리량 향상
- Core 수
    - 동시성(Concurrency): 멀티 작업을 위해 하나의 코어에서 멀티 스레드가 번갈아 가며 실행하는 성질, 한 번에 하나의 작업
        - Time Slice 방식
        - Context Switching
        - CPU Scheduler Scheduling
        - Thread Scheduling : 스레드를 어떤 순서로 실행할 것인가를 결정
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
```Java
```
#### 함수형 Interface 생성과 Thread 함수 설정
- 함수형 인터페이스 : 추상 메서드가 하나 뿐인 인터페이스
- 람다식을 사용하여 함수형 인터페이스의 인스턴스를 나타낼 수 있다.
- 함수형 인터페이스에는 여러 개의 default 메서드가 있을 수 있다.
- 함수형 인터페이스의 인스턴스를 생성하기 위해 익명 구현 객체를 주로 사용했지만 람다식을 이용하면 편하다.
- @FunctionalInterface 어노테이션 사용
- Runnable, ActionListener, Comparable
- java.util.function 패키지
    - Predicate<T> : 하나의 매개변수를 주는 boolean형을 반환하는 test 메서드
        - Predicate 결합 : negate(), and(), or()
        - isEqual(), test()
    - Consumer<T> : 하나의 매개변수를 주는 void형 accept 메서드
    - Function<T, R> : T유형의 인수를 취하고 R 유형의 결과를 반환하는 추상 메서드 apply
        - f.andThen(g), f.compose(g)
    - Supplier<T> : 메서드 인자는 없고 T 유형의 결과를 반환하는 추사 메서드 get
    - UnaryOperator<T> : 하나의 인자와 리턴 타입을 가진다 T -> T
    - BinaryOperator : 두 개의 인수, 동일한 타입의 결과를 반환하는 추상 메서드 apply (T, T) -> T
```Java
    Runnable r = () -> System.out.println("Runnable 객체");
    Thread t = new Thread(() -> System.out.println("Thread 객체));
```
#### Thread 생성
```Java
    // 1 : Interface Runnable 구현 객체
    class Task implements Runnable {
        public void run() {}
    }
    Runnable task = new Task();
    Thread thread = new Thread(task);
    // 2 : Interface Runnable 익명 구현 객체
    Thread thread = new Thread(new Runnable() {
        public void run() {}
    });
    // 3 : 람다식 Interface Runnable 익명 구현 객체
    Thread thread = new Thread(() -> {});
    // 4 : Thread 상속
    class WorkerThread extends Thread {
        @Override
        public void run() {}
    }
    Thread thread = new WorkThread();
    // 5 :  Thread 익명 구현 객체
    Thread thread = new Thread() {
        public void run() {}
    }
```
#### Thread 실행
```Java
    thread.start();
```
#### Thread 종료
- 실행 중인 스레드를 즉시 종료 : ~~stop()~~
    - 스레드를 즉시 종료시킨다.
    - 갑자기 종료하면, 사용 중이던 자원들이 불안전한 상태로 남겨진다.
- stop flag
    - stop 플래그로 run() 메서드의 정상 종료를 유도한다.
```Jave
    private boolean stop = false;
    public void run() {
        while (!stop) {}
    }
```
- interrupt()
    - 일시 정지 상태일 경우, InterruptException을 발생 시킴
    - 실행 대기 또는 실행 상태에서는 InterruptException이 발생하지 않는다.
```Java
    // 일시 정지 상태로 만들지 않고, while 문을 빠져 나오는 방법
    threadB.interrupt();
    boolean status = Thread.interrupted();
    boolean status = objThread.isInterrupted();
```
### Thread 속성, 메서드
#### Thread 속성
```Java
    // 1(낮음)부터 10(높음)까지
    thread.setPriority(Thread.MAX_PRIORITY);
    thread.setPriority(Thread.NORM_PRIORITY);   // 5
```
- 스레드 상태
    - NEW : 객체 생성, start() 전
    - RUNNABLE : 실행 대기, 실행 상태로 가기 전 상태, start() 됨
    - 실행 상태
    - 일시 정지
        - BLOCKED : 사용할 객체의 락이 풀릴 때까지 기다리는 상태
        - WAITING : 다른 스레드가 통지할 때까지 기다리는 상태
        - TIMED_WAITING : 주어진 시간 동안 기다리는 상태 sleep()
    - TERMINATED : 종료, 실행을 마친 상태
```Java
    Thread.State state = thread.getState();
```
- 스레드 상태 제어
    - NEW -> 실행 대기
    - 실행 -> 실행 대기
        - Thread.yield() : 무의미한 반복을 하지않고, 다른 스레드에게 실행을 양보
    - 실행 -> 일시 정지
        - sleep() : TIMED_WAITING 상태로 진입
        - join() : 다른 스레드의 종료를 기다림. 계산 작업을 하는 스레드가 모든 계산을 작업을 마쳤을 때, 계산 결과값을 받아 이용하는 경우에 주로 사용. join()을 호출한 스레드는 일시 정지 상태가 됨. 실행 동기화?
        - wait() : WAINTING 상태 진입.
        - ~~suspend()~~
    - 일시 정지 -> 실행 대기
        - interrupt()
        - notify()
        - notifyAll()
        - ~~resume()~~
    - 실행 -> 종료
        - ~~stop()~~ : 차라리 종료 Check Flag를 사용하라!
- 스레드간의 협업
    - 두 개의 스레드가 교대로 번갈아 가며 실행해야할 경우에 주로 사용 : 생산자 스레드 <-> 소비자 스레드
    - 동기화 메서드나 블록에서만 호출 가능한 Object의 메서드
    - wait(), notify(), notifyAll()
    - wait() : 호출한 스레드는 일시 정지가 된다.
    - 다른 스레드가 notify() 나 notifyAll()을 호출하면 실행 대기 상태가 된다.
    - wait(long timeout) : notify()가 호출되지 않아도 시간이 지나면 스레드가 자동적으로 실행 대기 상태가 된다.
#### Thread 메서드
```Java
    try {Thread.sleep(1000);} catch (InterruptedException ex) {}
    Thread thread = Thread.currentThread();
    // 메인 스레드 == "main"
    // 작업 스레드 이름 == "Thread-n"
    thread.getName();
    thread.setName("스레드 이름");
    // 데몬 스레드 : daemon
    // 주 스레드의 작업을 돕는 보조적인 역할을 수행하는 스레드
    // 주 스레드가 종료되면 데몬 스레드는  강제적으로 자동 종료
    // 반드시 start() 호출 전에 setDaemon(true) 호출
    thread.setDaemon(true);
    boolean isDaemon = isDaemon();
    // 동기화
    threadB.start();
    try {threadB.join();} catch (InterruptedException ex) {}
    try {
        threadB.notify(); threadB.wait();
    } catch (InterruptedException ex) {}
```
##### Thread 실행 시간 측정
```Java
```
#### ThreadPool
- 병렬 작업 처리가 많아지면, 스레드의 개수가 폭증
- 스레드 생성과 스케쥴링으로 인해 CPU가 바빠지고, 메모리 사용량이 늘어나면, 애플리케이션의 성능이 급격히 저하된다.
- 작업 처리에 사용되는 스레드를 제한된 개수만큼 미리 생성
- 작업 큐에 들어오는 작업들을 하나씩 스레드가 맡아 처리
- 작업 처리가 끝난 스레드는 작업 결과를 애플리케이션으로 전달
- 스레드는 다시 작업큐에서 새로운 작업을 가져와 처리
- 스레드풀 생성
    - java.util.concurrent 패키지
    - Executors의 정적 메서드를 이용해서 ExecutorService 구현 객체 생성
    - 스레드 풀 == ExecutorService 객체
    - 초기 스레드 수
    - 코어 스레드 수 : 최소한 유지 스레드 수
    - 최대 스레드 수
    - newCachedThreadPool : 0, 0, Interger.MAX_VALUE
        - int 값이 가질 수 있는 최대 값만큼 스레드가 추가되나, 운영체제의 메모리 상황에 따라 달라진다.
        - 1개 이상의 스레드가 추가 되었을 경우, 60초 동안 추가된 스레드가 아무 작업을 하지 않으면 추가된 스레드를 종료하고 풀에서 제거한다.
        - `ExecutorService es = Executors.newCachedThreadPool();`
    - newFixedThreadPool(int nThreads) : 0, nThreads, nThreads
        - 코어 스레드 개수와 최대 스레드 개수가 매개값으로 준 nThread이다.
        - 스레드가 작업을 처리하지 않고 놀고 있더라도 스레드 개수가 줄지 않는다.
        - `ExecutorService es = Executors.newFixedThreadPool(Runtime.getRuntime().availableProcessors());`
    - ThreadPoolExecutor을 이용한 직접 생성
        - newCachedThreadPool()과 newFixedThreadPool(int nThreads)가 내부적으로 생성
        - 스레드의 수를 자동으로 관리하고 싶을 경우 직접 생성해서 사용
        - 코어 스레드 개수가 3, 최대 스레드 개수가 100인 스레드풀을 생성
        - 3개를 제외한 나머지 추가된 스레드가 120초 동안 놀고 있을 경우, 해당 스레드를 제거해서 스레드 수를 관리
```Java
    ExecutorService threadPool = new ThreadPoolExecutor(
        3,  // 코어 스레드 개수 : getPoolSize()
        100,    // 최대 스레드 개수
        120L,   // 놀고 있는 시간
        TimeUnit.SECONDS,   // 놀고 있는 시간 단위
        newSynchronousQueue<Runnable>() // 작업큐
    );
- 작업 생성
    - 하나의 작업은 Runnable 또는 Callable 객체로 표현한다.
        - Runnable : 작업 처리 완료 후, 리턴값이 없다.
        - Callable : 작업 처리 완료 후, 리턴값이 있다.
    - 스레드풀에서 작업 처리
        - 작업 큐에서 Runnable 또는 Callable 객체를 가져와서 스레드로 하여금 run()과 call() 메서드를 실행토록 하는 것이다.
```Java
    Runnable task = new Runnable() {
        @Override
        public void run() {}
    }
    Callable<T> task = new Callable<T> {
        @Override
        public T call() throws Exception { return T; }
    }
```
- 작업 처리 요청
    - ExecutorService의 작업 큐에 Runnable 또는 Callable 객체를 넣는 행위를 말한다.
    - 작업 처리 요청을 위한 ExecutorService의 메서드
        - void execute(Runnable command) : Runnable을 작업큐에 저장. 작업 처리 결과를 받지 못함
        - 주로 submit() 사용
            - Future<?> submit(Runnable task)
            - Future<V> submit(Runnable task, V result)
            - Future<V> submit(Callable<V> task)
            - Runnable 또는 Callable을 작업큐에 저장. 리턴된 Future를 통해 작업 처리 결과를 얻음
- 작업 처리 도중 예외가 발생할 경우
    - execute() : 스레드가 종료되고 해당 스레드는 제거된다. 따라서 스레드 풀은 다른 작업 처리를 위해 새로운 스레드를 생성한다.
    - submit() : 스레드가 종료되지 않고 다음 작업을 위해 재사용된다.
- 블로킹 방식의 작업 완료 통보 받기
    - Future
        - 작업 결과가 아니라 지연 완료(pending completion) 객체
        - 작업이 완료될 때까지 기다렸다가 최종 결과를 얻기 위해서 get() 메서드 사용
            - V get() : 작업이 완료될 때까지 블로킹 되었다가 처리 결과 V를 리턴
            - V get(long timeout, TimeUnit unit) : timeout 시간 동안 작업이 완료되면 결과 V를 리턴하지만, 작업이 완료되지 않으면 TimeoutException을 발생시킴
    - Futer의 get()은 UI 스레드에서 호출하면 안된다.
        - UI를 변경하고 이벤트를 처리하는 스레드가 get() 메서드를 호출하면 작업을 완료하기 전까진는 UI를 변경할 수도 없고 이벤트를 처리할 수도 없게 된다.
        - 새로운 스레드를 생성해서 호출
```Java
    new Thread(new Runnable() {
        @Override
        public void run() {
            try {
                future.get();
            } catch (InterruptException e) {
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }).start();
```
        - 스레드풀의 스레드가 호출
```Java
    executorService.submit(new Runnable() {
        @Override
        public void run() {
            try {
                future.get();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    });
```
- 작업 완료를 확인을 위한 다른 메서드
    - boolean cancel(boolean mayInterruptIfRunning) : 작업 처리가 진행 중일 경우 취소 시킴
    - boolean isCancelled() : 작업이 취소되었는지 여부 확인
    - boolean isDone() : 작업 처리가 완료되었는지 여부 확인
- 스레드풀 종료
    - 스레드풀의 스레드는 기본적으로 데몬 스레드가 아니다.
    - main 스레드가 종료되더라도 스레드풀의 스레드는 작업을 처리하기 위해 계속 실행되므로 애플리케이션은 종료되지 않는다.
    - 따라서 스레드풀을 종료해서 모든 스레드를 종료시켜야 한다.
    - void shutdown() : 현재 처리 중인 작업뿐ㅗ만 아니라 작업큐에 대기하고 있는 모든 작업을 처리한 뒤에 스레드풀을 종료
    - List<Runnable> shutdownNow() : 현재 작업 처리 중인 스레드를 interrupt 해서 작업 중지를 시도하고 스레드풀을 종료시킨다. 리턴값은 작업큐에 있는 미처리된 작업의 목록이다.
    - bool awaitTermination(long timeout, TimeUnit unit) : shutdown() 메서드 호출 이후, 모든 작업 처리를 timeout 시간 내에 완료하면 true를 리턴하고, 완료하지 못하면 작업 처리 중인 스레드를 interrupt하고 false를 리턴한다.
#### Thread Group
- 관련된 스레드를 묶어서 관리할 목적으로 이용
- 스레드 그룹은 계층적으로 하위 스레드 그룹을 가질 수 있다
- 자동 생성되는 스레드 그룹
    - system 그룹 : JVM 운영에 필요한 스레드들을 포함
    - system/main 그룹 : 메인 스레드를 포함
- 스레드는 반드시 하나의 스레드 그룹에 포함
    - 기본적으로 자신을 생성한 스레드와 같은 스레드 그룹에 속하게 된다.
    - 명시적으로 스레드 그룹에 포함시키지 않으면 기본적으로 system/main 그룹에 속한다.
```Java
    // Map<Thread, StackTraceElement[]> map = Thread.getAllStackTraces();
    // Set<Thread> threads = map.KeySet();
    ThreadGroup group = Thread.currentThread.getThreadGroup();
    String groupName = group.getName();
    // parent 그룹을 지정하지 않으면 현재 스레드가 속한 그룹의 하위 그룹으로 생성
    ThreadGroup group = new ThreadGroup(String name);
    ThreadGroup group = new ThreadGroup(ThreadGroup parent, String name);
    // 스레드를 그룹에 명시적으로 포함시키는 방법
    Thread t = new Thread(ThreadGroup group, Runnable target);
    Thread t = new Thread(ThreadGroup group, Runnable target, String name);
    Thread t = new Thread(ThreadGroup group, Runnable target, String name, long stackSize);
    Thread t = new Thread(ThreadGroup group, String target);    // Thread
    // 스레드 그룹의 일괄 interrupt()
    group.interrupt();
```
### Thread Stack
- Call Stack
- Local 변수
#### Thread Debugging
### Thread 동기화 : 공유 자원 사용 문제
- Field, Shared Resource
- Thread Safe
- 작업들 사이의 수행 시기를 맞추는 것
- 다수의 스레드가 동시에 공유 자원을 사용할 때, 순서를 정하는 것
- 자원을 한 번에 하나의 스레드가 사용하도록 보장
#### synchronized
- Critical Section
- 코드 영역을 한 번에 한 스레드만 사용하도록 보장
```Java
    public synchronized void method() { // 임계 영역}   // 동기화 메서드
    // 혹은
    public void method() {
        //synchronized(공유 객체 : 잠김) {   // 동기화 블록
        synchronized(this) {   // 동기화 블록
            // 임계 영역
        }
    }
```
### NetWork 구현: Server, Client
- Server
    - TcpListener
        - AcceptTcpClient()
    - TcpClient
        - new Thread(ServerProcess)
- Client