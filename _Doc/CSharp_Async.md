# C# 문법
## async, await
- 비동기 호출을 마치 동기 방식처럼 호출하는 코드 작성
### async
- 메서드, 이벤트 처리기, 태스크, 람다식 등 수식하기만 하면 비동기 코드 생성
### 반환 형식 : await로 대기할 수 있는 xxxAsync 메서드의 반환값
- Task, Task<TResult> : 작업이 완료될 때까지 기다리는 메서드
    - Task : Action Action<object>
- void : 실행하고 잊어버릴 (Shoot and Forget) 작업을 담고 있는 메서드
    - async void 유형은 해당 메서드 내에서 예외가 발생했을 때 그것이 처리되지 안는 경우 프로세스가 비정상적으로 종료.
    - System.Windows.Forms.dll의 이벤트 처리기의 delegate Type
    `public delegate void EventHandler(object sender, EventArgs e);`
    - 이벤트 처리기를 제외하고는 async void 는 지양한다
- await 없이 Task 타입을 단독으로 사용하면, ThreadPool.QueueUserWorkItem() 대용으로 쓸 수 있다
### Await
- Async 류의 비동기 호출에 await가 함께 쓰이면, C# 컴파일러는 await 이후의 코드를 묶어서 Async의 비동기 호출이 끝난 후에 ThreadPool 에서 실행되도록 코드를 변경해서 컴파일한다.
- await 는 메서드에 async가 지정되지 않으면 예약어로 인식되지 않는다
- async void : await 연산자가 없어도 비동기로 실행
- async Task, async Task<TResult>
    - await 기술된 곳에서 호출자에게 제어 반환
    - await가 없는 경우 동기로 실행
### async, await 사용
```C#
    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        string url = "https://www.naver.com";
        WebClient client = new WebClient();
        string t = await client.DownloadStringTaskAsync(new Uri(url));
        // await client.DownloadStringTaskAsync(new Uri(url)).ConfigureWait(false);
        Debug.WriteLine(t);
    }
```
### 비동기 API
- System.IO.Stream
    - FileStream.ReadAsync(), FileStream.WriteAsync()
```C#
    string text = await new WebClient().DownloadStringTaskAsync("http://www.microsoft.com");
    int received = await new TcpListener(IPAddress.Any, 11200).AcceptTcpClient().GetStream().ReadAsync(buffer, 0, buffer.Length);
```
- Async 메서드가 제공되지 않는 모든 동기 방식의 메서드를 비동기로 변환하기
```C#
    static void Main(string[] args)
    {
        AwaitFileRead(@"C:\windows\system32\drivers\etc\HOSTS");
    }
    static async Task AwaitFileRead(string filePath)
    {
        string fileText = await ReadAllTextAsync(filePath);
        Console.WriteLine(fileText);
    }
    static Task<string> ReadAllTextAsync(string filePath)
    {
        return Task.Factory.StartNew(() =>
        {
            return File.ReadAllText(filePath);
        });
    }
```
### 비동기 호출의 병렬 처리
