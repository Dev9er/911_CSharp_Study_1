# C# 문법
## delegate
### delegate : 대리자, 대리인, 사절
#### 개념
- Event (사건)
- Event Driven Programming
- Callback : 어떤 일을 대신해줄 코드를 두고, 이 코드가 실행할 세부 코드는 컴파일 시점이 아닌 실행 시점에 부여하는 것.
- 대리자는 콜백 구현을 위해 사용
    - 메서드 입장에서의 호출자(Caller)와 피호출자(Callee) 관계
    - 사용자가 만든 Source 타입에서 Target 타입내에 정의된 메서드를 호출한다고 하면, Source는 호출자가 되고 Target는 피호출자가 된다.
    - 콜백이란 역으로 피호출자에서 호출자의 메서드를 호출하는 것을 의미하고, 이때 역으로 호출된 "호출자 측의 메서드"를 "콜백 메서드"라고 한다.
    - 콜백은 메서드를 호출하는 것이기 때문에 이 상황에서 실제 필요한 것은 타입이 아니라 하나의 메서드일 뿐이다. 따라서 타입 자체를 전달해서 실수를 유발할 여지를 남기기보다는 메서드에 대한 델리게이트만 전달해서 이 문제를 해결할 수 있다.
    - 피호출자가 호출하게될 메서드가 꼭 호출자 내부에 정의된 메서드로 한정되지는 않는다. 다른 타입에 정의된 메서드를 피호출자에 전달해서 호출돼도 이런 식의 역호출을 보통 콜백이라 한다.
    - Target 타입의 Do 메서드를 호출하면서 콜백 메서드를 전달했다. 이로 인해 Do 메서드는 내부의 동작에 Callback 메서들 반영하게 된다. 이것은 마치 이미 정의돼 있는 메서드 내의 특정 코드 영역을 콜백 메서드에 정의된 코드로 치환하는 것과 같은 역할을 한다.
    - 인터페이스(계약, Contract)를 이용한 콜백 구현
        - 인터페이스에 포함된 메서드는 상속된 클래스에서 반드시 구현한다는 보장이 있다. 바로 이 점을 이용하여 인터페이스를 이용한 콜백 구현이 가능하다.
        - `Array.Sort(intArray, new IntegerCompare());`
```C#
    class Source
    {
        public int GetResult() { return 10; }
        public void Test() {
            Target target = new Target();
            target.Do(this);
        }
    }
    class Target
    {
        public void Do(Source obj) {obj.GetResult()}
    }
```
```C#
delegate int CallbackDelegate(); // int를 반환하고 매개변수가 없는 델리게이트 타입을 정의
class 피호출자
{
    public void Callee(CallbackDelegate callback)
    {
        Console.WriteLine(callback()); // 콜백 메서드 호출
    }
}
class 호출자
{
    public int CallBack() // 콜백 용도로 전달될 메서드
    {
        return 10;
    }
    public void Caller()
    {
        피호출자 callee = new 피호출자();
        callee.Callee(new CallbackDelegate(this.CallBack));
    }
}
class Program
{
    static void Main(string[] args)
    {
        호출자 caller = new 호출자();
        caller.Caller();
    }
}
```
```C#
    interface ISource
    {
        int Callback(); // 콜백용으로 사용될 메서드를 인터페이스로 분리한다.
    }
    class Source : ISource
    {
        public int Callback() { return 10; }
        public void Test()
        {
            Target target = new Target();
            target.Do(this);
        }
    }
    class Target
    {
        public void Do(ISource obj) // Source 타입이 아닌 ISource 인터페이스를 받는다.
        {
            Console.WriteLine(obj.Callback()); // 콜백 메서드 호출
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Source source = new Source();
            source.Test();
        }
    }
```
- 참조 변수 : 객체의 주소
- 메서드를 가리킬 수 있는 타입의 간편 표기법
- 대리자 : 메서드에 대한 참조 (함수 포인터)
    - 메서드를 인자로 갖는 타입의 인스턴스 생성
    - [타입] method = new [타입](Class.Method);
- 값이 아닌 코드 자체를 매개변수로 넘기고 싶을 때 사용.
- 대리자에 메서드의 주소를 할당한 후 대리자를 호출하면 이 대리자가 메서드를 호출해 줌
- 대리자는 형식(Type)이다.
    - 변수가 사용되는 곳이라면 델리게이트도 사용 가능하다.
    - 메서드의 반환값으로 델리게이트를 사용 가능하다.
    - 메서드의 인자로 델리게이트를 전달할 수 있다.
    - 클래스의 멤버(필드)로 델리게이트를 정의할 수 있다.
    - 델리게이트를 담는 배열도 가능하다.
    - MyDelegate[] my = new MyDelegate[] {Plus, Minus};
    - 델리게이트는 메서드를 가리키므로,
        - 메서드의 반환값으로 메서드를 사용할 수 있다.
        - 메서드의 인자로 메서드를 전달할 수 있다.
        - 클래스의 멤버로 메서드를 정의할 수 있다.
        - 메서드가 프로그래밍 언어에서 이런 특성을 지닐 때 그것을 1급 함수(first-class function)라 한다.
        - C#은 1급 함수가 지원되는 언어다.
    - delegate 예약어는 메서드를 가리킬 수 있는 내부 닷넷 타입 MulticastDelegate 에 대한 "간편 표기법" 이다.
        - System.Object <- System.Delegate <- System.MulticastDelegate
- delegate는 형식이므로 "메서드를 참조하는 그 무엇"을 만들려면 delegate의 인스턴스를 따로 만들어야 한다.
- 대리자는 여러 개의 메서드를 동시에 참조할 수 있다. 대리자 체인 (+=, -=)
- 대리자 체인은 한 번만 호출하면 자신이 참조하고 있는 모든 메서드를 호출한다.
    - MyDelegate myDelegate = A;
        - myDelegate += B;
        - myDelegate -= A;
    - Delegate.Combine() as MyDelegate;
    - Delegate.Remove() as MyDelegate;
#### 선언
- 대리자가 참조할 메서드 선언
    - `int Plus(int a, int b) => a + b;`
    - `static int Minus(int a, int b) => a - b;`
- 대리자는 메서드에 대한 참조이므로 자신이 참조할 메서드의 Signiture를 명시해 주어야 한다.
- `한정자 delegate 반환형식 대리자이름(매개변수 목록);`
- `public delegate int MyDelegate(int a, int b);`
- `public delegate int Compare<T>(T a, T b);`
- 대리자의 인스턴스 생성시, 대리자가 참조할 메서드를 매개변수로 넘긴다.
- 대리자를 호출하면 대리자는 현재 자신이 참조하고 있는 주소에 있는 메서드의 코드를 실행하고 그 결과를 호출자에게 반환한다.
#### Callback 만들기와 메서드 호출
 public delegate int Calculate(int a, int b);
    Calculate calc;
    calc = delegate (int a, int b) { return a + b};
    Console.WriteLine($"3 + 4 = {calc(3, 4)}");
```
### 람다식 : Lambda Expression, Anonymous Function, 무명 함수
#### 분류
- 코드로서의 람다 식
    - 익명 메서드의 간편 표기 용도
- 데이터로서의 람다 식
    - 람다 식 자체가 데이터가 되어 구문 분석의 대상이 된다.
    - 이 람다 식은 별도로 컴파일할 수 있으며, 그렇게 되면 메서드로 실행 할수도 있다.
#### 코드로서의 람다식
- 분명하고 간결한 방법으로 함수를 묘사하기 위해 고안
- 함수의 정의와 변수, 그리고 함수의 적용으로 구성
- 매개 변수 목록 => 식;
- ; 을 사용해 여러 줄의 코드를 넣을 수 없다
- () => {}
- => : 입력 연산자, Goes to 연산자, Arrow 연산자, Gives me
#### 람다식 작성법
- 매개변수 타입은 런타임 시에 대입값에 따라 자동으로 인식하기 때문에 생략 가능 : (v) -> {}
- 하나의 매개변수만 있을 경우에는 괄호() 생략 가능 : v -> {}
- 하나의 실행문만 있다면 중괄호 생략 가능 : v -> Console.Write();
- 매개변수가 없다면 오히려 괄호 생략 불가 : () -> {}
- 리턴값이 있는 경우, return 문을 사용 : (x, y) -> { return x + y; }
- 중괄호에 return 문만 있을 경우, 중괄호와 return 생략 가능 : (x, y) -> x + y;
#### Lambda 식 형식
##### 식 형식
```C#
    delegate int Calculate(int a, int b);
    Calculate calc = (int a, int b) => a + b;
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
#### 익명 메서드를 람다식으로
```C#
    delegate int Calculate(int a, int b);
    Calculate calc = delegate(int a, int b) { return a + b;};
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
- 람다 식은 기존의 메서드와 달리 일회성으로 사용되는 간단한 코드를 표현할 때 사용되는데, 정작 그러한 목적으로 델리게이트를 일일이 정의해야 한다는 불편함이 발생한다.
- 자주 사용되는 델리게이트의 형식을 일반화
    - 반환값 없음 : Action
    - 반환값 있음 : Func<TResult>
```C#
    Action act = () => Console.WriteLine("Hi");
    int result = 0;
    Action<int> act2 = (x) => result = x * x;
    Action<int> act2 = x => result = x * x;
    Action<double, double> act3 = (x, y) =>
    {
        double pi = x / y;
        Console.WriteLine($"Result: {x} / {y} = {pi}");
    }
    act3(22.0, 7.0);

    Func<int> func1 = () => 10;
    Func<int, int> func2 = (x) => x * 2;
    Func<int, int> func2 = x => x * 2;
    Func<int, int, int> func3 = (x, y) => x + y;
```
#### Generic Collection 과 함께 사용
```C#
    //List<T> : public void ForEach(Action<T> action);
    //Array : public static void ForEach<T>(T[] array, Actoin<T> action);
    list.ForEach(delegate(int elem){});
    list.ForEach((elem) => {});
    Array.ForEach(list.ToArray(), (elem) => {});
    public List<T> findAll(Predicate<T> match);
```
#### Linq 에서 사용 : 지연 평가
```C#
    public static int Count<TSource>(this Inumerable<Tsource> source);
    public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
    var list = data.Where(name => name.StartWith("A"));
    List<int> list = new List<int> { 3, 1, 4, 5, 2 };
    IEnumerable<double> doubleList = list.Select((elem) => (double)elem);
    IEnumerable<Person> personList = list.Select(
        (elem) => new Person { Age = elem, Name = Guid.NewGuid().ToString() });
    var itemList = list.Select((elem) => new { TypeNo = elem, CreatedDate = DateTime.Now.Ticks });
```
#### 식 트리 : Expression Tree
- 람다 식을 CPU에 의해 실행되는 코드가 아닌, 그 자체로 "식을 표현한 데이터"로 사용한 케이스
- 식을 트리로 표현한 자료 구조
- 코드를 데이터로 보관 가능
    - 식 트리로 담긴 람다 식은 익명 메서드의 대체물이 아니기 때문에 델리게이트 타입으로 전달되는 것이 아니라 식에 대한 구문 분석을 할 수 있는 System.Linq.Expressions.Expression 타입의 인스턴스가 된다.
    - 즉, 람다 식이 코드가 아니라 Expression 객체의 인스턴스 데이터의 역할을 하는 것이다.
    - `Expression<Func<int, int, int>> exp = (a, b) => a + b;`
        - exp는 코드를 담지 않고 람다 식을 데이터로서 담고 있다.
        - 데이터로 담겨 있는 람다 식은 컴파일도 가능하다.
            - `Func<int, int, int> func = exp.Compile();`
            - `func(10, 2);`
- Expression<T> 객체를 람다 식으로 초기화하지 않고, 직접 코드와 관련된 Expression 객체로 구성할 수도 있다.
    - 부모 노드(연산자)가 단 두 개의 자식 노드(피연산자)만 갖는 이진 트리
    - 트리의 잎 노드부터 계산해서 루트까지 올라가면 전체 식의 결과
    - C#은 코드에서 직접 식 트리를 조립 및 컴파일 해서 사용할 수 있는 기능 제공
    - 식 트리 자료 구조는 컴파일러나 인터프리터 제작에도 도움
    - 컴파일러는 프로그래밍 언어의 문법에 따라 작성된 소스 코드를 분석해서 식 트리로 만든 후 이를 바탕으로 실행 파일을 만듬
    - 프로그램 실행 중에 동적으로 무명 함수를 만들어 사용
- System.Linq.Expressions.Expression 클래스
    - 식 트리를 구성하는 노드 표현
    - 파생 클래스들의 객체를 생성하는 역할(팩토리 메서드)
    - Expression<TDelegate> 클래스를 이용한 람다식으로 컴파일
```C#
    using System.Linq.Expressions
    Expression<Func<int, int, int>> expression =
        (a, b) => 1 * 2 + (a - b);
    Func<int, int, int> func = expression.Compile();
    func(7, 8);
    
    Expression<Func<int, int, int>> exp = (a, b) => a + b;
    // 람다 식 본체의 루트는 2항 연산자인 + 기호
    BinaryExpression opPlus = exp.Body as BinaryExpression;
    Console.WriteLine(opPlus.NodeType); // 출력 결과: Add // 2항 연산자의 좌측 연산자의 표현식
    ParameterExpression left = opPlus.Left as ParameterExpression;
    Console.WriteLine(left.NodeType + ": " + left.Name); // 출력 결과: Parameter: a // 2항 연산자의 우측 연산자의 표현식
    ParameterExpression right = opPlus.Right as ParameterExpression;
    Console.WriteLine(right.NodeType + ": " + right.Name); // 출력 결과: Parameter: b
    Func<int, int, int> func = exp.Compile();
    Console.WriteLine(func(10, 2)); // 출력 결과: 12

    ParameterExpression leftExp = Expression.Parameter(typeof(int), "a");
    ParameterExpression rightExp = Expression.Parameter(typeof(int), "b");
    BinaryExpression addExp = Expression.Add(leftExp, rightExp);
    Expression<Func<int, int, int>> addLambda =
            Expression<Func<int, int, int>>.Lambda<Func<int, int, int>>(addExp, new ParameterExpression[] { leftExp, rightExp });
    Console.WriteLine(addLambda.ToString()); // 출력 결과: (a, b) => (a + b)
    Func<int, int, int> addFunc = addLambda.Compile();
    Console.WriteLine(addFunc(10, 2)); // 출력 결과: 12

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
- 메서드, 속성, 인덱서, 생성자, 종료자의 본문 {} 간소화 문법
- 멤버의 본문을 식 만으로 구현
- 단일 식인 경우만 가능
- 멤버 => 식;
```C#
    private List<string> list = new List<string>();
    public FriendList() => Console.WriteLine("생성자");
    ~FriendList() => Console.WriteLine("종료자");
    public int Capacity => list.Capacity;   // 읽기 전용 속성
    public int Capacity // 속성
    {
        get => list.Capacity;
        set => list.Capacity = value;
    }
    public string this[int index] => list[index];   // readonly 인덱서
    public double this[int index]
    {
        get => (index == 0) ? x : y;
        set => _ = (index == 0) ? x = value : y = value;
    }
    public void Add(string name) => list.Add(name);
    public void Remove(string name) => list.Remove(name);
    public event EventHandler positionChanged
    {
        add => this.positionChanged += value;
        remove => this.positionChanged -= value;
    }
    positionChanged?.Invoke(this, EventArgs.Empty);
```
### Event
#### 개념
- 알람 시계처럼 어떤 일이 생겼을때 이를 알려주는 객체
- 이벤트는 대리자를 event 한정자로 수식해서 만든다
- 객체의 상태 변화나 사건의 발생을 알리는 용도로 사용한다.
- 아래와 같은 정형화된 콜백 패턴을 구현하려고 할 때 event 예약어를 사용한다.
    1. 클래스에서 이벤트(콜백)을 제공한다.
    1. 외부에서 자유롭게 해당 이벤트(콜백)를 구독하거나 해지하는 것이 가능하다.
    1. 외부에서 구독/해지는 가능하지만 이벤트 발생은 오직 내부에서만 가능하다.
    1. 이벤트(콜백)의 첫 번째 인자로는 이벤트를 발생시킨 타입의 인스턴스다.
    1. 이벤트(콜백)의 두 번째 인자로는 해당 이벤트에 속한 의미있는 값이 제공된다.
- 이벤트는 그래픽 사용자 인터페이스를 제공하는 응용 프로그램에서 매우 일반적으로 사용된다.
    - 버튼이 눌렸을때(Click) 파일을 생성한다.
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
```C#
// Delegate로 Event 흉내
namespace ConsoleApplication1
{
    class CallbackArg { } // 콜백의 값을 담는 클래스의 최상위 부모 클래스 역할

    class PrimeCallbackArg : CallbackArg // 콜백 값을 담는 클래스 정의
    {
        public int Prime;
        public PrimeCallbackArg(int prime)
        {
            this.Prime = prime;
        }
    }

    // 소수 생성기: 소수가 발생할 때마다 등록된 콜백 메서드를 호출
    class PrimeGenerator
    {
        // 콜백을 위한 델리게이트 타입 정의
        public delegate void PrimeDelegate(object sender, CallbackArg arg);

        // 콜백 메서드를 보관하는 델리게이트 인스턴스 필드
        PrimeDelegate callbacks;

        // 콜백 메서드를 추가
        public void AddDelegate(PrimeDelegate callback)
        {
            callbacks = Delegate.Combine(callbacks, callback) as PrimeDelegate;
        }

        // 콜백 메서드를 삭제
        public void RemoveDelegate(PrimeDelegate callback)
        {
            callbacks = Delegate.Remove(callbacks, callback) as PrimeDelegate;
        }

        // 주어진 수까지 루프를 돌면서 소수가 발견되면 콜백 메서드 호출
        public void Run(int limit)
        {
            for (int i = 2; i <= limit; i++)
            {
                if (IsPrime(i) == true && callbacks != null)
                {
                    // 콜백을 발생시킨 측의 인스턴스와 발견된 소수를 콜백 메서드에 전달
                    callbacks(this, new PrimeCallbackArg(i));
                }
            }
        }

        // 소수 판정 메서드. 이해하지 못해도 상관없음.
        private bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
            {
                return candidate == 2;
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0) return false;
            }

            return candidate != 1;
        }
    }

    class Program
    {
        // 콜백으로 등록될 메서드 1
        static void PrintPrime(object sender, CallbackArg arg)
        {
            Console.Write((arg as PrimeCallbackArg).Prime + ", ");
        }

        static int Sum;

        // 콜백으로 등록될 메서드 2
        static void SumPrime(object sender, CallbackArg arg)
        {
            Sum += (arg as PrimeCallbackArg).Prime;
        }

        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            // PrintPrime 콜백 메서드 추가
            PrimeGenerator.PrimeDelegate callprint = PrintPrime;
            gen.AddDelegate(callprint);

            // SumPrime 콜백 메서드 추가
            PrimeGenerator.PrimeDelegate callsum = SumPrime;
            gen.AddDelegate(callsum);

            // 1 ~ 10까지 소수를 구하고,
            gen.Run(10);
            Console.WriteLine();
            Console.WriteLine(Sum);

            // SumPrime 콜백 메서드를 제거한 후 다시 1 ~ 15까지 소수를 구하는 메서드 호출
            gen.RemoveDelegate(callsum);
            gen.Run(15);
        }
    }
}
```
```C#
// 예약어를 사용한 예제
namespace ConsoleApplication1
{
    class PrimeCallbackArg : EventArgs // 콜백 값을 담는 클래스 정의
    {
        public int Prime;
        public PrimeCallbackArg(int prime)
        {
            this.Prime = prime;
        }
    }

    // 소수 생성기: 소수가 발생할 때마다 등록된 콜백 메서드를 호출
    class PrimeGenerator
    {
        public event EventHandler PrimeGenerated;

        // 주어진 수까지 루프를 돌면서 소수가 발견되면 콜백 메서드 호출
        public void Run(int limit)
        {
            for (int i = 2; i <= limit; i++)
            {
                if (IsPrime(i) == true && PrimeGenerated != null)
                {
                    // 콜백을 발생시킨 측의 인스턴스와 발견된 소수를 콜백 메서드에 전달
                    PrimeGenerated(this, new PrimeCallbackArg(i));
                }
            }
        }

        // 소수 판정 메서드. 이해하지 못해도 상관없음.
        private bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
            {
                return candidate == 2;
            }

            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0) return false;
            }

            return candidate != 1;
        }
    }

    class Program
    {
        // 콜백으로 등록될 메서드 1
        static void PrintPrime(object sender, EventArgs arg)
        {
            Console.Write((arg as PrimeCallbackArg).Prime + ", ");
        }

        static int Sum;

        // 콜백으로 등록될 메서드 2
        static void SumPrime(object sender, EventArgs arg)
        {
            Sum += (arg as PrimeCallbackArg).Prime;
        }

        static void Main(string[] args)
        {
            PrimeGenerator gen = new PrimeGenerator();

            // PrintPrime 콜백 메서드 추가
            gen.PrimeGenerated += PrintPrime;
            // SumPrime 콜백 메서드 추가
            gen.PrimeGenerated += SumPrime;

            // 1 ~ 10까지 소수를 구하고,
            gen.Run(10);
            Console.WriteLine();
            Console.WriteLine(Sum);

            // SumPrime 콜백 메서드를 제거한 후 다시 1 ~ 15까지 소수를 구하는 메서드 호출
            gen.PrimeGenerated -= SumPrime;
            gen.Run(15);
        }
    }
}
```