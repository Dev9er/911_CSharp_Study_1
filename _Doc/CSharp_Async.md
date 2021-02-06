# C# 문법
## async, await
- BeginInvoke(), EndInvoke()
### async
- 메서드, 이벤트 처리기, 태스크, 람다식 등 수식하기만 하면 비동기 코드 생성
### 반환 형식
- Task, Task<TResult> : 작업이 완료될 때까지 기다리는 메서드
- void : 실행하고 잊어버릴 (Shoot and Forget) 작업을 담고 있는 메서드
### Await
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