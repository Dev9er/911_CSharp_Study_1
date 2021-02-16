# Java 문법
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
```Java
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
```Java
```
### Thread 속성, 메서드
#### Thread 속성
```Jave
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
```Jave
    try {Thread.sleep(1000);} catch (InterruptedException ex) {}
    Thread thread = Thread.currentThread();
    // 메인 스레드 == "main"
    // 작업 스레드 이름 == "Thread-n"
    thread.getName();
    thread.setName("스레드 이름");
    //
    threadB.start();
    threadB.join();
```
##### Thread 실행 시간 측정
```Java
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