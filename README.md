# JSFW.PrivateKEY
비밀번호 관리 프로그램

목적 : 요즘은 브라우저가 비밀번호까지도 관리를 해주고 있으니... 필요없을진 몰라도...
  이 프로그램을 만들 당시에는 우리집 공유기 ID와 비번을 자꾸 잊어버리는 상황이 되었다.
  1년에 몇번 들어갈까 말까한 Iptime의 관리화면의 로그인 ID와 PWD를 잊어버렸다. 포스트잇에 적어 iptime 하단에 부착해놨었는데, 
  그것도 잃어버렸다. 그래서 만들었다.     
  예전에 만든것이라 xml형태로 만들고 xml전체를 암호화해서 파일에 저장이 된다.
  그리고 hdd 시리얼 번호를 암호화키로 사용하므로 다른 드라이브로 복사해서 읽을 수 없다.
```diff
- hdd교체를 위하여 컨텐츠로딩 방법은 별도로 존재한다. 
   (보완: 사용하다보니..., 그래서 자기가 처음입력한 비밀번호는 노출되면 안된다. )
- .bak 파일을 이용한 복원방법도 존재한다. 
   (보완: 사용하다보니..., 최후에 비밀번호를 잊어버렸을때 사용할 수단으로 복원방법을 기록하지 않는다. )
```
- 처음 로그인 창<br />
  :: 최초 생성 => pwd(실제실행시 ****로 마스킹됨)를 입력하고 [x]생성체크박스 체크 후 열쇠버튼(또는 엔터키) 클릭<br />
  :: 저장할 파일명을 생성!<br />
  :: 이후 로그인 => 지정한 비밀번호를 입력하고 열쇠버튼(또는 엔터키) 클릭<br />
![image](https://user-images.githubusercontent.com/116536524/197905151-22315ed3-27e5-4309-8fc9-ede421fde3be.png)

- 비밀번호 관리 폼<br />
![image](https://user-images.githubusercontent.com/116536524/197905307-c01da573-cd77-4a93-82ef-6ae75a83fe86.png)

- 사이트 관리 ID및 비밀번호 등록<br />
![image](https://user-images.githubusercontent.com/116536524/197905443-14c585f0-0706-474d-9aa0-e7b302057faa.png)

```diff
+ 저장을 누르면
 //파일 전체 암호화 : {처음 입력된 비밀번호} + {드라이브 시리얼}
 content.Enc(StartForm.PWD + KeyData.SubKEY);
 
+ 생성버튼을 클릭하면
 //비밀번호 생성 : ( {처음 입력된 비밀번호} + GUID + {고정된 특정 문자열} ) <-- 암호화 대상,  ^{사이트명}＠{사용자아이디}$ <-- 암호화 키 
 1. GUID = Guid.NewGuid().ToString("N");
 2. readonly string SubKEY = "TempSubKEY";
 3. 특수문자배열을 섞은 후 UI상에서 체크된 특수문자 위치의 값을 뽑아내서 
 4. 비밀번호를 마지막으로 꺼낼때 선택된 위치의 특수문자를 섞어서 뽑아낸다.
 
 string publicKey = StartForm.PWD + "_"+ GUID + "_" + SubKEY; 
 string pwd = Encrypt.EncSHA1(publicKey.Trim(), "^" + SiteName + "＠" + UID + "$"); 
```

매번 생성을 누르면 계속 다른 문자들로 비밀번호를 생성하여 준다. <br />
저장을 누르면 생성되었던 조합을 기억하였다가 언제든 프로그램을 통해서 동일한 비번을 뽑아내준다. <br />

- 생성예시<br />
![image](https://user-images.githubusercontent.com/116536524/197907237-f87b7109-f968-4117-95c0-33da1f73b571.png)

- 저장 후 <br />
![image](https://user-images.githubusercontent.com/116536524/197907369-59f3a65d-7651-44e9-a2c5-b9a01f147cd3.png)

[GEN] 버튼 클릭시 > "q!XA@GpUfdiS9pIg" 를 클립보드에 넣어준다. <br />
ID를 더블클릭하면 ID도 클립보드에 넣어준다. <br />

- 유튜브 영상 <br />
[![IMAGE ALT TEXT](http://img.youtube.com/vi/e3SOWvpvwCE/0.jpg)](https://youtu.be/e3SOWvpvwCE?t=0s)
 
