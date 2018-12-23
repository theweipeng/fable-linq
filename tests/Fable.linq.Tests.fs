module Fable.Linq.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Linq.Main
open Fable.Core.Testing

[<Global>]
let it (msg: string) (f: unit->unit): unit = jsNative

type rr = {
    a: int
}

it "where works" <| fun () ->
    let x = [1;2;3;4;5;6;7;8]
    let y = fablequery {
        for s in x do 
        where (s > 5)
    }
    Assert.AreEqual(y.Length, 3)

it "select works" <| fun () ->
    let x = [4;5;9;7;8]
    let y = fablequery {
        for s in x do 
        select {
            a = s
        }
    }
    Assert.AreEqual(y.[0].a, 4)
    Assert.AreEqual(y.[1].a, 5)
    Assert.AreEqual(y.[2].a, 9)
    Assert.AreEqual(y.[3].a, 7)
    Assert.AreEqual(y.[4].a, 8)

it "order works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        sortBy s
        select {
            a = s
        }
    }
    Assert.AreEqual(y.[0].a, 4)
    Assert.AreEqual(y.[1].a, 5)
    Assert.AreEqual(y.[2].a, 7)
    Assert.AreEqual(y.[3].a, 8)
    Assert.AreEqual(y.[4].a, 9)

it "order descending works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        sortByDescending s
        select {
            a = s
        }
    }
    Assert.AreEqual(y.[4].a, 4)
    Assert.AreEqual(y.[3].a, 5)
    Assert.AreEqual(y.[2].a, 7)
    Assert.AreEqual(y.[1].a, 8)
    Assert.AreEqual(y.[0].a, 9)