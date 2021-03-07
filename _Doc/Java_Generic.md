## Generic
### Generic Type
- 타입을 파라미터로 가지는 클래스와 인터페이스
- 선언시 클래스 또는 인터페이스 이름 뒤에 <> 부호
- <> 사이에는 타입 파라미로
- `public class 클래스명<T> {}`
- 타입 파라미터
    - 일반적으로 대문자 알파벳 한 문자로 표시
    - 개발 코드에서는 타입 파라미터 자리에 구체적인 타입을 지정해야 한다.
- 비제너릭
    - Object 타입을 사용하므로서 빈번한 타입 변환 발생 -> 성능 저하
    - 다형성 : Object <-> Object 자식 객체 변환, Boxing, Unboxing
- 멀티 타입 파라미터
    - 중복된 타입 파라미터 생략
        - Product<Tv, String> product = new Product<>();
### Generic Method
- 매개변수 타입과 리턴 타입으로 타입 파라미터를 갖는 메서드
- 리턴 타입 앞에 <> 기호를 추가하고 타입 파라미터를 기술한다.
- 타입 파라미터를 리턴 타입과 매개변수에 사용한다.
    - `public <T> Box<T> Boxing(T t) {}`
- 호출 방법
    - Box<Integer> box = <Integer>Boxing(100);
    - Box<Integer> box = Boxing(100);
- 와일드카드 <?> Type
    - 제너릭 타입을 매개변수나 리턴타입으로 사용할때, 타입 파라미터를 제한할 목적으로 사용.
    - <T extends 상위 또는 인터페이스>는 제너릭 타입과 제너릭 메서드를 선언할때 제한을 한다.
    - `제너릭타입<?>`
    - `제너릭타입<? extends 상위타입>`  // 상위 클래스 제한
    - `제너릭타입<? super 하위타입>`    // 하위 클래스 제한