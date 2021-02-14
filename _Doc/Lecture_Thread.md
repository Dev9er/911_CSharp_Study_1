# Thread
## 개념
- 작업 관리자
- OS, Process, Thread(실) 실행 구조
    - Pointer
- 응답성 : CLI, Event Driven Programming
- Callback : 함수 포인터
    - 메서드 입장에서의 호출자(Caller)와 피호출자(Callee) 관계
    - 사용자가 만든 Source 타입에서 Target 타입내에 정의된 메서드를 호출한다고 하면, Source는 호출자가 되고 Target는 피호출자가 된다.
    - 콜백이란 역으로 피호출자에서 호출자의 메서드를 호출하는 것을 의미하고, 이때 역으로 호출된 "호출자 측의 메서드"를 "콜백 메서드"라고 한다.
    - 대리자는 콜백 구현을 위해 사용 ex) Timer
    - 메서드를 인자로 갖는 타입의 인스턴스 생성
    - [타입] method = new [타입](Class.Method);
- Lambda Expression
- 알람 Event, 타이머 (Set, Start, Tick)
- Context Switching
- 병렬 처리와 비동기 처리 비교
    - 동기 : 검사의 찌르기 공격
    - 비동기 : 궁수의 활쏘기
## 사용
- Join()
- WinForm 적용
## 동기화
- 목수의 망치