# C# 문법
## 개발 환경 구축
- 비주얼 스튜디오 2019 커뮤니티 설치
## 첫 C# 프로그램 작성하기
-  출력문 : Top level Program
```C#
    System.Console.WriteLine("Hello, World!");
```
- Name Space, Class, Method
- 식별자, 예약어(Key Word) : 대, 소문자
- () 연산자
- 단문: 문장 종결자 ;
    - 공문
    - 식
```C#
    System.Console.WriteLine();
    System.Console.Write();
```
- 공백, 들여쓰기 (Indent)
- Literal
    - 숫자: 3, 3L, 3.14D, 3.0F, 3M, 3U, 5LU, 3.14E-3,
        - 이진 리터럴: 0B_1010L
        - 16진 리터럴: 0X_FL
    - 문자(값 형식에 속하는 유니코드 문자, UTF-16 기반): 'A'
        - '\uAC00' ~ '\uD7A3' (유니코드 이스케이프 시퀀스)
        - '\x006A' == '\x6A' (16진수 이스케이프 시퀀스)
        - 리틀 엔디언, 빅 엔디언 (Endian)
    - 문자열(참조 형식에 속하는 유니코드 문자열): "A"
        - 이스케이프 시퀀스 (Escape Sequence)
            - \\, \n, \t, \", \'
- 연산: 수나 식을 일정한 규칙에 따라 계산하는 것
        하나 이상의 피연산자를 연산자의 정의에 따라 계산하여 하나의 결과값을 도출해 내는 과정
    - 숫자 연산: +, -, *, /, %
    - 문자열 연결: +
        - "A" + 1 + 'a'
## 사용자 입력 받기
```C#
    var input = System.Console.ReadLine();
    System.Console.WriteLine($"{input}");
    System.Console.ReadKey();   // Debug용 입력 사용 안하기
```
- 식별자, 예약어(Key Word)
- Type : 데이터 형식, (3.14).GetType()
    - var
    - int, long, sbyte, short : .NET 형식, SByte, Int16, Int32, Int64
    - uint, ulong, byte, ushort : Byte, UInt16, UInt32, UInt64
    - float, double, decimal : Single, Double, Decimal
        - MinValue, MaxValue
    - Cast 연산자 : 묵시적, 명시적
    - char, string
- 변수 : Type 데이터 형식의 저장 공간의 이름
    - 선언, 초기화, 정의
    - 결정된 정수 리터럴 형식이 int이고 리터럴이 나타내는 값이 대상 형식의 범위 내에 있는 경우, 해당 값이 암시적으로 sbyte, byte, short, ushort, uint 또는 ulong으로 변환될 수 있습니다.
    - default
    _ 형식이 같은 변수 여러 개를 한 번에 선언하기
```C#
    int number1, number2, number3;
```
    - L-Value, R-Value
- 상수 : 리터럴과 차이점?
```C#
    const double PI = 3.14;
```
- 할당 연산자: =, +=
    - 문자열 연결, 숫자 연산 추가
```C#
    a += b -= c = 10;
```
- 주석문 (코드 설명문) : //, /* */, ///
```C#
    #region *전처리기 지시문 영역*
    var comment = "/// : XML 문서 주석";
    System.Console.WriteLine("comment");
    #endregion *전처리기 지시문 영역*
```
- 문자열 보간법: $""
    - Place Holder
    - string.Format("{0}님, {1}", "홍길동", "안녕하세요.")
- @"" : 공백/탭 포함 문자열
- using 문 : namespace
    - using System; // 멀티 커서
    - using static System.Console;
- Convert Type
```C#
    string input = Console.ReadLine(); // "10"을 입력한다면
    int number = Convert.ToInt32(input); // "10"을 정수 10으로 변환
    int number2 = int.Parse(input);
    Console.WriteLine($"{number:#,###.00}");

    // 10진수 10을 2진수로 변경하면 1010
    Console.WriteLine($"10 : {Convert.ToString(10, 2)}");
    // 2진수 1010을 10진수로 변경하면 10
    Console.WriteLine($"1010 : {Convert.ToInt32("1010", 2)}");
```
- DateTime 형식 : Year, Month, Day, Hour, Minute, Second