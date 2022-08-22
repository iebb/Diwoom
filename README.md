# Diwoom
A Windows Divoom Client (unofficial)

Download: https://github.com/iebb/Diwoom/releases

Includes a Web Server, which provides easy integration with other apps.

Python Example:
```
with io.BytesIO() as output:
    img.save(output, format="png")
    requests.post("http://127.0.0.1:10119/img", data=output.getvalue())
```



References:
https://github.com/RomRider/node-divoom-timebox-evo/blob/master/PROTOCOL.md#channels

