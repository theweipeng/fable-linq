module Fable.Linq.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Linq.Main
open Fable.Core.Testing
open System.Collections.Generic

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


it "count works" <| fun () ->
    let x = [4;7;9;5;8]
    let y = fablequery {
        for s in x do 
        count 
    }
    Assert.AreEqual(y, 5)


it "distinct works" <| fun () ->
    let x = [1;1;2;2]
    let y = fablequery {
        for s in x do 
        distinct 
    }
    Assert.AreEqual(y.Length, 2) 
    Assert.AreEqual(List.contains 1 y, true)
    Assert.AreEqual(List.contains 2 y, true)


it "exactlyOne works" <| fun () ->
    let x = [1]
    let y = fablequery {
        for s in x do 
        exactlyOne 
    }
    Assert.AreEqual(y, 1) 

it "exists works" <| fun () ->
    let x = [1]
    let y = fablequery {
        for s in x do 
        exists (s=1)
    }
    let z = fablequery {
        for s in x do 
        exists (s=2)
    }
    Assert.AreEqual(y, true) 
    Assert.AreEqual(z, false) 


it "find works" <| fun () ->
    let x = [1;2;3]
    let y = fablequery {
        for s in x do 
        find (s=1)
    }
    Assert.AreEqual(y, 1) 
    try
        let z = fablequery {
            for s in x do 
            find (s=4)
        }
        let s = Some z
        Assert.AreEqual(false, true) 
    with
        | _ -> Assert.AreEqual(true, true) 



it "join works" <| fun () ->
    let x = [{bar=1}; {bar=2};{bar=3};{bar=4}]
    let y = [{bar=5}; {bar=2};{bar=3};{bar=4};{bar=3}]
    let k = fablequery {
        for s in x do 
        join m in y on (s.bar=m.bar) 
        select (s.bar, m)
    }
    for m in k do
        match m with
            | (4, a) ->
                Assert.AreEqual(a, y.[3])
            | (2, b) ->
                Assert.AreEqual(b, y.[1])
            | (3, c) ->
                let a = (c = y.[2])
                let b = (c = y.[4])
                Assert.AreEqual(true, a || b)
            | (1, c) ->
               Assert.AreEqual(false, true)
            | (5, c) ->
               Assert.AreEqual(false, true)

it "leftOuterJoin works" <| fun () ->
    let x = [{bar=1}; {bar=2};{bar=3};{bar=4}]
    let y = [{bar=5}; {bar=2};{bar=3};{bar=4};{bar=3}]
    let k = fablequery {
        for s in x do 
        leftOuterJoin m in y on (s.bar=m.bar)  into p
        select (s.bar, p)
    }
    for m in k do
        match m with
            | (4, a) ->
                Assert.AreEqual(a.[0], y.[3])
            | (2, b) ->
                Assert.AreEqual(b.[0], y.[1])
            | (3, c) ->
                Assert.AreEqual(c.[0], y.[2])
                Assert.AreEqual(c.[1], y.[4])
            | (1, c) ->
               Assert.AreEqual(c.Length, 0)

it "groupJoin works" <| fun () ->
    let x = [{bar=1}; {bar=2};{bar=3};{bar=4}]
    let y = [{bar=5}; {bar=2};{bar=3};{bar=4};{bar=3}]
    let k = fablequery {
        for s in x do 
        leftOuterJoin m in y on (s.bar=m.bar)  into p
        select (s.bar, p)
    }
    for m in k do
        match m with
            | (4, a) ->
                Assert.AreEqual(a.[0], y.[3])
            | (2, b) ->
                Assert.AreEqual(b.[0], y.[1])
            | (3, c) ->
                Assert.AreEqual(c.[0], y.[2])
                Assert.AreEqual(c.[1], y.[4])
            | (1, c) ->
               Assert.AreEqual(c.Length, 0)


it "groupValBy works" <| fun () ->
    let x = [{bar=1}; {bar=2};{bar=2};{bar=3};{bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        groupValBy s s.bar into p
        select p
    }
    for m in k do
        match m with
            | (2, b) ->
                Assert.AreEqual(b.Length, 2)
                Assert.AreEqual(List.contains x.[1] b, true)
                Assert.AreEqual(List.contains x.[2] b, true)
            | (3, c) ->
                Assert.AreEqual(c.Length, 3)
                Assert.AreEqual(List.contains x.[3] c, true)
                Assert.AreEqual(List.contains x.[4] c, true)
                Assert.AreEqual(List.contains x.[5] c, true)
            | (1, c) ->
               Assert.AreEqual(c.Length, 1)
               Assert.AreEqual(x.[0], c.[0])



it "minBy works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        minBy s.bar
    }
    Assert.AreEqual(k, x.[3])

it "maxBy works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        minBy s.bar
    }
    Assert.AreEqual(k, x.[5])

it "head works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        head
    }
    Assert.AreEqual(k, x.[0])

it "last works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        last
    }
    Assert.AreEqual(k, x.[5])

it "skip works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        skip 3
    }
    Assert.AreEqual(k.Length, 2)
    Assert.AreEqual(k.[0], x.[4])
    Assert.AreEqual(k.[1], x.[5])

it "skipWhile works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        skipWhile (s.bar = 3)
    }
    Assert.AreEqual(k.Length, 3)
    Assert.AreEqual(k.[0], x.[0])
    Assert.AreEqual(k.[1], x.[1])
    Assert.AreEqual(k.[2], x.[3])

it "take works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        take 3
    }
    Assert.AreEqual(k.Length, 3)
    Assert.AreEqual(k.[0], x.[0])
    Assert.AreEqual(k.[1], x.[1])
    Assert.AreEqual(k.[2], x.[2])

it "takeWhile works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        takeWhile (s.bar = 3)
    }
    Assert.AreEqual(k.Length, 3)
    Assert.AreEqual(k.[0], x.[2])
    Assert.AreEqual(k.[1], x.[4])
    Assert.AreEqual(k.[2], x.[5])

it "nth works" <| fun () ->
    let x = [{bar=2};{bar=2};{bar=3};{bar=1}; {bar=3};{bar=3}]
    let k = fablequery {
        for s in x do 
        nth 3
    }
    Assert.AreEqual(k, x.[3])
