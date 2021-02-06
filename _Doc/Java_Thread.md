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
```Java
```
#### 함수형 Interface 생성과 Thread 함수 설정
```Java
```
#### Thread 생성
```Java
```
#### Thread 실행
```Java
```
#### Thread 종료
```Java
```
### Thread 속성, 메서드
#### Thread 속성
- Priority
#### Thread 메서드
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
        //synchronized(공유 객체) {   // 동기화 블록
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