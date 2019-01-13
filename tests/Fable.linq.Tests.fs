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
        thenBy s
        select {
            bar = s
        }
    }
    Assert.AreEqual(y.[0].bar, 4)
    Assert.AreEqual(y.[1].bar, 5)
    Assert.AreEqual(y.[2].bar, 7)
    Assert.AreEqual(y.[3].bar, 8)
    Assert.AreEqual(y.[4].bar, 9)

it "sortBy descending works" <| fun () ->
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

it "thenBy descending works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        thenByDescending s
        select {
            bar = s
        }
    }
    Assert.AreEqual(y.[4].bar, 4)
    Assert.AreEqual(y.[3].bar, 5)
    Assert.AreEqual(y.[2].bar, 7)
    Assert.AreEqual(y.[1].bar, 8)
    Assert.AreEqual(y.[0].bar, 9)


it "sumBy works" <| fun () ->
    let x = [{bar=1}; {bar=2}; {bar=3}]
    let y = fablequery {
        for s in x do 
        sumBy s.bar
    }
    Assert.AreEqual(y, 6)

    let x = [1;2;3;4]
    let y = fablequery {
        for s in x do 
        sumBy s
    }
    Assert.AreEqual(y, 10)

it "groupBy works" <| fun () ->
    let m = [{bar=1}; {bar=3}; {bar=2}; {bar=2}; {bar=3}; {bar=3}; ]
    let y = fablequery {
        for s in m do 
        groupBy s.bar
    }
    for x in y do
        match x with
            | (1, a) ->
                Assert.AreEqual(a.Length, 1)
                Assert.AreEqual(a.[0], m.[0])
            | (2, b) ->
                Assert.AreEqual(b.Length, 2)
                Assert.AreEqual(List.contains m.[2] b, true)
                Assert.AreEqual(List.contains m.[3] b, true)
            | (3, c) ->
                Assert.AreEqual(c.Length, 3)
                Assert.AreEqual(List.contains m.[1] c, true)
                Assert.AreEqual(List.contains m.[4] c, true)
                Assert.AreEqual(List.contains m.[5] c, true)


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


it "averageBy works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        averageBy s
    }
    Assert.AreEqual(y, 33 / 5)


it "contains works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        contains 4
    }
    Assert.AreEqual(y, true)
    let y = fablequery {
        for s in x do 
        contains 40
    }
    Assert.AreEqual(y, false)

    