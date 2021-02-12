## Delegate
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