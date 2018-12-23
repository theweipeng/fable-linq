# Fable.Fable.Linq

## Usage

```Fsharp
open Fable.Linq.Main
```

## Minimal example

```Fsharp
open Fable.Linq.Main

let x = [4;7;9;5;8]
let y = fablequery {
    for s in x do
    sortBy s
    select {
        a = s
    }
}
```

## Building

Make sure the following **requirements** are installed in your system:

-   [dotnet SDK](https://www.microsoft.com/net/download/core) 2.0 or higher
-   [node.js](https://nodejs.org) 6.11 or higher
-   [yarn](https://yarnpkg.com)
-   [Mono](http://www.mono-project.com/) if you're on Linux or macOS.

Then you just need to type `./build.cmd` or `./build.sh`
