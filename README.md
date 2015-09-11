# unity-150908-udpTimeGraph

UDP time graph

- implemented on Unity 5.1.2-f1 on MacOS X 10.8.5
- udp receive port: 6000

two types of UDP string can be sent
- data string 
  - e.g. "12:30,0.5" where 12:30 is hour:minute and 0.5 is y value
- command string
  - e.g. "set,yrange,-3.0,3.0" to set y range to [-3.0,3.0]


### data string example

```
00:00,0.1
02:10,0.45 
03:40,-0.45
05:30,-0.45 
09:30,0.2 
11:30,0.4
13:30,0.1 
16:30,0.5 
18:30,0.9 
22:00,-0.9
```

![sampleRun](https://qiita-image-store.s3.amazonaws.com/0/32870/fb2482d0-43d5-9a34-e9ee-d8e30d276d40.jpeg)

### set yrange command example

```
set,yrange,-3.0,3.0
```

![sampleRun2](https://qiita-image-store.s3.amazonaws.com/0/32870/b2b2f681-fe7e-9285-4cae-1e925f193341.jpeg)
