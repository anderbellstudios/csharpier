Fork of the [CSharpier](https://github.com/belav/csharpier) project with the K&R Style (opening brace on the same line, rather than on the next line).

### Quick Start
Unfortunately you'll have to compile this by yourself. With dotnet SDK set up, it shouldn't be too hard.

```bash
dotnet pack ./Src/CSharpier.Cli/CSharpier.Cli.csproj -c Release -o dist
```

Your binaries should be available in `Src/CSharpier.Cli/bin/Release/net8.0` (and `net9.0`)

Running `CSharpier.exe format .` formats a whole dir.

### VSCode (format on save)
After compiling your own binary, install the `vehmloewff.custom-format` extension (which allows you to run any command to format code)
```jsonc
    "custom-format.formatters": [
        {
            "language": "csharp",
            "command": "path/to/CSharpier.exe format"
        },
    ],
    "[csharp]": {
        "editor.rulers": [ 100 ],
        "editor.formatOnSave": true,
        "editor.defaultFormatter": "Vehmloewff.custom-format",
    },
```

---

### Before
```c#
public class ClassName {
    public void CallMethod() { 
        this.LongUglyMethod("1234567890", "abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }
}
```

### After (this fork)
```c#
public class ClassName {
    public void CallMethod() {
        this.LongUglyMethod(
            "1234567890",
            "abcdefghijklmnopqrstuvwxyz",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );
    }
}
```

### After (original CSharpier)
```c#
public class ClassName
{
    public void CallMethod()
    {
        this.LongUglyMethod(
            "1234567890",
            "abcdefghijklmnopqrstuvwxyz",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        );
    }
}
```

## Contributing
See [Development Readme](CONTRIBUTING.md)  

Join Us [![Discord](https://img.shields.io/badge/Discord-chat?label=&logo=discord&logoColor=ffffff&color=7389D8&labelColor=6A7EC2)](https://discord.gg/HfAKGEZQcX)

## Sponsors

Thanks to the following companies for sponsoring the ongoing development of CSharpier.

[.NET on AWS Open Source Software Fund](https://github.com/aws/dotnet-foss) \
 \
[<img src="./Src/Website/static/img/aws.png" />](https://github.com/aws/dotnet-foss)

[Fern](https://buildwithfern.com/) \
 \
[<img src="./fern.svg" />]((https://buildwithfern.com/))

And a huge thanks to all the others who sponsor the project through [Github sponsors](https://github.com/sponsors/belav)
