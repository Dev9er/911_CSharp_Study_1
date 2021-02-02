# C# 문법
## delegate
### Anonymous Function
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