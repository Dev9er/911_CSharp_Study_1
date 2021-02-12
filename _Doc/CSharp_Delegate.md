# C# 문법
## delegate
### delegate : 대리자, 대리인, 사절
#### 개념
- Event (사건)
- Event Driven Programming
- Callback : 어떤 일을 대신해줄 코드를 두고, 이 코드가 실행할 세부 코드는 컴파일 시점이 아닌 실행 시점에 부여하는 것.
- 대리자는 콜백 구현을 위해 사용
- 참조 변수 : 객체의 주소
- 대리자 : 메서드에 대한 참조
- 값이 아닌 코드 자체를 매개변수로 넘기고 싶을 때 사용.
- 대리자에 메서드의 주소를 할당한 후 대리자를 호출하면 이 대리자가 메서드를 호출해 줌
- 대리자는 형식(Type)이다.
- delegate는 형식이므로 "메서드를 참조하는 그 무엇"을 만들려면 delegate의 인스턴스를 따로 만들어야 한다.
- 대리자는 여러 개의 메서드를 동시에 참조할 수 있다. 대리자 체인 (+=, -=)
- 대리자 체인은 한 번만 호출하면 자신이 참조하고 있는 모든 메서드를 호출한다.
    - MyDelegate myDelegate += A;
        - myDelegate += B;
        - myDelegate -= A;
    - Delegate.Combine() as MyDelegate;
    - Delegate.Remove() as MyDelegate;
#### 선언
- 대리자가 참조할 메서드 선언
    - `int Plus(int a, int b) => a + b;`
    - `int Minus(int a, int b) => a - b;`
- 대리자는 메서드에 대한 참조이므로 자신이 참조할 메서드의 Signiture를 명시해 주어야 한다.
- `한정자 delegate 반환형식 대리자이름(매개변수 목록);`
- `public delegate int MyDelegate(int a, int b);`
- `public delegate int Compare<T>(T a, T b);`
- 대리자의 인스턴스 생성시, 대리자가 참조할 메서드를 매개변수로 넘긴다.
- 대리자를 호출하면 대리자는 현재 자신이 참조하고 있는 주소에 있는 메서드의 코드를 실행하고 그 결과를 호출자에게 반환한다.
#### Callback 만들기와 메서드 호출
```C# 
    MyDelegate callback;
    callback = new MyDelegate(Plus);
    Console.WriteLine($"3 + 4 = {callback(3, 4)}");
    callback = new MyDelegate(Minus);
    Console.WriteLine($"3 - 4 = {callback(3, 4)}");
```
### Anonymous Method : 익명 메서드
- 이름이 없는 메서드
```C#
    public delegate int Calculate(int a, int b);
    Calculate calc;
    calc = delegate (int a, int b) { return a + b};
    Console.WriteLine($"3 + 4 = {calc(3, 4)}");
```
### Anonymous Function
### Event
#### 개념
- 알람 시계처럼 어떤 일이 생겼을때 이를 알려주는 객체
- 이벤트는 대리자를 event 한정자로 수식해서 만든다
- 객체의 상태 변화나 사건의 발생을 알리는 용도로 사용한다.
- delegate와 달리 event는 외부에서 직접 사용할 수 없다.
#### 선언
```C#
    class EventFirer
    {
        public delegate void EventHandler(string message);
        public event EventHandler SomethingHappened;
        public void DoWork() { SomethingHappened("메시지"); }
    }
    class EventSubscriber
    {
        static private void MyHandler(string mes) { Console.Write(mes);}
        private void Subscribe()
        {
            EventFirer firer = new EventFirer();
            firer.SomethingHappened += new EventHandler(MyHandler);
            firer.DoWork();
        }
    }
```
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