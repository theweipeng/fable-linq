module Fable.Linq.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Linq.Main
open Fable.Core.Testing

[<Global>]
let it (msg: string) (f: unit->unit): unit = jsNative

it "where works" <| fun () ->
    let x = [1;2;3;4;5;6;7;8]
    let y = fablequery {
        for s in x do 
        where (s > 5)
    }
    Assert.AreEqual(y.Length, 3)
