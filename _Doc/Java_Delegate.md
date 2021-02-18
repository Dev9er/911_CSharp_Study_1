## Delegate
### 인터페이스
- interface {상수, abstract 메서드, default 메서드, 정적 메서드}
```Java
    public interface MyInterface {  // 인터페이스
        public final int volume = 123;
        public abstract void turnOn();
        default void turnOff() {System.out.println("껏네!");}   // 인스턴스 멤버 : myClass.turnOff();
        static void toggle() {System.out.println("바꿔라!");}   // Static 멤버 : MyInterface.toggle();
    }
    default class MyClass implements MyInterface {} // 구현클래스
    public class MyAnotherClass {
        // 사용법
        MyInterface.toggle();
        MyClass myClass = new MyClass();
        MyInterface my = new MyClass(); // 자동 타입 변환 UpCast(Interface 배열)
        if (my instanceof MyClass) {
            MyClass myClass = (MyClass)my;  // 강제 타입 변환 DownCast
        }
        my.turnOff();
        // 사용처 : 필드, 생성자, 로컬 변수, 함수 인자
        MyInterface my = new MyInterfaceClass();
        MyClass(MyInterface my) {this.my = my}  // 하동
        void methodA() { MyInterface my = new MyInterfaceClass();}
        void methodB(MyInterface my) {} // 다형성 : MyInterface or 구현객체
    }
```
### 익명 구현 객체
- 명시적인 구현 클래스 작성을 생략하고 바로 구현 객체를 얻는 방법
- 이름이 없는 구현 클래스 선언과 동시에 객체를 생성한다.
- 인터페이스의 추상 메서드들을 모두 재정의하는 실제 메서드가 있어야 한다.
- 추가적으로 필드와 메서드를 선언할 수 있지만, 익명 객체 안에서만 사용할 수 있고, 인터페이스 변수로 접근할 수 없다.
- UI 프로그래밍에서 이벤트를 처리하기 위해 주로 사용
- 임시 작업 스레드를 만들기 위해 사용
- 람다식은 내부적으로 익명 구현 객체를 사용
- 익명 구현 객체도 클래스(바이트코드) 파일을 가지고 있다
```Java
    public interface MyInterface {
        public final int volume = 123;
        public abstract void turnOn();
        default void turnOff() {System.out.println("껏네!");}
        static void toggle() {System.out.println("바꿔라!");}
    }
    MyInterface my = new MyInterface() {
        // 인터페이스에 선언된 추상 메서드의 실제 메서드 구현
        @Override   // annotation
        public void turnOn() {}
        @Overrride public void turnOff() {}
        public toggle() {}
    }
```
### 익명 (자식) 객체 : 이름이 없는 객체
- 익명 객체는 단독으로 생성할 수 없다.
- 클래스를 상속하거나 인터페이스를 구현해야만 생성할 수 있다.
- C#의 익명 타입과 완전히 다르다.
```Java
    // 사용처 : 필드나 로컬변수의 초기값, 매개변수의 매개값 대입
    // UI 이벤트 처리 객체, 스레드 객체
    ParentClass pc = new ParentClass() {
        int childField; // 익명 객체 내부에서만 사용 가능
        void childMethod() {} // 익명 객체 내부에서만 사용 가능
        @Override
        void parentMethod() {}
    }
```
###
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
        - andThen() : method()
        - compose() : method()
    - Consumer : 소비 accept()
        - Consumer<String> consumer = s -> (); consumer.accept("s");
        - IntConsumer intConsumer = i -> (); intConsumer.accept(3);
    - Supplier : 공급 getXXX()
        - Supplier<String> supplier = () -> "Hi"; supplier.get();
        - IntSupplier intSupplier = () -> 123;   intSupplier.getAsInt();
    - Function : 매핑(타입 변환) applyXXX()
        - BinaryOperator<T>
            - public interface Comparator<T> {
                public int compare(T o1, T o2);
            }
            - BinaryOperator<T> Comparator<T> : minBy(), maxBy()
        - Function<String, Student> function = s -> {
            new Student(s); }; function.apply(s);
        - ToIntFunction<Student> toIntFunction = S -> 3; toIntFunction.applyAsInt(S);
    - Operator : 연산 수행 applyXXX()
    - Predicate : testXXX()
        - and(), or(), negate(), isEqual()
- Method References
    - 정적 메서드 참조
        - 메서드를 참조해서 매개변수의 정보 및 리턴 타입을 알아내어 람다식에서 불필요한 매개변수를 제거한다.
        - 람다식이 기존 메서드를 단순하게 호출만 하는 경우.
        - 클래스::정적메서드 표기 사용
            - Math::max
        - IntBinaryOperator operator = Math::math
            - (left, right) -> Math.max(left, right);
    - 인스턴스 메서드 참조
        - 참조변수::인스턴스메서드 표기 사용
            - instance::method
    - 람다식의 매개변수의 메서드 참조
        - 클래스::인스턴스메서드 표기 사용
            - ToIntBiFunction<String, String> function;
            - function = (a, b) -> a.compareToIgnoreCase(b);
            - function = String::compareToIgnoreCase;
    - 생성자 참조
        - (a, b) -> { return new 클래스(a,b); }
        - 클래스::new 표기 사용
            - 클래스::new
            - Function<String, Member> function = Member::new;
            - Member member = function.apply("member");
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