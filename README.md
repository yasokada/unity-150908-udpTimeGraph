# unity-150908-udpTimeGraph

UDP time graph

- implemented on Unity 5.1.2-f1 on MacOS X 10.8.5
- udp receive port: 6000

two types of UDP string can be sent
- data string 
  - e.g. "12:30,0.5" where 12:30 is hour:minute and 0.5 is y value
  - e.g. "2015/09/11 12:30,0.3" for Sep.11 2015 data
- command string
  - set,yrange,[min],[max] 
    - e.g. "set,yrange,-3.0,3.0" to set y range to [-3.0,3.0]
  - set,xtype,[type]
    - where [type] includes { daily, weekly, monthy, yearly } 
    - e.g. "set,xtype,weekly" to show the weekly graph
  - set,export
    - to extract data stored in the graph (see further below) 

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

```
set,xtype,weekly
```

![weekly](https://qiita-image-store.s3.amazonaws.com/0/32870/bc097299-c417-568f-0d65-d94f6e1fd6bf.jpeg)

```
set,xtype,monthly
```

![monthly](https://qiita-image-store.s3.amazonaws.com/0/32870/566a78a6-e07a-f9cc-07ea-570acceafa19.jpeg)


### data export

By sending "set,export" command, you can obtain data stored in the graph.
Following is the example on CentOS 6.5 bash script to obtain data from 192.168.10.6 (or other IP).

```
$echo "set,export" | nc -u 192.168.10.3 6000
SOT
2015/01/10 05:30:00,-0.1320719
2015/02/05 12:30:00,0.2371362
2015/03/01 08:30:00,-0.6964777
2015/04/07 11:50:00,-0.1670557
2015/07/11 09:30:00,0.07135642
2015/08/12 12:30:00,-0.2585875
2015/09/13 08:30:00,-0.4348444
2015/09/13 10:00:00,-0.4057344
2015/09/13 11:30:00,-0.9436774
2015/09/13 04:30:00,0.2064427
2015/09/13 08:10:00,0.678241
2015/09/13 11:40:00,-0.9753504
2015/09/14 09:30:00,-0.5227482
2015/09/15 11:30:00,-0.4138082
2015/09/16 01:30:00,-0.4343764
2015/09/17 01:30:00,0.1775364
EOT
^C
```
