module Fable.Linq.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Linq.Main
open Fable.Core.Testing

[<Global>]
let it (msg: string) (f: unit->unit): unit = jsNative

type Foo = {
    bar: int
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
            bar = s
        }
    }
    Assert.AreEqual(y.[0].bar, 4)
    Assert.AreEqual(y.[1].bar, 5)
    Assert.AreEqual(y.[2].bar, 9)
    Assert.AreEqual(y.[3].bar, 7)
    Assert.AreEqual(y.[4].bar, 8)

it "sortBy works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        sortBy s
        select {
            bar = s
        }
    }
    Assert.AreEqual(y.[0].bar, 4)
    Assert.AreEqual(y.[1].bar, 5)
    Assert.AreEqual(y.[2].bar, 7)
    Assert.AreEqual(y.[3].bar, 8)
    Assert.AreEqual(y.[4].bar, 9)

it "thenBy works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        sortBy s
        select {
            bar = s
        }
    }
    Assert.AreEqual(y.[0].bar, 4)
    Assert.AreEqual(y.[1].bar, 5)
    Assert.AreEqual(y.[2].bar, 7)
    Assert.AreEqual(y.[3].bar, 8)
    Assert.AreEqual(y.[4].bar, 9)

it "order descending works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        sortByDescending s
        select {
            bar = s
        }
    }
    Assert.AreEqual(y.[4].bar, 4)
    Assert.AreEqual(y.[3].bar, 5)
    Assert.AreEqual(y.[2].bar, 7)
    Assert.AreEqual(y.[1].bar, 8)
    Assert.AreEqual(y.[0].bar, 9)

it "all works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        select {
            bar = s
        }
        all (s.bar = 4)
    }
    Assert.AreEqual(y, false)
    let m = [4;4;4;4;4]
    let k = fablequery {
        for s in m do 
        select {
            bar = s
        }
        all (s.bar = 4)
    }
    Assert.AreEqual(k, true)
    